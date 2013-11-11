using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Helper;
using Justin.FrameWork.WinForm.Models;
using Justin.Log;

namespace Justin.Controls.Executer
{
    public partial class SqlExecuterCtrl : JUserControl, IFile
    {
        public SqlExecuterCtrl()
        {
            InitializeComponent();
            this.LoadAction = (fileName) =>
            {
                using (StreamReader sr = new StreamReader(txtSQLFileName.Text, Encoding.Default))
                {
                    string sql = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(sql))
                    {
                        using (new JStopWatch("ms").Start(LogMode.Warn, "自定义开始", "自定义结束"))
                        {

                            txtSQLPreview.Text = sql;
                            //设定光标所在位置 
                            txtSQLPreview.BoxPart.SelectionStart = txtSQLPreview.BoxPart.TextLength - 1;
                            //滚动到当前光标处    
                            txtSQLPreview.BoxPart.ScrollToCaret();
                        }
                    }
                    this.ShowMessage(string.Format("文件{0}加载完，共{1}行", txtSQLFileName.Text, txtSQLPreview.BoxPart.Lines.Count()));
                }
            };
            this.SaveAction = (fileName) =>
            {
                FileHelper.OverWrite(fileName, txtSQLPreview.Text);
            };
        }

        public string Extension
        {
            get { return ".sql"; }
        }

        #region override

        public override string FileName
        {
            get
            {
                return txtSQLFileName.Text;
            }
            set
            {
                txtSQLFileName.Text = value;
            }
        }

        #endregion

        #region 按钮事件

        private void btnPreviewSQLFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSQLFileName.Text))
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = txtSQLFileName.Text = openFileDialog.FileName;
                    this.LoadFile(this.FileName);
                }
            }
            else
            {
                this.FileName = txtSQLFileName.Text;
                this.LoadFile(this.FileName);
            }

        }
        private void btnBrowerSQLFile_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FileName = txtSQLFileName.Text = openFileDialog.FileName;
                txtSQLPreview.Text = "";
            }
        }

        private void btnExecuteSQLByLine_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                try
                {
                    using (StreamReader sr = new StreamReader(txtSQLFileName.Text, Encoding.Default))
                    {

                        StringBuilder builder = new StringBuilder();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            builder.Append(line).AppendLine();
                            if (builder.Length > Constants.SqlBufferSize)
                            {
                                SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, builder.ToString(), null);
                                builder.Clear();
                            }
                        }
                        if (builder.Length > 0)
                        {
                            SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, builder.ToString(), null);
                            builder.Clear();
                        }
                        sr.Close();
                    }
                    this.ShowMessage("逐行执行SQL完成");
                }
                catch (Exception ex)
                {
                    JLog.Write(LogMode.Error, ex);
                    this.ShowMessage(ex.Message.ToString(), ex.ToString());
                }
            });
        }

        private void btnExecuteAllSQL_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                using (StreamReader sr = new StreamReader(txtSQLFileName.Text, Encoding.Default))
                {
                    string sql = sr.ReadToEnd();
                    try
                    {
                        SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, sql.ToString(), null);
                        sr.Close();
                        this.ShowMessage("一次执行所有SQL完成");
                    }
                    catch (Exception ex)
                    {
                        JLog.Write(LogMode.Error, ex);
                        this.ShowMessage(ex.Message.ToString(), sql.ToString() + Environment.NewLine + ex.ToString());
                    }

                }
            });
        }

        private void btnIntelligentExecuteSQL_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                try
                {
                    using (StreamReader sr = new StreamReader(txtSQLFileName.Text, Encoding.Default))
                    {
                        bool canEnd = true;
                        bool hasStart = false;
                        StringBuilder builder = new StringBuilder();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith(Constants.SQLParagraphStartFlag))   //当遇到开始符时，则设置已开始标识
                            {
                                hasStart = true;
                                canEnd = false;
                            }
                            if (hasStart && line.StartsWith(Constants.SQLParagraphEndFlag))
                            {
                                canEnd = true;
                            }
                            if (hasStart && canEnd)
                            {
                                hasStart = false;
                            }
                            builder.Append(line).AppendLine();
                            if (canEnd)
                            {
                                if (builder.Length > Constants.SqlBufferSize)
                                {
                                    SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, builder.ToString(), null);
                                    JLog.Write(LogMode.Info, builder.ToString());
                                    builder.Clear();
                                }
                            }
                        }
                        if (builder.Length > 0)
                        {
                            SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, builder.ToString(), null);
                            builder.Clear();
                        }
                        sr.Close();
                    }
                    this.ShowMessage("智能执行所有SQL完成");
                }
                catch (Exception ex)
                {
                    JLog.Write(LogMode.Error, ex);
                    this.ShowMessage(ex.Message.ToString(), ex.ToString());
                }
            });

        }

        private void btnEnableEditSQL_Click(object sender, EventArgs e)
        {
            txtSQLPreview.BoxPart.ReadOnly = false;
        }

        private void btnModifySQLFileContent_Click(object sender, EventArgs e)
        {
            this.SaveFile(this.FileName, this.Extension);
        }

        private void btnShowLineNum_Click(object sender, EventArgs e)
        {
            txtSQLPreview.ShowLineNumber = !txtSQLPreview.ShowLineNumber;
        }

        #endregion

        private void txtSQLPreview_Load(object sender, EventArgs e)
        {

            txtSQLPreview.BoxPart.ReadOnly = false;
            #region tips

            btnIntelligentExecuteSQL.Tag = string.Format(@"{0}
{1}   
以上两行之内的SQL将一次性执行.
分段SQL开始符：{0}
分段SQL结束符：{1}
", Constants.SQLParagraphStartFlag, Constants.SQLParagraphEndFlag);
            ToolTip tips = new ToolTip();
            this.SetToolTipsForButton(tips);
            #endregion

            #region openFileDialog

            openFileDialog.InitialDirectory = Constants.OuputSQLFileFolder;

            StringBuilder filterBuilder = new StringBuilder();
            //FileInfoAttribute fia = FileType.SQL.GetFileInfoAttribute();
            //filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, fia.DefaultFileExtension, fia.DefaultDisplayName);
            //foreach (string fileExtension in fia.GetAllowFileExtensions(true))
            //{
            //    filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, fileExtension, fileExtension);
            //}
            filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, "sql", "sql文件");
            filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, "*", "所有");
            openFileDialog.Filter = filterBuilder.ToString().TrimEnd('|');

            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            #endregion
            txtSQLPreview.AllowDrop = true;
        }




    }
}
