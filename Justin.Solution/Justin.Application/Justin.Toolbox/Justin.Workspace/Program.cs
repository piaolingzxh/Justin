using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Log;
using Justin.FrameWork.Services;
using Justin.Core;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Extensions;
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
                bool enableFileLog = JSetting.ReadAppSetting("EnableFileLog").Value<bool>();
                if (enableFileLog)
                    MessageSvc.Default.MessageReceived += MessageReceived;

                MessageSvc.Default.MessageReceived += OutPutWindow.Instance.MessageReceived;

                Application.Run(wk);
            }
            catch (Exception ex)
            {
                JLog.Default.Write(LogMode.Error, ex);
            }
        }

        public static void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message != null && !string.IsNullOrEmpty(e.Message.Trim()))
            {
                JLog.Default.Write(LogMode.Info, e.Message);
            }
        }

    }
}
