using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.Extensions;
namespace FormUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connStr = "data source=.;initial catalog=gtp_bi_dw;integrated security=True;multipleactiveresultsets=True;";
            treeView1.BuildTree(connStr, "SELECT DEPT_ID_KEY ID,FK_PARENT_DEPT_ID PID,DEPT_VAL Name FROM D_DEPT");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            txtId.Text = e.Node.Text;
            txtName.Text = e.Node.ToolTipText;
            txtPId.Text = e.Node.Parent == null ? "" : e.Node.Parent.Text;
            txtPName.Text = e.Node.Parent == null ? "" : e.Node.Parent.ToolTipText;
        }
    }
}
