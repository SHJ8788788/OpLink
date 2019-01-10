using OpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpLink.Interface
{
    /// <summary>
    /// 定义一个通用的tag使用方式
    /// </summary>
    public interface ITagService: IDependency
    {
        //Func<IEnumerable<Tag>, IEnumerable<Tag>> GetTagsValues {get; set; }
        Action<string> MsgHandle { get; set; }
        /// <summary>
        /// 连接服务器
        /// </summary>
        void Connect();
        /// <summary>
        /// 执行时间间隔ms
        /// </summary>
        int RunInterval { get; set; }
        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
       void TagChangedExecute(Tag tag);
        /// <summary>
        /// 周期执行的方法
        /// </summary>
        void InvokeService();
    }
}
