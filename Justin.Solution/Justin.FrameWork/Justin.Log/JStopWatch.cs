using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.Log;

namespace Justin.Log
{
    public class JStopWatch
    {
        private string Format { get; set; }
        public JStopWatch(string format = "")
        {
            this.Format = format;
        }

        public IDisposable Start(LogMode logMode, string startFormat, string endFormat, params object[] args)
        {
            if (!string.IsNullOrWhiteSpace(startFormat))
            {
                JLog.Default.Write(logMode, startFormat, args);
            }
            Stopper stopper = new Stopper(this, Format, logMode, endFormat, args);
            return stopper;
        }
        private void Stop(LogMode logMode, string endMsg, params object[] args)
        {

            JLog.Default.Write(logMode, endMsg, args);
        }

        private class Stopper : IDisposable
        {
            private string Format { get; set; }
            private DateTime _startTime;
            private string EndMsg { get; set; }
            private readonly JStopWatch _owner;
            private LogMode LogMode { get; set; }

            public Stopper(JStopWatch owner, string format, LogMode logMode, string endFormat, params object[] args)
            {

                _owner = owner;
                this.Format = format;
                this.LogMode = logMode;
                this.EndMsg = string.Format(endFormat, args);
                _startTime = DateTime.Now;
            }

            public void Dispose()
            {
                _elapsed = DateTime.Now.Subtract(_startTime);
                _owner.Stop(LogMode, this.EndMsg + ": 耗时{0}", this.ToString());
                GC.SuppressFinalize(this);
            }
            private TimeSpan _elapsed;

            public override string ToString()
            {
                switch (Format)
                {
                    case "ss": return _elapsed.TotalSeconds.ToString() + "s";
                    case "ms": return _elapsed.TotalMilliseconds.ToString() + "ms";
                    default: return _elapsed.TotalMilliseconds.ToString() + "ms";
                }
            }
        }
    }
}
