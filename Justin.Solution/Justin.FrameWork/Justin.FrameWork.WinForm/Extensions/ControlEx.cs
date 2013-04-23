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

        public static void SetToolTipsForButton(this Control ctrl, ToolTip tips)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is Button)
                {
                    Button btn = item as Button;
                    if (btn.Tag != null)
                    {
                        tips.SetToolTip(btn, btn.Tag.ToJString());
                    }
                }
                else
                {
                    SetToolTipsForButton(item, tips);
                }
            }
        }
    }
}
