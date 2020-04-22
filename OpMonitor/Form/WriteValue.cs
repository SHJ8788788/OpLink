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
    public partial class WriteValue : Form
    {
        public string NewBlockName { get { return txtTagValue.Text; } }
        public Tag tag;
        /// <summary>
        /// 块操作委托
        /// </summary>
        private Func<Tag,bool> WriteValueHandle { get; set;}
        public WriteValue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="模拟值委托"></param>
        public WriteValue(Func<Tag, bool> func,Tag bi)
        {
            InitializeComponent();
            this.WriteValueHandle = func;
            this.tag = bi;
            this.txtTagName.Text = bi.TagName;
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //西门子string点写法,暂时不用
            //tag.TagName = txtTagName.Text;
            //byte[] bytValue = System.Text.Encoding.Default.GetBytes(txtTagValue.Text);

            //byte[] bytTemp = new byte[2];
            //bytTemp[0] = 254;
            //bytTemp[1] = 254;           

            //byte[] finalValue = new byte[bytValue.Length + bytTemp.Length];
            //bytTemp.CopyTo(finalValue, 0);
            //bytValue.CopyTo(finalValue, bytTemp.Length);

            //tag.Value =  System.Text.Encoding.Default.GetString(finalValue);          

            tag.Value = txtTagValue.Text;
            if (WriteValueHandle(tag))
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
