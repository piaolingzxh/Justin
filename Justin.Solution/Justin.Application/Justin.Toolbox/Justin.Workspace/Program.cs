using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Log;

namespace Justin.Workspace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Workbench wk = null;
            if (args != null && args.Length > 0)
            {
                wk = new Workbench(args);
            }
            else
            {
                wk = new Workbench();
            }
            try
            {
                Application.Run(wk);
            }
            catch (Exception ex)
            {
                JLog.Default.Write(LogMode.Error, ex);
            }
        }
    }
}
