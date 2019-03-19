using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpcClient;

namespace OpLink.Interface
{
    /// <summary>
    /// 通用的tag使用方式
    /// 抽象类
    /// </summary>
    public abstract class TagServiceBase : ITagService
    {
        /// <summary>
        /// 执行时间间隔ms
        /// </summary>
        public int RunInterval { get; set; }
        public Action<string> MsgHandle { get; set; }

        public TagServiceBase(int runInterval)
        {
            RunInterval = runInterval;
            //新建定时器
            System.Timers.Timer t = new System.Timers.Timer(runInterval);//实例化Timer类，设置间隔时间为runInterval毫秒；

            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；

            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；

            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        void ITagService.Connect()
        {
            //连接操作
            this.Connect();
        }
        /// <summary>
        /// 断开服务器连接
        /// </summary>
        void ITagService.Disconnect()
        {
            //断开服务器连接
            this.DisConnect();
        }

        /// <summary>
        /// 周期执行
        /// </summary>
        void ITagService.InvokeService()
        {
            this.InvokeService();
        }


        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
        void ITagService.TagChangedExecute(Tag tag)
        {
            this.TagChangedExecute(tag);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public virtual void Connect()
        {          
        }
        //断开服务器连接
        public virtual void DisConnect()
        {

        }

        /// <summary>
        /// 周期执行
        /// </summary>
        public virtual void InvokeService()
        {
          
        }


        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
        public virtual void TagChangedExecute(Tag tag)
        {         
        }

        /// <summary>
        /// 到达时间的时候执行事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                InvokeService();
            }
            catch (Exception ex)
            {
                MsgHandle?.Invoke(ex.ToString());
            }
            
        }
    }
}
