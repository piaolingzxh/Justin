using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using ICSharpCode.TextEditor.Document;
using Justin.Controls.Executer;
using Justin.FrameWork;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Properties;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Controls.CubeView
{
    public partial class CubeViewCtrl : JUserControl
    {
        public override string ConnStr
        {
            get
            {
                return cboxConnStrings.Text;
            }
            set
            {
                cboxConnStrings.Text = value;
            }
        }
        Dictionary<string, CubeOperate> cos = new Dictionary<string, CubeOperate>();

        public CubeViewCtrl()
        {
            InitializeComponent();
        }

        private void CubeViewCtrl_Load(object sender, EventArgs e)
        {
            this.cboxConnStrings.Items.Clear();
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("OLAPConnStr", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.cboxConnStrings.Items.Add(ConfigurationManager.AppSettings[item]);
                }
            }

            if (string.IsNullOrEmpty(cboxConnStrings.Text))
            {
                this.cboxConnStrings.Text = JSetting.Get("OLAPConnStr");
            }
            JSetting.SetUseAppSetting("OLAPConnStr", "OLAPConnStr");
            cboxFilterType.Items.Clear();
            cboxFilterType.Items.AddRange(Enum.GetNames(typeof(FilterType)));
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                cos.Clear();
                if (this.ConnStr.ToLower().Contains("catalog"))
                {
                    CubeOperate co = new CubeOperate(this.ConnStr);
                    cos.Add(co.Conn.Database, co);
                }
                else
                {
                    AdomdConnection conn = new AdomdConnection(this.ConnStr);
                    conn.Open();
                    DataSet dsCatalogs = conn.GetSchemaDataSet("DBSCHEMA_CATALOGS", null);
                    conn.Close();
                    foreach (DataRow catalogRow in dsCatalogs.Tables[0].Rows.Cast<DataRow>())
                    {
                        string catalog = catalogRow[0].ToString();
                        string connStr = string.Format("{0};Catalog ={1};", this.ConnStr, catalog);
                        cos.Add(catalog, new CubeOperate(connStr));
                    }
                }
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

            foreach (var catalogInfo in cos)
            {
                //var catalogs = cos[catalogInfo.Key].Cubes;.Select(r => r.Properties["CATALOG_NAME"].Value.ToString()).Distinct();
                TreeNode catalogNode = new TreeNode(catalogInfo.Key);
                catalogNode.Name = catalogInfo.Key;
                catalogNode.SelectedImageKey = catalogNode.ImageKey = "Catalog";
                tvServerInfo.Nodes.Add(catalogNode);
                BindCatalog(catalogNode, catalogInfo);
            }
        }

        private void BindCatalog(TreeNode catalogNode, KeyValuePair<string, CubeOperate> catalogInfo)
        {
            catalogNode.Nodes.Add("Cubes_", "Cubes", "Cubes", "Cubes");
            catalogNode.Nodes.Add("Dimensions_", "Dimensions", "Dims", "Dims");
            catalogNode.Expand();
            var tempCubes = cos[catalogInfo.Key].Cubes.Where(r => r.Properties["CATALOG_NAME"].Value.ToString().Equals(catalogInfo.Key));
            BindServerCubes(catalogNode.Nodes["Cubes_"], tempCubes);
            var dimensions = cos[catalogNode.Name].Dimensions.Where(r => r.Properties["CATALOG_NAME"].Value.ToString().Equals(catalogInfo.Key));
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
                tempNode.ContextMenuStrip = serverMenu;
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
        }
        #endregion

        #region 加载单个Cube信息


        private void BindCubeInfo(string catalogName, string cubeName)
        {
            TreeNode catalogNode = null;

            #region 没有就新建一个CatalogNode

            if (tvCubeInfo.Nodes[catalogName] == null)
            {
                tvCubeInfo.Nodes.Add(catalogName, catalogName, "Catalog", "Catalog");
            }
            catalogNode = tvCubeInfo.Nodes[catalogName];
            catalogNode.Tag = cos[catalogName];

            #endregion

            //重新绑定该Cube
            #region 重新生成一个Cube节点

            if (catalogNode.Nodes[cubeName] != null) catalogNode.Nodes.RemoveByKey(cubeName);

            CubeDef cubeDef = cos[catalogName].GetCube(cubeName);
            catalogNode.Nodes.Add(cubeDef.Name, cubeDef.Caption, "Cube", "Cube");
            catalogNode.Nodes[cubeDef.Name].Tag = cubeDef;

            #endregion

            //在该Cube节点上添加Measures文件夹
            catalogNode.Nodes[cubeDef.Name].Nodes.Add("Measures_", "Measures", "Measure", "Measure");
            catalogNode.Nodes[cubeDef.Name].Expand();

            BindMeasuresForCube(catalogNode.Nodes[cubeDef.Name].Nodes["Measures_"], cubeName, cos[catalogName]);
            BindDimensionsForCube(catalogNode.Nodes[cubeDef.Name], cubeDef);
            catalogNode.Nodes[cubeDef.Name].ContextMenuStrip = cubeMenu;
            catalogNode.Expand();
        }

        private void BindMeasuresForCube(TreeNode measuresNode, string cubeName, CubeOperate co)
        {
            IEnumerable<Measure> measures = co.GetMeasures(cubeName);
            measuresNode.Nodes.Clear();

            if (measures == null) return;
            bool hasGroup = measures.FirstOrDefault().Properties.Find("MEASUREGROUP_NAME") != null;
            if (hasGroup)
            {
                var groups = measures.Select(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString()).Distinct();
                foreach (var group in groups.OrderBy(r => r))
                {
                    var tempMeasures = measures.Where(r => r.Properties["MEASUREGROUP_NAME"].Value.ToString().Equals(group));
                    measuresNode.Nodes.Add(group, group, "Group", "Group");
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
                        measuresNode.Nodes[group].Nodes.Add(tempNode);
                    }
                }
            }
            else
            {
                measuresNode.Nodes.Add("DefaultGroup", "DefaultGroup", "Group", "Group");
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
                    measuresNode.Nodes[0].Nodes.Add(tempNode);
                }
            }
            measuresNode.Collapse();
        }

        private void BindDimensionsForCube(TreeNode cubeNode, CubeDef cubeDef)
        {
            IEnumerable<Dimension> dimensions = cubeDef.Dimensions.Cast<Dimension>();
            if (dimensions == null) return;
            //删除除Measures之外的其他所有节点（即所有维度节点）
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
            dimNode.Collapse();
        }
        private void BindLevels(TreeNode hierarchyNode, LevelCollection levels)
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
                hierarchyNode.Nodes.Add(tempNode);
            }
            hierarchyNode.Collapse();
        }

        private void ExpendMembers(TreeNode root)
        {
            if (!root.ImageKey.Equals("Level") && !root.ImageKey.Equals("Member")) return;
            if (root.Nodes.Count > 0) return;

            CubeOperate co = cos[GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode).Parent.Name];
            string connString = co.ConnectionString.ToLower();
            if (connString.Contains("mondrian"))
            {
                ExpendMondrianMembers(co, root);
            }
            else if (connString.Contains("msolap"))
            {
                ExpendSSASMembers(root);
            }

            root.Expand();
        }
        private void ExpendSSASMembers(TreeNode root)
        {
            MemberCollection members = null;
            if (root.ImageKey.Equals("Level"))
            {
                Level level = root.Tag as Level;

                members = level.GetMembers();
            }
            else if (root.ImageKey.Equals("Member"))
            {
                MemberInfo member = root.Tag as MemberInfo;
                members = member.Member.GetChildren();
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
                tempNode.Tag = new MemberInfo(member);

                root.Nodes.Add(tempNode);
            }
        }

        private void ExpendMondrianMembers(CubeOperate co, TreeNode root)
        {
            DataTable membersData = null;

            if (root.ImageKey.Equals("Level"))
            {
                Level level = root.Tag as Level;
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("CATALOG_NAME", level.Properties["CATALOG_NAME"].Value.ToString());
                dic.Add("CUBE_NAME", level.Properties["CUBE_NAME"].Value.ToString());
                dic.Add("LEVEL_UNIQUE_NAME", level.Properties["LEVEL_UNIQUE_NAME"].Value.ToString());
                membersData = QueryMondrianMembers(co, dic);
            }
            else if (root.ImageKey.Equals("Member"))
            {
                MemberInfo rootMember = root.Tag as MemberInfo;
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("CATALOG_NAME", rootMember.Properties.FirstOrDefault(p => p.Name.Equals("CATALOG_NAME")).Value.ToString());
                dic.Add("CUBE_NAME", rootMember.Properties.FirstOrDefault(p => p.Name.Equals("CUBE_NAME")).Value.ToString());
                dic.Add("MEMBER_UNIQUE_NAME", rootMember.Properties.FirstOrDefault(p => p.Name.Equals("MEMBER_UNIQUE_NAME")).Value.ToString());
                dic.Add("TREE_OP", 1);
                membersData = QueryMondrianMembers(co, dic);
            }
            List<MemberInfo> childMembers = GetMemberList(membersData);
            if (childMembers == null || childMembers.Count <= 0) return;
            foreach (var member in childMembers)
            {
                string name = member.Name.Replace("$", "");
                string caption = member.Caption.Replace("$", "");
                TreeNode tempNode = new TreeNode(caption);
                tempNode.Name = name;
                tempNode.SelectedImageKey = tempNode.ImageKey = "Member";
                tempNode.Tag = member;

                root.Nodes.Add(tempNode);
            }
        }

        private DataTable QueryMondrianMembers(CubeOperate co, Dictionary<string, object> dic)
        {
            AdomdRestrictionCollection restrictions = new AdomdRestrictionCollection();

            foreach (var item in dic)
            {
                restrictions.Add(item.Key, item.Value);
            }
            if (co.Conn.State != ConnectionState.Open)
            {
                co.Conn.Open();
            }
            return co.Conn.GetSchemaDataSet("MDSCHEMA_MEMBERS", restrictions).Tables[0];
        }
        public List<MemberInfo> GetMemberList(DataTable data)
        {
            IEnumerable<DataColumn> columns = data.Columns.Cast<DataColumn>();
            List<MemberInfo> members = new List<MemberInfo>();
            foreach (var item in data.Rows.Cast<DataRow>())
            {
                MemberInfo member = new MemberInfo();

                member.Name = item["MEMBER_NAME"].ToString();
                member.Caption = item["MEMBER_CAPTION"].ToString();
                member.UniqueName = item["MEMBER_UNIQUE_NAME"].ToString();
                member.Properties = new List<PropertyInfo>();
                foreach (var column in columns)
                {
                    member.Properties.Add(new PropertyInfo() { Name = column.ColumnName, Type = column.DataType, Value = item[column.ColumnName] });
                }
                members.Add(member);
            }
            return members;
        }

        #region 注释
        //private KeyValuePair<string, CubeOperate> GetCataloginfoOfCubeInfo(TreeNode selectNode)
        //{
        //    string catalogName = GetCatalogNodeOfCubInfo(selectNode).Name;
        //    foreach (var item in cos)
        //    {
        //        if (item.Key == catalogName)
        //            return item;
        //    }
        //    throw new Exception(string.Format("Catalog:{0}不存在", catalogName));
        //}
        //private TreeNode GetCatalogNodeOfCubInfo(TreeNode selectNode)
        //{
        //    TreeNode parentNode = selectNode.Parent;

        //    while (parentNode != null)
        //    {
        //        if (parentNode.Parent == null)
        //        {
        //            return parentNode;
        //        }
        //        else
        //        {
        //            parentNode = parentNode.Parent;
        //        }
        //    }
        //    return selectNode;
        //}
        //private TreeNode GetCatalogNode(string catalogName)
        //{
        //    return new TreeNode();
        //}
        //private TreeNode GetCubeNode(string cubeName)
        //{
        //    return tvCubeInfo.Nodes[cubeName];
        //}
        //private TreeNode GetMeasuresRoot(string cubeName)
        //{
        //    return tvCubeInfo.Nodes[cubeName].Nodes["Measures_"];
        //}

        #endregion

        #endregion

        #region ServerInfo Treeview 操作

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
        private void browerCubeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectNode = tvServerInfo.SelectedNode;

                if (selectNode.Parent != null && selectNode.Parent.Name.Equals("Cubes_") && selectNode.ImageKey.Equals("Cube"))
                {
                    BindCubeInfo(selectNode.Parent.Parent.Name, selectNode.Name);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        #endregion

        #region CubeInfo Treeview 操作

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
                case "Measure": Measure calMeasure = tempNode.Tag as Measure; table = calMeasure.Properties.PrepareData(); break;
                case "CalMeasure": Measure measure = tempNode.Tag as Measure; table = measure.Properties.PrepareData(); break;
                case "Dim": Dimension dim = tempNode.Tag as Dimension; table = dim.Properties.PrepareData(); break;
                case "SingleHie": Hierarchy singleHie = tempNode.Tag as Hierarchy; table = singleHie.Properties.PrepareData(); break;
                case "Hie": Hierarchy hie = tempNode.Tag as Hierarchy; table = hie.Properties.PrepareData(); break;
                case "Level": Level level = tempNode.Tag as Level; table = level.Properties.PrepareData(); break;
                case "Member": MemberInfo member = tempNode.Tag as MemberInfo; table = member.PrepareData(); break;

            }

            dgvObjectInfo.DataSource = table;
        }
        private void tvCubeInfo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvCubeInfo.SelectedNode = e.Node;
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
        private void tvCubeInfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        //Treeview 菜单
        private void generateSampleMdxTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode cubeNode = GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode);
            tabControlMdxEditorCollection.SelectedTab = AddMdxEditorTabPage(cubeNode.Text, cos[cubeNode.Parent.Name].ConnectionString, GenerateSampleMdx());
        }
        private void closeCurrentCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode currentCubeNode = GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode);
            tvCubeInfo.Nodes[currentCubeNode.Parent.Name].Nodes.Remove(currentCubeNode);
        }
        private void closeAllCubesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode currentCubeNode = GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode);
            tvCubeInfo.Nodes[currentCubeNode.Parent.Name].Nodes.Clear();
        }
        private void collapseAllCubesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode currentCubeNode = GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode);
            tvCubeInfo.Nodes[currentCubeNode.Parent.Name].Collapse();
        }

        public TreeNode GetCubeNodeOfCubeInfoTree(TreeNode selectedNode)
        {
            while (selectedNode.Parent != null)
            {
                if (selectedNode.Parent.Parent == null)
                {
                    return selectedNode;
                }
                else
                {
                    selectedNode = selectedNode.Parent;
                }
            }
            return selectedNode;
        }
        private string GenerateSampleMdx()
        {

            if (tvCubeInfo.SelectedNode == null) return "";

            TreeNode cubeNode = GetCubeNodeOfCubeInfoTree(tvCubeInfo.SelectedNode);
            string measureExpression = (cubeNode.Nodes[0].Nodes[0].Nodes[0].Tag as Measure).UniqueName;

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

        #endregion

        #region CubeInfo Treeview 节点过滤

        private Dictionary<string, List<string>> cubeNames = new Dictionary<string, List<string>>();
        private void btnFilter_Click(object sender, EventArgs e)
        {
            cubeNames.Clear();

            foreach (TreeNode catalogNode in tvCubeInfo.Nodes)
            {
                cubeNames.Add(catalogNode.Name, GetNodes(catalogNode, FilterType.Cube).Select(cube => cube.Name).ToList());
            }

            FilterType filterType = (FilterType)Enum.Parse(typeof(FilterType), cboxFilterType.Text.Trim());
            foreach (TreeNode catalogNode in tvCubeInfo.Nodes)
            {
                List<TreeNode> nodes = GetNodes(catalogNode, filterType);
                List<TreeNode> resultNodes = new List<TreeNode>();
                resultNodes.AddRange(nodes);
                foreach (TreeNode node in resultNodes)
                {
                    if (!FilterType.Cube.Equals(filterType))
                    {
                        if (!string.IsNullOrEmpty(txtFilterUniqueName.Text.Trim()))
                        {
                            resultNodes = resultNodes.Where(
                                row => row.Tag.GetPropertyValue("UniqueName").ToString().Contains(txtFilterUniqueName.Text.Trim())
                                ).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFilterName.Text.Trim()))
                    {
                        resultNodes = resultNodes.Where(
                            row => row.Name.Contains(txtFilterName.Text.Trim())
                            ).ToList();
                    }
                    if (!string.IsNullOrEmpty(txtFilterText.Text.Trim()))
                    {
                        resultNodes = resultNodes.Where(
                            row => row.Text.Contains(txtFilterText.Text.Trim())
                            ).ToList();
                    }

                }

                foreach (TreeNode item in nodes)
                {
                    if (!resultNodes.Contains(item))
                    {
                        //Item：非结果值
                        //在父节点中，把非结果值都删掉
                        TreeNode parent = item.Parent;
                        if (parent != null)
                        {
                            parent.Nodes.Remove(item);
                            //删除其他非结果值
                            for (int i = parent.Nodes.Count - 1; i >= 0; i--)
                            {
                                TreeNode nodeTemp = parent.Nodes[i];
                                if (!resultNodes.Contains(nodeTemp))
                                {
                                    parent.Nodes.Remove(nodeTemp);
                                }
                            }

                        }
                    }
                }


            }

        }
        private void btnCancelFilter_Click(object sender, EventArgs e)
        {
            foreach (var catalogInfo in cubeNames)
            {
                foreach (var item in catalogInfo.Value)
                {
                    BindCubeInfo(catalogInfo.Key, item);
                }
            }
        }
        public List<TreeNode> GetNodes(TreeNode catalogNode, FilterType filterType)
        {
            List<TreeNode> list = new List<TreeNode>();
            switch (filterType)
            {
                case FilterType.Cube:
                    list.AddRange(catalogNode.Nodes.Cast<TreeNode>());
                    break;
                case FilterType.Dimension:
                    catalogNode.Nodes.Cast<TreeNode>().ForEach(
                       cube =>
                       {
                           list.AddRange(cube.Nodes.Cast<TreeNode>().Where(dim => dim.ImageKey.Equals("Dim")));
                       });
                    break;
                case FilterType.Hierarchy:
                    catalogNode.Nodes.Cast<TreeNode>().ForEach(
                          cube =>
                          {
                              cube.Nodes.Cast<TreeNode>().ForEach(dim =>
                              {
                                  list.AddRange(dim.Nodes.Cast<TreeNode>().Where(hierarchy => hierarchy.ImageKey.Equals("Hie") || hierarchy.ImageKey.Equals("SingleHie")));
                              });
                          });
                    break;
                case FilterType.Level:
                    catalogNode.Nodes.Cast<TreeNode>().ForEach(
                 cube =>
                 {
                     cube.Nodes.Cast<TreeNode>().ForEach(dim =>
                     {
                         dim.Nodes.Cast<TreeNode>().ForEach(hierarchy =>
                         {
                             list.AddRange(hierarchy.Nodes.Cast<TreeNode>().Where(level => level.ImageKey.Equals("Hie") || level.ImageKey.Equals("SingleHie")));
                         });
                     });
                 });
                    break;
                case FilterType.Measure:
                    catalogNode.Nodes.Cast<TreeNode>().ForEach(
                 cube =>
                 {
                     cube.Nodes.Cast<TreeNode>().ForEach(measures =>
                     {
                         measures.Nodes.Cast<TreeNode>().ForEach(group =>
                         {
                             list.AddRange(group.Nodes.Cast<TreeNode>().Where(measure => measure.ImageKey.Equals("Measure") || measure.ImageKey.Equals("CalMeasure")));
                         });
                     });
                 });
                    break;
                default: return list;
            }

            return list;
        }

        #endregion

        #region Tab操作

        private void saveMdxInCurrentTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MdxCodeSnip> list = new List<MdxCodeSnip>();
            MdxExecuterCtrl editor = FindMdxEditorCtrl(tabControlMdxEditorCollection.SelectedTab);
            list.Add(new MdxCodeSnip()
            {
                Name = tabControlMdxEditorCollection.SelectedTab.Text,
                ConnStr = editor.ConnStr,
                Mdx = editor.Mdx
            });
            SaveToFile(list);

        }
        private void saveMdxInAllTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MdxCodeSnip> list = new List<MdxCodeSnip>();

            foreach (TabPage currentTabPage in tabControlMdxEditorCollection.TabPages)
            {
                MdxExecuterCtrl editor = FindMdxEditorCtrl(currentTabPage);
                list.Add(new MdxCodeSnip()
                {
                    Name = currentTabPage.Text,
                    ConnStr = editor.ConnStr,
                    Mdx = editor.Mdx
                });
            }
            SaveToFile(list);
        }

        private void loadMdxFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string folder = JSetting.ReadAppSetting("ConfigFileFolder"); ;
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
        private void closeAllTabsButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in tabControlMdxEditorCollection.TabPages)
            {
                if (!item.Equals(tabControlMdxEditorCollection.SelectedTab))
                {
                    tabControlMdxEditorCollection.TabPages.Remove(item);
                }
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

        private void SaveToFile(List<MdxCodeSnip> list)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            var form = this.FindForm();
            saveFileDialog1.InitialDirectory = JSetting.ReadAppSetting("ConfigFileFolder"); ;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "xml Files (.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                SerializeHelper.XmlSerializeToFile<List<MdxCodeSnip>>(list, fileName, true);
            }

        }
        private MdxExecuterCtrl FindMdxEditorCtrl(TabPage page)
        {
            var ctrl = page.Controls.Cast<Control>().Where(r => r.GetType().Equals(typeof(MdxExecuterCtrl))).FirstOrDefault();
            return ctrl as MdxExecuterCtrl;
        }
        private TabPage AddMdxEditorTabPage(string cubeText, string connStr, string mdx)
        {
            TabPage page = new TabPage(cubeText);
            MdxExecuterCtrl mdxEditor = new MdxExecuterCtrl();
            mdxEditor.ConnStr = connStr;
            mdxEditor.Mdx = mdx;
            mdxEditor.Dock = DockStyle.Fill;
            page.Controls.Add(mdxEditor);
            tabControlMdxEditorCollection.TabPages.Add(page);
            return page;
        }


        #endregion

    }

    #region 辅助类
    public class MemberInfo
    {
        public MemberInfo() { }
        public MemberInfo(Member member)
        {
            this.Member = member;
            this.Name = member.Name;
            this.Caption = member.Caption;
            this.UniqueName = member.UniqueName;
        }
        public Member Member { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public List<PropertyInfo> Properties { get; set; }
        public string Caption { get; set; }

        public DataTable PrepareData()
        {
            if (Member != null)
                return Member.Properties.PrepareData();
            else
            {
                DataTable table = new DataTable();


                foreach (var item in Properties)
                {
                    table.Columns.Add(item.Name.ToLower(), item.Type);
                }
                DataRow row = table.NewRow();
                foreach (var item in Properties)
                {
                    row[item.Name.ToLower()] = item.Value == null ? DBNull.Value : item.Value;
                }
                table.Rows.Add(row);
                return table;
            }
        }
    }
    public class PropertyInfo
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public Object Value { get; set; }
    }

    public class CatalgInfo
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public CubeOperate co { get; set; }
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
    //public class CubeViewCtrlSetting
    //{
    //    public static string DefaultConnStr { get; set; }
    //}

    public enum FilterType
    {
        Cube,
        Measure,
        Dimension,
        Hierarchy,
        Level,
        Member,
    }

    #endregion
}
