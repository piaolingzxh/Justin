using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Justin.FrameWork.Services;

namespace Justin.Core
{
    public partial class WorkbenchBase : Form
    {
        public WorkbenchBase()
        {
            InitializeComponent();
        }

        public OutPutWindow OutPutWin;
        public virtual DockPanel DockPanel { get { return null; } }

        #region 关闭子窗体

        //关闭窗体 (不关闭OutPutWindow)   
        public void CloseAllDocumentBut(JForm exceptForm)
        {
            if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form != exceptForm && !(form is OutPutWindow))
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in DockPanel.DocumentsToArray())
                {
                    if (document != exceptForm && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        public void CloseAllDocumentButCurrent()
        {
            if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi && !(form is OutPutWindow))
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in DockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        public void CloseAllDocuments()
        {
            if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (!(form is OutPutWindow))
                    {
                        form.Close();
                    }
                }
            }
            else
            {
                for (int index = DockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (DockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)DockPanel.Contents[index];
                        if (!(content is OutPutWindow))
                        {
                            content.DockHandler.Close();
                        }
                    }
                }
            }
        }
        public void CloseCurrent()
        {
            if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi && !(ActiveMdiChild is OutPutWindow))
            {
                ActiveMdiChild.Close();
            }
            else
            {
                foreach (IDockContent document in DockPanel.DocumentsToArray())
                {
                    if (document.DockHandler.IsActivated && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        protected void CloseAllContents()
        {
            OutPutWin.DockPanel = null;
            CloseAllDocuments();
        }


        #endregion

    }
}
