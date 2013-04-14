using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Log
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
    public class LogService
    {
        private LogService()
        {

        }
        private static LogService service;

        public static LogService Instance
        {
            get
            {
                if (service == null)
                {
                    service = new LogService();
                }

                return service;
            }
        }

        public MessageReceivedEventHandler MessageReceived;
    }

    public class MessageReceivedEventArgs : EventArgs
    {

        public Exception Exception { get; set; }
        public string ShortMsg { get; set; }
        public string DetailMsg { get; set; }
        public bool Native { get; set; }

        public MessageReceivedEventArgs(string shortMsg, bool native = false) : this(shortMsg, shortMsg, native) { }

        public MessageReceivedEventArgs(string shortMsg, string detailMsg, bool native = false)
        {
            this.ShortMsg = shortMsg;
            this.DetailMsg = detailMsg;
            this.Native = native;
        }
        public MessageReceivedEventArgs(Exception ex, bool native = false)
        {
            this.Exception = ex;
        }
    }
}
