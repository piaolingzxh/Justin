using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Extensions;

namespace System.Windows.Forms
{
    public static class ControlEx
    {

        public static void ShowToolTips(this Control ctrl, ToolTip tips)
        {
            foreach (Control item in ctrl.Controls)
            {
                if ((item is Button || item is TextBox) && item.Tag != null)
                {
                    tips.SetToolTip(item, item.Tag.ToJString());
                }
                else
                {
                    ShowToolTips(item, tips);
                }
            }
        }
    }
}
