using DaOpcClient;
using OpcClient;
using OpcClient.Config;
using OpcSBForGX.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using UaOpcClient;

namespace OpcSBForGX
{
    public partial class Main : Form
    {
        private IOpcClient client;
        private string opcIP;
        private string ServerName;
        private int groupTriggerUpdateRate;
        private int groupDataUpdateRate;
        private bool reconnectEnable;
        private int reConnectInterval;
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
                #region 读取配置文件
                var opcConfig = ConfigurationManagerExtend.SectionsCast<OpcAddressConfiguration>("Address").FirstOrDefault().Value;
                var tagConfig = ConfigurationManagerExtend.SectionsCast<TagConfiguration>("Tag").FirstOrDefault().Value;
                groupTriggerUpdateRate = tagConfig.TriggerUpdateRate;
                groupDataUpdateRate = tagConfig.DataUpdateRate;
                reconnectEnable = opcConfig.ReconnectEnable;
                reConnectInterval = opcConfig.ReconnectInterval;
                #endregion
                #region OpcClient初始化
                if (opcConfig.OpcProtocolByEnum == OpcProtocol.DA)
                {
                    client = new DaOpc();
                }
                else if (opcConfig.OpcProtocolByEnum == OpcProtocol.UA)
                {
                    client = new UaOpc();
                }

                client.Init(opcConfig);
                //断开服务通知
                client.OpcStatusChangeHandle += this.OpcServerDisConnected;
                ConnectOpc(client);             
                #endregion
            }
            catch (Exception ex)
            {
                //记日志
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
            try
            {
                 if (client.Connect().Result)
                {
                    //创建组及绑定组内的tags
                    client.CreateGroup("GroupTrigger")
                        .SetUpdateRate(groupTriggerUpdateRate)
                        .AddItems(TagConfig.QueryTagsByGroupName<Tag>("GroupTrigger"))
                        .ValueChangedHandle = TagValueChanged;
                    client.CreateGroup("GroupData")
                        .SetUpdateRate(groupDataUpdateRate)
                        .AddItems(TagConfig.QueryTagsByGroupName<Tag>("GroupData"))
                        .AddQueue(100)
                        .ValueChangedHandle = TagValueChanged;
                    OpcServerRefreshUI(client);
                    return true;
                }
                else
                {
                    OpcServerRefreshUI(client);
                    return false;
                }
            }
            catch (Exception)
            {
                
                throw;
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
                client.RemoveGroupsAll();
                OpcServerRefreshUI(client);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 断线重连
        /// </summary>
        /// <param name="client"></param>
        private void ReconnectOpcAsync(IOpcClient client)
        {
            var msg = "10s后尝试重连";
            while (true)
            {

                if (!client.OpcStatus)
                {
                    //成功连接后直接返回
                    if (this.ConnectOpc(client))
                    {
                        return;
                    }
                    //连接失败则循环继续
                    else
                    {
                        AddMsgToList(msg);
                        //重连时间间隔
                        Thread.Sleep(reConnectInterval);
                    }
                }
                else
                {
                    return;
                }
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
                lblStatus.Text = client.OpcStatus ? "运行" : "停止";
                lblStatus.BackColor = Color.Green;
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
                this.lbxMsg.Items.Add(msg);
                this.lbxMsg.TopIndex = this.lbxMsg.Items.Count - (int)(this.lbxMsg.Height / this.lbxMsg.ItemHeight);
            }
        }
        #endregion
        #region 事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            ConnectOpc(client);
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
            double tran_id=0;
            string source_para_code = "";
            string source_app_code = "G1";
            int equip_type = 2;
            double para_value=0;


            switch (tag.TagName)
            {
                case "H1YAOGANG":
                    source_para_code = "G1_MILL_1_STATUS";
                    break;
                case "H7YAOGANG":
                    source_para_code = "G1_MILL_7_STATUS";
                    break;
                case "H13YAOGANG":
                    source_para_code = "G1_MILL_13_STATUS";
                    break;
                case "PL_RULU":
                    source_para_code = "G1_FURN_IN_STATUS";
                    break;
                case "JRL_CHUGANG":
                    source_para_code = "G1_FURN_OUT_STATUS";
                    break;
                default:
                    source_para_code = "UNKNOW";
                    return;
            }
            //格式转换
            if (tag.DataType == "Boolean")
            {
                para_value = Convert.ToInt32(Convert.ToBoolean(tag.Value));
            }
            if (para_value == 1)
            {
                var db = SugarDao.Instance;
                tran_id = db.GetSingle<double>("select Itf_Tran_ID_S.NEXTVAL  from  dual");
                db.Insert(new ITF_PARA_VALUE() { TRAN_ID = tran_id, EQUIP_TYPE = equip_type, SOURCE_APP_CODE = source_app_code, SOURCE_PARA_CODE = source_para_code, PARA_VALUE = para_value, TRAN_DATE = DateTime.Now, SEND_DATE = DateTime.Now, STATUS = 0 });
            }
            AddMsgToList(string.Format("DateTime ={0},TagName={1}, Value={2}, DataType={3}",DateTime.Now.ToString(), tag.TagName, tag.Value, tag.DataType));
        }
        /// <summary>
        /// 服务端断开通知
        /// </summary>
        /// <param name="msg"></param>
        private void OpcServerDisConnected(OpcStatusEventArgs e)
        {

        }
        #endregion

        private void btnMax_Click(object sender, EventArgs e)
        {
            var value = client["GroupData"]
                .GetTagValue("RandomXaxis")
                .TagHistory.Max();
            var tvalue = client["GroupData"]
                .GetTagHistory("RandomXaxis")                
                .ByDateLargerThan(DateTime.Now.AddSeconds(-10))
                .Max();
            var bvalue = client["GroupData"]
               .GetTagHistory("RandomXaxis")
               .ByDateLargerThan(DateTime.Now.AddSeconds(-10))
               .Average();
        }

        /// <summary>
        /// 添加双击托盘图标事件（双击显示窗口）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
