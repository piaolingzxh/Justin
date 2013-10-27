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
using Justin.Controls.Executer;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Helper;
using System.Xml.Serialization;
using Justin.FrameWork;

namespace Justin.Controls.CubeView
{
    public partial class CubeViewCtrl : JUserControl
    {
        public CubeViewCtrl()
        {
            InitializeComponent();

        }

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
            {
                this.txtConnectionString.Text = CubeViewCtrlSetting.DefaultConnStr;
            }
            MdxExecuterCtrlSetting.DefaultConnStr = CubeViewCtrlSetting.DefaultConnStr;
            //splitContainerMain.Collapse();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                co = new CubeOperate(this.ConnStr);
                BindServerInfo();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
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
            if (tvCubeInfo.Nodes[cubeName] != null)
            {
                tvCubeInfo.Nodes.RemoveByKey(cubeName);
            }
            CubeDef cubeDef = co.GetCube(cubeName);
            tvCubeInfo.Nodes.Add(cubeDef.Name, cubeDef.Caption, "Cube", "Cube");
            tvCubeInfo.Nodes[cubeDef.Name].Tag = cubeDef;
            tvCubeInfo.Nodes[cubeDef.Name].Nodes.Add("Measures_", "Measures", "Measure", "Measure");
            tvCubeInfo.Nodes[cubeDef.Name].Expand();

            BindMeasuresForCube(cubeName);
            BindDimensionsForCube(cubeDef);

        }
        private TreeNode GetCubeNode(string cubeName)
        {
            return tvCubeInfo.Nodes[cubeName];
        }
        private TreeNode GetMeasuresRoot(string cubeName)
        {
            return tvCubeInfo.Nodes[cubeName].Nodes["Measures_"];
        }
        private void BindMeasuresForCube(string cubeName)
        {
            IEnumerable<Measure> measures = co.GetMeasures(cubeName);
            TreeNode rootOfMeasures = GetMeasuresRoot(cubeName);
            rootOfMeasures.Nodes.Clear();

            if (measures == null) return;
            bool hasGroup = measures.FirstOrDefault().Properties.Find("MEASUREGROUP_NAME") != null;
            if (hasGroup)
            {
                var groups = measures.Select(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString()).Distinct();
                foreach (var group in groups.OrderBy(r => r))
                {
                    var tempMeasures = measures.Where(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString().Equals(group));
                    rootOfMeasures.Nodes.Add(group, group, "Group", "Group");
                    foreach (var item in tempMeasures.OrderBy(r => r.Caption))
                    {
                        string name = item.Name;//.Replace("$", "");
                        string caption = item.Caption;//.Replace("$", "");
                        TreeNode tempNode = new TreeNode(caption);
                        tempNode.Name = name;
                        tempNode.Tag = item;
                        bool visible = item.Properties["MEASURE_IS_VISIBLE"].Value.Value<bool>(true);
                        Property expressionProperty = item.Properties.Find("EXPRESSION");
                        string expression = expressionProperty == null ? "" : expressionProperty.Value.ToJString();
                        string key = string.IsNullOrEmpty(expression) ? "Measure" : "CalMeasure";
                        tempNode.SelectedImageKey = tempNode.ImageKey = visible ? key : "" + key;
                        tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                        rootOfMeasures.Nodes[group].Nodes.Add(tempNode);
                    }
                }
            }
            else
            {
                foreach (var item in measures.OrderBy(r => r.Caption))
                {
                    string name = item.Name;//.Replace("$", "");
                    string caption = item.Caption;//.Replace("$", "");
                    TreeNode tempNode = new TreeNode(caption);
                    tempNode.Name = name;
                    tempNode.Tag = item;
                    bool visible = item.Properties["MEASURE_IS_VISIBLE"].Value.Value<bool>(true);
                    Property expressionProperty = item.Properties.Find("EXPRESSION");
                    string expression = expressionProperty == null ? "" : expressionProperty.Value.ToJString();
                    string key = string.IsNullOrEmpty(expression) ? "Measure" : "CalMeasure";
                    tempNode.SelectedImageKey = tempNode.ImageKey = visible ? key : "" + key;
                    tempNode.ToolTipText = string.Format("Name:[{0}]Caption:[{1}]", item.Name, item.Caption);
                    rootOfMeasures.Nodes.Add(tempNode);
                }
            }

        }

        private void BindDimensionsForCube(CubeDef cubeDef)
        {
            IEnumerable<Dimension> dimensions = cubeDef.Dimensions.Cast<Dimension>();
            if (dimensions == null) return;
            TreeNode cubeNode = GetCubeNode(cubeDef.Name);
            for (int i = cubeNode.Nodes.Count - 1; i >= 0; i--)
            {
                if (!cubeNode.Nodes[i].Name.Equals("Measures_", StringComparison.CurrentCultureIgnoreCase))
                {
                    cubeNode.Nodes.RemoveAt(i);
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
                cubeNode.Nodes.Add(tempNode);
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
                string key = hierarchy.Levels.Count > 2 ? "Hie" : "SingleHie";
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
            try
            {
                var selectNode = tvServerInfo.SelectedNode;

                if (selectNode.Parent != null && selectNode.Parent.Name.Equals("Cubes_") && selectNode.ImageKey.Equals("Cube"))
                {
                    BindCubeInfo(selectNode.Name);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        private void tvCubeInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvCubeInfo.SelectedNode = e.Node;
        }

        public TreeNode GetCubeNode(TreeNode node)
        {
            TreeNode parentNode = node.Parent;

            while (parentNode != null)
            {
                if (parentNode.Parent == null)
                {
                    return parentNode;
                }
                else
                {
                    parentNode = parentNode.Parent;
                }
            }
            return node;
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
                case "SingeHie":
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
            try
            {
                if (e.Node != null && (e.Node.ImageKey == "Level" || e.Node.ImageKey == "Member"))
                {
                    ExpendMembers(e.Node);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }
        #endregion

        private string GenerateSampleMdx()
        {

            if (tvCubeInfo.SelectedNode == null) return "";

            TreeNode cubeNode = GetCubeNode(tvCubeInfo.SelectedNode);
            string measureExpression = (cubeNode.Nodes[0].Nodes[0].Tag as Measure).UniqueName;

            string dimensionExpression = (cubeNode.Nodes[1].Nodes[0].Tag as Hierarchy).UniqueName; ;

            return string.Format(@"
SELECT 
NON EMPTY
{{
    {0}
}}
 ON COLUMNS,
NON EMPTY
{{
   {1}.Members
}}
ON ROWS
FROM [{2}]
", measureExpression, dimensionExpression, cubeNode.Name);
        }

        private void generateSampleMdxTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode cubeNode = GetCubeNode(tvCubeInfo.SelectedNode);
            AddMdxEditorTabPage(cubeNode.Text, this.ConnStr, GenerateSampleMdx());
        }
        private void AddMdxEditorTabPage(string cubeText, string connStr, string mdx)
        {
            TabPage page = new TabPage(cubeText);
            MdxExecuterCtrl mdxEditor = new MdxExecuterCtrl();
            mdxEditor.ConnStr = connStr;
            mdxEditor.Mdx = mdx;
            mdxEditor.Dock = DockStyle.Fill;
            page.Controls.Add(mdxEditor);
            tabControlMdxEditorCollection.TabPages.Add(page);
        }
        private void saveMdxInCurrentTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MdxCodeSnip> list = new List<MdxCodeSnip>();
            MdxExecuterCtrl editor = FindCtrl(tabControlMdxEditorCollection.SelectedTab);
            list.Add(new MdxCodeSnip()
            {
                Name = tabControlMdxEditorCollection.SelectedTab.Text,
                ConnStr = editor.ConnStr,
                Mdx = editor.Mdx
            });
            SaveToFile(list);

        }
        private MdxExecuterCtrl FindCtrl(TabPage page)
        {
            var ctrl = page.Controls.Cast<Control>().Where(r => r.GetType().Equals(typeof(MdxExecuterCtrl))).FirstOrDefault();
            return ctrl as MdxExecuterCtrl;
        }

        private void saveMdxInAllTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MdxCodeSnip> list = new List<MdxCodeSnip>();

            foreach (TabPage currentTabPage in tabControlMdxEditorCollection.TabPages)
            {
                MdxExecuterCtrl editor = FindCtrl(currentTabPage);
                list.Add(new MdxCodeSnip()
                {
                    Name = currentTabPage.Text,
                    ConnStr = editor.ConnStr,
                    Mdx = editor.Mdx
                });
            }
            SaveToFile(list);
        }

        private void SaveToFile(List<MdxCodeSnip> list)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            var form = this.FindForm();
            saveFileDialog1.InitialDirectory = Constants.ConfigFileFolder;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "xml Files (.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                SerializeHelper.XmlSerializeToFile<List<MdxCodeSnip>>(list, fileName, true);
            }

        }

        private void tabControlMdxEditorCollection_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            for (int i = 0; i < tabControlMdxEditorCollection.TabCount; i++)
            {
                Rectangle recTab = tabControlMdxEditorCollection.GetTabRect(i);
                if (recTab.Contains(pt))
                {
                    tabControlMdxEditorCollection.SelectedIndex = i;
                    break;
                }
            }
        }

        private void loadMdxFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string folder = Constants.ConfigFileFolder;
            fileDialog.InitialDirectory = folder;
            fileDialog.RestoreDirectory = true;

            fileDialog.Filter = "XML Files (.txt)|*.xml|All Files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                List<MdxCodeSnip> list = SerializeHelper.XmlDeserializeFromFile<List<MdxCodeSnip>>(fileDialog.FileName);
                if (list == null) return;
                foreach (var item in list)
                {
                    AddMdxEditorTabPage(item.Name, item.ConnStr, item.Mdx);
                }
            }
        }

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMdxEditorCollection.TabPages.Clear();
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMdxEditorCollection.TabPages.Remove(tabControlMdxEditorCollection.SelectedTab);
        }

    }
    public class MdxCodeSnip
    {
        public string Name { get; set; }
        public string ConnStr { get; set; }
        public CDATA Mdx { get; set; }

    }
    public static class CollectionHelper
    {
        public static string Display(this Microsoft.AnalysisServices.AdomdClient.PropertyCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in collection.Cast<Property>())
            {
                sb.AppendFormat("{0}    {1} {2}", item.Name, item.Type.ToString(), item.Value == null ? " " : item.Value).AppendLine();
            }
            return sb.ToString();
        }
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

        public static List<TreeNode> GetCheckNodes(this TreeView tree)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            foreach (TreeNode item in tree.Nodes)
            {
                item.PrepareCheckNodesInner(checkedNodes);
            }
            return checkedNodes;
        }
        public static void PrepareCheckNodesInner(this TreeNode node, List<TreeNode> checkedNodes)
        {
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Checked)
                {
                    checkedNodes.Add(item);
                }
                item.PrepareCheckNodesInner(checkedNodes);
            }
        }
    }
    public class CubeViewCtrlSetting
    {
        public static string DefaultConnStr { get; set; }
    }
}
