using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Justin.FrameWork.Services;
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

        private string JREExecuteFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(this.txtJREExecuteFileName.Text)) return "";
                if (!checkBoxRenameJREFile.Checked || string.IsNullOrEmpty(this.txtJREReName.Text)) return this.txtJREExecuteFileName.Text;

                string newJREName = this.txtJREReName.Text.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase) ? this.txtJREReName.Text : this.txtJREReName.Text + ".exe";
                string finalJREFilePath = Path.Combine(Path.GetDirectoryName(this.txtJREExecuteFileName.Text), newJREName);
                return finalJREFilePath;
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
        public bool StopServiceWhenExitForm
        {
            get { return this.checkBoxStopServiceWhenExit.Checked; }
            set { this.checkBoxStopServiceWhenExit.Checked = value; }
        }

        private void MondrianServiceCtrl_Load(object sender, EventArgs e)
        {
            linkLabelDeafultLocation_LinkClicked(null, null);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPort.Text))
            {
                this.ShowMessage("请输入tomcat的端口号。");
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
            if (this.checkGroupBoxMondrian.Checked && string.IsNullOrEmpty(this.ConnStr))
            {
                this.ShowMessage("请配置连接字符串。");
                return;
            }
            if (this.checkBoxRenameJREFile.Checked)
            {
                if (string.IsNullOrEmpty(this.txtJREReName.Text))
                {
                    this.ShowMessage("请设置重命名后的JRE可执行文件名，包含“.exe”后缀。");
                    return;
                }
                if (!File.Exists(this.JREExecuteFilePath))
                {
                    File.Copy(this.checkBoxRenameJREFile.Text, this.JREExecuteFilePath);
                }
            }

            this.ShowMessage("开始启动服务。。。");
            if (checkGroupBoxMondrian.Checked)
            {
                mondrian.Sync(this.ConnStr, this.txtMondrianRootPath.Text);
                this.ShowMessage("同步mondrian数据源。。。");
            }
            string args = "";
            StopService();
            this.ShowMessage("启动服务中。。。");

            Process portProcess = GetProcessByPort(int.Parse(this.txtPort.Text));
            if (portProcess != null)
            {
                this.ShowMessage("tomcat的端口号:{0}已经被进程Id:{1} Name:{2}占用。请重新指定", txtPort.Text, portProcess.Id, portProcess.ProcessName);
                return;
            }

            tomcat.Start(txtTomcatRootPath.Text, this.JREExecuteFilePath, int.Parse(txtPort.Text), out args);

            if (!checkBoxShowCmd.Checked)
            {
                Process p = new Process();
                p.OutputDataReceived += new DataReceivedEventHandler((tSender, ee) =>
                {
                    this.ShowMessage(ee.Data);
                });
                p.ErrorDataReceived += new DataReceivedEventHandler((tSender, ee) =>
                {
                    MessageSvc.Default.Write(MessageLevel.Error, ee.Data);
                });
                p.Exited += new EventHandler((tSender, ee) => { this.ShowMessage("mondrian服务已停止。。。"); });
                p.EnableRaisingEvents = true;

                p.StartInfo.FileName = this.JREExecuteFilePath;
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
                p.StartInfo.FileName = this.JREExecuteFilePath;
                p.StartInfo.Arguments = args;
                p.StartInfo.WorkingDirectory = txtTomcatRootPath.Text;
                p.Start();
            }
        }
        private void btnStopService_Click(object sender, EventArgs e)
        {
            StopService();
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
        private void linkLabelDeafultLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtPort.Text = "8894";
            txtTomcatRootPath.Text = @"C:\Program Files (x86)\Glodon\GTP\Services\BIServer\";
            this.ConnStr = "Provider=sqloledb;Data Source=192.168.130.111;Initial Catalog=gtp_zxh;User Id=sa;Password=bisa;";
            txtJREReName.Text = "java_mondrian.exe";
        }

        public void StopService()
        {
            this.ShowMessage("检查服务进程。");

            Process[] javaProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(this.JREExecuteFilePath));
            if (javaProcess != null && javaProcess.Count() > 0)
            {
                foreach (Process item in javaProcess)
                {
                    item.CloseMainWindow();
                    item.Kill();
                }
                this.ShowMessage("停止服务进程。。。");
            }

        }

        public static bool PortInUse(int port)
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Count(iep => iep.Port == port) > 0;
        }
        public static Process GetProcessByPort(int port)
        {
            Process result = null;

            Process proTool = new Process();
            proTool.StartInfo.FileName = "cmd.exe";
            proTool.StartInfo.UseShellExecute = false;
            proTool.StartInfo.RedirectStandardInput = true;
            proTool.StartInfo.RedirectStandardOutput = true;
            proTool.StartInfo.RedirectStandardError = true;
            proTool.StartInfo.CreateNoWindow = true;
            proTool.Start();
            proTool.StandardInput.WriteLine("netstat -ano");
            proTool.StandardInput.WriteLine("exit");

            Regex reg = new Regex("\\s+", RegexOptions.Compiled);
            string line = null;
            while ((line = proTool.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");

                    string[] arr = line.Split(',');
                    if (arr[1].EndsWith(":" + port))
                    {
                        result = Process.GetProcessById(int.Parse(arr[4]));
                        break;
                    }
                }
            }

            proTool.Close();
            return result;
        }

    }
}
