using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;
using Microsoft.AnalysisServices.AdomdClient;
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
            txtMdx.ActiveTextAreaControl.AllowDrop = true;
            txtMdx.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(txtMdx_DragDrop);
            this.txtMdx.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(txtMdx_DragEnter);
        }

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
            if (string.IsNullOrEmpty(txtConnectionString.Text))
                this.txtConnectionString.Text = MdxExecuterCtrlSetting.DefaultConnStr;
        }

        void txtMdx_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
            string dragText = "";
            switch (node.ImageKey)
            {
                case "Cube": dragText = string.Format("[{0}]", GetDragText(node, new string[] { "Tag.Name" })); break;
                case "Dim": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "SingleHie": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "Hie": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "Level": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "Member": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "Measure": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                case "CalMeasure": dragText = GetDragText(node, new string[] { "Tag.UniqueName" }); break;
                default: return;
            }
            txtMdx.ActiveTextAreaControl.TextArea.InsertString(dragText);
        }
        private void txtMdx_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private string GetDragText(TreeNode node, string[] pathOfPropertyNames)
        {
            for (int i = 0; i < pathOfPropertyNames.Count(); i++)
            {
                string propertyPath = pathOfPropertyNames[i];
                string dragText = node.ToDragText(propertyPath);
                if (string.IsNullOrEmpty(dragText))
                {
                    continue;
                }
                return dragText;
            }
            return "";
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

        public string Mdx
        {
            get
            {
                return txtMdx.Text;
            }
            set
            {
                txtMdx.Text = value;
            }
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
            this.txtConnectionString.Text = MdxExecuterCtrlSetting.DefaultConnStr;
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


    public class MdxExecuterCtrlSetting
    {
        public static string DefaultConnStr { get; set; }
    }
}
