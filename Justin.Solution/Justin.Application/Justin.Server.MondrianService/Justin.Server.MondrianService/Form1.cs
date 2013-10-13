using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.Server.MondrianService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPort.Text) || string.IsNullOrEmpty(txtTomcatRootPath.Text))
            {
                MessageBox.Show("请指定端口号和Tomcat根目录");
            }
            MondrianService service = new MondrianService();


            service.Start(txtTomcatRootPath.Text, txtJREExecuteFileName.Text, txtMondrianRootPath.Text, int.Parse(txtPort.Text));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtPort.Text = "8899";
            txtTomcatRootPath.Text = @"D:\Programs\MondrianServer";
            txtJREExecuteFileName.Text = @"D:\Programs\MondrianServer\tools\jre\bin\java.exe";
            txtMondrianRootPath.Text = @"D:\Programs\MondrianServer\webapps\mondrian";

        }
    }
}
