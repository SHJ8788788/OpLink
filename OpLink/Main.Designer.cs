namespace OpLink
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbxMsg = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslversion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslServerStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnServiceSetting = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.NotifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 22);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(110, 22);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.Location = new System.Drawing.Point(556, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 16);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "停止";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 281);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbxMsg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbxMsg
            // 
            this.lbxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxMsg.FormattingEnabled = true;
            this.lbxMsg.ItemHeight = 12;
            this.lbxMsg.Location = new System.Drawing.Point(3, 3);
            this.lbxMsg.Name = "lbxMsg";
            this.lbxMsg.Size = new System.Drawing.Size(624, 249);
            this.lbxMsg.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 67);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.lblStatus);
            this.splitContainer1.Size = new System.Drawing.Size(638, 346);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerState,
            this.tsslversion,
            this.tsslServerStartTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 39);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 22);
            this.statusStrip1.TabIndex = 131;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslServerState
            // 
            this.tsslServerState.Name = "tsslServerState";
            this.tsslServerState.Size = new System.Drawing.Size(112, 17);
            this.tsslServerState.Text = "                          ";
            // 
            // tsslversion
            // 
            this.tsslversion.Name = "tsslversion";
            this.tsslversion.Size = new System.Drawing.Size(112, 17);
            this.tsslversion.Text = "                          ";
            // 
            // tsslServerStartTime
            // 
            this.tsslServerStartTime.Name = "tsslServerStartTime";
            this.tsslServerStartTime.Size = new System.Drawing.Size(112, 17);
            this.tsslServerStartTime.Text = "                          ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Controls.Add(this.btnServiceSetting);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 67);
            this.panel1.TabIndex = 4;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(389, 22);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(77, 23);
            this.btnSetting.TabIndex = 3;
            this.btnSetting.Text = "Opc设置";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnServiceSetting
            // 
            this.btnServiceSetting.Location = new System.Drawing.Point(483, 22);
            this.btnServiceSetting.Name = "btnServiceSetting";
            this.btnServiceSetting.Size = new System.Drawing.Size(128, 23);
            this.btnServiceSetting.TabIndex = 2;
            this.btnServiceSetting.Text = "Service路径设置";
            this.btnServiceSetting.UseVisualStyleBackColor = true;
            this.btnServiceSetting.Visible = false;
            this.btnServiceSetting.Click += new System.EventHandler(this.btnServiceSetting_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.NotifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "OpLinkCollect";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // NotifyMenu
            // 
            this.NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemShow,
            this.ToolStripMenuItemExit});
            this.NotifyMenu.Name = "NotifyMenu";
            this.NotifyMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // ToolStripMenuItemShow
            // 
            this.ToolStripMenuItemShow.Name = "ToolStripMenuItemShow";
            this.ToolStripMenuItemShow.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemShow.Text = "显示";
            this.ToolStripMenuItemShow.Click += new System.EventHandler(this.ToolStripMenuItemShow_Click);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemExit.Text = "退出";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 413);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "OpLink";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.NotifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxMsg;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerState;
        private System.Windows.Forms.ToolStripStatusLabel tsslversion;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerStartTime;
        private System.Windows.Forms.Button btnServiceSetting;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip NotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemShow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
    }
}

