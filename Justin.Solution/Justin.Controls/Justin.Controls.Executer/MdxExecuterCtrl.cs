using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using Justin.FrameWork.Settings;
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
                    connection = new AdomdConnection(cboxConnStrings.Text);
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    this.ShowMessage("连接OLAP成功:" + cboxConnStrings.Text);
                }
                catch (Exception ex)
                {
                    this.ShowMessage("连接OLAP失败", ex.ToString());
                }

                return connection;
            }
        }
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

        private void MdxExecuterCtrl_Load(object sender, EventArgs e)
        {
            this.cboxConnStrings.Items.Clear();
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("OLAPConnStr", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.cboxConnStrings.Items.Add(ConfigurationManager.AppSettings[item]);
                }
            }
            txtMdx.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtMdx.Encoding = Encoding.GetEncoding("GB2312");

            this.SetToolTipsForButton(new ToolTip());
            if (string.IsNullOrEmpty(cboxConnStrings.Text))
                this.cboxConnStrings.Text = JSetting.Get("OLAPConnStr");

        }

        #region 拖动

        private void txtMdx_DragDrop(object sender, DragEventArgs e)
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

        #endregion

        #region 按钮 事件

        private void btnConnectOLAP_Click(object sender, EventArgs e)
        {
            var conn = Connection;
        }

        private void btnDefaultConnStr_Click(object sender, EventArgs e)
        {
            this.cboxConnStrings.Text = JSetting.Get("OLAPConnStr");
        }

        bool lastUseFormattedValue = false;
        private void btnExecute_Click(object sender, EventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                ClearQueryresult();
                string mdx = txtMdx.Text;
                if (txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    mdx = txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                }
                CellSet cst = MdxHelper.ExecuteCellSet(Connection, mdx);
                gvMdxresult.Tag = cst;
                lastUseFormattedValue = sender == this.btnExecuteWithFormatted;
                //DataTable dt = cst.ToDataTable2(lastUseFormattedValue);

                BindCellSet(gvMdxresult, cst, lastUseFormattedValue);
                //gvMdxresult.DataSource = dt;
                ShowResult(gvMdxresult);
            }
            catch (AdomdException aex)
            {
                this.ShowMessage("Mdx查询出错{0},", aex.ToString());
            }
            catch (Exception ex)
            {
                this.ShowMessage("Mdx查询出错{0},", ex.ToString());
            }
            finally
            {
                watch.Stop();
                this.ShowMessage("查询耗时{0}毫秒,", watch.ElapsedMilliseconds);
            }
        }
        private void btnExecuteDataSet_Click(object sender, EventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                ClearQueryresult();
                string mdx = txtMdx.Text;
                if (txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    mdx = txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                }

                DataTable dt = MdxHelper.ExecuteDataTable(Connection, mdx);
                gvMdxresult.DataSource = dt;
                ShowResult(gvMdxresult);
            }
            catch (Exception ex)
            {
                this.ShowMessage("Mdx查询出错{0},", ex.ToString());
            }
            finally
            {
                watch.Stop();
                this.ShowMessage("查询耗时{0}毫秒,", watch.ElapsedMilliseconds);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearQueryresult();
        }

        private void ClearQueryresult()
        {
            this.gvMdxresult.DataSource = null;
            if (this.gvMdxresult.Rows.Count > 0)
            {
                this.gvMdxresult.Rows.Clear();
            }
            if (this.gvMdxresult.Columns.Count > 0)
            {
                this.gvMdxresult.Columns.Clear();
            }
            this.txtResult.Text = "";
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (gvMdxresult.DataSource != null && (gvMdxresult.DataSource is DataTable || gvMdxresult.Tag is CellSet))
            {
                DataTable dt;
                if (gvMdxresult.DataSource is DataTable)
                    dt = gvMdxresult.DataSource as DataTable;
                else
                    dt = (gvMdxresult.Tag as CellSet).ToDataTable();

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

        #endregion

        private void ShowResult(DataGridView dgv)
        {
            txtResult.Text = string.Format("查询结果:{0}行/{1}列,", dgv.Rows.Count, dgv.Columns.Count);
        }

        public void BindCellSet(DataGridView grid, CellSet cs, bool useFormattedValue = false)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            if (cs == null)
                return;
            try
            {

                //DataTable dt = new DataTable();

                int columnCountOfRowHeader = 0;

                #region 构造表头

                //当有行头时，添加行头占的列
                if (cs.Axes.Count > 1 && cs.Axes[1].Set.Tuples.Count > 0)
                {
                    columnCountOfRowHeader = cs.Axes[1].Set.Tuples[0].Members.Count;
                    for (int i = 0; i < columnCountOfRowHeader; i++)
                    {
                        Member member = cs.Axes[1].Set.Tuples[0].Members[i];
                        DataGridViewColumn column = new DataGridViewTextBoxColumn();
                        column.Name = member.UniqueName;
                        column.Tag = member;
                        grid.Columns.Add(column);
                    }
                }

                //继续添加列头
                foreach (Microsoft.AnalysisServices.AdomdClient.Tuple tp in cs.Axes[0].Set.Tuples)
                {

                    string columnName = "";
                    foreach (Member member in tp.Members)
                    {
                        columnName = columnName + member.Caption + " ";
                    }
                    DataGridViewColumn column = new DataGridViewTextBoxColumn();
                    column.Name = columnName.Trim();
                    column.Tag = null;

                    grid.Columns.Add(column);
                }

                #endregion

                #region 构造数据

                int rowCount = cs.Axes.Count <= 1 ? 0 : cs.Axes[1].Set.Tuples.Count;
                int cellIndex = 0;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {

                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(grid);
                    if (cs.Axes.Count > 1)
                    {
                        for (int columnIndexOfRowHeader = 0; columnIndexOfRowHeader < columnCountOfRowHeader; columnIndexOfRowHeader++)
                        {
                            var member = cs.Axes[1].Set.Tuples[rowIndex].Members[columnIndexOfRowHeader];
                            dr.Cells[columnIndexOfRowHeader].Value = member.Caption;
                            dr.Cells[columnIndexOfRowHeader].Tag = member;
                        }
                    }

                    //数据列
                    for (int columnIndexOfData = columnCountOfRowHeader; columnIndexOfData < cs.Axes[0].Positions.Count + columnCountOfRowHeader; columnIndexOfData++)
                    {
                        try
                        {
                            Cell cell = cs[cellIndex++];
                            dr.Cells[columnIndexOfData].Tag = cell;

                            if (useFormattedValue)
                            {
                                dr.Cells[columnIndexOfData].Value = cell.FormattedValue;
                            }
                            else
                            {
                                dr.Cells[columnIndexOfData].Value = cell.Value;
                            }

                            if (dr.Cells[columnIndexOfData].ToString() == "null")
                            {
                                dr.Cells[columnIndexOfData].Value = "";
                            }
                        }
                        catch
                        {
                            dr.Cells[columnIndexOfData].Value = "";
                        }
                    }
                    grid.Rows.Add(dr);

                }

                #endregion

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ShowCellInfo(int rowIndex, int columnIndex)
        {
            propertyGrid.SelectedObject = gvMdxresult.Rows[rowIndex].Cells[columnIndex].Tag;
        }

        private void gvMdxresult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowCellInfo(e.RowIndex, e.ColumnIndex);
        }


    }

    //public class MdxExecuterCtrlSetting
    //{
    //    public static string DefaultConnStr { get; set; }
    //}
}
