using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OpLink
{
    public partial class ServicePath : Form
    {
        static string path = Application.StartupPath;
        public ServicePath()
        {
            InitializeComponent();
        }

        private void ServicePath_Load(object sender, EventArgs e)
        {            
            this.txtServicePath.Text = GetServicePathBinding(path);
        }

        /// <summary>
        /// 设置services的加载路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        private void SetServicePathBinding(string path, string value)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Path.Combine(path, "OpLink.exe.config"));
            }
            catch (FileNotFoundException)
            {
                return;
            }

            XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("bindings", "urn:schemas-microsoft-com:asm.v1");

            XmlNode root = doc.DocumentElement;

            XmlNode node = root.SelectSingleNode("//bindings:probing", manager);

            if (node == null)
            {
                throw (new Exception("Invalid Configuration File"));
            }

            node = node.SelectSingleNode("@privatePath");

            if (node == null)
            {
                throw (new Exception("Invalid Configuration File"));
            }

            node.Value = value;

            doc.Save(Path.Combine(path, "OpLink.exe.config"));
        }

        /// <summary>
        /// 获取services的加载路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        private string GetServicePathBinding(string path)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Path.Combine(path, "OpLink.exe.config"));
            }
            catch (FileNotFoundException)
            {
                return "";
            }

            XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("bindings", "urn:schemas-microsoft-com:asm.v1");

            XmlNode root = doc.DocumentElement;

            XmlNode node = root.SelectSingleNode("//bindings:probing", manager);

            if (node == null)
            {
                throw (new Exception("Invalid Configuration File"));
            }

            node = node.SelectSingleNode("@privatePath");

            if (node == null)
            {
                throw (new Exception("Invalid Configuration File"));
            }

            return node.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetServicePathBinding(path,this.txtServicePath.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
