using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Net;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using OpcClient;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using OpcClient.Opc;
using OpcClient.Config;

namespace DaOpcClient
{
    /// <summary>
    /// 采用Da Opc连接方式
    /// </summary>
    public class DaOpc : IOpcClient
    {
        #region 私有变量
        //*****************OPC服务器变量**************
        /// <summary>
        /// OPCServer Object
        /// </summary>
        public OPCServer KepServer;
        /// <summary>
        /// OPCserver节点浏览器，可遍历其中的分组名称与变量名称
        /// </summary>
        //private OPCBrowser oPCBrowser;
        /// <summary>
        /// OPC是否连接
        /// </summary>
        private bool opc_connected = false;
        /// <summary>
        /// OPCserver分组列表
        /// </summary>
        private List<string> branches;
        /// <summary>
        /// OPC变量列表
        /// </summary>
        private List<OpcNode> leafs;
        /// <summary>
        /// 创建浏览器对象，由于服务器端的菜单是树形结构，可以通过创建浏览器对象，一步步浏览菜单，寻找需要浏览的Item
        /// </summary>
        private OPCBrowser oPCBrowser;
        /// <summary>
        /// OPCGroup字典
        /// </summary>
        private readonly Dictionary<string, IGroup> groupDictionary = new Dictionary<string, IGroup>();
        object syncLock = new object();
        #endregion
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static readonly Lazy<IOpcClient> instance = new Lazy<IOpcClient>(() => new DaOpc());
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static IOpcClient Instance
        {
            get { return instance.Value; }
        }
        /// <summary>
        /// OPC连接状态
        /// </summary>
        public bool OpcStatus { get { return KepServer.ServerState == 1; } }
        /// <summary>
        /// 是否已初始化
        /// </summary>
        public bool IsInit { get; set; }
        /// <summary>
        /// OPC服务端运行开始时间
        /// </summary>
        public string ServerStartTime { get; private set; }
        /// <summary>
        ///  OPC服务端版本号
        /// </summary>
        public string ServerVersion { get; private set; }
        public string ServerName { get; private set; }
        /// <summary>
        /// OPC服务端状态描述
        /// </summary>
        public string ServerStateDesc { get; private set; }
        /// <summary>
        /// OPCServer列表
        /// </summary>
        /// <returns></returns>
        public List<string> ServerList { get
            {
                 var serverList = new List<string>();
                //object list = ((Array)KepServer.GetOPCServers(strHostName));// .net3.5用法
                //遍历指定IP下的OPCServer列表
                //var list = ((object)KepServer.GetOPCServers(Address.ServerName));// .net4.0用法      
                var list = ((object)KepServer.GetOPCServers());// .net4.0用法   
                foreach (string turn in (Array)list)
                {
                    serverList.Add(turn);
                }
                return serverList;
            }}
        public OpcAddressConfiguration Address { get; private set; }
        /// <summary>
        /// OPC重新连接成功后触发
        /// </summary>
        public Action<IOpcClient> ReconnectCompleteHandle { get; set; }
        /// <summary>
        /// OPC重连接时触发
        /// </summary>
        public Action<IOpcClient> ReconnectStartingHandle { get; set; }
        /// <summary>
        /// OPC成功连接或断开之后触发
        /// </summary>
        public Action<IOpcClient> ConnectCompleteHandle { get; set; }
        /// <summary>
        /// 当OPC状态发生变化时触发
        /// </summary>
        public Action<OpcStatusEventArgs> OpcStatusChangeHandle { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DaOpc()
        {
            KepServer = new OPCServer();
        }
        #endregion
        #region IOpcClient 成员
        /// <summary>
        /// 初始化设置
        /// 1、初始化OPCServer
        /// 2、遍历OPCServer列表
        /// </summary>
        /// <param name="ip"></param>
        public void Init(OpcAddressConfiguration address)
        {
            this.Address = address;
            //获取本地计算机上的OPCServerName
                try
                {                    
                    KepServer.ServerShutDown += new DIOPCServerEvent_ServerShutDownEventHandler(KepServer_DisConnected);
                    KepServer.OPCGroups.DefaultGroupIsActive = true;
                    KepServer.OPCGroups.DefaultGroupDeadband = 0;
                    KepServer.OPCGroups.DefaultGroupUpdateRate = 250;                   
                }
                catch (Exception err)
                {
                    throw new Exception("枚举本地OPC服务器出错," + err.Message);
                }
        }
        /// <summary>
        /// 连接OPCServer服务
        /// 1、初始化daOpcItems集合
        /// 2、创建浏览器对象
        /// 3、OPCServer信息获取
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public async Task<bool> Connect()
        {
            try
            {
                if (Address.DaAddress.ServerName==""|| Address.DaAddress.ServerName is null)
                {
                    return false;
                }
                //尝试连接OPC服务
                lock (syncLock)
                {
                    if (opc_connected) throw new Exception("opc已经连接，请误重复连接");
                    KepServer.Connect(Address.DaAddress.ServerName, Address.DaAddress.Ip);
                    if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                    {
                        oPCBrowser = KepServer.CreateBrowser();
                        this.ServerStateDesc = "已连接到-" + KepServer.ServerName + "   ";
                        this.ServerName = KepServer.ServerName;
                        this.ServerStartTime = KepServer.StartTime.ToString() + "   ";
                        this.ServerVersion = KepServer.MajorVersion.ToString() + "." + KepServer.MinorVersion.ToString() + "." + KepServer.BuildNumber.ToString() + "   ";
                        opc_connected = true;
                        ConnectCompleteHandle?.Invoke(this);
                        return true;
                    }
                    else
                    {
                        opc_connected = false;
                        //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                        this.ServerStateDesc = "状态：连接失败";
                        this.ServerStartTime = DateTime.Now.ToString();
                        this.ServerVersion = "0.0.0";
                        ServerName = "Error";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //保存连接状态
                opc_connected = false;
                //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                var test = "状态：" + KepServer.ServerState.ToString() + "   ";
                this.ServerStateDesc = string.Format("无法连接-连接状态{0}", KepServer.ServerState.ToString());
                this.ServerStartTime = DateTime.Now.ToString();
                this.ServerVersion = "0.0.0";
                ServerName = "Error";
                return false;
            }
        }

        /// <summary>
        /// 断开与OPC服务器的连接(使用 KepServer.CreateBrowser会导致无法正常使用)
        /// 释放OPC中的所有Group成员及解除事件监听
        /// 主动断开
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (!opc_connected) return false;
            try
            {
                KepServer.Disconnect();
                var state = KepServer.ServerState.ToString();
                this.opc_connected = false;
                this.ServerStateDesc = "已断开到-" + ServerName + "   ";
                return true;
            }
            catch (Exception)
            {
                this.ServerStateDesc = "断开连接失败";
                return false;
            }
        }
        /// <summary>
        /// OPCBrowser遍历组下一层节点，并构建Tree节点
        /// </summary>
        /// <param name="nodeId"></param>     
        public List<OpcNode> ShowBranches(string nodeId = "")
        {  
            if (nodeId.IsNullOrEmpty())
            {
                //返回根节点，需要顺序执行下列3个函数。
                oPCBrowser.MoveTo(oPCBrowser.CurrentPosition.Split('.'));
                oPCBrowser.ShowLeafs(true);
                oPCBrowser.MoveToRoot();
            }
            else
            {
                oPCBrowser.MoveTo(new String[] { nodeId });
            }

            oPCBrowser.ShowBranches();
            List<OpcNode> listNode = Task.Run(() =>
            {
                List<OpcNode> list = new List<OpcNode>();
                foreach (object branch in oPCBrowser)
                {  
                    string key = "ClassIcon";                     
                    bool isExpand = false;
                    string branchNodeId = "";
                    //拼接nodeId
                    if (nodeId.IsNullOrEmpty())
                    {
                        branchNodeId = branch.ToString();
                    }
                    else
                    {
                        branchNodeId = nodeId + "." + branch.ToString();                        
                    }
                    oPCBrowser.MoveDown(branch.ToString());
                    oPCBrowser.ShowBranches();
                    if (oPCBrowser.Count > 0)
                    {
                        isExpand = true;
                    }
                    var child = new OpcNode(branch.ToString(), branchNodeId, key, isExpand);
                    list.Add(child);
                    oPCBrowser.MoveUp();
                }
                return list;
            }
            ).Result;
            oPCBrowser.MoveToRoot();
            return listNode;
        }        
        /// <summary>
        /// 列出OPC服务端分组下所有标签名称
        /// </summary>
        /// <param name="groupName"></param>
        public List<OpcNode> ShowLeafs(string nodeId)
        {
            string groupName = nodeId;

            if (groupName== "Browsering...")
            {
                return new List<OpcNode>();
            }
            try
            {
                leafs = new List<OpcNode>();
                Array branches = groupName.Split('.');
                //创建浏览器对象，由于服务器端的菜单是树形结构，可以通过创建浏览器对象，一步步浏览菜单，寻找需要浏览的Item
                oPCBrowser.MoveTo(branches);
                //true 显示父节点
                oPCBrowser.ShowLeafs(true);
                foreach (object turn in oPCBrowser)
                {
                    OpcNode child = new OpcNode(turn.ToString().Substring(turn.ToString().LastIndexOf('.') + 1), turn.ToString(), "", false);
                    child.Attribute = new OpcNodeAttribute { NodeClass = "Variable", Name = "", Type = "", Value = "", AccessLevel = "null", Description = "" };
                    leafs.Add(child);

                }
                oPCBrowser.MoveToRoot();
                return leafs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IGroup CreateGroup(IGroup group)
        {
            try
            {
                var kepGroup = KepServer.OPCGroups.Add(group.GroupName);
                groupDictionary.Add(group.GroupName, group);
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IGroup CreateGroup(string groupName)
        {
            try
            {
                var kepGroup = KepServer.OPCGroups.Add(groupName);
                var group = new DaGroup(groupName,kepGroup);
                groupDictionary.Add(groupName, group);
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 在group发生变化后刷新并生效(DA模式不使用)
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool RefreshGroupByGroupName(string groupName)
        {
            return true;
        }

        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool RemoveGroupByGroupName(string groupName)
        {
            try
            {
                KepServer.OPCGroups.Remove(groupName);
                groupDictionary.Remove(groupName);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 清空删除所有组(当前有问题，多次调用时会报错)
        /// </summary>
        /// <returns></returns>
        public void RemoveGroupsAll()
        {
            this.groupDictionary.Clear();
            Console.WriteLine("RemoveGroupsAll End,groupDictionary.count is {0}", groupDictionary.Count);

        }
        /// <summary>
        /// 获取Tag值
        /// </summary>
        /// <param name="biList"></param>
        /// <param name="grouName"></param>
        /// <returns></returns>
        public List<Tag> GetTagValuesFromGroup(ref List<Tag> biList, string grouName)
        {
            biList = groupDictionary[grouName].GetTags().ToList();
            return biList;
        }

        // 摘要: 
        //     根据组名获取组
        //     获取或设置与指定的键相关联的值。
        //
        // 参数: 
        //   key:
        //     要获取或设置的值的键。
        public IGroup this[string groupName] { get { return this.groupDictionary[groupName]; } }
        #endregion
        /// <summary>
        /// 断线重连
        /// </summary>
        /// <param name="client"></param>
        private void ReconnectOpcAsync()
        {
            while (true)
            {    
                if (!this.OpcStatus&&this.Address.ReconnectEnable)
                {
                    ReconnectStartingHandle?.Invoke(this);
                    ServerStateDesc = "连接已断开,尝试重连";
                    ServerStartTime = DateTime.Now.ToString() + "   ";
                    //成功连接后直接返回
                    if (Connect().Result)
                    {
                        ServerStateDesc = "重连成功";
                        ServerStartTime = DateTime.Now.ToString() + "   ";
                        ReconnectCompleteHandle?.Invoke(this);
                        return;
                    }
                    //连接失败则循环继续
                    else
                    {                     
                        //重连时间间隔
                        Thread.Sleep(this.Address.ReconnectInterval);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        #region 事件
        /// <summary>
        /// 写入TAG值时执行的事件（异步写入）
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            //lblState.Text = "";
            //for (int i = 1; i <= NumItems; i++)
            //{
            //    lblState.Text += "Tran:" + TransactionID.ToString() + "   CH:" + ClientHandles.GetValue(i).ToString() + "   Error:" + Errors.GetValue(i).ToString();
            //}
        }
        /// <summary>
        /// 服务器端断开通知
        /// </summary>
        /// <param name="msg"></param>
        void KepServer_DisConnected(string msg)
        {
            //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
            ServerStateDesc = "已断开到-" + ServerName;
            ServerStartTime = DateTime.Now.ToString() + "   ";
            opc_connected = false;

            if (OpcStatusChangeHandle != null)
            {
                OpcStatusChangeHandle(new OpcStatusEventArgs { Error = false,Text= ServerStateDesc,Time= DateTime.Now });
            }
            //另起线程循环执行断线重连
            Task.Run(() =>
            {
                ReconnectOpcAsync();
            });
        }
        #endregion
        #region IDisposable
        /// <summary>
        /// 获取是否已释放
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// 关闭和释放所有相关资源
        /// </summary>
        public void Dispose()
        {
            // 如果资源未释放 这个判断主要用了防止对象被多次释放
            if (this.IsDisposed == false)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            this.IsDisposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~DaOpc()
        {
            this.Dispose(false);
        }
        /// <summary>
        /// 释放资源
        ///参数为true表示释放所有资源，只能由使用者调用
        //参数为false表示释放非托管资源，只能由垃圾回收器自动调用
        //如果子类有自己的非托管资源，可以重载这个函数，添加自己的非托管资源的释放
        //但是要记住，重载此函数必须保证调用基类的版本，以保证基类的资源正常释放
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            this.KepServer = null;
            opc_connected = false;
            foreach (var item in groupDictionary)
            {
                item.Value.Dispose();
            }
            groupDictionary.Clear();


            // 释放托管资源(一般用不到，不需要手动释放，依赖垃圾回收器自动回收)
            if (disposing)
            {
                //GC.Collect();
            }
        }
        #endregion
    }
}
