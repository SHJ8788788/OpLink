using Autofac;
using OpcClient;
using OpcClient.Config;
using OpcClient.Opc;
using OpMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OpMonitor
{
    public partial class UAIniSetting : Form
    {
        private OpcAddressConfiguration opcConfig;
        private TagConfiguration tagConfig;
        public UAIniSetting()
        {
            InitializeComponent();
            //设置默认选择项
            opcConfig = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
            tagConfig = ConfigurationManagerExtend.SectionsCast<TagConfiguration>("Tag").FirstOrDefault().Value;


            this.cmbOpcProtocol.Items.AddRange(new object[] { OpcProtocol.DA, OpcProtocol.UA });
            this.cmbUseSecurity.Items.AddRange(new object[] { true, false });
            this.cmbReconnectEnable.Items.AddRange(new object[] { true, false });

            this.cmbOpcProtocol.SelectedIndexChanged += new System.EventHandler(this.cmbOpcProtocol_SelectedIndexChanged);
            this.cmbUseSecurity.SelectedIndexChanged += new System.EventHandler(this.cmbUseSecurity_SelectedIndexChanged);

            this.cmbOpcProtocol.SelectedItem = opcConfig.OpcProtocolByEnum;
            this.txtIP.Text = opcConfig.DaAddress.Ip;
            this.cmbServerName.Items.Add(opcConfig.DaAddress.ServerName);
            this.cmbServerName.SelectedIndex = 0;
            this.txtUri.Text = opcConfig.UaAddress.Uri;
            this.txtUserName.Text = opcConfig.UaAddress.UserName;
            this.txtPassword.Text = opcConfig.UaAddress.Password;
            this.cmbUseSecurity.SelectedItem = opcConfig.UaAddress.UseSecurity;
            this.cmbReconnectEnable.SelectedItem = opcConfig.ReconnectEnable;
            this.cmbReconnectInterval.SelectedItem = opcConfig.ReconnectInterval.ToString();
            this.cmbTagsQueueNum.SelectedItem = tagConfig.TagsQueueNum.ToString();
            LayoutInit();
        }

        private void UAIniSetting_Load(object sender, EventArgs e)
        {
            //增加CLR搜索的路径
            CLRPrivatePathInit();
        }
        /// <summary>
        /// 控件状态判断与控制
        /// </summary>
        private void LayoutInit()
        {
            if ((OpcProtocol)this.cmbOpcProtocol.SelectedItem == OpcProtocol.DA)
            {

            }
            else if ((OpcProtocol)this.cmbOpcProtocol.SelectedItem == OpcProtocol.UA)
            {
                this.cmbUseSecurity.Enabled = true;
                if ((Boolean)cmbUseSecurity.SelectedItem == true && this.cmbUseSecurity.Enabled == true)
                {
                    this.txtUserName.Enabled = true;
                    this.txtPassword.Enabled = true;
                }
                else
                {
                    this.txtUserName.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.txtUserName.Text = "";
                    this.txtPassword.Text = "";
                }
            }
            else
            {
                tabUA.Parent = null;
                tabDA.Parent = null;
                MessageBox.Show("配置不存在！");
            }
        }

        private void btnGetServNames_Click(object sender, EventArgs e)
        {
            List<string> list = OpcFinder("DaOpcClient.DaOpc").ServerList;
            cmbServerName.Items.Clear();
            foreach (string turn in list)
            {
                cmbServerName.Items.Add(turn);
            }
            cmbServerName.SelectedIndex = 0;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //OPC设置
            OpcProtocol iOpcProtocol = (OpcProtocol)cmbOpcProtocol.SelectedItem;
            opcConfig.OpcProtocol = Convert.ToInt32(iOpcProtocol);
            opcConfig.OpcTypeName = txtTypeName.Text.ToString();
            opcConfig.ReconnectInterval = Convert.ToInt32(cmbReconnectInterval.SelectedItem);
            opcConfig.ReconnectEnable = (Boolean)cmbReconnectEnable.SelectedItem;            
            if (iOpcProtocol == OpcProtocol.DA)
            {
                var iIp = txtIP.Text;
                var iServerName = cmbServerName.Text;
                opcConfig.DaAddress.Ip = iIp.ToString();
                opcConfig.DaAddress.ServerName = iServerName.ToString();
            }
            else if (iOpcProtocol == OpcProtocol.UA)
            {
                var iUri = txtUri.Text;
                var iUserName = txtUserName.Text;
                var iPassword = txtPassword.Text;
                var iUseSecurity = cmbUseSecurity.SelectedItem;
                opcConfig.UaAddress.Uri = iUri.ToString();
                opcConfig.UaAddress.UserName = iUserName.ToString();
                opcConfig.UaAddress.Password = iPassword.ToString();
                opcConfig.UaAddress.UseSecurity = (Boolean)iUseSecurity;
            }
            else
            {
                MessageBox.Show("请选择UA DA后重试！");
                return;
            }
            //Tag设置
            tagConfig.TagsQueueNum = Convert.ToInt32(cmbTagsQueueNum.SelectedItem);

            //保存设置       
            try
            {
                ConfigurationManagerExtend.SectionSave(opcConfig, "default", "Address");
                ConfigurationManagerExtend.SectionSave(tagConfig, "default", "Tag");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("保存失败！内容:{0}", ee.Message), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void cmbOpcProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpcProtocol selected = (OpcProtocol)((ComboBox)sender).SelectedItem;
            if (selected == OpcProtocol.DA)
            {
                tabUA.Parent = null;
                tabDA.Parent = tabControl1;
                txtTypeName.Text = "DaOpcClient.DaOpc";
            }
            else if (selected == OpcProtocol.UA)
            {
                tabDA.Parent = null;
                tabUA.Parent = tabControl1;
                txtTypeName.Text = "UaOpcClient.UaOpc";
            }
            else
            {
                tabUA.Parent = null;
                tabDA.Parent = null;
                txtTypeName.Text = "unknow";
            }
        }

        private void cmbUseSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayoutInit();
        }

        /// <summary>
        /// 初始化Opc服务
        /// </summary>
        private IOpcClient OpcFinder(string opcName)
        {
            ContainerBuilder builder = new ContainerBuilder();
            Type opcType = default;
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
                                                                                                                                                           //必须采用Assembly.Load才能使用全局已初始化的静态方法
                                                                                                                                                           //var assembly = Assembly.Load(dllFileName);
                        var assembly = Assembly.Load(dllFileName);// LoadFile不需要指定加载路径app.config
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
    }
}
