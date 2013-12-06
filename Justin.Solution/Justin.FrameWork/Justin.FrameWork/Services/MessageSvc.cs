using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Services
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);
    public class MessageSvc
    {
        private static MessageSvc service;

        public static MessageSvc Instance
        {
            get
            {
                if (service == null)
                {
                    service = new MessageSvc();
                }

                return service;
            }
        }

        public MessageEventHandler MessageReceived;


        public static void Write(MessageLevel level, Exception ex)
        {
            if (Instance.MessageReceived == null) return;

            MessageEventArgs msgArgs = new MessageEventArgs(level, ex.ToString());
            Instance.MessageReceived(null, msgArgs);
        }
        public static void Write(MessageLevel level, Exception ex, string messageFormat, params object[] args)
        {
            if (Instance.MessageReceived == null) return;

            string exMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
            string msg = args == null || args.Count() < 1 ? messageFormat : string.Format(messageFormat, args);

            MessageEventArgs msgArgs = new MessageEventArgs(level, msg + string.Format("\r\n异常信息：{0}", exMessage));
            Instance.MessageReceived(null, msgArgs);

        }
        public static void Write(MessageLevel level, string messageFormat, params object[] args)
        {
            if (Instance.MessageReceived == null) return;

            string message = args == null || args.Count() < 1 ? messageFormat : string.Format(messageFormat, args);
            MessageEventArgs msgArgs = new MessageEventArgs(level, message);
            Instance.MessageReceived(null, msgArgs);

        }
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageLevel Level { get; set; }
        public string Message { get; set; }


        public MessageEventArgs(MessageLevel level, string messageFormat)
        {
            this.Level = level;
            this.Message = messageFormat;
        }
    }

    public enum MessageLevel
    {
        Debug = 0x10,
        Info = 0x100,
        Warn = 0x1000,
        Error = 0x10000,
        Fatal = 0x100000,
    }
}
