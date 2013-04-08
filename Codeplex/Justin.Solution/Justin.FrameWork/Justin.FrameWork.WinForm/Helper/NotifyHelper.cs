using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.FrameWork.WinForm.FormUI;

namespace Justin.FrameWork.WinForm.Helper
{
    public class NotifyHelper
    {
        public static NotifyForm CurrentForm = new NotifyForm();

        public static void Set(int width = 0, int height = 0, double opacity = 0.6)
        {
            if (width > 0)
                CurrentForm.Width = width;
            if (height > 0)
                CurrentForm.Height = height;
            CurrentForm.Opacity = opacity;
        }
        public static void Show(string message)
        {
            CurrentForm.Show(message);
        }
        public static void Show(string format, params object[] args)
        {
            CurrentForm.Show(string.Format(format, args));
        }
        public static void Show(int width, int height, string format, params object[] args)
        {
            CurrentForm.Width = width;
            CurrentForm.Height = height;
            CurrentForm.Show(string.Format(format, args));
        }
    }
}
