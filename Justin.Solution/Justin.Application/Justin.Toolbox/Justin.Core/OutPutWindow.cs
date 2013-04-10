using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Utility;
using Justin.Log;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Core
{
    public partial class OutPutWindow : JDockForm
    {
        private const int msgMaxLength = 4096000;//2147483647

        public OutPutWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            if (OutPutService.Instance.MessageReceived == null)
            {
                OutPutService.Instance.MessageReceived += MessageReceived;
            }
        }
        public delegate void AppendTextCallback(MessageReceivedEventArgs e);

        public void ProcessMessageObj(MessageReceivedEventArgs e)
        {
            if (this.txtMessage.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(ProcessMessageObj);
                this.txtMessage.Invoke(d, e);
            }
            else
            {
                if (e.Exception != null)
                {
                    ShowMessage(e.Exception, e.Native);
                }
                else
                {
                    ShowMessage(e.ShortMsg, e.DetailMsg, e.Native);
                }
            }

        }

        private void OutPutWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void clearScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtMessage.Clear();
        }


        private void OutPutWindow_Load(object sender, EventArgs e)
        {
            this.txtMessage.Clear();
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.Exception != null)
            {
                ProcessMessageObj(e);
            }
            else
            {
                ProcessMessageObj(e);
            }
        }

        protected void ShowMessage(string shortMsg, string detailMsg = "", bool native = false)
        {
            string msg = !string.IsNullOrEmpty(detailMsg) ? detailMsg : shortMsg;
            this.txtMessage.AppendText(msg);
            this.txtMessage.AppendText(Environment.NewLine);
        }

        public void ShowMessage(Exception ex, bool native)
        {
            StringBuilder sb = new StringBuilder();
            Exception tempex = ex;
            sb.AppendLine(ex.ToString());
            while (ex.InnerException != null)
            {
                sb.AppendLine(ex.InnerException.Message);
                ex = ex.InnerException;
            }
            ShowMessage(ex.Message, sb.ToString(), native);
        }
    }
}
