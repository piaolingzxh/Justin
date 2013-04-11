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
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.FrameWork.Helper;
using System.Configuration;
using WeifenLuo.WinFormsUI.Docking;
using Justin.Log;
using System.Security.AccessControl;
using Justin.Toolbox.Tools;
using Justin.FrameWork.Settings;
using Justin.BI.DBLibrary.Utility;
using Justin.BI.DBLibrary.DAL;
using Justin.Core;


namespace Justin.Toolbox.Tools
{
    public partial class TestDataGenerator : JDBForm
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
            TableConfigurator form = new TableConfigurator(table);
            form.MdiParent = this.MdiParent;
            form.Show();
        }

        private DBTable GetDBTableBySourceTreeNode(TreeNode sourceTreeNode)
        {
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

        private void tvAllTables_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.Checked = !e.Node.Checked;
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
                string fileName = JTools.GetFileName(dbTable.TableName, FileType.TableConfig);
                JTable table = JTools.ReadTableSettingByFile(fileName);
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

        private void fomr1_Load(object sender, EventArgs e)
        {

            ToolTip tips = new ToolTip();

            foreach (Control item in this.Controls)
            {
                JTools.SetToolTips(item, tips);
            }

        }

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.ConnStr);
        }

        private void tvAllTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvAllTables.SelectedNode = e.Node;
        }


    }



}
