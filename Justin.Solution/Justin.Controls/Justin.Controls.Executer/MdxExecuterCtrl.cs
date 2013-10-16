using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using System.IO;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;
using System.Diagnostics;
namespace Justin.Controls.Executer
{
    public partial class MdxExecuterCtrl : JUserControl, IFile
    {
        public MdxExecuterCtrl()
        {
            InitializeComponent();
            this.LoadAction = (fileName) =>
            {
                this.txtMdx.LoadFile(fileName);
            };
            this.SaveAction = (fileName) =>
            {
                this.txtMdx.SaveFile(fileName);
            };
        }

        public static string DefaultConnStr { get; set; }
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

        private void btnConnectOLAP_Click(object sender, EventArgs e)
        {
            var conn = Connection;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                gvMdxresult.DataSource = null;
                string mdx = txtMdx.Text;
                if (txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    mdx = txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                }
                CellSet cst = MdxHelper.ExecuteCellSet(Connection, mdx);


                bool useFormattedValue = sender == this.btnExecute;
                DataTable dt = cst.ToDataTable(useFormattedValue);
                gvMdxresult.DataSource = dt;
                ShowResult(dt);
            }
            catch (AdomdException aex)
            {
                this.ShowMessage(string.Format("Mdx查询出错{0},", aex.ToString()));
            }
            catch (Exception ex)
            {
                this.ShowMessage(string.Format("Mdx查询出错{0},", ex.ToString()));
            }
            finally
            {
                watch.Stop();
                this.ShowMessage(string.Format("查询耗时{0}毫秒,", watch.ElapsedMilliseconds));
            }
        }

        private void MdxExecuterCtrl_Load(object sender, EventArgs e)
        {
            txtMdx.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtMdx.Encoding = Encoding.GetEncoding("GB2312");
            this.SetToolTipsForButton(new ToolTip());
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

        private void btnExecuteDataSet_Click(object sender, EventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                gvMdxresult.DataSource = null;
                string mdx = txtMdx.Text;
                if (txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    mdx = txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                }

                DataTable dt = MdxHelper.ExecuteDataTable(Connection, mdx);
                gvMdxresult.DataSource = dt;
                ShowResult(dt);
            }
            catch (Exception ex)
            {
                this.ShowMessage(string.Format("Mdx查询出错{0},", ex.ToString()));
            }
            finally
            {
                watch.Stop();
                this.ShowMessage(string.Format("查询耗时{0}毫秒,", watch.ElapsedMilliseconds));
            }
        }

        public string Extension
        {
            get { return ".mdx"; }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.txtConnectionString.Text = "Provider=mondrian;Data Source=http://localhost:8080/mondrian_mssql/xmla;Initial Catalog=gtp;";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.gvMdxresult.DataSource = null;
            this.txtResult.Text = "";
        }

        private void ShowResult(DataTable dt)
        {
            txtResult.Text = string.Format("查询结果:{0}行/{1}列,", dt == null ? 0 : dt.Rows.Count, dt == null ? 0 : dt.Columns.Count);
        }

        private void btnDefaultConnStr_Click(object sender, EventArgs e)
        {
            this.txtConnectionString.Text = DefaultConnStr;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (gvMdxresult.DataSource != null && gvMdxresult.DataSource is DataTable)
            {
                DataTable dt = gvMdxresult.DataSource as DataTable;

                SaveFileDialog sfd = new SaveFileDialog();
                //设置文件类型 
                sfd.Filter = "Excel文件（*.xls）|*.xls";
                //设置默认文件类型显示顺序 
                sfd.FilterIndex = 1;

                //点了保存按钮进入 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExcelHelper.Output(dt, sfd.FileName);
                }
                this.ShowMessage("导出成功！");
            }
            this.ShowMessage("没有数据可以导出！");
        }

    }
}
