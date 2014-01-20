using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Services;

namespace System.Windows.Forms
{
    public static class UserControlEx
    {

        public static void ShowMessage(this UserControl instance, Exception ex)
        {
            //if (MessageSvc.Default.MessageReceived != null)
            //{
            //    foreach (MessageEventHandler tempEvent in MessageSvc.Default.MessageReceived.GetInvocationList())
            //    {
            //        tempEvent(null, new MessageEventArgs(MessageLevel.Error, ex.GetAllMessage()));
            //    }
            //}

            MessageSvc.Default.Write(MessageLevel.Error, ex);
        }
        public static void ShowMessage(this UserControl instance, Exception ex, string messageFormat, params object[] args)
        {
            MessageSvc.Default.Write(MessageLevel.Error, ex, messageFormat, args);
        }
        public static void ShowMessage(this UserControl instance, string messageFormat, params object[] args)
        {
            //if (MessageSvc.Default.MessageReceived != null)
            //{
            //    foreach (MessageEventHandler tempEvent in MessageSvc.Default.MessageReceived.GetInvocationList())
            //    {
            //        tempEvent(null, new MessageEventArgs(MessageLevel.Info, msg));
            //    }
            //}

            MessageSvc.Default.Write(MessageLevel.Info, messageFormat, args);
        }

        public static void ShowToolTips(this UserControl instance, ToolTip tips)
        {
            foreach (Control item in instance.Controls)
            {
                item.ShowToolTips(tips);
            }
        }
    }
}
