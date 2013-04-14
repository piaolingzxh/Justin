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
    }
}
