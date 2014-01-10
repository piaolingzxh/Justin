using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Services;

namespace System.Windows.Forms
{
    public static class FormEx
    {

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

        public static void ShowMessage(this Form instance, Exception ex)
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
        public static void ShowMessage(this Form instance, Exception ex, string messageFormat, params object[] args)
        {
            MessageSvc.Default.Write(MessageLevel.Error, ex, messageFormat, args);
        }
        public static void ShowMessage(this Form instance, string messageFormat, params object[] args)
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
