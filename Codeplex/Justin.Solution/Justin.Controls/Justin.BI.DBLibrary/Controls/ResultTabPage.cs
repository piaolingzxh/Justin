using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.BI.DBLibrary.Controls
{
    public class ResultTabPage : TabPage
    {

        public ResultTabPage(DataTable table, string sql)
        {
            SqlResultCtrl = new SqlResult(table, sql);
            SqlResultCtrl.Dock = DockStyle.Fill;
            this.Controls.Add(SqlResultCtrl);
        }
        public SqlResult SqlResultCtrl { get; private set; }


    }
}
