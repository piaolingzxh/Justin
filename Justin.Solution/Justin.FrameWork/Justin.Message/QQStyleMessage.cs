using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSharpWin;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Message
{
    public partial class QQStyleMessage : SkinForm, INotify
    {
        public QQStyleMessage()
        {
            InitializeComponent();
            //int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            //int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            //this.SetDesktopLocation(x, y);
            LocationPosition();
            this.FormClosing += new FormClosingEventHandler(QQForm_FormClosing);
        }

        void QQForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return this.labelMessage.Text; }
            set { this.labelMessage.Text = value; }
        }

        private void QQForm_Load(object sender, EventArgs e)
        {
        }

        #region Show

        #region 覆盖系统

        public new void Show()
        {
            this.ShowNotify();
            this.timShow.Enabled = true;
            this.TopMost = true;
            //this.TopLevel = true;
            //this.SetTopLevel(true);
            //this.BringToFront();
        }
        public new void Show(IWin32Window owner)
        {
            this.Show();
        }
        public new void Close()
        {
            this.Hide();
        }
        public new void Hide()
        {
            base.Hide();
            this.labelMessage.ResetText();
            this.timShow.Enabled = false;
        }

        #endregion


        public void Show(string msg, string title = "", string tips = "")
        {
            if (!string.IsNullOrEmpty(title))
                this.Title = title;
            this.Message = msg;
            tips = string.IsNullOrEmpty(tips) ? this.Message : tips;

            toolTip1.SetToolTip(this.labelMessage, tips);
            this.Show();
        }

        public void Show(string msgFormat, string detailMsg = "", params object[] msgArgs)
        {
            string message = msgArgs == null || msgArgs.Count() < 1 ? msgFormat : string.Format(msgFormat, msgArgs);

            this.Show(message, "", detailMsg);
        }

        public void Show(string msgFormat, params object[] args)
        {
            this.Show(msgFormat, "", args);
        }


        #endregion

        private void timShow_Tick(object sender, EventArgs e)
        {
            if (!this.Bounds.Contains(Cursor.Position))
            {
                this.Hide();
            }
        }

        private void LocationPosition()
        {
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
        }

        private void btnHelper_Click(object sender, EventArgs e)
        {

        }

        private void label1BottomLeft_Click(object sender, EventArgs e)
        {

        }
        public new int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
                int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
                int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
                this.SetDesktopLocation(x, y);
            }
        }
        public new int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
                int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
                int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
                this.SetDesktopLocation(x, y);
            }
        }

    }
}
