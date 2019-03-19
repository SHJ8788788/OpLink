﻿using OpcClient;
using OpcClient.Opc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OpMonitor
{
    public partial class AddTags : Form
    {
        private IOpcClient client;
        private List<string> branches;

        private string groupName;
        private string blockName;
        private ImageList imageList = new ImageList();

        public AddTags()
        {
            InitializeComponent();
        }

        public AddTags(IOpcClient iOpc, string groupName, string blockName)
        {
            InitializeComponent();
            this.client = iOpc;
            this.groupName = groupName;
            this.blockName = blockName;
            lblGroup.Text = this.groupName;
            lblBlock.Text = this.blockName;
            imageList.Images.Add("Class_489", Properties.Resources.Class_489);
            imageList.Images.Add("ClassIcon", Properties.Resources.ClassIcon);
            imageList.Images.Add("brackets", Properties.Resources.brackets_Square_16xMD);
            imageList.Images.Add("VirtualMachine", Properties.Resources.VirtualMachine);
            imageList.Images.Add("Enum_582", Properties.Resources.Enum_582);
            imageList.Images.Add("Method_636", Properties.Resources.Method_636);
            imageList.Images.Add("Module_648", Properties.Resources.Module_648);
            imageList.Images.Add("Loading", Properties.Resources.loading);
            treeViewBranches.ImageList = imageList;

            treeViewBranches.Nodes.Add(new TreeNode("Browsering...", 7, 7));
            List<TreeNode> listNode = Task.Run(() =>
            {
                List<TreeNode> list = new List<TreeNode>();
                List<OpcNode> opcNodes = client.ShowBranches();
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
            }).Result;
            treeViewBranches.Nodes.Clear();
            treeViewBranches.Nodes.AddRange(listNode.ToArray());          
        }     

        private void AddTags_Load(object sender, EventArgs e)
        {
            //client = new DaOpc();
            //client.Connect("KEPware.KEPServerEx.V4");

            //client.ShowBranchesTree(treeViewBranches.Nodes);

            //branches=client.ShowBranches();
            //listGroups.Items.Clear();
            //foreach (string item in branches)
            //{
            //    listGroups.Items.Add(item);
            //}
        }

        #region 事件
        //显示items
        private void treeViewBranches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }
            dataGridViewItems.Rows.Clear();
            int activityIndex = this.dataGridViewItems.Rows.Add();
            dataGridViewItems.Rows[0].Cells[1].Value = "Loading...";
            dataGridViewItems.Enabled = false;

            List<OpcNode> leafs = client.ShowLeafs(((OpcNode)e.Node.Tag).NodeId).Where(p => p.Attribute.NodeClass == "Variable").ToList();
            dataGridViewItems.Rows.Clear();
            foreach (OpcNode node in leafs)
            {
                int index = this.dataGridViewItems.Rows.Add();
                dataGridViewItems.Rows[index].Cells[1].Value = node.NodeName;
                dataGridViewItems.Rows[index].Cells[2].Value = node.NodeId;
                dataGridViewItems.Rows[index].Cells[3].Value = node.Attribute.Value;
                dataGridViewItems.Rows[index].Cells[4].Value = node.Attribute.Type;
                dataGridViewItems.Rows[index].Cells[5].Value = node.Attribute.AccessLevel;
                dataGridViewItems.Rows[index].Cells[6].Value = node.Attribute.Description;
            }
            dataGridViewItems.Enabled = true;
            tsslItemsNum.Text = "Item数量:" + leafs.Count.ToString() + "    ";
        }
        //新增tag
        private void btnAddTags_Click(object sender, EventArgs e)
        {
            List<Tag> tags = new List<Tag>();
            foreach (DataGridViewRow row in dataGridViewItems.Rows)
            {
                if (row.Cells[0].EditedFormattedValue.ToString()=="True")
                {
                    tags.Add(new Tag() { OpcTagName = row.Cells[2].Value.ToString(), TagName = row.Cells[1].Value.ToString() });
                }
            }
            //foreach (string item in listItems.CheckedItems)
            //{
            //    tags.Add(new Tag() { OpcTagName = item, TagName = item.Remove(0, item.LastIndexOf(".") + 1) });
            //}
            //Tag t = new Tag();

            //判断是否在在重复项
            for (int i = 0; i < dataGridTags.Rows.Count; i++)
            {
                Tag dfd;
                string TagName = dataGridTags.Rows[i].Cells["TagName"].Value.ToString();
                int count = tags.Where(p => p.TagName == TagName).Count();
                if (count != 0)
                {
                    MessageBox.Show("在在重复项：item = " + OpcTagName, "提示", MessageBoxButtons.OK);
                    return;
                }
            }

            //将勾选的tags添加进griview
            for (int i = 0; i < tags.Count; i++)
            {
                int index = dataGridTags.Rows.Add();
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["TagName"].Value = (tags[i]).TagName;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["DataType"].Value = (tags[i]).DataTypeName;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Value"].Value = (tags[i]).Value;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Group"].Value = lblGroup.Text;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Block"].Value = lblBlock.Text;

                //dataGridView1.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                //dataGridView1.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
            }
            tsslTagsNum.Text = "Tag数量:" + dataGridTags.Rows.Count.ToString();
        }
        //全选/反选
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewItems.Rows)
            {
                row.Cells[0].Value = chkAll.Checked;
            }          
        }
        //列表中删除
        private void toolStripTagDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridTags.SelectedRows)
            {             
                dataGridTags.Rows.RemoveAt(dataGridTags.CurrentRow.Index);
            }
            tsslTagsNum.Text = "Tag数量:" + dataGridTags.Rows.Count.ToString();
        }
        //校验重复性
        private void btnCheckRepeat_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                //Xml文件中在在此名称，修改dataGridTags的显示背景色
                if (TagConfig.ExistTag(row.Cells["TagName"].Value.ToString()))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

            }
        }
        //保存提交的数据
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridTags.Rows.Count == 0)
            {
                MessageBox.Show("不存在需要提交的数据集！");
                return;
            }
            List<Tag> tags = new List<Tag>();
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                Tag tag = new Tag() { TagName = row.Cells["TagName"].Value.ToString(), OpcTagName = row.Cells["OpcTagName"].Value.ToString() };
                tags.Add(tag);
            }
            if (TagConfig.CreateTag(groupName, blockName, tags))
            {
                this.DialogResult = DialogResult.OK;
                //this.Close();
                this.Dispose();
            }
        }
        //gridview弹出ContextMenuStrip
        private void dataGridTags_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridTags.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridTags.ClearSelection();
                        dataGridTags.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridTags.SelectedRows.Count == 1)
                    {
                        dataGridTags.CurrentCell = dataGridTags.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuTags.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }
        #endregion
        private void treeViewBranches_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 1)
            {
                return;
            }
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add(new TreeNode("Browsering...", 7, 7));
        }
        private void treeViewBranches_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 1)
            {
                return;
            }
            List<TreeNode> list = new List<TreeNode>();
            List<OpcNode> opcNodes = client.ShowBranches(((OpcNode)e.Node.Tag).NodeId);
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
            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(list.ToArray());
        }
    }
}
