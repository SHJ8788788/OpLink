using EasySocket.vs13.Core;
using EasySocket.vs13.Serializers;
using EasySocket.vs13.Telegram.Easy;
using Models;
using OpcClient;
using OpLink.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService
{
    /// <summary>
    /// 使用EasySocket中间件
    /// </summary>
    public class ServiceForEasySocket: EasyTcpClient,ITagService
    {
        public Func<IEnumerable<Tag>, IEnumerable<Tag>> GetTagsValues { get; set; }
        public Action<string> msgHandle;
        /// <summary>
        /// 执行时间间隔ms
        /// </summary>
        public int RunInterval { get; set; }

        public ServiceForEasySocket(int runInterval)
        {
            RunInterval = runInterval;
            EasyTcpClient.Instance.TimeOut = TimeSpan.FromSeconds(30);
            //连接时采用同步方式，等待服务端返回结果，会引起阻塞，（异步方式无法判断连接状态，不采用）
            EasyTcpClient.Instance.Async = false;
            //是否开启断线重连,(在网络异常的情况，将尝试重连),
            //客户端使用时不应开启，在使用OPC或通讯中间件时为了保证通讯正常，可开启
            EasyTcpClient.Instance.ReconnectEnable = true;
            //采用序列化方式
            EasyTcpClient.Instance.Serializer = new ProtoBufSerializer();
            //委托绑定UI委托，在调用时可通过判断调用是否在同一线程内
            //不在同一线程可使用控件的Invoke方法调用MsgHandle
            MsgHandle = msg;
            EasyTcpClient.Instance.MsgHandle = msgHandle; 
            //顷绑定当前程序集
            EasyTcpClient.Instance.BindService(Assembly.GetExecutingAssembly());
            //不在同一线程可使用控件的Invoke方法调用CloseHandle
            if (EasyTcpClient.Instance.Connect("127.0.0.1", 5555))
            {
                //执行注册效验
                var result = ClientProxy.Verification("opc");
            }
            else
            {
                //MessageBox.Show("服务器无法连接");
            }
        }

        private void msg(string test)
        {
            return;
        }

        public void InvokeController(int runInterval)
        {
            RunInterval = runInterval;
        }

        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
        public void TagChangedExecute(Tag tag)
        {            
        }


        [Api]
        public string GetTagValue(string tagName)
        {
            var inParamTag = new Tag[] { new Tag { TagName = tagName } };
            Tag tag = GetTagsValues(inParamTag).Where(t => t.TagName == tagName).FirstOrDefault();
            return tag
                .Value.ToString();
        }
        [Api]
        public List<TagSimple> GetTags(List<string> tagNames)
        {
            var inParamTags=tagNames.Select(t => new Tag { TagName = t });
            var list = GetTagsValues(inParamTags)
                  .Select(p => new TagSimple { TagName = p.TagName, TagValue = p.Value.ToString(), TagTypeName = p.DataType })
                  .ToList();
            return inParamTags
                  .Select(p => new TagSimple { TagName = p.TagName, TagValue = p.Value.ToString(), TagTypeName = p.DataType })
                  .ToList();
        }

        [Api]
        public string GetTagValueMaxBetweenDate(string tagName, DateTime dateFrom, DateTime dateTo = default(DateTime))
        {
            var inParamTag = new Tag[] {new Tag { TagName = tagName } };
            Tag tag = GetTagsValues(inParamTag).Where(t => t.TagName == tagName).FirstOrDefault();

            return tag != null?tag.TagHistory.ByDateBetweenThan(dateFrom, dateTo).Max().ToString():"";         
        }

        private IGroup OpcData
        {
            get { return EasyTcpClient.Instance.Extra.Tag.Get("opc").As<IOpcClient>()["GroupData"]; }
        }

    }
}
