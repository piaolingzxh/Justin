using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Models;
namespace Justin.FrameWork.WinForm.FormUI
{
    public partial class NotifyForm : Form, INotify
    {
        public NotifyForm()
        {
            InitializeComponent();
            //int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            //int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            //this.SetDesktopLocation(x, y); 
            //this.Opacity = 0.6;
            LocationPosition();
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

        }
        void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        //[DllImport("user32")]
        //public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //const int AW_HOR_POSITIVE = 0x0001;//从左到右打开窗口  
        //const int AW_HOR_NEGATIVE = 0x0002;//从右到左打开窗口 
        //const int AW_VER_POSITIVE = 0x0004;//从上到下打开窗口
        //const int AW_VER_NEGATIVE = 0x0008;//从下到上打开窗口
        //const int AW_CENTER = 0x0010;//看不出任何效果
        //const int AW_HIDE = 0x10000;//在窗体卸载时若想使用本函数就得加上此常量
        //const int AW_ACTIVATE = 0x20000;//在窗体通过本函数打开后，默认情况下会失去焦点，除非加上本常量    
        //const int AW_SLIDE = 0x40000;//看不出任何效果
        //const int AW_BLEND = 0x80000;//淡入淡出效果

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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

        #region Show

        #region 覆盖系统

        public new void Show()
        {
            //AnimateWindow(this.Handle, 3, AW_VER_NEGATIVE | AW_ACTIVATE | AW_BLEND);//从下到上且不占其它程序焦点 
            this.ShowNotify();
            this.timShow.Enabled = true;
        }
        public new void Show(IWin32Window owner)
        {
            this.Show();
        }
        public new void Close()
        {
            //AnimateWindow(this.Handle, 1500, AW_VER_POSITIVE | AW_HIDE | AW_BLEND);
            this.Hide();
        }
        public new void Hide()
        {
            //ShowWindow(this.Handle, 4);
            //AnimateWindow(this.Handle, 3, AW_VER_POSITIVE | AW_HIDE | AW_BLEND);
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

        private void timShow_Tick(object sender, EventArgs e)
        {
            if (!this.Bounds.Contains(Cursor.Position))
            {
                this.Hide();
            }
        }

        private void TopMostMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.TopMostMenuItem.Checked = this.TopMost;
        }


        private void LocationPosition()
        {
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
        }
    }
}
