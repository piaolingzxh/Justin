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
using Justin.FrameWork.WinForm.FormUI;

namespace Justin.Controls.Mondrian
{
    public partial class MondrianServiceCtrl : JUserControl
    {
        public MondrianServiceCtrl()
        {
            InitializeComponent();
        }
        TomcatService tomcat = new TomcatService();
        MondrianService mondrian = new MondrianService();
        private void linkLabelDeafultLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtPort.Text = "8894";
            txtTomcatRootPath.Text = @"C:\Program Files (x86)\Glodon\GTP\Services\BIServer\";
            this.ConnStr = "Provider=sqloledb;Data Source=192.168.130.111;Initial Catalog=gtp_zxh;User Id=sa;Password=bisa;";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPort.Text))
            {
                this.ShowMessage("请输入端口号。");
                return;
            }
            if (string.IsNullOrEmpty(txtTomcatRootPath.Text))
            {
                this.ShowMessage("请输入Tomcat根目录。");
                return;
            }
            if (string.IsNullOrEmpty(txtJREExecuteFileName.Text))
            {
                this.ShowMessage("请输入java.exe路径。");
                return;
            }
            if (string.IsNullOrEmpty(txtMondrianRootPath.Text))
            {
                this.ShowMessage("请输入Mondrian根目录。");
                return;
            }
            if (string.IsNullOrEmpty(this.ConnStr))
            {
                this.ShowMessage("请配置连接字符串。");
                return;
            }
            if (checkGroupBoxMondrian.Checked)
                mondrian.Sync(this.ConnStr, this.txtMondrianRootPath.Text);

            string args = "";
            string javaExecutePath = Path.Combine(Path.GetDirectoryName(txtJREExecuteFileName.Text), "java_mondrian.exe");
            if (!File.Exists(javaExecutePath))
                File.Copy(txtJREExecuteFileName.Text, javaExecutePath);
            Process[] javaProcess = Process.GetProcessesByName("java_mondrian");
            if (javaProcess != null && javaProcess.Count() > 0)
            {
                foreach (Process item in javaProcess)
                {
                    item.CloseMainWindow();
                    item.Kill();
                }
            }
            tomcat.Start(txtTomcatRootPath.Text, javaExecutePath, int.Parse(txtPort.Text), out args);

            if (!checkBoxShowCmd.Checked)
            {
                Process p = new Process();
                p.EnableRaisingEvents = true;

                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                p.Exited += new EventHandler(p_Exited);
                p.StartInfo.FileName = javaExecutePath;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
            else
            {

                Process p = new Process();
                p.StartInfo.FileName = javaExecutePath;
                p.StartInfo.Arguments = args;
                p.StartInfo.WorkingDirectory = txtTomcatRootPath.Text;
                p.Start();
            }
        }
        void p_Exited(object sender, EventArgs e)
        {
            this.ShowMessage("程序已结束");
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.ShowMessage(e.Data);
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.ShowMessage(e.Data);
        }
        private void txtTomcatRootPath_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJREExecuteFileName.Text))
            {
                txtJREExecuteFileName.Text = Path.Combine(txtTomcatRootPath.Text, @"tools\jre\bin\java.exe");
            }

            if (string.IsNullOrEmpty(txtMondrianRootPath.Text))
            {
                txtMondrianRootPath.Text = Path.Combine(txtTomcatRootPath.Text, @"webapps\mondrian"); ;
            }
        }

        public override string ConnStr
        {
            get
            {
                return txtConnStr.Text;
            }
            set
            {
                txtConnStr.Text = value;
                base.ConnStr = value;
            }
        }
    }
}
