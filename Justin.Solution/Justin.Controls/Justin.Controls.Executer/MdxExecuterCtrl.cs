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
            this.LoadAction = (fileName) =>
            {
                this.txtMdx.LoadFile(fileName);
            };
            this.SaveAction = (fileName) =>
            {
                this.txtMdx.SaveFile(fileName);
            };
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
                gvMdxresult.DataSource = null;
                string mdx = txtMdx.Text;
                if (txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    mdx = txtMdx.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                }
                CellSet cst = MdxHelper.ExecuteCellSet(Connection, mdx);
                DataTable dt = cst.ToDataTable();
                gvMdxresult.DataSource = dt;
            }
            catch (Exception ex)
            {
                this.ShowMessage(string.Format("Mdx查询出错{0},", ex.ToString()));
            }
        }

        private void MdxExecuterCtrl_Load(object sender, EventArgs e)
        {
            txtMdx.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtMdx.Encoding = Encoding.GetEncoding("GB2312");
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

    }
}
