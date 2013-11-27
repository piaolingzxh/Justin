using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using SatanShortcutDemo.Utility;

namespace Justin.FormTest
{
    public partial class Form1 : Form
    {
        private VirtualDeskTopHelper vdtHelper;

        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

        }


        void Application_ApplicationExit(object sender,EventArgs e)
        {
            //缷载系统热键
            vdtHelper.UnRegisterHotKey();
            //恢复显示所有虚拟桌面组内的窗体
            vdtHelper.Dispose();
        }
        private void buttonDeskIndex_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            int targetGroupIndex = Int32.Parse(clickedBtn.Text.ToString()) - 1;

            vdtHelper.SwitchGroup(targetGroupIndex);
       
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //vdtHelper = new VirtualDeskTopHelper(this.Handle);

            //IntPtr hwnd = new WindowInteropHelper(this).Handle;
            //if (hwnd != null && hwnd != IntPtr.Zero)
            //{
            //    HwndSource m_source = HwndSource.FromHwnd(hwnd);
            //    m_source.AddHook(new HwndSourceHook(MessageHookHandler));
            //}
            ////注册系统热键
            //vdtHelper.RegisterHotKey(VirtualDeskTopHelper.ModifyKeys.Ctrl, VirtualDeskTopHelper.HotKey.D1ToD9);
       
        }
        public IntPtr MessageHookHandler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //window消息定义的注册的按键消息
            if (msg.Equals(0x0312))
            {
                int pc = wParam.ToInt32() - VirtualDeskTopHelper.HotKeyID;
                if (pc >= 0 && pc < 9)
                {
                    vdtHelper.SwitchGroup(pc);
                }
                else if (pc == -1)
                {
                    this.Visible = !this.Visible;
                }
            }
            return IntPtr.Zero;
        }
    
    }
}
