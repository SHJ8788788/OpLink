using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Config
{
    public class DaAddress : ConfigurationElement, ICloneable<DaAddress>
    {
        /// <summary>
        /// IP地址
        /// </summary>
        [ConfigurationProperty("ip", IsRequired = true, DefaultValue = "")]
        public string Ip { get => (string)base["ip"]; set => base["ip"] = value; }
        /// <summary>
        /// OPC服务端名称,DA专用
        /// </summary>
        [ConfigurationProperty("serverName", IsRequired = false, DefaultValue = "")]
        public string ServerName { get => (string)base["serverName"]; set => base["serverName"] = value; }

        /// <summary>
        /// 克隆构造器
        /// </summary>
        /// <returns></returns>
        DaAddress ICloneable<DaAddress>.Clone()
        {
            return new DaAddress
            {
                Ip = this.Ip,
                ServerName = this.ServerName,
            };
        }
    }
}
