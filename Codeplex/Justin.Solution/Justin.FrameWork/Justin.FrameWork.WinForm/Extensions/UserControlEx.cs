using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Log;

namespace System.Windows.Forms
{
    public static class UserControlEx
    {
        public static void ShowMessage(this UserControl instance, Exception ex, bool native = false)
        {
            if (OutPutService.Instance.MessageReceived != null)
            {
                foreach (MessageReceivedEventHandler tempEvent in OutPutService.Instance.MessageReceived.GetInvocationList())
                {
                    tempEvent(null, new MessageReceivedEventArgs(ex, native));
                }
            }
        }

        public static void ShowMessage(this UserControl instance, string msg, string detailMsg = "", bool native = false)
        {
            if (OutPutService.Instance.MessageReceived != null)
            {
                foreach (MessageReceivedEventHandler tempEvent in OutPutService.Instance.MessageReceived.GetInvocationList())
                {
                    tempEvent(null, new MessageReceivedEventArgs(msg, detailMsg, native));
                }
            }
        }

    }
}
