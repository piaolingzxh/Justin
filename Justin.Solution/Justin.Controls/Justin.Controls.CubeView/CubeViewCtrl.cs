using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.FormUI;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Controls.CubeView
{
    public partial class CubeViewCtrl : JUserControl
    {
        public CubeViewCtrl()
        {
            InitializeComponent();
        }
        public static string DefaultConnStr { get; set; }
        public override string ConnStr
        {
            get
            {
                return txtConnectionString.Text;
            }
            set
            {
                txtConnectionString.Text = value;
            }
        }
        CubeOperate co;
        public AdomdConnection Connection
        {
            get
            {
                AdomdConnection connection = null;
                try
                {
                    connection = new AdomdConnection(txtConnectionString.Text);
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    this.ShowMessage("连接OLAP成功:" + txtConnectionString.Text);
                }
                catch (Exception ex)
                {
                    this.ShowMessage("连接OLAP失败", ex.ToString());
                }

                return connection;
            }
        }
        private void CubeViewCtrl_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text))
                this.txtConnectionString.Text = CubeViewCtrl.DefaultConnStr;
            tvServerinfo.AllowDrop = true;
            tvCubeInfo.AllowDrop = true;
            tvServerinfo.Nodes.Clear();
            tvServerinfo.Nodes.Add("Cubes_", "Cubes");
            tvServerinfo.Nodes.Add("Dimensions_", "Dimensions");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            co = new CubeOperate(this.ConnStr);
            InitTree();
        }
        private void InitTree()
        {

            BindServerCubes(tvServerinfo.Nodes["Cubes_"], co.Cubes);
            BindServerDimensions(tvServerinfo.Nodes["Dimensions_"], co.Dimensions);


        }

        private void BindServerCubes(TreeNode cubeNodeRoot, IEnumerable<CubeDef> cubes)
        {
            cubeNodeRoot.Nodes.Clear();
            if (cubes == null) return;
            foreach (var item in cubes)
            {
                string name = item.Name;//.Replace("$", "");
                string caption = item.Caption;//.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                cubeNodeRoot.Nodes.Add(tempNode);
            }
        }
        private void BindServerDimensions(TreeNode dimensionNodeRoot, IEnumerable<CubeDef> dimensions)
        {
            dimensionNodeRoot.Nodes.Clear();
            if (dimensions == null) return;
            foreach (var item in dimensions)
            {
                string name = item.Name.Replace("$", "");
                string caption = item.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                dimensionNodeRoot.Nodes.Add(tempNode);
            }
        }
        private void BindCubeInfo(string cubeName)
        {
            CubeDef cubeDef = co.GetCube(cubeName);
            tvCubeInfo.Nodes.Clear();
            BindMeasuresForCube(co.GetMeasures(cubeName));
            BindDimensionsForCube(cubeDef);

        }
        private void BindMeasuresForCube(IEnumerable<Measure> measures)
        {
            TreeNode measureRootNode = new TreeNode("Measures");
            measureRootNode.Name = "Measures_";
            tvCubeInfo.Nodes.Add(measureRootNode);

            if (measures == null) return;

            foreach (var item in measures)
            {
                string name = item.Name;//.Replace("$", "");
                string caption = item.Caption;//.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.Tag = item;
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                measureRootNode.Nodes.Add(tempNode);
            }
        }
        private void BindDimensionsForCube(CubeDef cubeDef)
        {
            IEnumerable<Dimension> dimensions = cubeDef.Dimensions.Cast<Dimension>();
            if (dimensions == null) return;
            for (int i = tvCubeInfo.Nodes.Count - 1; i >= 0; i--)
            {
                if (!tvCubeInfo.Nodes[i].Name.Equals("Measures_", StringComparison.CurrentCultureIgnoreCase))
                {
                    tvCubeInfo.Nodes.RemoveAt(i);
                }
            }

            foreach (var item in dimensions)
            {
                string name = item.Name.Replace("$", "");
                string caption = item.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                tvCubeInfo.Nodes.Add(tempNode);
            }
        }


        private void browerCubeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvServerinfo.SelectedNode.Parent != null && tvServerinfo.SelectedNode.Parent.Name.Equals("Cubes_"))
            {
                BindCubeInfo(tvServerinfo.SelectedNode.Name);
            }
        }

        private void tvServerinfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvServerinfo.SelectedNode = e.Node;
        }

        private void tvServerinfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvServerinfo.SelectedNode.Parent != null && tvServerinfo.SelectedNode.Parent.Name.Equals("Cubes_"))
            {
                BindCubeInfo(tvServerinfo.SelectedNode.Name);
            }
            //richTextBox1.AppendText(string.Format("Name:{0},Text:{1}{2}", tvServerinfo.SelectedNode.Name, tvServerinfo.SelectedNode.Text, Environment.NewLine));
            if (string.IsNullOrEmpty(tvServerinfo.SelectedNode.Name))
            {
                MessageBox.Show("");
            }
        }

        private void tvCubeInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //richTextBox1.AppendText(string.Format("Name:{0},Text:{1}{2}", tvCubeInfo.SelectedNode.Name, tvCubeInfo.SelectedNode.Text, Environment.NewLine));
            if (string.IsNullOrEmpty(tvCubeInfo.SelectedNode.Name))
            {
                MessageBox.Show("");
            }
        }

        private void tvCubeInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvCubeInfo.SelectedNode = e.Node;
        }

        private void tvServerinfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            File.AppendAllText(@"d:\drag.log", "tvServerinfo_ItemDrag" + Environment.NewLine);

            //if (e.Button == MouseButtons.Left)
            //{
            DoDragDrop(e.Item, DragDropEffects.All);
            //}
            File.AppendAllText(@"d:\drag.log", "tvServerinfo_ItemDrag" + Environment.NewLine);

        }

        private void tvServerinfo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvCubeInfo_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            //{
            //    e.Effect = DragDropEffects.Move;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private void tvCubeInfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            File.AppendAllText(@"d:\drag.log", "tvCubeInfo_ItemDrag" + Environment.NewLine);

            //if (e.Button == MouseButtons.Left)
            //{
            DoDragDrop(e.Item, DragDropEffects.All);
            //}
            File.AppendAllText(@"d:\drag.log", "tvCubeInfo_ItemDrag" + Environment.NewLine);

        }

    }
}
