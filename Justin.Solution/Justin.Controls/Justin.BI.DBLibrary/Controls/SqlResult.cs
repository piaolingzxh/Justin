using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.BI.DBLibrary.Controls
{
    public partial class SqlResult : UserControl
    {
        private DataTable DataSource { get; set; }
        private string SQL { get; set; }

        public SqlResult(DataTable table, string sql)
        {
            InitializeComponent();
            this.DataSource = table;
            this.SQL = sql;
        }

        private void SqlResult_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataSource;
            txtSql.Text = SQL;
            txtMessage.Text = string.Format("{0}行受影响！", DataSource == null ? 0 : DataSource.Rows.Count);

        }
    }
}
