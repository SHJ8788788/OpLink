using DaOpcClient;
using Opc.Ua;
using OpcClient.Config;
using OpcClient.Opc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UaOpcClient;

namespace TestForUa
{
    public partial class Form1 : Form
    {
        private DaOpc opc;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //opc = new UaOpc();
            //var config = new OpcAddressConfiguration();
            //config.Ip = "127.0.0.1";
            //config.Port = "49328";
            //config.OpcProtocol = (int)OpcProtocol.UA;
            //opc.Init(config);
            //var result = await opc.Connect();
            //opc.CreateGroup("GroupTrigger");
            //opc.CreateGroup("GroupData");

            opc = new DaOpc();
            var config = new OpcAddressConfiguration();
            config.Ip = "127.0.0.1";
            config.Port = "49328";
            config.ServerName = "KEPware.KEPServerEx.V4";
            config.OpcProtocol = (int)OpcProtocol.DA;
            opc.Init(config);
            for (int i = 0; i < 100; i++)
            {
                var result = await opc.Connect();
                var result1 = await opc.Connect();
                var dd = opc.Disconnect();
            }
            return;
            
            opc.CreateGroup("GroupTrigger");
            opc.CreateGroup("GroupData");

            //opc.ShowBranchesByTree(treeViewBranches.Nodes);
            treeViewBranches.Nodes.Add(new TreeNode("Browsering...", 7, 7));

            List<TreeNode> listNode = await Task.Run(() =>
            {
                List<TreeNode> list = new List<TreeNode>();
                List<OpcNode> opcNodes = opc.ShowBranches();
                foreach (var nodeSon in opcNodes)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Name = nodeSon.NodeName;
                    treeNode.Text = nodeSon.NodeName;
                    treeNode.Tag = nodeSon;
                    treeNode.ImageKey = nodeSon.Key;
                    treeNode.SelectedImageKey = nodeSon.Key;
                    if (nodeSon.IsExpand)
                    {
                        treeNode.Nodes.Add(new TreeNode());
                    }
                    list.Add(treeNode);
                }
                return list;
            });
            treeViewBranches.Nodes.Clear();
            treeViewBranches.Nodes.AddRange(listNode.ToArray());


            //try
            //{
            //    await m_OpcUaClient.ConnectServer("opc.tcp://127.0.0.1:49328");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //DataValue dataValue = opc.ReadNode(new NodeId(textBox1.Text));
            //textBox2.Text = dataValue.WrappedValue.Value.ToString();
        }
        
        private async void treeViewBranches_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count>1)
            {
                return;
            }
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add(new TreeNode("Browsering...", 7, 7));
            List<TreeNode> listNode = await Task.Run(() =>
            {
                List<TreeNode> list = new List<TreeNode>();
                List<OpcNode> opcNodes = opc.ShowBranches(((OpcNode)e.Node.Tag).NodeId);
                foreach (var nodeSon in opcNodes)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Name = nodeSon.NodeName;
                    treeNode.Text = nodeSon.NodeName;
                    treeNode.Tag = nodeSon;
                    treeNode.ImageKey = nodeSon.Key;
                    treeNode.SelectedImageKey = nodeSon.Key;
                    if (nodeSon.IsExpand)
                    {
                        treeNode.Nodes.Add(new TreeNode());
                    }
                    list.Add(treeNode);
                }
                return list;
            });
            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(listNode.ToArray());
        }

        private void treeViewBranches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag ==null)
            {
                return;
            }
            List<OpcNode> list = opc.ShowLeafs(((OpcNode)e.Node.Tag).NodeId);
            //list.Select(p => { checkedListBox1.Items.Add(p); return p; });
            checkedListBox1.Items.Clear();
            foreach (var item in list)
            {
                checkedListBox1.Items.Add(item.NodeId);
            }
            
        }
    }
}
