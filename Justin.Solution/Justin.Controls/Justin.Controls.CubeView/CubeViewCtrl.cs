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
using Justin.FrameWork.Extensions;
using Justin.FrameWork.WinForm.Properties;
using ICSharpCode.TextEditor.Document;

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
            //this.btnCloseOpen.Image = Resources.opened;

            if (string.IsNullOrEmpty(txtConnectionString.Text))
                this.txtConnectionString.Text = CubeViewCtrl.DefaultConnStr;
            txtMdx.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtMdx.Encoding = Encoding.GetEncoding("GB2312");
            txtMdx.ActiveTextAreaControl.AllowDrop = true;
            txtMdx.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(txtMdx_DragDrop);
            this.txtMdx.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(txtMdx_DragEnter);
            //co = new CubeOperate(this.ConnStr);
            //if (tvServerInfo.Nodes.Count < 1)
            //    Bindcatalog();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            co = new CubeOperate(this.ConnStr);
            BindServerInfo();
        }

        #region 服务器连接信息

        private void BindServerInfo()
        {
            tvServerInfo.Nodes.Clear();
            var catalogs = co.Cubes.Select(r => r.Properties["CATALOG_NAME"].Value.ToString()).Distinct();
            foreach (var catalog in catalogs)
            {
                TreeNode catalogNode = new TreeNode(catalog);
                catalogNode.Name = "catalog";
                catalogNode.SelectedImageKey = catalogNode.ImageKey = "Catalog";
                var tempCubes = co.Cubes.Where(r => r.Properties["CATALOG_NAME"].Value.ToString().Equals(catalog));
                BindCatalog(catalogNode, catalog, tempCubes);
            }
        }

        private void BindCatalog(TreeNode catalogNode, string catalog, IEnumerable<CubeDef> cubes)
        {
            catalogNode.Nodes.Add("Cubes_", "Cubes", "Cubes", "Cubes");
            catalogNode.Nodes.Add("Dimensions_", "Dimensions", "Dims", "Dims");
            catalogNode.Expand();
            tvServerInfo.Nodes.Add(catalogNode);

            BindServerCubes(catalogNode.Nodes["Cubes_"], cubes);
            var dimensions = co.Dimensions.Where(r => r.Properties["CATALOG_NAME"].Value.ToString().Equals(catalog));
            BindServerDimensions(catalogNode.Nodes["Dimensions_"], dimensions);
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
                tempNode.SelectedImageKey = tempNode.ImageKey = "Cube";
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                cubeNodeRoot.Nodes.Add(tempNode);
            }
            cubeNodeRoot.Expand();
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
                tempNode.SelectedImageKey = tempNode.ImageKey = "Dim";
                tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                dimensionNodeRoot.Nodes.Add(tempNode);
            }
            dimensionNodeRoot.Expand();
        }
        #endregion

        #region 加载单个Cube信息

        private void BindCubeInfo(string cubeName)
        {
            CubeDef cubeDef = co.GetCube(cubeName);
            tvCubeInfo.Nodes.Clear();
            tvCubeInfo.Nodes.Add("Cube_", cubeDef.Caption, "Cube", "Cube");
            tvCubeInfo.Nodes[0].Nodes.Add("Measures_", "Measures", "Measure", "Measure");
            tvCubeInfo.Nodes[0].Expand();

            BindMeasuresForCube(co.GetMeasures(cubeName));
            BindDimensionsForCube(cubeDef);

        }
        private TreeNode CubeNode
        {
            get
            {
                return tvCubeInfo.Nodes[0];
            }
        }
        private TreeNode MeasuresRoot
        {
            get
            {
                return tvCubeInfo.Nodes[0].Nodes["Measures_"];
            }
        }
        private void BindMeasuresForCube(IEnumerable<Measure> measures)
        {
            MeasuresRoot.Nodes.Clear();

            if (measures == null) return;
            var groups = measures.Select(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString()).Distinct();
            foreach (var group in groups.OrderBy(r => r))
            {
                var tempMeasures = measures.Where(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString().Equals(group));
                MeasuresRoot.Nodes.Add(group, group, "Group", "Group");
                foreach (var item in tempMeasures.OrderBy(r => r.Caption))
                {
                    string name = item.Name;//.Replace("$", "");
                    string caption = item.Caption;//.Replace("$", "");
                    TreeNode tempNode = new TreeNode(caption);
                    tempNode.Name = name;
                    tempNode.Tag = item;
                    bool visible = item.Properties["MEASURE_IS_VISIBLE"].Value.Value<bool>(true);
                    string key = string.IsNullOrEmpty(item.Expression) ? "Measure" : "CalMeasure";
                    tempNode.SelectedImageKey = tempNode.ImageKey = visible ? key : "" + key;
                    tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                    MeasuresRoot.Nodes[group].Nodes.Add(tempNode);
                }
            }

        }

        private void BindDimensionsForCube(CubeDef cubeDef)
        {
            IEnumerable<Dimension> dimensions = cubeDef.Dimensions.Cast<Dimension>();
            if (dimensions == null) return;
            for (int i = CubeNode.Nodes.Count - 1; i >= 0; i--)
            {
                if (!CubeNode.Nodes[i].Name.Equals("Measures_", StringComparison.CurrentCultureIgnoreCase))
                {
                    CubeNode.Nodes.RemoveAt(i);
                }
            }

            foreach (var item in dimensions)
            {
                if (item.Name.Equals("Measures") || item.Caption.Equals("Measures")) continue;
                string name = item.Name.Replace("$", "");
                string caption = item.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                bool visible = item.Properties["DIMENSION_IS_VISIBLE"].Value.Value<bool>(true);
                tempNode.SelectedImageKey = tempNode.ImageKey = visible ? "Dim" : "Dim";
                tempNode.Tag = item;
                BindHierarchies(tempNode, item.Hierarchies);
                CubeNode.Nodes.Add(tempNode);
            }
        }
        private void BindHierarchies(TreeNode dimNode, HierarchyCollection hierarchies)
        {
            if (hierarchies == null || hierarchies.Count == 0) return;

            foreach (var hierarchy in hierarchies)
            {
                string name = hierarchy.Name.Replace("$", "");
                string caption = hierarchy.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.Tag = hierarchy;
                bool visible = hierarchy.Properties["HIERARCHY_IS_VISIBLE"].Value.Value<bool>(true);
                string key = hierarchy.Levels.Count > 2 ? "Hie" : "Level";
                tempNode.SelectedImageKey = tempNode.ImageKey = visible ? key : "" + key;
                BindLevels(tempNode, hierarchy.Levels);
                dimNode.Nodes.Add(tempNode);
            }
        }
        private void BindLevels(TreeNode root, LevelCollection levels)
        {
            if (levels == null || levels.Count == 0) return;

            foreach (var level in levels)
            {
                string name = level.Name.Replace("$", "");
                string caption = level.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                bool visible = level.Properties["LEVEL_IS_VISIBLE"].Value.Value<bool>(true);
                tempNode.SelectedImageKey = tempNode.ImageKey = visible ? "Level" : "Level";
                tempNode.Tag = level;
                root.Nodes.Add(tempNode);
            }
        }

        private void ExpendMembers(TreeNode root)
        {
            if (!root.ImageKey.Equals("Level") && !root.ImageKey.Equals("Member")) return;
            if (root.Nodes.Count > 0) return;
            MemberCollection members = null;
            if (root.ImageKey.Equals("Level"))
            {
                Level level = root.Tag as Level;
                members = level.GetMembers();


            }
            else if (root.ImageKey.Equals("Member"))
            {
                Member member = root.Tag as Member;
                members = member.GetChildren();
            }
            else
            {
                return;
            }
            if (members == null || members.Count <= 0) return;
            foreach (var member in members)
            {
                string name = member.Name.Replace("$", "");
                string caption = member.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.SelectedImageKey = tempNode.ImageKey = "Member";
                tempNode.Tag = member;

                root.Nodes.Add(tempNode);
            }

            root.Expand();
        }




        #endregion

        #region Treeview 操作

        private void tvServerInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvServerInfo.SelectedNode = e.Node;
        }
        private void tvServerInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvServerInfo.SelectedNode.Parent != null && tvServerInfo.SelectedNode.Parent.Name.Equals("Cubes_"))
            {
                BindCubeInfo(tvServerInfo.SelectedNode.Name);
            }
            if (string.IsNullOrEmpty(tvServerInfo.SelectedNode.Name))
            {
                MessageBox.Show("节点Name不能为空");
            }
        }
        private void tvServerInfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void browerCubeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvServerInfo.SelectedNode.Parent != null && tvServerInfo.SelectedNode.Parent.Name.Equals("Cubes_"))
            {
                BindCubeInfo(tvServerInfo.SelectedNode.Name);
            }
        }

        private void tvCubeInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvCubeInfo.SelectedNode = e.Node;
        }
        private void tvCubeInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (string.IsNullOrEmpty(tvCubeInfo.SelectedNode.Name))
            {
                MessageBox.Show("节点Name不能为空");
            }
            if (tvCubeInfo.SelectedNode.Tag == null) return;
            TreeNode tempNode = tvCubeInfo.SelectedNode;
            DataTable table = null;
            switch (tvCubeInfo.SelectedNode.ImageKey)
            {
                case "Cube": CubeDef cube = tempNode.Tag as CubeDef; table = cube.Properties.PrepareData(); break;
                case "Measure":
                case "CalMeasure": Measure measure = tempNode.Tag as Measure; table = measure.Properties.PrepareData(); break;
                case "Dim": Dimension dim = tempNode.Tag as Dimension; table = dim.Properties.PrepareData(); break;
                case "Hie": Hierarchy hie = tempNode.Tag as Hierarchy; table = hie.Properties.PrepareData(); break;
                case "Level": Level level = tempNode.Tag as Level; table = level.Properties.PrepareData(); break;
                case "Member": Member member = tempNode.Tag as Member; table = member.Properties.PrepareData(); break;

            }

            dgvObjectInfo.DataSource = table;

        }
        private void tvCubeInfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void tvCubeInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && (e.Node.ImageKey == "Level" || e.Node.ImageKey == "Member"))
            {
                ExpendMembers(e.Node);
            }
        }
        #endregion

        private void btnCloseOpen_Click(object sender, EventArgs e)
        {
            //if (this.btnCloseOpen.Image == Resources.closed)
            //{
            //    this.splitContainer1.Panel2.Show();
            //}
            //else
            //{
            //    this.splitContainer1.Panel2.Hide();
            //}
        }


        void txtMdx_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
            string text = string.IsNullOrEmpty(node.Name) ? node.Text : node.Name;
            txtMdx.ActiveTextAreaControl.TextArea.InsertString("[" + text + "]");
        }
        private void txtMdx_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


    }

    public static class CollectionHelper
    {
        public static DataTable PrepareData(this Microsoft.AnalysisServices.AdomdClient.PropertyCollection collection)
        {
            DataTable table = new DataTable();

            var list = collection.Cast<Property>();
            foreach (var item in list)
            {
                table.Columns.Add(item.Name.ToLower(), item.Type);
            }
            DataRow row = table.NewRow();
            foreach (var item in list)
            {
                row[item.Name.ToLower()] = item.Value == null ? DBNull.Value : item.Value;
            }
            table.Rows.Add(row);
            return table;
        }
    }
}
