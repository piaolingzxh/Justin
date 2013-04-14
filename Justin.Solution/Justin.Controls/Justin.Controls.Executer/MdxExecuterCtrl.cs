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
namespace Justin.Controls.Executer
{
    public partial class MdxExecuterCtrl : JUserControl, IFile
    {
        public MdxExecuterCtrl()
        {
            InitializeComponent();
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
            try
            {
                CellSet cst = MdxHelper.ExecuteCellSet(Connection, txtMdx.Text);
                DataTable dt = cst.ToDataTable();
                gvMdxresult.DataSource = dt;
            }
            catch (Exception ex)
            {
                this.ShowMessage("Mdx查询出错,", ex.ToString());
            }
        }

        private void MdxExecuterCtrl_Load(object sender, EventArgs e)
        {
            txtMdx.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtMdx.Encoding = Encoding.GetEncoding("GB2312");
            
            if (!string.IsNullOrEmpty(FileName) && File.Exists(FileName))
            {
                txtMdx.LoadFile(FileName);
            }
        }

        public string ConnStr
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

        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            txtMdx.SaveFile(fileName);
        }
        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                txtMdx.LoadFile(fileName);
        }
    }
}
