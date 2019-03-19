namespace OpMonitor
{
    partial class UAIniSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UAIniSetting));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageProtocol = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDA = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.btnGetServNames = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabUA = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUri = new System.Windows.Forms.TextBox();
            this.cmbUseSecurity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbReconnectInterval = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbOpcProtocol = new System.Windows.Forms.ComboBox();
            this.cmbReconnectEnable = new System.Windows.Forms.ComboBox();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.tabPageElse = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnCannel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTagsQueueNum = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl2.SuspendLayout();
            this.tabPageProtocol.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabDA.SuspendLayout();
            this.tabUA.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageElse.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageProtocol);
            this.tabControl2.Controls.Add(this.tabPageElse);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(398, 611);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPageProtocol
            // 
            this.tabPageProtocol.Controls.Add(this.tabControl1);
            this.tabPageProtocol.Controls.Add(this.groupBox2);
            this.tabPageProtocol.Location = new System.Drawing.Point(4, 22);
            this.tabPageProtocol.Name = "tabPageProtocol";
            this.tabPageProtocol.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProtocol.Size = new System.Drawing.Size(390, 585);
            this.tabPageProtocol.TabIndex = 0;
            this.tabPageProtocol.Text = "通讯协议";
            this.tabPageProtocol.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDA);
            this.tabControl1.Controls.Add(this.tabUA);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 233);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 349);
            this.tabControl1.TabIndex = 9;
            // 
            // tabDA
            // 
            this.tabDA.Controls.Add(this.label2);
            this.tabDA.Controls.Add(this.txtIP);
            this.tabDA.Controls.Add(this.cmbServerName);
            this.tabDA.Controls.Add(this.btnGetServNames);
            this.tabDA.Controls.Add(this.label1);
            this.tabDA.Location = new System.Drawing.Point(4, 22);
            this.tabDA.Name = "tabDA";
            this.tabDA.Padding = new System.Windows.Forms.Padding(3);
            this.tabDA.Size = new System.Drawing.Size(419, 323);
            this.tabDA.TabIndex = 0;
            this.tabDA.Text = "DA协议";
            this.tabDA.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "OPC服务名称：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(122, 35);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(179, 21);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
            // 
            // cmbServerName
            // 
            this.cmbServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(122, 78);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(179, 20);
            this.cmbServerName.TabIndex = 1;
            // 
            // btnGetServNames
            // 
            this.btnGetServNames.Location = new System.Drawing.Point(307, 34);
            this.btnGetServNames.Name = "btnGetServNames";
            this.btnGetServNames.Size = new System.Drawing.Size(41, 23);
            this.btnGetServNames.TabIndex = 5;
            this.btnGetServNames.Text = "Chk";
            this.btnGetServNames.UseVisualStyleBackColor = true;
            this.btnGetServNames.Click += new System.EventHandler(this.btnGetServNames_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // tabUA
            // 
            this.tabUA.Controls.Add(this.label3);
            this.tabUA.Controls.Add(this.txtUri);
            this.tabUA.Controls.Add(this.cmbUseSecurity);
            this.tabUA.Controls.Add(this.label8);
            this.tabUA.Controls.Add(this.txtUserName);
            this.tabUA.Controls.Add(this.label7);
            this.tabUA.Controls.Add(this.txtPassword);
            this.tabUA.Controls.Add(this.label6);
            this.tabUA.Location = new System.Drawing.Point(4, 22);
            this.tabUA.Name = "tabUA";
            this.tabUA.Padding = new System.Windows.Forms.Padding(3);
            this.tabUA.Size = new System.Drawing.Size(376, 323);
            this.tabUA.TabIndex = 1;
            this.tabUA.Text = "UA协议";
            this.tabUA.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "链接地址：";
            // 
            // txtUri
            // 
            this.txtUri.Location = new System.Drawing.Point(122, 35);
            this.txtUri.Name = "txtUri";
            this.txtUri.Size = new System.Drawing.Size(179, 21);
            this.txtUri.TabIndex = 2;
            // 
            // cmbUseSecurity
            // 
            this.cmbUseSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUseSecurity.FormattingEnabled = true;
            this.cmbUseSecurity.Location = new System.Drawing.Point(122, 77);
            this.cmbUseSecurity.Name = "cmbUseSecurity";
            this.cmbUseSecurity.Size = new System.Drawing.Size(179, 20);
            this.cmbUseSecurity.TabIndex = 1;
            this.cmbUseSecurity.SelectedIndexChanged += new System.EventHandler(this.cmbUseSecurity_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "密码：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(122, 117);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(179, 21);
            this.txtUserName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "用户名：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(122, 163);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(179, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "安全认证：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbReconnectInterval);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmbOpcProtocol);
            this.groupBox2.Controls.Add(this.cmbReconnectEnable);
            this.groupBox2.Controls.Add(this.txtTypeName);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 230);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPC设置";
            // 
            // cmbReconnectInterval
            // 
            this.cmbReconnectInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReconnectInterval.FormattingEnabled = true;
            this.cmbReconnectInterval.Items.AddRange(new object[] {
            "10",
            "20",
            "60"});
            this.cmbReconnectInterval.Location = new System.Drawing.Point(126, 127);
            this.cmbReconnectInterval.Name = "cmbReconnectInterval";
            this.cmbReconnectInterval.Size = new System.Drawing.Size(179, 20);
            this.cmbReconnectInterval.TabIndex = 127;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "重连间隔(S)：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "断线重连(S)：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "OPC类名：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "OPC协议：";
            // 
            // cmbOpcProtocol
            // 
            this.cmbOpcProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpcProtocol.FormattingEnabled = true;
            this.cmbOpcProtocol.Location = new System.Drawing.Point(126, 28);
            this.cmbOpcProtocol.Name = "cmbOpcProtocol";
            this.cmbOpcProtocol.Size = new System.Drawing.Size(179, 20);
            this.cmbOpcProtocol.TabIndex = 1;
            this.cmbOpcProtocol.SelectedIndexChanged += new System.EventHandler(this.cmbOpcProtocol_SelectedIndexChanged);
            // 
            // cmbReconnectEnable
            // 
            this.cmbReconnectEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReconnectEnable.FormattingEnabled = true;
            this.cmbReconnectEnable.Location = new System.Drawing.Point(126, 95);
            this.cmbReconnectEnable.Name = "cmbReconnectEnable";
            this.cmbReconnectEnable.Size = new System.Drawing.Size(179, 20);
            this.cmbReconnectEnable.TabIndex = 1;
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(126, 61);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.ReadOnly = true;
            this.txtTypeName.Size = new System.Drawing.Size(179, 21);
            this.txtTypeName.TabIndex = 2;
            // 
            // tabPageElse
            // 
            this.tabPageElse.Controls.Add(this.groupBox1);
            this.tabPageElse.Location = new System.Drawing.Point(4, 22);
            this.tabPageElse.Name = "tabPageElse";
            this.tabPageElse.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageElse.Size = new System.Drawing.Size(390, 585);
            this.tabPageElse.TabIndex = 1;
            this.tabPageElse.Text = "其它";
            this.tabPageElse.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEnter);
            this.panel1.Controls.Add(this.btnCannel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 537);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 74);
            this.panel1.TabIndex = 7;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(75, 21);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 3;
            this.btnEnter.Text = "Save";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnCannel
            // 
            this.btnCannel.Location = new System.Drawing.Point(217, 21);
            this.btnCannel.Name = "btnCannel";
            this.btnCannel.Size = new System.Drawing.Size(75, 23);
            this.btnCannel.TabIndex = 4;
            this.btnCannel.Text = "Cannel";
            this.btnCannel.UseVisualStyleBackColor = true;
            this.btnCannel.Click += new System.EventHandler(this.btnCannel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTagsQueueNum);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 230);
            this.groupBox1.TabIndex = 130;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tag设置";
            // 
            // cmbTagsQueueNum
            // 
            this.cmbTagsQueueNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTagsQueueNum.FormattingEnabled = true;
            this.cmbTagsQueueNum.Items.AddRange(new object[] {
            "100",
            "250",
            "500",
            "1000",
            "2000",
            "4000"});
            this.cmbTagsQueueNum.Location = new System.Drawing.Point(126, 28);
            this.cmbTagsQueueNum.Name = "cmbTagsQueueNum";
            this.cmbTagsQueueNum.Size = new System.Drawing.Size(179, 20);
            this.cmbTagsQueueNum.TabIndex = 131;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 130;
            this.label12.Text = "缓存数量：";
            // 
            // UAIniSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 611);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UAIniSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UAIniSetting";
            this.Load += new System.EventHandler(this.UAIniSetting_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPageProtocol.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabDA.ResumeLayout(false);
            this.tabDA.PerformLayout();
            this.tabUA.ResumeLayout(false);
            this.tabUA.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageElse.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageProtocol;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Button btnGetServNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabUA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUri;
        private System.Windows.Forms.ComboBox cmbUseSecurity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbOpcProtocol;
        private System.Windows.Forms.ComboBox cmbReconnectEnable;
        private System.Windows.Forms.TextBox txtTypeName;
        private System.Windows.Forms.TabPage tabPageElse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnCannel;
        private System.Windows.Forms.ComboBox cmbReconnectInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTagsQueueNum;
        private System.Windows.Forms.Label label12;
    }
}