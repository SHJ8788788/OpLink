using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient.Config
{
    /// <summary>
    /// Tag配置
    /// </summary>
    public class TagConfiguration : System.Configuration.ConfigurationSection
    { 
        /// <summary>
        /// tag基础配置文件路径
        /// </summary>
        [ConfigurationProperty("tagPath", IsRequired = true, DefaultValue = "")]
        public string TagPath { get => (string)base["tagPath"]; set => base["tagPath"] = value; }
        /// <summary>
        /// 信号量更新频率
        /// </summary>
        [ConfigurationProperty("triggerUpdateRate", IsRequired = false, DefaultValue = 250)]
        public int TriggerUpdateRate
        {
get
            {
                return (int)base["triggerUpdateRate"];
            }
            set
            {
                base["triggerUpdateRate"] = value;
            }
        }
        /// <summary>
        /// 变量更新频率
        /// </summary>
        [ConfigurationProperty("dataUpdateRate", IsRequired = false, DefaultValue = 1000)]
        public int DataUpdateRate
        {
get
            {
                return (int)base["dataUpdateRate"];
            }
            set
            {
                base["dataUpdateRate"] = value;
            }
        }  
    }
}
