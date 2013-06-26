using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Log;

namespace System.Windows.Forms
{
    public static class FormEx
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// 不抢夺当前焦点
        /// </summary>
        /// <param name="form"></param>
        public static void ShowNotify(this Form form)
        {
            ShowWindow(form.Handle, 4);
        }

        public static void ShowMessage(this Form instance, Exception ex, bool native = false)
        {
            if (LogService.Instance.MessageReceived != null)
            {
                foreach (MessageReceivedEventHandler tempEvent in LogService.Instance.MessageReceived.GetInvocationList())
                {
                    tempEvent(null, new MessageReceivedEventArgs(ex));
                }
            }
        }

        public static void ShowMessage(this Form instance, string msg, string detailMsg = "", bool native = false)
        {
            if (LogService.Instance.MessageReceived != null)
            {
                foreach (MessageReceivedEventHandler tempEvent in LogService.Instance.MessageReceived.GetInvocationList())
                {
                    tempEvent(null, new MessageReceivedEventArgs(msg));
                }
            }
        }

        public static void ShowTips(this Form instance, ToolTip tips)
        {
            foreach (Control item in instance.Controls)
            {
                item.SetToolTipsForButton(tips);
            }
        }

        public static void Shake(this Form instance)
        {
            int recordx = instance.Left;             //保存原来窗体的左上角的x坐标
            int recordy = instance.Top;              //保存原来窗体的左上角的y坐标
            int rand = 10;
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(rand);
                int y = random.Next(rand);
                if (x % 2 == 0)
                {
                    instance.Left = instance.Left + x;
                }
                else
                {
                    instance.Left = instance.Left - x;
                }
                if (y % 2 == 0)
                {
                    instance.Top = instance.Top + y;
                }
                else
                {
                    instance.Top = instance.Top - y;
                }

                instance.Left = recordx;             //还原原始窗体的左上角的x坐标
                instance.Top = recordy;              //还原原始窗体的左上角的y坐标
            }

        }
    }
}
