using Autofac;
using OpcClient;
using OpcClient.Config;
using OpLink.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpLink
{
    public partial class Main : Form
    {
        private IOpcClient client;
        private int groupTriggerUpdateRate;
        private int groupDataUpdateRate;
        private bool reconnectEnable;
        private int reConnectInterval;
        private Dictionary<string,ITagService> tagServices= new Dictionary<string,ITagService>();

        //消息最大显示条数
        private int maxMsg = 100;
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //增加CLR搜索的路径
                CLRPrivatePathInit();
                #region 读取配置文件 
                var opcConfig = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
                var tagConfig = ConfigurationManagerExtend.SectionsCast<TagConfiguration>("Tag").FirstOrDefault().Value;
                groupTriggerUpdateRate = tagConfig.TriggerUpdateRate;
                groupDataUpdateRate = tagConfig.DataUpdateRate;
                reconnectEnable = opcConfig.ReconnectEnable;
                reConnectInterval = opcConfig.ReconnectInterval;
                #endregion
                #region OpcClient初始化
                client = OpcFinder(opcConfig.OpcTypeName);
                client.Init(opcConfig);
                //断开服务通知
                client.OpcStatusChangeHandle += this.OpcServerDisConnected;
                ConnectOpc(client);
                #endregion
                ServicesInit();
                #region  连接远程服务器
                //ConnectServer();
                #endregion
            }
            catch (Exception ex)
            {
                //记录日志
                AddMsgToList("通讯服务器校验结果：" + ex.ToString());
                return;
            }
  
        }
        #region 函数 
        /// <summary>
        /// 连接opc
        /// </summary>
        /// <param name="client"></param>
        private bool ConnectOpc(IOpcClient client)
        {
            if (client.Connect().Result==true)
            {
                client.RemoveGroupsAll();
                //创建组及绑定组内的tags
                client.CreateGroup("GroupTrigger")
                    .SetUpdateRate(groupTriggerUpdateRate)
                    .AddItems(TagConfig.QueryTagsByGroupName<Tag>("GroupTrigger"))                    
                    .ValueChangedHandle = TagValueChanged;
                client.CreateGroup("GroupData")
                    .SetUpdateRate(groupDataUpdateRate)
                    .AddItems(TagConfig.QueryTagsByGroupName<Tag>("GroupData"))
                    .AddQueue(100);                   
                OpcServerRefreshUI(client);
                return true;
            }
            else
            {
                OpcServerRefreshUI(client);
                return false;
            }
        }
        /// <summary>
        /// 断开opc
        /// </summary>
        /// <param name="client"></param>
        private bool DisconnectOpc(IOpcClient client)
        {
            if (client.Disconnect())
            {
                OpcServerRefreshUI(client);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// UI信息刷新
        /// </summary>
        /// <param name="msg"></param>
        private void OpcServerRefreshUI(IOpcClient client)
        {
            //查询控件的 InvokeRequired 属性。
            //如果 InvokeRequired 返回 true，则使用实际调用控件的委托来调用 Invoke。
            //如果 InvokeRequired 返回 false，则直接调用控件。
            if (this.InvokeRequired)
            {
                // 加个委托显示msg,因为on系列都是在工作线程中调用的,ui不允许直接操作
                // 很帅的调自己
                this.Invoke(new Action<IOpcClient>((m) => OpcServerRefreshUI(m)), client);
            }
            else
            {
                AddMsgToList(client.ServerStateDesc);
                btnStart.Enabled = !client.OpcStatus;
                btnStop.Enabled = client.OpcStatus;
                lblStatus.Text = client.OpcStatus? "运行":"停止";
                lblStatus.BackColor = client.OpcStatus ? Color.Green:Color.Red;
                tsslServerState.Text = client.ServerStateDesc.PadRight(20, ' ');
                tsslServerStartTime.Text = client.ServerStartTime.PadRight(10, ' ');
                //tsslServerStartTime.Text = client.ServerStartTime == null ? DateTime.Now.ToString() : client.ServerStartTime.PadRight(10, ' ');
                tsslversion.Text = ("版本号：" + client.ServerVersion).PadRight(15, ' ');
            }
        }
        /// <summary>
        /// 监听通讯服务器断开
        /// </summary>
        private void ServerClosedToList()
        {
            AddMsgToList("通讯服务器已断开");
        }
        /// <summary>
        /// 往listbox加一条项目
        /// </summary>
        /// <param name="msg">信息</param>
        private void AddMsgToList(string msg)
        {
            //查询控件的 InvokeRequired 属性。
            //如果 InvokeRequired 返回 true，则使用实际调用控件的委托来调用 Invoke。
            //如果 InvokeRequired 返回 false，则直接调用控件。
            if (this.lbxMsg.InvokeRequired)
            {
                // 加个委托显示msg,因为on系列都是在工作线程中调用的,ui不允许直接操作
                // 很帅的调自己
                this.lbxMsg.Invoke(new Action<string>((m) => AddMsgToList(m)), msg);
            }
            else
            {
                if (this.lbxMsg.Items.Count > maxMsg)
                {
                    this.lbxMsg.Items.RemoveAt(0);
                }
                var time = DateTime.Now;
                this.lbxMsg.Items.Add(time+" : "+msg);
                this.lbxMsg.TopIndex = this.lbxMsg.Items.Count - (int)(this.lbxMsg.Height / this.lbxMsg.ItemHeight);
            }
        }
        #endregion
        #region service
        /// <summary>
        /// 初始化自定义服务
        /// </summary>
        private void ServicesInit()
        {
            //string path = Application.StartupPath;
            //var dfd = GetServicePathBinding(path);
            //SetServicePathBinding(path, "Services/ServiceForPRARE;Services/ServiceForEasySocketService");
            ContainerBuilder builder = new ContainerBuilder();
            List<string> serviceNames = new List<string>();
            Type baseType = typeof(IDependency);
            // 获取所有相关类库的程序集
            Assembly[] assemblies = GetApiAssemblies();
            List<NamedParameter> ListNamedParameter = new List<NamedParameter>() { new NamedParameter("opcClient", client)};
            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> types = assembly.GetTypes()
                    .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract);
                serviceNames.AddRange(types.Select(type => type.Name));
                foreach (Type type in types)
                {
                    builder.RegisterType(type)
                        .Named<ITagService>(type.Name)
                        .WithParameters(ListNamedParameter)
                        .SingleInstance();
                }
            }
            //using (Autofac.IContainer container = builder.Build())
            //{               
            //    tagServices.Clear();
            //    foreach (var serviceName in serviceNames)
            //    {
            //        ITagService tagService = container.ResolveNamed<ITagService>(serviceName); 
            //        tagService.MsgHandle = AddMsgToList;
            //        tagService.Connect();
            //        tagServices.Add(serviceName, tagService);
            //        AddMsgToList(serviceName+" > "+"[Loaded]");
            //    }
            //}
            
            //构建容器来完成注册并准备对象解析。
            Autofac.IContainer container = builder.Build();
            // 现在您可以使用Autofac解决服务问题。例如，这一行将执行注册到IConfigReader服务的lambda表达式。
            using (var scope = container.BeginLifetimeScope())
            {
                tagServices.Clear();
                foreach (var serviceName in serviceNames)
                {
                    ITagService tagService = container.ResolveNamed<ITagService>(serviceName);
                    tagService.MsgHandle = AddMsgToList;
                    tagService.Connect();
                    tagServices.Add(serviceName, tagService);
                    AddMsgToList(serviceName + " > " + "[Loaded]");
                }
            }
        }
        /// <summary>
        /// 获取程序域内所有第三方程序集,在Api目录下
        /// 不包含当前程序集和全局程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetApiAssemblies()
        {
            List<Assembly> assemblys = new List<Assembly>();
            string windowsPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Services");
            DirectoryInfo folder = new DirectoryInfo(windowsPath);
            foreach (DirectoryInfo NextFolder in folder.GetDirectories())
            {
                foreach (string dllFile in Directory.GetFiles(NextFolder.FullName, "*.dll"))
                {
                    try
                    {
                        string dllFileName = dllFile.Substring(dllFile.LastIndexOf("\\") + 1, (dllFile.LastIndexOf(".") - dllFile.LastIndexOf("\\") - 1)); //文件名
                                                                                                                                                           //必须采用Assembly.Load才能使用全局已初始化的静态方法
                        var assembly = Assembly.Load(dllFileName);
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
        /// 初始化Opc服务
        /// </summary>
        private IOpcClient OpcFinder(string opcName)
        {           
            ContainerBuilder builder = new ContainerBuilder();
            Type opcType=null;
            Type baseType = typeof(IOpcClient);
            // 获取所有OPC相关类库的程序集
            Assembly[] assemblies = GetOpcAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                try
                {                  
                    // opcType = assembly.GetTypes()
                    //.Where(type => type.Name == opcName && baseType.IsAssignableFrom(type) && !type.IsAbstract).FirstOrDefault();
                    opcType = assembly.GetType(opcName); 
                    if (opcType != null)
                    {
                        builder.RegisterType(opcType)
                           .Named<IOpcClient>(opcName)
                           .SingleInstance();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    AddMsgToList(ex.ToString());
                }               
            }
            //Autofac.IContainer container = builder.Build();
            //IOpcClient opcClient = container.ResolveNamed<IOpcClient>(opcName);
           
            IOpcClient opcClient;
            //构建容器来完成注册并准备对象解析。
            Autofac.IContainer container = builder.Build();
            // 现在您可以使用Autofac解决服务问题。例如，这一行将执行注册到IConfigReader服务的lambda表达式。
            using (var scope = container.BeginLifetimeScope())
            {
                opcClient = container.ResolveNamed<IOpcClient>(opcName);
            }
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
                        //必须采用Assembly.Load才能使用全局已初始化的静态方法
                        var assembly = Assembly.Load(dllFileName);
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
        #endregion
        #region 事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            ConnectOpc(client);
            //ConnectServer();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            DisconnectOpc(client);
        }
        /// <summary>
        /// Tag点变化时通知
        /// </summary>
        /// <param name="tag"></param>
        private void TagValueChanged(Tag tag)
        {
            foreach (var dic in tagServices)
            {
                Task.Run(() =>
                {
                    try
                    {
                        dic.Value.TagChangedExecute(tag);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                });               
            }
            //AddMsgToList(string.Format("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataType));
        }
        /// <summary>
        /// 服务端断开通知
        /// </summary>
        /// <param name="msg"></param>
        private void OpcServerDisConnected(OpcStatusEventArgs e)
        {
            
        }     
        private void btnServiceSetting_Click(object sender, EventArgs e)
        {
            ServicePath servicePathWindow = new ServicePath();
            if (servicePathWindow.ShowDialog() == DialogResult.OK)
            {                
                MessageBox.Show("重启后配置生效~");
            }
        }
        private void btnMax_Click(object sender, EventArgs e)
        {
            var value = client["GroupData"]
                .GetTagValue("Ramp101")
                .TagHistory.Max();
            var tvalue = client["GroupData"]
                .GetTagHistory("Ramp102")
                .ByDateLargerThan(DateTime.Now.AddSeconds(-10))
                .Max();
            var bvalue = client["GroupData"]
               .GetTagHistory("Ramp103")
               .ByDateLargerThan(DateTime.Now.AddSeconds(-10))
               .Average();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            //执行OpMonitor.exe
            String proc = "OpMonitor";
            //Get the list of all processes by that name   
            Process[] processes = Process.GetProcessesByName(proc);
            //If there is more than one process   
            if (processes.Length >=1)
            {
                AddMsgToList("OpMonitor.exe启动失败,【已启动实例】...");
                return;
            }
            else
            {
                var path = Path.Combine( Application.StartupPath ,"OpMonitor.exe");
                Process.Start(path); //filiName 是你要运行的程序名，是物理路径
            }
        }

        private void ToolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon.Visible = false;
            }
            //还原窗体显示    
            WindowState = FormWindowState.Normal;
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon.Visible = false;
            }
        }
        #endregion
    }
}
