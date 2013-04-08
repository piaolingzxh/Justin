using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Extensions;
namespace Justin.FrameWork.WinForm.FormUI
{
    public partial class NotifyForm : Form
    {
        public NotifyForm()
        {
            InitializeComponent();
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            this.Opacity = 0.6;
            this.SetDesktopLocation(x, y);
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

        public void Show(string message)
        {
            richTextBox1.Text = message;
            this.ShowNotify();
            //AnimateWindow(this.Handle, 3, AW_VER_NEGATIVE | AW_ACTIVATE | AW_BLEND);//从下到上且不占其它程序焦点 
            this.Show();
            this.timer1.Enabled = true;
        }

        public new void Close()
        {
            //AnimateWindow(this.Handle, 1500, AW_VER_POSITIVE | AW_HIDE | AW_BLEND);
            //base.Close();
            //this.Close();
            base.Hide();
            richTextBox1.Clear();
            this.timer1.Enabled = false;
        }
        public new void Hide()
        {
            //ShowWindow(this.Handle, 4);
            //AnimateWindow(this.Handle, 3, AW_VER_POSITIVE | AW_HIDE | AW_BLEND);
            //this.ShowNotify();
            base.Hide();
            richTextBox1.Clear();
            this.timer1.Enabled = false;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.Close();
            this.Hide();
        }

        private void TopMostMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

    }
}
