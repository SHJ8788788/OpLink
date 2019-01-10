using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Opc
{
    /// <summary>
    /// OPC连接配置
    /// 兼容DA,UA
    /// </summary>
    public class OpcAddressConfiguration
    {
        private string ip;
        private string serverName;
        private OpcProtocol opcProtocol;
        private int port;
        private string userName;
        private string password;
        private bool useSecurity = false;
        /// <summary>
        /// DA方式
        /// </summary>
        /// <param name="opcProtocol"></param>
        /// <param name="ip"></param>
        /// <param name="serverName"></param>
        public OpcAddressConfiguration(OpcProtocol opcProtocol, string ip, string serverName)
        {
            this.opcProtocol = opcProtocol;
            this.ip = ip ?? throw new ArgumentNullException(nameof(ip));
            this.serverName = serverName ?? throw new ArgumentNullException(nameof(serverName));
            this.useSecurity = false;
        }
        /// <summary>
        /// UA方式
        /// </summary>
        /// <param name="opcProtocol"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public OpcAddressConfiguration(OpcProtocol opcProtocol, string ip, int port)
        {
            this.opcProtocol = opcProtocol;
            this.ip = ip ?? throw new ArgumentNullException(nameof(ip));
            this.port = port;
            this.useSecurity = false;
        }
        /// <summary>
        /// UA方式
        /// 采用帐号密码
        /// </summary>
        /// <param name="opcProtocol"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public OpcAddressConfiguration(OpcProtocol opcProtocol, string ip, int port,string userName,string password)
        {
            this.opcProtocol = opcProtocol;
            this.ip = ip ?? throw new ArgumentNullException(nameof(ip));
            this.port = port;
            this.userName = userName;
            this.password = password;
            this.useSecurity = true;
        }
        public string Ip { get => ip; set => ip = value; }
        public string ServerName { get => serverName; set => serverName = value; }
        public int Port { get => port; set => port = value; }
        /// <summary>
        /// 是否采用安全加密
        /// </summary>
        public bool UseSecurity { get => useSecurity;}
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
    }
    public enum OpcProtocol
    {
        DA = 1,
        UA = 2
    }
}
