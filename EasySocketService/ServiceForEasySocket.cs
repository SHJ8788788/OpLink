using EasySocket.vs13.Core;
using EasySocket.vs13.Serializers;
using EasySocket.vs13.Telegram.Easy;
using EasySocketService.PF;
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
    public class ServiceForEasySocket: TagServiceBase
    {
        private IOpcClient opcClient;
        private static string serviceName = "EasySocket";
        private TcpClientForPF client;

        /// <summary>
        /// 连接EasySocket服务器
        /// </summary>
        public override void Connect()
        {
            //不在同一线程可使用控件的Invoke方法调用CloseHandle
            if (EasyTcpClient.Instance.Connect("172.16.6.30", 5555))
            //if (EasyTcpClient.Instance.Connect("172.22.197.45", 5555))
            {
                //执行注册效验
                var result = ClientProxy.Verification("opc");
                MsgHandle(serviceName + $">EasySocket>{EasyTcpClient.Instance.Ip}已连接");
            }
            else
            {
                MsgHandle(serviceName+">服务器无法连接");
            }           
        }
        /// <summary>
        /// 断开EasySocket服务器
        /// </summary>
        public override void DisConnect()
        {
            //不在同一线程可使用控件的Invoke方法调用CloseHandle
            if (EasyTcpClient.Instance.Stop())
            {
                MsgHandle(serviceName + ">服务器已断开");
            }
            else
            {
                MsgHandle(serviceName + ">服务器无法断开");
            }         
           
        }

        public ServiceForEasySocket(IOpcClient opcClient,int runInterval) : base(runInterval = 3600000)
        {
            this.opcClient = opcClient;
            EasyTcpClient.Instance.Extra.Tag.Set("opc", opcClient);
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
            EasyTcpClient.Instance.MsgHandle = Msg;
            //重连后执行注册效验
            EasyTcpClient.Instance.ReconnectCompleteHandle =() => { ClientProxy.Verification("opc"); };
            //顷绑定当前程序集
            EasyTcpClient.Instance.BindService(Assembly.GetExecutingAssembly());
            //Log4Ex.LogHelper.Debug("测试--更换后的dll");
        }

        public ServiceForEasySocket(IOpcClient opcClient) : base(5000)
        {
            this.opcClient = opcClient;
            EasyTcpClient.Instance.Extra.Tag.Set("opc", opcClient);
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
            EasyTcpClient.Instance.MsgHandle = Msg;
            //重连后执行注册效验
            EasyTcpClient.Instance.ReconnectCompleteHandle = () => { ClientProxy.Verification("opc"); };
            //顷绑定当前程序集
            EasyTcpClient.Instance.BindService(Assembly.GetExecutingAssembly());
            PFClientInit();            
        }

        /// <summary>
        /// PF线钩号接收-通讯初始化
        /// </summary>
        public void PFClientInit()
        {
            client = new TcpClientForPF();
            var succ = client.Connect("172.15.1.28", 50000);
            client.ReconnectEnable = true;
            client.PFValueChangedHandle = PFTagChangedExecute;
        }
        /// <summary>
        /// 周期执行
        /// </summary>
        public override void InvokeService()
        {
            //opcClient["GroupData"]
            //  .GetTags()
            //  .ToList();
            var serverTime =ClientProxy.TimeSync().Result;
            if(SysTimeSetting.SetLocalTimeByStr(serverTime.ToString("yyyyMMddHHmmss")))
            {
                Log4Ex.LogHelper.Debug(string.Format("周期获取服务器时间={0}, 执行成功", serverTime.ToString("yyyyMMddHHmmss")));
            }
            else
            {
                Log4Ex.LogHelper.Debug(string.Format("周期获取服务器时间={0}, 执行失败,尝试用管理员启动程序", serverTime.ToString("yyyyMMddHHmmss")));
            }          

        }

        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
        public override void TagChangedExecute(Tag tag)
        {
           ClientProxy.TagEventChange(new TagSimple { TagName = tag.TagName, TagValue = tag.Value.ToString(), TagType = tag.DataType});
        }

        /// <summary>
        /// 钩号变化后触发
        /// </summary>
        /// <param name="tag"></param>
        private void PFTagChangedExecute(Tag tag)
        {
            ClientProxy.TagEventChange(new TagSimple { TagName = tag.TagName, TagValue = tag.Value.ToString(), TagType = tag.DataType });
            Console.WriteLine(string.Format("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataTypeName));
            Log4Ex.LogHelper.Debug(string.Format("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataTypeName));
        }
        /// <summary>
        /// 接收消息通知
        /// </summary>
        /// <param name="msg"></param>
        private void Msg(string msg)
        {
            MsgHandle?.Invoke(serviceName+msg);
        }
    }
}
