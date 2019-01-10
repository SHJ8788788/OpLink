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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnCannel = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnGetServNames = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDA = new System.Windows.Forms.TabPage();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbOpcProtocol = new System.Windows.Forms.ComboBox();
            this.cmbReconnectEnable = new System.Windows.Forms.ComboBox();
            this.txtReconnectInterval = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabDA.SuspendLayout();
            this.tabUA.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "OPC服务名称：";
            // 
            // cmbServerName
            // 
            this.cmbServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(103, 77);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(179, 20);
            this.cmbServerName.TabIndex = 1;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(103, 34);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(179, 21);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
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
            // btnGetServNames
            // 
            this.btnGetServNames.Location = new System.Drawing.Point(288, 33);
            this.btnGetServNames.Name = "btnGetServNames";
            this.btnGetServNames.Size = new System.Drawing.Size(41, 23);
            this.btnGetServNames.TabIndex = 5;
            this.btnGetServNames.Text = "Chk";
            this.btnGetServNames.UseVisualStyleBackColor = true;
            this.btnGetServNames.Click += new System.EventHandler(this.btnGetServNames_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 411);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDA);
            this.tabControl1.Controls.Add(this.tabUA);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 162);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 246);
            this.tabControl1.TabIndex = 7;
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
            this.tabDA.Size = new System.Drawing.Size(380, 220);
            this.tabDA.TabIndex = 0;
            this.tabDA.Text = "DA协议";
            this.tabDA.UseVisualStyleBackColor = true;
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
            this.tabUA.Size = new System.Drawing.Size(380, 220);
            this.tabUA.TabIndex = 1;
            this.tabUA.Text = "UA协议";
            this.tabUA.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "地址：";
            // 
            // txtUri
            // 
            this.txtUri.Location = new System.Drawing.Point(106, 34);
            this.txtUri.Name = "txtUri";
            this.txtUri.Size = new System.Drawing.Size(179, 21);
            this.txtUri.TabIndex = 2;
            // 
            // cmbUseSecurity
            // 
            this.cmbUseSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUseSecurity.FormattingEnabled = true;
            this.cmbUseSecurity.Location = new System.Drawing.Point(106, 76);
            this.cmbUseSecurity.Name = "cmbUseSecurity";
            this.cmbUseSecurity.Size = new System.Drawing.Size(179, 20);
            this.cmbUseSecurity.TabIndex = 1;
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
            this.txtUserName.Location = new System.Drawing.Point(106, 116);
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
            this.txtPassword.Location = new System.Drawing.Point(106, 162);
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
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmbOpcProtocol);
            this.groupBox2.Controls.Add(this.cmbReconnectEnable);
            this.groupBox2.Controls.Add(this.txtTypeName);
            this.groupBox2.Controls.Add(this.txtReconnectInterval);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 145);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "OPC重连间隔";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "OPC断线重连";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "OPC协议";
            // 
            // cmbOpcProtocol
            // 
            this.cmbOpcProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpcProtocol.FormattingEnabled = true;
            this.cmbOpcProtocol.Location = new System.Drawing.Point(98, 19);
            this.cmbOpcProtocol.Name = "cmbOpcProtocol";
            this.cmbOpcProtocol.Size = new System.Drawing.Size(179, 20);
            this.cmbOpcProtocol.TabIndex = 1;
            // 
            // cmbReconnectEnable
            // 
            this.cmbReconnectEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReconnectEnable.FormattingEnabled = true;
            this.cmbReconnectEnable.Location = new System.Drawing.Point(98, 83);
            this.cmbReconnectEnable.Name = "cmbReconnectEnable";
            this.cmbReconnectEnable.Size = new System.Drawing.Size(179, 20);
            this.cmbReconnectEnable.TabIndex = 1;
            // 
            // txtReconnectInterval
            // 
            this.txtReconnectInterval.Location = new System.Drawing.Point(98, 118);
            this.txtReconnectInterval.Name = "txtReconnectInterval";
            this.txtReconnectInterval.Size = new System.Drawing.Size(179, 21);
            this.txtReconnectInterval.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEnter);
            this.panel1.Controls.Add(this.btnCannel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 74);
            this.panel1.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "OPC类名";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(98, 51);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.ReadOnly = true;
            this.txtTypeName.Size = new System.Drawing.Size(179, 21);
            this.txtTypeName.TabIndex = 2;
            // 
            // UAIniSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 485);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "UAIniSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UAIniSetting";
            this.Load += new System.EventHandler(this.UAIniSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabDA.ResumeLayout(false);
            this.tabDA.PerformLayout();
            this.tabUA.ResumeLayout(false);
            this.tabUA.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnCannel;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnGetServNames;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ComboBox cmbUseSecurity;
        private System.Windows.Forms.ComboBox cmbOpcProtocol;
        private System.Windows.Forms.TextBox txtUri;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbReconnectEnable;
        private System.Windows.Forms.TextBox txtReconnectInterval;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDA;
        private System.Windows.Forms.TabPage tabUA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTypeName;
    }
}