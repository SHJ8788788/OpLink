using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using OpcClient;
using System.Windows.Forms;
using System.Threading.Tasks;
using OpcClient.Opc;
using System.Collections.ObjectModel;
using OpcClient.Config;

namespace OpcClient
{
    public interface IOpcClient : IDisposable
    {
        OpcAddressConfiguration Address { get;}
        /// <summary>
        /// 初始化设置
        /// 1、初始化OPCServer
        /// </summary>
        /// <param name="address"></param>
        void Init(OpcAddressConfiguration address);
        /// <summary>
        /// 连接OPC
        /// </summary>
        /// <returns></returns>
        Task<bool> Connect();
        /// <summary>
        /// 断开OPC
        /// </summary>
        /// <returns></returns>
        bool Disconnect();
        /// <summary>
        /// OPC连接状态
        /// </summary>
        bool OpcStatus { get; }
        /// <summary>
        /// Opc服务端开始时间
        /// </summary>
        /// <returns></returns>
        string ServerStartTime { get; }
        string ServerVersion { get; }
        string ServerStateDesc { get; }
        /// <summary>
        /// 获取OPCServer列表
        /// </summary>
        List<string> ServerList { get; }
        /// <summary>
        /// OPCBrowser遍历下一级子节点集合
        /// </summary>
        /// <param name="nodeId"></param>
        List<OpcNode> ShowBranches(string nodeId = null);
        /// 取出指定组下一级的所有Items信息
        /// </summary>
        /// <returns></returns>
        List<OpcNode> ShowLeafs(string nodeId);
        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        IGroup CreateGroup(string groupName);
        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        IGroup CreateGroup(IGroup group);
        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool RemoveGroupByGroupName(string groupName);
        /// <summary>
        /// 在Group结构发生变化后刷新并生效
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool RefreshGroupByGroupName(string groupName);
        /// <summary>
        /// 删除所有组
        /// </summary>
        /// <returns></returns>
        void RemoveGroupsAll();
        /// <summary>
        /// 获取Tag值
        /// </summary>
        /// <param name="biList">需要取值的tag集合</param>
        /// <param name="grouName"></param>
        /// <returns></returns>
        List<Tag> GetTagValuesFromGroup(ref List<Tag> biList, string grouName);
        /// <summary>
        /// 服务端状态变化通知
        /// </summary>
        Action<OpcStatusEventArgs> OpcStatusChangeHandle { get; set; }

        /// <summary>
        /// OPC重新连接成功后触发
        /// </summary>
        Action<IOpcClient> ReconnectCompleteHandle { get; set; }
        /// <summary>
        /// OPC重连接时触发
        /// </summary>
        Action<IOpcClient> ReconnectStartingHandle { get; set; }
        /// <summary>
        /// OPC成功连接或断开之后触发
        /// </summary>
        Action<IOpcClient> ConnectCompleteHandle { get; set; }
        IGroup this[string groupName] { get; }
    }
}
