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
using Justin.FrameWork.WinForm.Models;

namespace Justin.Core
{
    public delegate void ConnStrChangDelegate(string oldConnStr, string newConnStr);
    public partial class JForm : DockContent
    {

        public WorkbenchBase WorkspaceBase
        {
            get
            {
                return this.MdiParent as WorkbenchBase;
            }
        }
        public ContextMenuStrip TopContextMenu
        {
            get
            {
                return this.contextMenuTabPage;
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



        #endregion

        #endregion

        private void JDockForm_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        #region 菜单

        private ToolStripMenuItem chooseDataBaseToolStripMenuItem;
        private void chooseDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tempConnStr = DBConnectDialog.GetConnectionString(DBConnectDialog.DataSourceType.SqlDataSource);
            if (!string.IsNullOrEmpty(tempConnStr))
            {
                this.ConnStr = tempConnStr;
                this.ShowMessage("更改数据源。");
            }
        }

        private ToolStripMenuItem FormatFileToolStripMenuItem;
        private void FormatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile(this.FileName);
            JFormat.FormatFile(this.fileName);
            this.LoadFile(this.FileName);
        }
        private ToolStripMenuItem SaveFileToolStripMenuItem;
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile(this.FileName);
        }
        private ToolStripMenuItem SaveFileAsToolStripMenuItem;
        private void SaveFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.SaveFile(saveFileDialog1.FileName);
                this.FileName = saveFileDialog1.FileName;
            }

        }

        private ToolStripMenuItem closeMeToolStripMenuItem;
        private void CloseMeToolStripMenuItem_Click(object sender, EventArgs e)
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
        private ToolStripMenuItem closeOthersToolStripMenuItem;
        private void CloseOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WorkspaceBase.CloseAllDocumentBut(this);
        }
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WorkspaceBase.CloseAllDocuments();
        }

        private ToolStripMenuItem OpenFileLocationToolStripMenuItem;
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
        private ToolStripMenuItem ReloadFileToolStripMenuItem;
        private void ReloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFile(this.FileName);
        }

        private void InitMenu()
        {
            //ChooseDataSource
            if (this is IDB)
            {
                this.chooseDataBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.chooseDataBaseToolStripMenuItem.Name = "chooseDataBaseToolStripMenuItem";
                this.chooseDataBaseToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.chooseDataBaseToolStripMenuItem.Text = "ChooseDataBase";
                this.chooseDataBaseToolStripMenuItem.Click += chooseDataBaseToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.chooseDataBaseToolStripMenuItem);
            }
            //Format
            if (this.TopContextMenu.Items.Count != 0)
            {
                ToolStripSeparator file1Splitor = new ToolStripSeparator();
                this.TopContextMenu.Items.Add(file1Splitor);
            }
            if (this is IFormat)
            {
                this.FormatFileToolStripMenuItem = new ToolStripMenuItem();
                this.FormatFileToolStripMenuItem.Name = "formatFileToolStripMenuItem";
                this.FormatFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.FormatFileToolStripMenuItem.Text = "Format File";
                this.FormatFileToolStripMenuItem.Click += FormatFileToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.FormatFileToolStripMenuItem);
            }
            //Save 
            this.SaveFileToolStripMenuItem = new ToolStripMenuItem();
            this.SaveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.SaveFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.SaveFileToolStripMenuItem.Text = "Save";
            this.SaveFileToolStripMenuItem.Click += SaveFileToolStripMenuItem_Click;
            this.TopContextMenu.Items.Add(this.SaveFileToolStripMenuItem);

            //Save As
            this.SaveFileAsToolStripMenuItem = new ToolStripMenuItem();
            this.SaveFileAsToolStripMenuItem.Name = "saveFileAsToolStripMenuItem";
            this.SaveFileAsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.SaveFileAsToolStripMenuItem.Text = "Save As";
            this.SaveFileAsToolStripMenuItem.Click += SaveFileAsToolStripMenuItem_Click;
            this.TopContextMenu.Items.Add(this.SaveFileAsToolStripMenuItem);

            ToolStripSeparator closeSplitor = new ToolStripSeparator();
            this.TopContextMenu.Items.Add(closeSplitor);

            //Close Me
            this.closeMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMeToolStripMenuItem.Name = "menuItemCloseMe";
            this.closeMeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.closeMeToolStripMenuItem.Text = "Close";
            this.closeMeToolStripMenuItem.Click += new System.EventHandler(this.CloseMeToolStripMenuItem_Click);
            this.TopContextMenu.Items.Add(this.closeMeToolStripMenuItem);
            //Close Others
            this.closeOthersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeOthersToolStripMenuItem.Name = "menuItemCloseOthers";
            this.closeOthersToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.closeOthersToolStripMenuItem.Text = "Close Others";
            this.closeOthersToolStripMenuItem.Click += new System.EventHandler(this.CloseOtherToolStripMenuItem_Click);
            this.TopContextMenu.Items.Add(this.closeOthersToolStripMenuItem);
            //Close All
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem.Name = "menuItemCLoseAll";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            this.TopContextMenu.Items.Add(this.closeAllToolStripMenuItem);
            if (this is IFile)
            {
                ToolStripSeparator file2Splitor = new ToolStripSeparator();
                this.TopContextMenu.Items.Add(file2Splitor);
                //Open File Location
                this.OpenFileLocationToolStripMenuItem = new ToolStripMenuItem();
                this.OpenFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
                this.OpenFileLocationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.OpenFileLocationToolStripMenuItem.Text = "Open File Location";
                this.OpenFileLocationToolStripMenuItem.Click += OpenFileLocationToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.OpenFileLocationToolStripMenuItem);

                //Reload File
                this.ReloadFileToolStripMenuItem = new ToolStripMenuItem();
                this.ReloadFileToolStripMenuItem.Name = "reloadFileToolStripMenuItem";
                this.ReloadFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
                this.ReloadFileToolStripMenuItem.Text = "Reload File";
                this.ReloadFileToolStripMenuItem.Click += ReloadFileToolStripMenuItem_Click;
                this.TopContextMenu.Items.Add(this.ReloadFileToolStripMenuItem);
            }
        }

        #endregion

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
        public virtual void SaveFile(string fileName)
        {
            if (File.Exists(this.FileName))
            {
                File.Delete(FileName);
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = saveFileDialog1.FileName;
                }
            }
        }
        public virtual void LoadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                this.ShowMessage("文件不存在");
                return;
            }
        }


        private string connStr = "";
        public virtual string ConnStr
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
          
    }
}
