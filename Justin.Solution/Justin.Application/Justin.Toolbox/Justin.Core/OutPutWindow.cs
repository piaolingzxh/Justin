using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Services;
using Justin.FrameWork.WinForm.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Core
{
    public partial class OutPutWindow : JForm
    {
        private const int msgMaxLength = 4096000;//2147483647

        private OutPutWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            if (MessageSvc.Default.MessageReceived == null)
            {
                MessageSvc.Default.MessageReceived += MessageReceived;
            }
        }
        private static OutPutWindow win = new OutPutWindow();
        public static OutPutWindow Instance { get { return win; } }


        public void ProcessMessageObj(MessageEventArgs e)
        {
            if (this.txtMessage.InvokeRequired)
            {
                Action<MessageEventArgs> d = new Action<MessageEventArgs>(ProcessMessageObj);
                this.txtMessage.Invoke(d, e);
            }
            else
            {
                if (e.Message != null && !string.IsNullOrEmpty(e.Message.Trim()))
                {
                    this.txtMessage.AppendText(e.Message);
                    this.txtMessage.AppendText(Environment.NewLine);
                    if (e.Level.Equals(MessageLevel.Error))
                    {
                        this.txtError.AppendText(e.Message);
                        this.txtError.AppendText(Environment.NewLine);
                    }
                }
            }

        }

        private void clearScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox txtBox = contextMenuStrip1.SourceControl as RichTextBox;
            if (txtBox != null)
                txtBox.Clear();

        }


        private void OutPutWindow_Load(object sender, EventArgs e)
        {
            this.txtMessage.Clear();
        }

        public void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message != null && !string.IsNullOrEmpty(e.Message.Trim()))
            {
                ProcessMessageObj(e);
            }
        }
    }
}
