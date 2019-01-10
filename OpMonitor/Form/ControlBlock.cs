using OpcClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace OpMonitor
{  
    public partial class ControlBlock : Form
    {
        public string NewBlockName { get { return txtBlockName.Text; } }
        /// <summary>
        /// 块操作委托
        /// </summary>
        private Func<string,bool> blockControlHandle { get; set;}
        public ControlBlock()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="块操作委托"></param>
        public ControlBlock(Func<string,bool> func,string blockName="")
        {
            InitializeComponent();
            this.blockControlHandle = func;
            this.txtBlockName.Text = blockName;
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (blockControlHandle(txtBlockName.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                return;
            }
        }
    }
}
