using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;

namespace Justin.FrameWork.WinForm.Helper
{
    public class NotifyHelper
    {
        public static INotify notify = new NotifyForm();

        public static void Set(double opacity = 0.6)
        {
            notify.Opacity = opacity;
        }

        public static void Show(string msg, string title = "")
        {
            notify.Show(msg, title);
        }
        public static void Show(string msgFormat, params object[] args)
        {
            notify.Show(msgFormat, args);
        }
        public static void Show(string msgFormat, string detailMsg = "", params object[] msgArgs)
        {
            notify.Show(msgFormat, detailMsg, msgArgs);
        }
    }
}
