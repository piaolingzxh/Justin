using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Message
{
    public partial class NormalForm : Form, INotify
    {
        public NormalForm()
        {
            InitializeComponent();
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            this.SetDesktopLocation(x, y);
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return this.richTextBox1.Text; }
            set { this.richTextBox1.Text = value; }
        }

        private void QQForm_Load(object sender, EventArgs e)
        {
        }

        #region Show

        #region 覆盖系统

        //public new void Show()
        //{
        //    //NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
        //    this.ShowNotify();
        //    base.Show();

        //}
        //public new void Show(IWin32Window owner)
        //{
        //    //NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
        //    this.ShowNotify();
        //    base.Show(owner);            
        //}

        #endregion

        public void Show(string msg)
        {
            this.Message = msg;
            this.ShowNotify();
            //this.Show();
        }
        public void Show(string msg, string title = "")
        {
            if (!string.IsNullOrEmpty(title))
                this.Title = title;
            this.Show(msg);
        }

        public void Show(string msgFormat, params object[] args)
        {
            string message = args == null || args.Count() < 1 ? msgFormat : string.Format(msgFormat, args);

            this.Show(message);
        }

        #endregion
        public void Show(string msgFormat, string detailMsg = "", params object[] msgArgs)
        {

        }
        private void timShow_Tick(object sender, EventArgs e)
        {
            //鼠标不在窗体内时
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
