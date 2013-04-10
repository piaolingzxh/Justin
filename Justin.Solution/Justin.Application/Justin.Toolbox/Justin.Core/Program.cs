using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.Core
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WorkbenchXXX form = null;
            if (args.Length > 0)
            {
                form = new WorkbenchXXX(args[0]);
            }
            else
            {
                form = new WorkbenchXXX();
            }
            Application.Run(form);
        }
    }
}
