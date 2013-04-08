using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.BI.DBLibrary.Utility;

namespace Justin.Toolbox
{
    public partial class JDBDcokForm : JDockForm
    {
        public JDBDcokForm()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.ToolStripMenuItem chooseDataBaseToolStripMenuItem;

        private string connStr = "";
        protected virtual string ConnStr
        {
            get
            {
                return connStr;
            }
            set
            {
                string oldConnStr = connStr;
                connStr = value;
                if (string.Compare(oldConnStr, value, true) != 0)
                {
                    if (ConnStrChanged != null)
                    {
                        ConnStrChanged(oldConnStr, value);
                    }
                    this.toolStripStatusDataSource.Text = connStr;
                }
            }
        }


        public void CheckConnStringAssigned(Action action)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                action();
            }
            else
            {
                this.ShowMessage("请选择数据源。");
            }  
        }
        public ConnStrChangDelegate ConnStrChanged;

        private void chooseDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tempConnStr = DBConnectDialog.GetConnectionString(Justin.BI.DBLibrary.Utility.DBConnectDialog.DataSourceType.SqlDataSource);
            if (!string.IsNullOrEmpty(tempConnStr))
            {
                this.ConnStr = tempConnStr;
                this.ShowMessage("更改数据源。");
            }
        }

        protected virtual bool ShowStatus
        {
            get
            {
                return true;
            }
        }
        protected virtual bool NeedChooseDataSource
        {
            get
            {
                return true;
            }
        }

        public virtual String Title
        {
            get
            {
                return this.toolStripStatusTitle.Text;
            }
            protected set
            {
                this.toolStripStatusTitle.Text = value;
            }
        }

        private void JDBDcokForm_Load(object sender, EventArgs e)
        {
            if (NeedChooseDataSource)
            {
                this.chooseDataBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.chooseDataBaseToolStripMenuItem.Name = "chooseDataBaseToolStripMenuItem";
                this.chooseDataBaseToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.chooseDataBaseToolStripMenuItem.Text = "ChooseDataBase";
                this.chooseDataBaseToolStripMenuItem.Click += chooseDataBaseToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.chooseDataBaseToolStripMenuItem);
            }
            this.statusStrip1.Visible = ShowStatus;
        }

    }
}
