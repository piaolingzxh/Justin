using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using WeifenLuo.WinFormsUI.Docking;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Extensions;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.BI.DBLibrary.Utility;
using Justin.BI.DBLibrary.Controls;
using Justin.Core;
namespace Justin.Toolbox.Controls
{
    public partial class SqlExecuteorTool : JDBDcokForm
    {

        public SqlExecuteorTool(MainForm mainForm)
        {
            InitializeComponent();
        }

        #region 窗体事件

        private void SqlExecuteorTool_Load(object sender, EventArgs e)
        {
            BindSqlSyntax();
        }

        private void btnExecuteSQLText_Click(object sender, EventArgs e)
        {

            string executeSQL = txtSqlText.Text.Trim();
            if (!string.IsNullOrEmpty(txtSqlText.SelectedText))
            {
                executeSQL = txtSqlText.SelectedText.Trim();
            }

            tabControlSqlResultContainer.TabPages.Clear();
            SqlSyntax syntax = cBoxTxtSqlSyntax.Text.GetEnum<SqlSyntax>(SqlSyntax.MSSQL);
            using (IDbConnection conn = DBHelper.GetConnection(syntax, this.ConnStr))
            {
                if (syntax != null && !string.IsNullOrEmpty(executeSQL))
                {
                    string[] values = executeSQL.Split(';');

                    foreach (var sqlFragment in values)
                    {
                        if (string.IsNullOrEmpty(sqlFragment.Trim()))
                        {
                            continue;
                        }
                        switch (GetOperateType(sqlFragment))
                        {
                            case OperateType.ExecuteDataTable:

                                DataTable tempTable = DBHelper.ExecuteDataTable(conn, sqlFragment.Trim() + ";", syntax);
                                ResultTabPage page = new ResultTabPage(tempTable, sqlFragment.Trim() + ";");
                                page.Text = "Query" + tabControlSqlResultContainer.TabPages.Count;
                                tabControlSqlResultContainer.TabPages.Add(page);
                                break;
                            case OperateType.ExecuteNonQuery:
                                DBHelper.ExecuteNonQuery(conn, sqlFragment, syntax);
                                break;
                        }
                    }
                }
                conn.Close();
                conn.Dispose();
            }
        }

        private void btnExecuteSQLFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.NotImplemented);
        }

        #endregion

        #region 绑定数据

        public void BindSqlSyntax()
        {
            string[] synatax = { "MSSQL", "Oracle", "GSQL", "Mdx" };
            cBoxTxtSqlSyntax.DataSource = synatax.Clone();
            cBoxFileSqlSyntax.DataSource = synatax.Clone();
        }

        #endregion

        #region 辅助函数

        private OperateType GetOperateType(string sql)
        {
            if (sql.Trim().StartsWith("select ", StringComparison.CurrentCultureIgnoreCase))
            {
                return OperateType.ExecuteDataTable;
            }
            return OperateType.ExecuteNonQuery;
        }

        #endregion

    }
}
