using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.FormUI;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Controls.CubeView
{
    public partial class XMLForAnalysisToolCtrl : JUserControl
    {
        public XMLForAnalysisToolCtrl()
        {
            InitializeComponent();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            AdomdRestrictionCollection restrictions = new AdomdRestrictionCollection();

            try
            {

                foreach (DataGridViewRow row in gridRestrictions.Rows)
                {
                    if (row.Cells["AdomdRestrictionName"].Value != null && row.Cells["AdomdRestrictionName"].Value != DBNull.Value && !string.IsNullOrEmpty(row.Cells["AdomdRestrictionName"].Value.ToString()))
                        restrictions.Add(row.Cells["AdomdRestrictionName"].Value.ToString().Trim(), row.Cells["AdomdRestrictionValue"].Value.ToString().Trim());
                }
                AdomdConnection conn = new AdomdConnection(txtConnStr.Text);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                DataTable table = conn.GetSchemaDataSet(cboxRequestType.Text, restrictions).Tables[0];
                gridResults.DataSource = table;
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        private void XMLForAnalysisTool_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            table.Columns.Add("AdomdRestrictionName", typeof(String));
            table.Columns.Add("AdomdRestrictionValue", typeof(String));

            DataRow row1 = table.NewRow();
            row1[0] = "CATALOG_NAME"; row1[1] = "GTP.Task";
            table.Rows.Add(row1);
            DataRow row2 = table.NewRow();
            row2[0] = "CUBE_NAME"; row2[1] = "Task.BITESTBITheme.Task_LBDJBICS";
            table.Rows.Add(row2);
            DataRow row3 = table.NewRow();
            row3[0] = "LEVEL_UNIQUE_NAME"; row3[1] = "[Task.BMCZBIDim.hieInfo2].[DeptGroup]";
            table.Rows.Add(row3);
            gridRestrictions.DataSource = table;
        }
    }
}
