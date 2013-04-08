using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Justin.Stock.Service.Models;

namespace Justin.Stock.Extensions
{
    public static class Extensions
    {
        public static void SetText(this NotifyIcon ni, string text)
        {
            //  if (text.Length >= 128) throw new ArgumentOutOfRangeException("Text limited to 127 characters");
            Type t = typeof(NotifyIcon);
            BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden).SetValue(ni, text);
            if ((bool)t.GetField("added", hidden).GetValue(ni))
            {
                t.GetMethod("UpdateIcon", hidden).Invoke(ni, new object[] { true });
            }
        }
    }

}
