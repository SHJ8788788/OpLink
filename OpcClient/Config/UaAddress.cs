using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Config
{
    public class UaAddress : ConfigurationElement, ICloneable<UaAddress>
    {
        /// <summary>
        /// 端口号，UA专用
        /// </summary>
        [ConfigurationProperty("uri", IsRequired = true, DefaultValue = "")]
        public string Uri { get => (string)base["uri"]; set => base["uri"] = value; }
        /// <summary>
        /// 是否采用安全加密
        /// </summary>
        [ConfigurationProperty("useSecurity", IsRequired = false, DefaultValue = false)]
        public bool UseSecurity { get => (bool)base["useSecurity"]; set => base["useSecurity"] = value; }
        /// <summary>
        /// 帐号名称
        /// </summary>
        [ConfigurationProperty("userName", IsRequired = false, DefaultValue = "")]
        public string UserName { get => (string)base["userName"]; set => base["userName"] = value; }
        /// <summary>
        /// 帐号密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false, DefaultValue = "")]
        public string Password { get => (string)base["password"]; set => base["password"] = value; }

        /// <summary>
        /// 克隆构造器
        /// </summary>
        /// <returns></returns>
        UaAddress ICloneable<UaAddress>.Clone()
        {
            return new UaAddress
            {
                Uri = this.Uri,
                UseSecurity = this.UseSecurity,
                UserName = this.UserName,
                Password = this.Password
            };
        }
    }
}
