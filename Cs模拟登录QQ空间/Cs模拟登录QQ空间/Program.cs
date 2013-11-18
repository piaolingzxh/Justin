using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cs模拟登录QQ空间
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin("a",new System.Net.CookieContainer()));
        }
    }
}
