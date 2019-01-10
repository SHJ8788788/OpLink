using Opc.Ua;
using Opc.Ua.Client;
using OpcClient;
using OpcClient.Config;
using OpcClient.Opc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UaOpcClient
{
    public class UaOpc : UaOpcHelper, IOpcClient
    {
        // 摘要: 
        //     根据组名获取组
        //     获取或设置与指定的键相关联的值。
        //
        // 参数: 
        //   key:
        //     要获取或设置的值的键。
        public IGroup this[string groupName] { get { return this.groupDictionary[groupName]; } }

        /// <summary>
        /// OPC连接状态
        /// </summary>
        public bool OpcStatus { get { return m_IsConnected; } }
        /// <summary>
        /// OPC服务端运行开始时间
        /// </summary>
        public string ServerStartTime { get { return DateTime.Now.ToString(); } }
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
        public List<string> ServerList { get; private set; }
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
        /// <summary>
        /// OPCGroup字典
        /// </summary>
        private readonly Dictionary<string, IGroup> groupDictionary = new Dictionary<string, IGroup>();


        /// <summary>
        /// 唯一实例
        /// </summary>
        private static readonly Lazy<IOpcClient> instance = new Lazy<IOpcClient>(() => new UaOpc());
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static IOpcClient Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UaOpc() : base()
        {
            //绑定状态变化事件
            this.ReconnectComplete += M_UaOpc_ReconnectComplete;
            this.ReconnectStarting += M_UaOpc_ReconnectStarting;
            this.ConnectComplete += M_UaOpc_ConnectComplete;
            this.OpcStatusChange += M_UaOpc_StatusChange;
        } 

        #region IOpcClient 成员
        public IGroup CreateGroup(string groupName)
        {
            try
            {                
                var group = new UaGroup(groupName,this);
                groupDictionary.Add(groupName, group);
                group.AddItemsComplete = M_AddItemsComplete;
                base.RemoveSubscription(groupName);
                //base.AddSubscription(groupName, groupDictionary[groupName].GetTags().Select(p => p.OpcTagName).ToArray(), SubCallBack);
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IGroup CreateGroup(IGroup group)
        {
            try
            {
                groupDictionary.Add(group.GroupName, group);
                base.RemoveSubscription(group.GroupName);
                base.AddSubscription(group.GroupName, groupDictionary[group.GroupName].GetTags().Select(p => p.OpcTagName).ToArray(), SubCallBack);
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 在group发生变化后刷新并生效
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool RefreshGroupByGroupName(string groupName)
        {
            try
            {
                base.RemoveSubscription(groupName);
                base.AddSubscription(groupName, groupDictionary[groupName].GetTags().Select(p => p.OpcTagName).ToArray(), SubCallBack);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Tag> GetTagValuesFromGroup(ref List<Tag> biList, string grouName)
        {
            biList = groupDictionary[grouName].GetTags().ToList();
            return biList;
        }

        public void Init(OpcAddressConfiguration address)
        {
            this.Address = address;
            this.reconnectEnable = address.ReconnectEnable;
            this.reconnectInterval = address.ReconnectInterval;
        }

        public bool RemoveGroupByGroupName(string groupName)
        {
            try
            {
                foreach (var tag in groupDictionary[groupName].GetTags())
                {
                    RemoveSubscription(tag.OpcTagName);
                }
                groupDictionary.Remove(groupName);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void RemoveGroupsAll()
        {
            base.RemoveAllSubscription();
            this.groupDictionary.Clear();
            Console.WriteLine("RemoveGroupsAll End,groupDictionary.count is {0}", groupDictionary.Count);
        }
        #region BrowseNode Support    
        public List<OpcNode> ShowBranches(string nodeId = null)
        {
            String sourceId;
            if (nodeId is null)
            {
                sourceId = ObjectIds.ObjectsFolder.ToString();
            }
            else
            {
                sourceId = nodeId;
            }
            return ShowMember(sourceId);
            //// fetch references from the server.
            //return Task.Run(() =>
            //{
            //    ReferenceDescriptionCollection references = GetReferenceDescriptionCollection(sourceId);
            //    List<OpcNode> list = new List<OpcNode>();                
            //    if (references != null)
            //    {
            //        // process results.
            //        for (int ii = 0; ii < references.Count; ii++)
            //        {
            //            ReferenceDescription target = references[ii];
            //            string key = GetImageKeyFromDescription(target, sourceId);
            //            string nodeIdSon = target.NodeId.ToString();                        
            //            bool isExpand =false;
            //            if (GetReferenceDescriptionCollection((NodeId)target.NodeId).Count > 0)
            //            {
            //                isExpand = true;
            //            }
            //            OpcNode child = new OpcNode(Utils.Format("{0}", target), nodeIdSon, key, isExpand);
            //            child.Attribute = ReadOneNodeFiveAttribute(sourceId);
            //            list.Add(child);
            //        }
            //    }
            //    return list;
            //}).Result;
        }
        public List<OpcNode> ShowLeafs(string nodeId)
        {
            return ShowMember((NodeId)nodeId);
        }
        #endregion

        /// <summary>
        /// Tag点变化回调事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="monitoredItem"></param>
        /// <param name="eventArgs"></param>
        private void SubCallBack(string groupName, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs eventArgs)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new Action<string, MonitoredItem, MonitoredItemNotificationEventArgs>(SubCallBack), key, monitoredItem, eventArgs);
            //    return;
            //}


            MonitoredItemNotification notification = eventArgs.NotificationValue as MonitoredItemNotification;
            if (this.groupDictionary.Count()==0)
            {
                return;
            }
            var tag = this.groupDictionary[groupName].GetTags().Where(p => p.OpcTagName == monitoredItem.StartNodeId.ToString()).FirstOrDefault();
            //标签时间戳
            if (tag != null)
            {
                tag.TimeStamps = Convert.ToDateTime(DateTime.Now);
                tag.Qualities = "Good";
                //标签值,必须写在最后，值变化会触发事件处理，其它值必须在此之前完成赋值
                tag.Value = notification.Value.WrappedValue.Value;
                tag.DataType = notification.Value.WrappedValue.TypeInfo.ToString();
            }
        }
        //连接OPC
        public async Task<bool> Connect()
        {
            try
            {
                if (Address.UaAddress.UseSecurity == true)
                {
                    base.UserIdentity = new UserIdentity(this.Address.UaAddress.UserName, this.Address.UaAddress.Password);
                }
                await base.ConnectServer(AddressResolve(this.Address));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        bool IOpcClient.Disconnect()
        {
            RemoveAllSubscription();
            base.Disconnect();            
            return true;
        }
        #endregion
        #region Private
        /// <summary>
        /// 根据点类型设置对应图标
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        private string GetImageKeyFromDescription(ReferenceDescription target, NodeId sourceId)
        {
            if (target.NodeClass == NodeClass.Variable)
            {
                DataValue dataValue = base.ReadNode((NodeId)target.NodeId);

                if (dataValue.WrappedValue.TypeInfo != null)
                {
                    if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.Scalar)
                    {
                        return "Enum_582";
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.OneDimension)
                    {
                        return "brackets";
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.TwoDimensions)
                    {
                        return "Module_648";
                    }
                    else
                    {
                        return "ClassIcon";
                    }
                }
                else
                {
                    return "ClassIcon";
                }
            }
            else if (target.NodeClass == NodeClass.Object)
            {
                if (sourceId == ObjectIds.ObjectsFolder)
                {
                    return "VirtualMachine";
                }
                else
                {
                    return "ClassIcon";
                }
            }
            else if (target.NodeClass == NodeClass.Method)
            {
                return "Method_636";
            }
            else
            {
                return "ClassIcon";
            }
        }
        private ReferenceDescriptionCollection GetReferenceDescriptionCollection(NodeId sourceId)
        {
            TaskCompletionSource<ReferenceDescriptionCollection> task = new TaskCompletionSource<ReferenceDescriptionCollection>();

            // find all of the components of the node.
            BrowseDescription nodeToBrowse1 = new BrowseDescription();

            nodeToBrowse1.NodeId = sourceId;
            nodeToBrowse1.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse1.ReferenceTypeId = ReferenceTypeIds.Aggregates;
            nodeToBrowse1.IncludeSubtypes = true;
            nodeToBrowse1.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View | NodeClass.VariableType | NodeClass.DataType);
            nodeToBrowse1.ResultMask = (uint)BrowseResultMask.All;

            // find all nodes organized by the node.
            BrowseDescription nodeToBrowse2 = new BrowseDescription();

            nodeToBrowse2.NodeId = sourceId;
            nodeToBrowse2.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse2.ReferenceTypeId = ReferenceTypeIds.Organizes;
            nodeToBrowse2.IncludeSubtypes = true;
            nodeToBrowse2.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.View | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.VariableType | NodeClass.DataType);
            nodeToBrowse2.ResultMask = (uint)BrowseResultMask.All;

            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
            nodesToBrowse.Add(nodeToBrowse1);
            nodesToBrowse.Add(nodeToBrowse2);

            // fetch references from the server.
            ReferenceDescriptionCollection references = FormUtils.Browse(base.Session, nodesToBrowse, false);
            return references;
        }
        /// <summary>
        /// 根据ID号查询下一级子节点集合
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        private List<OpcNode> ShowMember(NodeId sourceId)
        {
            ReferenceDescriptionCollection references = GetReferenceDescriptionCollection(sourceId);
            if (references?.Count > 0)
            {
                // 获取所有要读取的子节点
                List<OpcNode> opcNodes = new List<OpcNode>();
                for (int ii = 0; ii < references.Count; ii++)
                {
                    ReferenceDescription target = references[ii];
                    //不为属性变量则显示
                    //if (target.NodeClass!=NodeClass.Variable)
                    //{
                    //    nodeIds.Add((NodeId)target.NodeId);
                    //} 
                    string key = GetImageKeyFromDescription(target, sourceId);
                    bool isExpand = false;
                    if (GetReferenceDescriptionCollection((NodeId)target.NodeId).Count > 0)
                    {
                        isExpand = true;
                    }
                    OpcNode child = new OpcNode(Utils.Format("{0}", target), target.NodeId.ToString(), key, isExpand);
                    child.Attribute = ReadOneNodeFiveAttribute(target.NodeId.ToString());
                    if (child.Attribute!=null)
                    {
                        opcNodes.Add(child);
                    }                    
                }
           
                return opcNodes;
            }
            else
            {
                return new List<OpcNode>();
            }
        }
        /// <summary>
        /// 根据ID号查询节点属性
        /// 0:NodeClass  1:Value  2:AccessLevel  3:DisplayName  4:Description
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        private OpcNodeAttribute ReadOneNodeFiveAttribute(NodeId nodeId)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();

                NodeId sourceId = nodeId;
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.NodeClass
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Value
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.AccessLevel
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.DisplayName
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Description
                });
            
            // read all values.
            base.Session.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
            try
            {
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
                DataValue[] dataValues = results.ToArray();
                OpcNodeAttribute attribute = new OpcNodeAttribute();
                NodeClass? nodeclass = dataValues[0].WrappedValue.Value == null ? new NodeClass() : ((NodeClass)dataValues[0].WrappedValue.Value);

            ClientBase.ValidateResponse(results, nodesToRead);
         
            var description = "";
            var name = dataValues[3].WrappedValue.Value.ToString();
            var accessLevel = GetDiscriptionFromAccessLevel(dataValues[2]);
            object type;
            object value;
            if (nodeclass == NodeClass.Object)
            {                
                value = "";
                type = nodeclass.ToString();
            }
            else if (nodeclass == NodeClass.Method)
            {             
                value = "";
                type = nodeclass.ToString();
            }
            else if (nodeclass == NodeClass.Variable)
            {
                DataValue dataValue = dataValues[1];

                if (dataValue.WrappedValue.TypeInfo != null)
                {
                    type = dataValue.WrappedValue.TypeInfo.BuiltInType;
                    // type = dataValue.Value.GetType().ToString();
                    if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.Scalar)
                    {
                        value = dataValue.WrappedValue.Value;                      
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.OneDimension)
                    {
                        value = dataValue.Value.GetType().ToString();                 
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.TwoDimensions)
                    {
                        value = dataValue.Value.GetType().ToString();                   
                    }
                    else
                    {
                        value = dataValue.Value.GetType().ToString();                  
                    }
                }
                else
                {
                    value = dataValue.Value;
                    type = "null";
                }
            }
            else
            {
                value = "";
                type = nodeclass.ToString();
            }
            if (dataValues[4].WrappedValue.Value!=null)
            {
                description = dataValues[4].WrappedValue.Value.ToString();
            }
            return new OpcNodeAttribute { Name = name, Value = value, Type = type, Description = description, NodeClass = nodeclass.ToString(),AccessLevel= accessLevel };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 节点操作等级
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetDiscriptionFromAccessLevel(DataValue value)
        {
            if (value.WrappedValue.Value != null)
            {
                switch ((byte)value.WrappedValue.Value)
                {
                    case 0: return "None";
                    case 1: return "CurrentRead";
                    case 2: return "CurrentWrite";
                    case 3: return "CurrentReadOrWrite";
                    case 4: return "HistoryRead";
                    case 8: return "HistoryWrite";
                    case 12: return "HistoryReadOrWrite";
                    case 16: return "SemanticChange";
                    case 32: return "StatusWrite";
                    case 64: return "TimestampWrite";
                    default: return "None";
                }
            }
            else
            {
                return "null";
            }
        }

        private string AddressResolve(OpcAddressConfiguration address)
        {           
            return address.UaAddress.Uri;
        }
        #endregion
        #region Event
        /// <summary>
        /// OPC服务器重新连接完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_UaOpc_ReconnectComplete(object sender, EventArgs e)
        {
            ServerStateDesc = "重连成功";
            ReconnectCompleteHandle?.Invoke(this);
        }
        /// <summary>
        /// OPC服务器重新连接中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_UaOpc_ReconnectStarting(object sender, EventArgs e)
        {
            ServerStateDesc = "连接已断开,尝试重连";
            ReconnectStartingHandle?.Invoke(this);
        }
        /// <summary>
        /// OPC服务器连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_UaOpc_ConnectComplete(object sender, EventArgs e)
        {
            var msg = string.Format("Opc服务 > {0}", OpcStatus ? "[OnConnected]" : "[Disconnected]");
            ServerStateDesc = msg;            
            ConnectCompleteHandle?.Invoke(this);
        }
        /// <summary>
        /// TAG新增事件
        /// </summary>
        /// <param name="group"></param>
        private void M_AddItemsComplete(IGroup group)
        {
            base.AddSubscription(group.GroupName, group.GetTags().Select(p => p.OpcTagName).ToArray(), SubCallBack);
        }

        /// <summary>
        /// OPC服务器状态发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_UaOpc_StatusChange(object sender, OpcStatusEventArgs e)
        {
            OpcStatusChangeHandle?.Invoke(e);
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
        ~UaOpc()
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
            //this.KepServer = null;
            //opc_connected = false;
            //foreach (var item in groupDictionary)
            //{
            //    item.Value.Dispose();
            //}
            //groupDictionary.Clear();


            // 释放托管资源(一般用不到，不需要手动释放，依赖垃圾回收器自动回收)
            if (disposing)
            {
                //GC.Collect();
            }
        }
        #endregion
    }
}
