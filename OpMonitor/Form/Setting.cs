using Autofac;
using OpcClient;
using OpcClient.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace OpMonitor
{
    public partial class Setting : Form
    {
        private XmlDocument doc = new XmlDocument();
        private IOpcClient client;
        private string groupName = "";
        private string blockName = "";
        private OpcAddressConfiguration opcConfig;
        private ImageList imageList = new ImageList();
        /// <summary>
        /// Tag表有元素发生变化标识，以此判断是否需要重新初始化Tags列表
        /// </summary>
        private bool isdDtaGridTagsChanged = false;

        public Setting()
        {
            InitializeComponent();
            imageList.Images.Add("grid_Data_16xLG", Properties.Resources.grid_Data_16xLG);
            imageList.Images.Add("Enum_582", Properties.Resources.Enum_582);
            treeTags.ImageList = imageList;
            this.dataGridTags.DoubleBufferedDataGirdView(true);
            //解决树节点锁定后节点渲染的问题
            treeTags.HideSelection = false;
            treeTags.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeTags.DrawNode += new DrawTreeNodeEventHandler(treeTags_DrawNode);
        }

        private void Setting_Load(object sender, EventArgs e)
        {           
            //读取基础配置文件
            ConfigInit();
            //增加CLR搜索的路径
            CLRPrivatePathInit();
            //加载xml
            XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
            RecursionTreeControl(doc, treeTags.Nodes);         
            treeTags.ExpandAll();//展开TreeView控件中的所有项 

            #region OpcClient初始化  
            client = OpcFinder(opcConfig.OpcTypeName);
            client.Init(opcConfig);

            //opc.tcp://127.0.0.1:49328
            //client.OpcStatusChangeHandle = this.OpcServerDisConnected;
            if (client.Connect().Result == false)
            {
                iniSetting();
                //退出
                System.Environment.Exit(0);
            }
            else
            {
                client.CreateGroup("GroupTrigger").ValueChangedHandle = TagValueChanged;
                client.CreateGroup("GroupData");
            }
            #endregion
            tsslServerState.Text = client.ServerStateDesc;
            tsslServerStartTime.Text = "时间" + client.ServerStartTime;
            tsslversion.Text = "版本号：" + client.ServerVersion;
            cmbInterval.SelectedIndex = 2;
            
            dataGridTags.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
        //关闭
        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
        }

        #region 函数
        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void ConfigInit()
        {
            opcConfig = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
            if (opcConfig.OpcProtocolByEnum ==OpcProtocol.Else)
            {
                UAIniSetting iniS = new UAIniSetting();
                if (iniS.ShowDialog() == DialogResult.OK)
                {
                    opcConfig = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
                }
                else
                {
                    this.Close();
                }
            }
        }
        /// </summary>
        ///  RecursionTreeControl:表示将XML文件的内容显示在TreeView控件中
        /// </summary>
        /// <param name="xmlNode">将要加载的XML文件中的节点元素</param>
        /// <param name="nodes">将要加载的XML文件中的节点集合</param>
        /// <param name="lev">设定遍历的初始层级</param>
        /// <param name="levelMax">设定遍历的最大层级</param>
        private void RecursionTreeControl(XElement xmlList, TreeNodeCollection nodes, int lev = 0, int levelMax = 2)
        {
            int level = lev + 1;
            foreach (XElement node in xmlList.Elements())
            {                
                if (level <= levelMax)
                {
                    TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象
                    //图标
                    if (level == 1)
                    {
                        new_child.ImageIndex= new_child.SelectedImageIndex = 0;
                    }
                    else
                    {
                        new_child.ImageIndex = new_child.SelectedImageIndex = 1;
                    }

                    new_child.Name = node.Attribute("Name").Value;

                    new_child.Text = node.Attribute("Name").Value;

                    nodes.Add(new_child);//向当前TreeNodeCollection集合中添加当前节点
                    RecursionTreeControl(node, new_child.Nodes, level);//调用本方法进行递归
                }
                else
                {
                    return;
                }
            }            
        }
        /// <summary>
        /// 重载dataGridTags的Tag定义
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        private void QueryTags(string groupName, string blockName)
        {
            //显示block中的tag点明细
            List<Tag> tags = TagConfig.QueryTagsByBlockName<Tag>(groupName, blockName);
            List<GridTag> list = tags.Select(p => new GridTag { TagName= p.TagName, OpcTagName=p.OpcTagName, TimeStamps=p.TimeStamps, Qualities=p.Qualities, Value= p.Value, Message=p.Message }).ToList();
            dataGridTags.DataSource = new BindingList<GridTag>(list);
        }
        /// <summary>
        /// 刷新dataGridTags的Tag数据
        /// </summary>
        /// <param name="groupName">groupName</param>
        private void QueryTagsRecord(string groupName)
        {
            if (groupName == "")
            {
                return;
            }
            List<Tag> tags = new List<Tag>();
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                Tag bi = new Tag();
                bi.OpcTagName = row.Cells["OpcTagName"].Value.ToString();
                bi.TagName = row.Cells["TagName"].Value.ToString();
                bi.TimeStamps = DateTime.Now;
                bi.Value = "";
                bi.Qualities = "";
                bi.Message = "";
                tags.Add(bi);
            }
            
            //异步方式获取返回数据集
            //兼容4.0
            //await Task.Run(() =>
            //{
            //    client.GetTagValuesFromGroup(ref tags, groupName);
            //});
            Task.WaitAny(Task.Run(() =>
           {
               client.GetTagValuesFromGroup(ref tags, groupName);
           }));

            //dataGridTags.DataSource = tags.Select(p => new { p.TagName, p.OpcTagName, p.TimeStamps, p.Qualities, p.Value, p.Message }).ToList(); ;

            for (int i = 0; i < tags.Count; i++)
            {                
                dataGridTags.Rows[i].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                dataGridTags.Rows[i].Cells["TagName"].Value = (tags[i]).TagName;
                dataGridTags.Rows[i].Cells["DataType"].Value = (tags[i]).DataTypeName;
                dataGridTags.Rows[i].Cells["Value"].Value = (tags[i]).Value;
                dataGridTags.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                dataGridTags.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
                dataGridTags.Rows[i].Cells["Message"].Value = (tags[i]).Message;
            }            
        }
        /// <summary>
        /// 重载dataGridTags的Tag数据
        /// </summary> 
        private void IniTagsRecord(string groupName)
        {
            try
            {
                if (groupName.Equals(null) || groupName.Equals(""))
                {
                    return;
                }
                //将需要重载的点压入集合
                List<Tag> listIn =((BindingList<GridTag>)dataGridTags.DataSource).Select(p => new Tag { OpcTagName = p.OpcTagName, TagName = p.TagName, TimeStamps = DateTime.Now, Value = "", Qualities = "", Message = "" }).ToList();
                
                //删除分组下所有tag
                client[groupName].RemoveItemsAll();
                //opc中重新加入tag
                client[groupName].AddItems(listIn);

                //if (!client[groupName].GetTags().Any())
                //{
                //    client[groupName].AddItems(listIn);
                //}
                //else
                //{
                //    client.RefreshGroupByGroupName(groupName);
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("刷新失败，节点不存在!" + e.Message.ToString());
            }
        }
        private void TagValueChanged(Tag tag)
        {
            //Console.WriteLine("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataType);
        }

        /// <summary>
        /// 服务端断开通知
        /// </summary>
        /// <param name="msg"></param>
        private void OpcServerDisConnected(OpcStatusEventArgs e)
        {
            Console.WriteLine("服务端断开：" + e.ToString());
            //client = null;
            //System.Environment.Exit(0);
        }

        /// <summary>
        /// IP设置
        /// </summary>
        private void iniSetting()
        {
            UAIniSetting iniS = new UAIniSetting();
            if (iniS.ShowDialog() == DialogResult.OK)
            {
                var config = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
                MessageBox.Show("重启后配置生效~");
            }
        }
        #endregion
        #region Opc
        /// <summary>
        /// 初始化Opc服务
        /// </summary>
        private IOpcClient OpcFinder(string opcName)
        {
            ContainerBuilder builder = new ContainerBuilder();
            Type opcType;
            Type baseType = typeof(IOpcClient);
            // 获取所有OPC相关类库的程序集
            Assembly[] assemblies = GetOpcAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    opcType = assembly.GetType(opcName);
                    if (opcType != null)
                    {
                        builder.RegisterType(opcType)
                           .Named<IOpcClient>(opcName)
                           .SingleInstance();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            Autofac.IContainer container = builder.Build();
            IOpcClient opcClient = container.ResolveNamed<IOpcClient>(opcName);
            return opcClient;
        }
        /// <summary>
        /// 获取程序域内所有Opc程序集,在根目录下
        /// 不包含当前程序集和全局程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetOpcAssemblies()
        {
            List<Assembly> assemblys = new List<Assembly>();
            string windowsPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Opc");
            DirectoryInfo folder = new DirectoryInfo(windowsPath);
            foreach (DirectoryInfo NextFolder in folder.GetDirectories())
            {
                foreach (string dllFile in Directory.GetFiles(NextFolder.FullName, "*.dll"))
                {
                    try
                    {
                        string dllFileName = dllFile.Substring(dllFile.LastIndexOf("\\") + 1, (dllFile.LastIndexOf(".") - dllFile.LastIndexOf("\\") - 1)); //文件名
                        var assembly = Assembly.Load(dllFileName);//LoadFile不需要指定加载路径app.config
                        assemblys.Add(assembly);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            return assemblys.ToArray();
        }
        /// <summary>
        /// 增加CLR搜索的路径
        /// </summary>
        public static void CLRPrivatePathInit()
        {
            string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            //OPC路径加载
            string opcPath = Path.Combine(baseDir, "Opc");
            foreach (var dir in new DirectoryInfo(opcPath).GetDirectories())
            {
                var path = dir.FullName.Replace(baseDir, "");
                AppDomain.CurrentDomain.AppendPrivatePath(path);
            }

            //Services路径加载
            string servicesPath = Path.Combine(baseDir, "Services");
            foreach (var dir in new DirectoryInfo(servicesPath).GetDirectories())
            {
                var path = dir.FullName.Replace(baseDir, "");
                AppDomain.CurrentDomain.AppendPrivatePath(path);
            }
        }
        /// <summary>
        /// 增加树图标
        /// </summary>
        /// <param name="tn"></param>
        private void SetIcon(TreeNode tn)
        {
            foreach (TreeNode node in tn.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    node.ImageIndex = 0;
                    SetIcon(node);
                }
                else
                {
                    node.ImageIndex = 1;
                }
            }

        }
        #endregion
        #region 事件
        //双击显示gridTags
        private void treeTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treeTags.GetNodeAt(clickPoint);
                if (treeNode != null)
                {

                    if (treeNode.Level == 0)
                    {
                        //不处理
                    }
                    else if (treeNode.Level == 1)
                    {
                        groupName = treeNode.Parent.Text.ToString();
                        blockName = treeNode.Text.ToString();

                        QueryTags(groupName, blockName);//在gridview中加载需要监视的tag点
                        isdDtaGridTagsChanged = true;
                        //IniTagsRecord(groupName);//OPC初始化监视的tag点
                        
                        lblGroup.Text = groupName;
                        lblBlock.Text = blockName;
                    }
                    //使treeTags的SelectedNode为当前新建的节点，也就是右键后的节点被选中  
                    treeTags.SelectedNode = treeNode;

                    //发生变化则重新初始化Tags
                    if (isdDtaGridTagsChanged)
                    {
                        IniTagsRecord(groupName);
                        isdDtaGridTagsChanged = false;
                    }
                    if (client != null)
                    {
                        QueryTagsRecord(groupName);
                    }
                }
            }
        }
        //treeview点击事件，根据层级不同弹出不同的ContextMenuStrip
        private void treeTags_MouseDown(object sender, MouseEventArgs e)
        {
            //右键点击，弹出对应的菜单
            if (e.Button == MouseButtons.Right)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treeTags.GetNodeAt(clickPoint);
                if (treeNode != null)
                {

                    if (treeNode.Level == 0)
                    {
                        //第一层关联菜单
                        treeNode.ContextMenuStrip = contextMenuGroup;
                    }
                    else if (treeNode.Level == 1)
                    {
                        //第二层关联菜单
                        treeNode.ContextMenuStrip = contextMenuBlock;
                    }
                }
                //使treeTags的SelectedNode为当前新建的节点，也就是右键后的节点被选中  
                treeTags.SelectedNode = treeNode;
            }
        }
        //新增block
        private void toolBlockAdd_Click(object sender, EventArgs e)
        {
            //定义块新增控制的委托
            Func<string,bool> addBlock = (blockName) =>
            {
                if (TagConfig.CreateBlock(treeTags.SelectedNode.Text, blockName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            ControlBlock ab = new ControlBlock(addBlock);
            if (ab.ShowDialog() == DialogResult.OK)
            {
                //清空后并重新加载
                treeTags.Nodes.Clear();
                XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
                RecursionTreeControl(doc, treeTags.Nodes);
                treeTags.ExpandAll();//展开TreeView控件中的所有项               
            }
        }
        //新增tag
        private void toolTagAdd_Click(object sender, EventArgs e)
        {
            chkRefresh.Checked = false;
            //设置当前选中的节点信息
            groupName = treeTags.SelectedNode.Parent.Text;
            blockName = treeTags.SelectedNode.Text;
            lblGroup.Text = groupName;
            lblBlock.Text = blockName;

            AddTags at = new AddTags(client, groupName, blockName);
            if (at.ShowDialog() == DialogResult.OK)
            {
                //显示block中的tag点明细
                List<Tag> tags = TagConfig.QueryTagsByBlockName<Tag>(groupName, blockName);

                List<GridTag> list = tags.Select(p => new GridTag { TagName = p.TagName, OpcTagName = p.OpcTagName, TimeStamps = p.TimeStamps, Qualities = p.Qualities, Value = p.Value, Message = p.Message }).ToList();
                dataGridTags.DataSource = new BindingList<GridTag>(list);
                isdDtaGridTagsChanged = true;
            }
        }
        //删除block
        private void toolBlockDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!TagConfig.DelBlock(treeTags.SelectedNode.Text))
                {
                    MessageBox.Show("删除失败，不在在节点");
                }
                else
                {
                    //成功删除后刷新
                    treeTags.Nodes.Clear();
                    //加载treeview 
                    XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
                    RecursionTreeControl(doc, treeTags.Nodes);
                    dataGridTags.Rows.Clear();
                    treeTags.ExpandAll();//展开TreeView控件中的所有项

                    groupName = "";
                    blockName = ""; ;
                    lblGroup.Text = groupName;
                    lblBlock.Text = blockName;
                }
            }
        }
        //删除tag
        private void toolTagDel_Click(object sender, EventArgs e)
        {         
            BindingList<GridTag> list = (BindingList < GridTag > )dataGridTags.DataSource;           
            foreach (DataGridViewRow row in dataGridTags.SelectedRows)
            {
                string tagName = dataGridTags.Rows[row.Index].Cells["TagName"].Value.ToString();
                Tag tag = new Tag() { TagName = tagName };
                if (TagConfig.DelTag(groupName, blockName, tag))
                {
                    list.RemoveAt(row.Index);
                }
            }
            isdDtaGridTagsChanged = true;
        }
        //treeview刷新
        private void toolGroupsRefresh_Click(object sender, EventArgs e)
        {
            //清空并重新加载treeview
            treeTags.Nodes.Clear();
            //加载treeview 
            XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
            RecursionTreeControl(doc, treeTags.Nodes);
            treeTags.ExpandAll();//展开TreeView控件中的所有项
        }
        /// <summary>
        /// block名称修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBlockNameEdit_Click(object sender, EventArgs e)
        {
            //定义块新增控制的委托
            Func<string, bool> editBlock = (newblockName) =>
            {
                string blockName = treeTags.SelectedNode.Text;
                string groupName = treeTags.SelectedNode.Parent.Text;
                if (TagConfig.EditBlock(groupName,blockName,newblockName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            ControlBlock ab = new ControlBlock(editBlock, treeTags.SelectedNode.Text);
            if (ab.ShowDialog() == DialogResult.OK)
            {
                //清空后并重新加载
                treeTags.Nodes.Clear();
                XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
                RecursionTreeControl(doc, treeTags.Nodes);
                treeTags.ExpandAll();//展开TreeView控件中的所有项
                lblBlock.Text = ab.NewBlockName;
            }
        }
        //gridview刷新
        private void toolTagsRefresh_Click(object sender, EventArgs e)
        {
            QueryTags(groupName, blockName);
        }
        private void toolTagsNameCopy_Click(object sender, EventArgs e)
        {
            if (dataGridTags.SelectedRows.Count==1)
            {
                Clipboard.SetDataObject(dataGridTags.SelectedRows[0].Cells["TagName"].Value.ToString());
            }            
        }
        /// <summary>
        /// 值模拟写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolTagWriteValue_Click(object sender, EventArgs e)
        {
            //当选择唯一时，获取tag点
            if (dataGridTags.SelectedRows.Count!= 1)
            {
                return;              
            }
            //opc重新绑定
            string tagName = dataGridTags.SelectedRows[0].Cells["TagName"].Value.ToString();  //将需要重载的点压入集合
            Tag  tag = ((BindingList<GridTag>)dataGridTags.DataSource).Where(k=>k.TagName== tagName).
                Select(p => new Tag { OpcTagName = p.OpcTagName, TagName = p.TagName, TimeStamps = DateTime.Now, Value = "", Qualities = "", Message = "" }).First();

            ////删除分组下所有tag
            //client[groupName].RemoveItemsAll();
            ////opc中重新加入tag
            //client[groupName].AddItem(tag);
            //QueryTagsRecord(groupName);

            //绑定完成后获取tag点
            string grouName = lblGroup.Text;
            Tag bi = client[grouName].GetTag(tagName);

            //定义块新增控制的委托
            Func<Tag, bool> writeValue = (tagNew) =>
            {
                try
                { 
                    client["GroupData"].SetTagValue(tagNew);
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
              
            };
            WriteValue ab = new WriteValue(writeValue, bi);
            if (ab.ShowDialog() == DialogResult.OK)
            {
                //模拟成功则刷新数据
                QueryTagsRecord(groupName);
            }




          
        }

        /// <summary>
        /// 复制当前数值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolTagCopyValue_Click(object sender, EventArgs e)
        {
            if (dataGridTags.SelectedRows.Count == 1)
            {
                Clipboard.SetDataObject(dataGridTags.SelectedRows[0].Cells["Value"].Value.ToString());
            }
        }

        //gridview刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //发生变化则重新初始化Tags
            if (isdDtaGridTagsChanged)
            {
                IniTagsRecord(groupName);
                isdDtaGridTagsChanged = false;
            }
            if (client != null)
            {
                QueryTagsRecord(groupName);
            }
           
        }

        //gridview弹出ContextMenuStrip
        private void dataGridTags_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridTags.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridTags.ClearSelection();
                        dataGridTags.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridTags.SelectedRows.Count == 1)
                    {
                        dataGridTags.CurrentCell = dataGridTags.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuTags.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }
        /// <summary>
        /// gridview行号显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridTags_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        //是否自动获取数据
        private void chkRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRefresh.Checked == false)
            {
                chkRefresh.ForeColor = Color.Black;
                timerRefresh.Stop();
                treeTags.Enabled = true;
                dataGridTags.Enabled = true;
                groupBoxWatch.Enabled = false;
                isdDtaGridTagsChanged = false;
            }
            else if (chkRefresh.Checked == true)
            {
                chkRefresh.ForeColor = Color.Green;
                treeTags.Enabled = false;
                dataGridTags.Enabled = false;
                groupBoxWatch.Enabled = true;
                timerRefresh.Interval = Convert.ToInt32(cmbInterval.Text);
                timerRefresh.Start();
                //发生变化则重新初始化Tags
                if (isdDtaGridTagsChanged)
                {
                    IniTagsRecord(groupName);
                }                
            }
        }
        //刷新数据
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (chkRefresh.Checked == true)
            {
                if (client!= null)
                {
                    QueryTagsRecord(groupName);
                    dataGridTags.ClearSelection();
                }              
            }
        }
        //改变刷新周期
        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerRefresh.Interval = Convert.ToInt32(cmbInterval.Text);
        }
        //IP设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            iniSetting();
        }
        #endregion

        //树节点渲染
        private void treeTags_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
/// <summary>
/// Grid显示用
/// </summary>
class GridTag
{
    public string TagName { get; set; }
    public string OpcTagName { get; set; }
    public DateTime TimeStamps { get; set; }    
    public object Value { get; set; }
    public string Qualities { get; set; }
    public string Message { get; set; }
}

////对于生成树的部分其实还有一种Linq的实现方式如下
//private void SaveToXml()

//        {

//            XDeclaration dec = new XDeclaration("1.0", "utf-8", "yes");

//            XDocument xml = new XDocument(dec);



//            XElement root = new XElement("Tree");



//            foreach (TreeNode node in treeTags.Nodes)

//            {

//                XElement e = CreateElements(node);

//                root.Add(e);

//            }

//            xml.Add(root);

//            xml.Save("TreeXml.xml");

//        }



//private XElement CreateElements(TreeNode node)

//{

//    XElement root = CreateElement(node);



//    foreach (TreeNode n in node.Nodes)

//    {

//        XElement e = CreateElements(n);

//        root.Add(e);

//    }

//    return root;

//}



//private XElement CreateElement(TreeNode node)

//{

//    return new XElement("Node",

//        new XAttribute("Name", node.Name),

//        new XAttribute("Text", node.Text)

//        );

//}
//    }
//}
