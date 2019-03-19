using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Config
{
    /// <summary>
    /// OPC连接配置
    /// 兼容DA,UA
    /// </summary>
    public class OpcAddressConfiguration : System.Configuration.ConfigurationSection, ICloneable<OpcAddressConfiguration>
    {  
        /// <summary>
        /// OPC协议
        /// </summary>
        [ConfigurationProperty("opcProtocol", IsRequired = false, DefaultValue = 0)]
        public int OpcProtocol
        {
            private
get
            {
                return (int)base["opcProtocol"];
            }
            set
            {
                base["opcProtocol"] = value;
            }
        }

        /// <summary>
        /// OPC类型
        /// </summary>
        [ConfigurationProperty("opcTypeName", IsRequired = false, DefaultValue = "")]
        public string OpcTypeName
        {
            get
            {
                return (string)base["opcTypeName"];
            }
            set
            {
                base["opcTypeName"] = value;
            }
        }
        /// <summary>
        /// OPC协议--以枚举的方式查询
        /// </summary>
        public OpcProtocol OpcProtocolByEnum
        {
            get
            {
                return (OpcProtocol)this.OpcProtocol;
            }
        }

        /// <summary>
        /// Da内容
        /// </summary>
        [ConfigurationProperty("da", IsRequired = false)]
        public DaAddress DaAddress
        {
            get
            {
                return (DaAddress)base["da"];
            }
            set
            {
                base["da"] = value;
            }
        }

        /// <summary>
        /// Ua内容
        /// </summary>
        [ConfigurationProperty("ua", IsRequired = false)]
        public UaAddress UaAddress
        {
            get
            {
                return (UaAddress)base["ua"];
            }
            set
            {
                base["ua"] = value;
            }
        }

        /// <summary>
        /// OPC服务是否断线重连
        /// </summary>
        [ConfigurationProperty("reconnectEnable", IsRequired = false, DefaultValue = true)]
        public bool ReconnectEnable
        {
            get
            {
                return (bool)base["reconnectEnable"];
            }
            set
            {
                base["reconnectEnable"] = value;
            }
        }
        /// <summary>
        /// 重连时间间隔
        /// </summary>
        [ConfigurationProperty("reconnectInterval", IsRequired = false, DefaultValue = 10000)]
        public int ReconnectInterval
        {
            get
            {
                return (int)base["reconnectInterval"];
            }
            set
            {
                base["reconnectInterval"] = value;
            }
        }
        /// <summary>
        /// 克隆构造器
        /// </summary>
        /// <returns></returns>
        public OpcAddressConfiguration Clone()
        {
            return new OpcAddressConfiguration
            {
                OpcProtocol = this.OpcProtocol,
                UaAddress = new UaAddress
                {
                    Uri = this.UaAddress.Uri,
                    UseSecurity = this.UaAddress.UseSecurity,
                    UserName = this.UaAddress.UserName,
                    Password = this.UaAddress.Password
                },
                DaAddress = new DaAddress
                {
                    Ip = this.DaAddress.Ip,
                    ServerName = this.DaAddress.ServerName
                },
                ReconnectEnable = this.ReconnectEnable,
                ReconnectInterval = this.ReconnectInterval
            };
        }
    }
    public enum OpcProtocol
    {
        Else = 0,
        DA = 1,
        UA = 2,
    }
}
