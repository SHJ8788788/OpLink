using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpcClient
{
    /// <summary>
    /// 配置config扩展类
    /// </summary>
    public class ConfigurationManagerExtend
    {
        static string path = Application.StartupPath + @"\Config\op.config";

        /// <summary>
        /// 根据sectionGroupName下的所有的section
        /// </summary>
        /// <typeparam name="T">映射的类</typeparam>
        /// <param name="sectionGroupName">组名</param>
        /// <returns></returns>
        public static Dictionary<string, T> SectionsCast<T>(string sectionGroupName) where T : System.Configuration.ConfigurationSection, new()
        {
            
            Dictionary<string, T> sections = new Dictionary<string, T>();
            //System.Configuration.Configuration config =
            //     ConfigurationManager.OpenExeConfiguration(
            //     ConfigurationUserLevel.None);
            System.Configuration.Configuration config =
            System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = path }, ConfigurationUserLevel.None);
            var sectionGroup = config.SectionGroups.Get(sectionGroupName);
            
            //foreach (T section in sectionGroup.Sections)
            //{
            //    sections.Add(sectionGroupName, section);
            //}
            foreach (String sectionName in sectionGroup.Sections.Keys)
            {
                var sectionFullName = sectionGroupName.IsNullOrEmpty() ? sectionName : sectionGroupName + "/" + sectionName;
                T section = (T)config.GetSection(sectionFullName);
                sections.Add(sectionName, section);
            }
            return sections;
        }




        /// <summary>
        /// 保存数据项，在禁用宿主进程后可以使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="sectionName"></param>
        /// <param name="sectionGroupName"></param>
        /// <returns></returns>
        public static void SectionSave<T>(T section, string sectionName, string sectionGroupName = "") where T : System.Configuration.ConfigurationSection, new()
        {
            System.Configuration.Configuration config =
                        System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = path }, ConfigurationUserLevel.None);
            var sectionFullName = sectionGroupName.IsNullOrEmpty() ? sectionName : sectionGroupName + "/" + sectionName;
            T data = (T)config.GetSection(sectionFullName);

            Type t = section.GetType();//获得该类的Type

            foreach (PropertyInfo pi in t.GetProperties())
            {
                //仅操作带有ConfigurationProperty特性的属性
                if (pi.GetCustomAttributes(true).Any(p => p.GetType().Equals(typeof(ConfigurationPropertyAttribute))))
                {
                    var value = pi.GetValue(section, null);//用pi.GetValue获得值
                    pi.SetValue(data, value, null);
                }
                //if (pi.CustomAttributes.Any(p => p.AttributeType.Equals(typeof(ConfigurationPropertyAttribute))))
                //{
                //    var value = pi.GetValue(section, null);//用pi.GetValue获得值
                //    pi.SetValue(data, value, null);
                //}
            }
            config.Save();
            ConfigurationManager.RefreshSection(sectionFullName);  //让修改之后的结果生效
        }

        /// <summary>
        /// 保存数据项，在禁用宿主进程后可以使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="propertyName"></param>
        /// <param name="sectionName"></param>
        /// <param name="sectionGroupName"></param>
        /// <returns></returns>
        public static bool SectionSave<T>(T section, string propertyName, string sectionName, string sectionGroupName = "") where T : System.Configuration.ConfigurationSection, new()
        {
            System.Configuration.Configuration config =
                        System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = path }, ConfigurationUserLevel.None);
            var sectionFullName = sectionGroupName.IsNullOrEmpty() ? sectionName : sectionGroupName + "/" + sectionName;
            T data = (T)config.GetSection(sectionFullName);

            Type t = section.GetType();//获得该类的Type

            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (propertyName == pi.Name)
                {
                    var value = pi.GetValue(section, null);//用pi.GetValue获得值
                    pi.SetValue(data, value, null);
                }
            }
            config.Save();
            ConfigurationManager.RefreshSection(sectionFullName);  //让修改之后的结果生效
            return true;
        }
    }
}
