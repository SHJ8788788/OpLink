using EasySocket.vs13;
using EasySocket.vs13.Session;
using EasySocket.vs13.Tasks;
using OpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService.PF
{
    public class TcpClientForPF : TcpSessionClient
    {
        private HookDic dic = new HookDic();
        public Action<Tag> PFValueChangedHandle { get; set; }

        public TcpClientForPF()
        {
            dic.Tags.Add(new Tag { TagName = "HookA", Value = "",DataType="string" });
            //钩号发生变化通知
            dic.ValueChangedHandle = TagValueChanged;
        }

        protected override Task OnReceiveAsync(ISession session)
        {
            try
            {
                var context = this.CreateContext(this);
                var packages = this.GenerateStringPackets(context);
                foreach (var package in packages)
                {
                    GetHook(package);
                }
            }
            catch (Exception ex)
            {
                Log4Ex.LogHelper.MiddlewareException("TcpClientForPF",ex.ToString());
            }           
            return TaskExtend.CompletedTask;
        }

        /// <summary>
        /// 创建上下文对象
        /// </summary>
        /// <param name="session">当前会话</param>
        /// <returns></returns>
        private IContext CreateContext(ISession session)
        {
            return new DefaultContext
            {
                Session = session,
                StreamReader = session.StreamReader
            };
        }

        /// <summary>
        /// 生成字符串格式数据包
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        private IList<string> GenerateStringPackets(IContext context)
        {
            var stringPackets = new List<string>();
            //生成EasyPacket,生成失败则直接返回
            if (this.GenerateStringPackets(context, out stringPackets) == false)
            {
                //var df  = Encoding.ASCII.GetString(context.StreamReader.ReadArray(), 0, context.StreamReader.Length);
                return stringPackets;
            }
            else if (stringPackets == null)
            {
                return stringPackets;
            }
            else
            {
                return stringPackets;
            }
        }

        /// <summary>
        /// 解析并返回多个字符串封包
        /// </summary>
        /// <param name="context"></param>
        /// <param name="packets"></param>
        /// <returns></returns>
        private bool GenerateStringPackets(IContext context, out List<string> packets)
        {
            packets = new List<string>();
            while (true)
            {
                var packet = default(string);
                if (Parse(context.StreamReader, context.Session, out packet) == false)
                {
                    return false;
                }
                else
                {
                    if (packet != null)
                    {
                        packets.Add(packet);
                    }
                    else
                    {
                        return true;
                    }

                }
            }
        }
        /// <summary>
        /// 解析一个数据包       
        /// 不足一个封包时packet返回null
        /// 中间件不符合则返回false
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="packet">数据包</param>
        /// <returns></returns>
        public virtual bool Parse(ISessionStreamReader streamReader, ISession session, out string packet)
        {
            packet = null;
            try
            {                
                int required = 28;
                int remain = session.RemainDataLength;//剩余长度
                while (remain >= required)
                {
                    // 实体数据
                    packet = Encoding.UTF8.GetString(streamReader.ReadArray(required));
                    // 清空本条数据
                    streamReader.Clear(required);
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        /// <summary>
        /// 从字符串中解析出位置、钩号信息,对比后更新后台数据
        /// </summary>
        /// <param name="s"></param>
        private void GetHook(string s)
        {
            string StrHookNoA = s.Substring(s.IndexOf('a') + 1,3);
            string StrHookNoB = s.Substring(s.IndexOf('b') + 1,3);
            string StrHookNoC = s.Substring(s.IndexOf('c') + 1,3);

            //信号点异常，过滤
            if (StrHookNoA.Equals("xxx"))
            {
                return;
            }
            else if (StrHookNoA.Equals("000"))
            {
                return;
            }
            //32号钩校称钩不处理
            else if (StrHookNoA == "032")
            {
                return;
            }
            dic.Tags.Where(t => t.TagName == "HookA").FirstOrDefault().Value = StrHookNoA;


        }

        /// <summary>
        /// Tag点变化时通知
        /// </summary>
        /// <param name="tag"></param>
        private void TagValueChanged(Tag tag)
        {
            this.PFValueChangedHandle?.Invoke(tag);
            //Console.WriteLine(string.Format("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataType));
            //AddMsgToList(string.Format("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataType));
        }
    }
}
