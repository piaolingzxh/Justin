using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Toolbox
{
    public delegate void ConnStrChangDelegate(string oldConnStr, string newConnStr);
    public partial class JDockForm : DockContent
    {

        public MainForm MainFormWin
        {
            get
            {
                return this.MdiParent as MainForm;
            }
        }

        public JDockForm()
        {
            InitializeComponent();
        }

        #region 功能无关

        public void Show(DockState dockState = DockState.Document)
        {
            if (this.MainFormWin.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                base.Show();
            }
            else
                base.Show(this.MainFormWin.DockPanel, dockState);
        }

        #region 关闭菜单

        private void menuItemCloseMe_Click(object sender, EventArgs e)
        {
            if (MainFormWin.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
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
            this.MainFormWin.CloseAllDocumentBut(this);
        }
        private void menuItemCLoseAll_Click(object sender, EventArgs e)
        {
            this.MainFormWin.CloseAllDocuments();
        }

        #endregion

        #endregion

        public ContextMenuStrip TopContextMenu
        {
            get
            {
                return this.contextMenuTabPage;
            }
        }
    }
}
