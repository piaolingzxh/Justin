using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Core;

namespace Justin.Toolbox
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
            WorkspaceBaseXXXXXXX form;
            if (args.Length > 0)
            {
                form = new WorkspaceBaseXXXXXXX(args);
            }
            else
            {
                form = new WorkspaceBaseXXXXXXX();
            }

            Application.Run(form);
        }
    }
}
