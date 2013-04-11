using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Core
{
    public delegate void ConnStrChangDelegate(string oldConnStr, string newConnStr);
    public partial class JDockForm : DockContent
    {
        private ToolStripMenuItem OpenFileLocationToolStripMenuItem;
        public WorkspaceBase WorkspaceBase
        {
            get
            {
                return this.MdiParent as WorkspaceBase;
            }
        }

        public JDockForm()
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
            set {
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

                this.OpenFileLocationToolStripMenuItem = new ToolStripMenuItem();
                this.OpenFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
                this.OpenFileLocationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.OpenFileLocationToolStripMenuItem.Text = "Open File Location";
                this.OpenFileLocationToolStripMenuItem.Click += OpenFileLocationToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.OpenFileLocationToolStripMenuItem);

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
    }
}
