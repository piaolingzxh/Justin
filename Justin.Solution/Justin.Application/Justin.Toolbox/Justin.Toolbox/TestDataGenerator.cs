using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Justin.FrameWork.Helper;
using System.Configuration;
using WeifenLuo.WinFormsUI.Docking;
using Justin.Log;
using System.Security.AccessControl;
using Justin.Toolbox;
using Justin.FrameWork.Settings;
using Justin.Core;
using Justin.Controls.TestDataGenerator.Entities;
using Justin.Controls.TestDataGenerator.DAL;
using Justin.Controls.TestDataGenerator.Utility;
using Justin.Controls.TestDataGenerator;


namespace Justin.Toolbox
{
    public partial class TestDataGenerator : JForm, IDB
    {
        public TestDataGenerator()
        {
            InitializeComponent();
        }

        public TestDataGenerator(string connStr)
            : this()
        {
            this.ConnStr = connStr;
        }

        #region 功能函数


        private void ShowConfigTableForm(JTable table)
        {
            TableConfigurator form = new TableConfigurator(table, this.ConnStr);
            form.MdiParent = this.MdiParent;
            form.ShowStatus = true;
            form.Show();
        }

        private DBTable GetDBTableBySourceTreeNode(TreeNode sourceTreeNode)
        {
            //找到table节点，返回tag中的DBTable信息
            var node = sourceTreeNode;
            while (node != null)
            {
                if (node.ImageIndex == 0)
                {
                    DBTable dbTable = node.Tag as DBTable;
                    return dbTable;
                }
                else
                {
                    node = node.Parent;
                }
            }
            return null;
        }

        #endregion

        #region 窗体事件

        private void fomr1_Load(object sender, EventArgs e)
        {
            ToolTip tips = new ToolTip();
            this.SetToolTipsForButton(tips);
        }

        #endregion

        #region 树 事件

        private void tvAllTables_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.Checked = !e.Node.Checked;
        }
        private void tvAllTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvAllTables.SelectedNode = e.Node;
        }

        #region TtvSource事件

        private void tvSource_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvSource.SelectedNode = e.Node;
        }

        private void loadMenuItemOfTVSource_Click(object sender, EventArgs e)
        {
            DBTable dbTable = GetDBTableBySourceTreeNode(tvSource.SelectedNode);
            if (dbTable != null)
            {
                string fileName = TableConfigCtrl.GetFullFileName(dbTable.TableName, TableConfigCtrl.configFileExtensions);
                JTable table = SerializeHelper.XmlDeserializeFromFile<JTable>(fileName);
                ShowConfigTableForm(table);
                this.ShowMessage(string.Format("已加载表【{0}】的配置文件[{1}]", dbTable.TableName, fileName));
            }
        }

        private void TranslateMenuItemOfTVSource_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                DBTable dbTable = GetDBTableBySourceTreeNode(tvSource.SelectedNode); ;
                if (dbTable != null)
                {
                    JTable table = new JTable(dbTable, this.ConnStr);
                    ShowConfigTableForm(table);
                    this.ShowMessage(string.Format("已翻译表【{0}】为配置文件", dbTable.TableName));
                }
            });
        }

        #endregion

        #endregion

        #region 按钮事件

        private void btnDataSourceChoose_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                string tableNameFilter = txtTableNameFilter.Text;
                MSSQLTableDAL tableDAL = new MSSQLTableDAL(this.ConnStr);
                IEnumerable<string> dsTables = tableDAL.GetAllTables();
                if (!string.IsNullOrEmpty(tableNameFilter))
                {
                    dsTables = dsTables.Where(row => row.ToUpper().Contains(tableNameFilter.ToUpper()));
                }
                tvAllTables.Nodes.Clear();
                foreach (var item in dsTables)
                {
                    tvAllTables.Nodes.Add(item);
                }
                this.ShowMessage("已选择数据源");
            });
        }

        private void btnPrepareDBTables_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                List<string> tableNames = new List<string>();
                foreach (TreeNode item in tvAllTables.Nodes)
                {
                    if (item.Checked)
                    {
                        tableNames.Add(item.Text);
                    }
                }

                tvSource.Nodes.Clear();
                tvSource.ImageList = imageList1;
                MSSQLTableDAL tableDAL = new MSSQLTableDAL(this.ConnStr);
                var dbTables = tableDAL.GetAllTableSchema(tableNames);
                foreach (var dbTable in dbTables)
                {
                    var tableNode = new TreeNode(dbTable.TableName);
                    tableNode.ImageIndex = tableNode.SelectedImageIndex = 0;
                    tableNode.Tag = dbTable;
                    if (dbTable.PrimaryKey != null)
                    {
                        var pkNode = new TreeNode(dbTable.PrimaryKey.ColumnName);
                        pkNode.ImageIndex = pkNode.SelectedImageIndex = 2;
                        pkNode.Tag = dbTable.PrimaryKey;
                        tableNode.Nodes.Add(pkNode);
                    }

                    foreach (var fk in dbTable.ForeignKeys)
                    {
                        var fkNode = new TreeNode(fk.ColumnName);
                        fkNode.ImageIndex = fkNode.SelectedImageIndex = 3;
                        fkNode.Tag = fk;
                        tableNode.Nodes.Add(fkNode);
                    }

                    foreach (var column in dbTable.Columns)
                    {
                        var commonNode = new TreeNode(column.ColumnName);
                        commonNode.ImageIndex = commonNode.SelectedImageIndex = 1;
                        commonNode.Tag = column;
                        tableNode.Nodes.Add(commonNode);
                    }

                    tvSource.Nodes.Add(tableNode);
                    this.ShowMessage(string.Format("已加载表【{0}】信息", dbTable.TableName));
                }
            });
        }


        #endregion

        #region override

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.ConnStr);
        }

        #endregion

    }

}
