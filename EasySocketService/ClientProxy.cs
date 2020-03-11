using EasySocket.vs13.Telegram.Easy;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService
{
    /// <summary>
    /// 代理方法类，需要使用的远程调用方法在此处描述
    /// </summary>
    class ClientProxy
    {
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static EasyTcpClient Instance
        {
            get
            {
                return EasyTcpClient.Instance;
            }
        }

        ///// <summary>
        ///// 登录服务器
        ///// </summary>
        ///// <param name="user">用户信息</param>
        ///// <param name="ifAdmin">是否为管理员</param>
        ///// <returns></returns>
        //public static Task<LoginResult> Login(UserInfo user, Boolean ifAdmin)
        //{
        //    return Instance.InvokeApi<LoginResult>("Login", user, ifAdmin).TimeoutAfter(TimeSpan.FromSeconds(5));
        //    //return this.InvokeApi<LoginResult>("Login", user).TimeoutAfter(TimeSpan.FromSeconds(1));
        //}

        ///// <summary>
        ///// 登出服务器
        ///// </summary>
        ///// <param name="user">用户信息</param>
        ///// <param name="ifAdmin">是否为管理员</param>
        ///// <returns></returns>
        //public static Task<LoginResult> Logoff(UserInfo user, Boolean ifAdmin)
        //{
        //    return Instance.InvokeApi<LoginResult>("Logoff", user, ifAdmin).TimeoutAfter(TimeSpan.FromSeconds(5));
        //}

        ///// <summary>
        ///// 登出服务器
        ///// </summary>
        ///// <param name="user">用户信息</param>
        ///// <param name="ifAdmin">是否为管理员</param>
        ///// <returns></returns>
        //public static Task<LoginResult> LogTest(List<UserInfo> users, Boolean ifAdmin)
        //{
        //    return Instance.InvokeApi<LoginResult>("LogTest", users, ifAdmin);
        //}

        //public static Task<LoginResult> Log2StringTest(List<string> users, Boolean ifAdmin)
        //{
        //    return Instance.InvokeApi<LoginResult>("Log2StringTest", users, ifAdmin);
        //}

        /// <summary>
        /// OPC校验注册
        /// </summary>
        /// <returns></returns>
        public static Task<bool> Verification(string msg)
        {          
            return Instance.InvokeApi<bool>("Verification",msg);
        }

        /// <summary>
        /// 时间同步-获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static Task<DateTime> TimeSync()
        {
            return Instance.InvokeApi<DateTime>("TimeSync");
        }
        /// <summary>
        /// Tag信号发生变化
        /// </summary>
        /// <returns></returns>
        public static Task<bool> TagEventChange(TagSimple tag)
        {
            return Instance.InvokeApi<bool>("TagEventChange", tag);
        }
    }
}
