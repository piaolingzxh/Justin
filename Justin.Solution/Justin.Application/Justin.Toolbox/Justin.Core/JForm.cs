using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Core
{
    public delegate void ConnStrChangDelegate(string oldConnStr, string newConnStr);
    public partial class JForm : DockContent
    {
        private ToolStripMenuItem OpenFileLocationToolStripMenuItem;
        private ToolStripMenuItem SaveFileToolStripMenuItem;
        private ToolStripMenuItem ReloadFileToolStripMenuItem;
        private ToolStripMenuItem FormatFileToolStripMenuItem;



        public WorkbenchBase WorkspaceBase
        {
            get
            {
                return this.MdiParent as WorkbenchBase;
            }
        }

        public JForm()
        {
            InitializeComponent();
        }

        #region 功能无关

        public void Show(DockState dockState = DockState.Document)
        {
            if (this.WorkspaceBase.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                base.Show();
            }
            else
                base.Show(this.WorkspaceBase.DockPanel, dockState);
        }

        #region 关闭菜单

        private void menuItemCloseMe_Click(object sender, EventArgs e)
        {
            if (WorkspaceBase.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                this.Close();
            }
            else
            {
                this.DockHandler.Close();
            }
        }
        private void menuItemCloseOthers_Click(object sender, EventArgs e)
        {
            this.WorkspaceBase.CloseAllDocumentBut(this);
        }
        private void menuItemCLoseAll_Click(object sender, EventArgs e)
        {
            this.WorkspaceBase.CloseAllDocuments();
        }

        #endregion

        #endregion

        protected virtual bool IsFile { get { return false; } }
        private string fileName;
        protected virtual string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                if (!string.IsNullOrEmpty(this.fileName))
                {
                    this.Text = Path.GetFileName(this.fileName);
                }
            }
        }

        public ContextMenuStrip TopContextMenu
        {
            get
            {
                return this.contextMenuTabPage;
            }
        }

        private void JDockForm_Load(object sender, EventArgs e)
        {
            if (IsFile)
            {
                ToolStripSeparator splitor = new ToolStripSeparator();
                this.TopContextMenu.Items.Add(splitor);
                this.OpenFileLocationToolStripMenuItem = new ToolStripMenuItem();
                this.OpenFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
                this.OpenFileLocationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.OpenFileLocationToolStripMenuItem.Text = "Open File Location";
                this.OpenFileLocationToolStripMenuItem.Click += OpenFileLocationToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.OpenFileLocationToolStripMenuItem);

                this.ReloadFileToolStripMenuItem = new ToolStripMenuItem();
                this.ReloadFileToolStripMenuItem.Name = "reloadFileToolStripMenuItem";
                this.ReloadFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.ReloadFileToolStripMenuItem.Text = "Reload File";
                this.ReloadFileToolStripMenuItem.Click += ReloadFileToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.ReloadFileToolStripMenuItem);

                this.FormatFileToolStripMenuItem = new ToolStripMenuItem();
                this.FormatFileToolStripMenuItem.Name = "formatFileToolStripMenuItem";
                this.FormatFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.FormatFileToolStripMenuItem.Text = "Format File";
                this.FormatFileToolStripMenuItem.Click += FormatFileToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.FormatFileToolStripMenuItem);

                this.SaveFileToolStripMenuItem = new ToolStripMenuItem();
                this.SaveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
                this.SaveFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.SaveFileToolStripMenuItem.Text = "Save";
                this.SaveFileToolStripMenuItem.Click += SaveFileToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.SaveFileToolStripMenuItem);

            }
        }

        private void OpenFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FileName) || !File.Exists(this.FileName))
            {
                this.ShowMessage(string.Format("文件【{0}】不存在", this.FileName));
            }
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + this.FileName;
            System.Diagnostics.Process.Start(psi);
        }

        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void ReloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ReloadFile();
        }
        private void FormatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormatFile();
        }
        private void FormatFile()
        {
            ProcessBackground pbg = new SyncProcessBackground(Path.Combine(Application.StartupPath, "AStyle.exe"));
            string args = string.Format("--style=allman -N {0}", this.fileName);
            pbg.MsgReceivedEvent += this.DisplayMessage;
            pbg.ExecuteCommand(args);

            this.ReloadFile();
        }

        public void DisplayMessage(string msg)
        {
            this.ShowMessage(msg);
        }

        protected virtual void Save()
        {
            if (File.Exists(this.fileName))
            {
                File.Delete(fileName);
            }
        }
        protected virtual void ReloadFile()
        {
            if (!File.Exists(this.fileName))
            {
                this.ShowMessage("文件不存在");
                return;
            }
        }
    }
}
