
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Justin.Message;

namespace Justin.Message
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
            //Application.Run(new FrmLogin());

            Application.Run(new FormMain());

        }
    }
}
