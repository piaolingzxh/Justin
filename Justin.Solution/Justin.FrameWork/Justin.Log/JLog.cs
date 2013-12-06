using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Justin.Log
{
    public class JLog
    {
        private Logger Instance { get; set; }

        private JLog(Logger logger)
        {
            this.Instance = logger;
        }
        private JLog(string logName)
        {
            this.Instance = LogManager.GetLogger(logName);
        }

        private static Dictionary<string, JLog> Loggers = new Dictionary<string, JLog>();
        private static JLog _defaultLog = new JLog("default");
        public static JLog Default
        {
            get
            {
                return _defaultLog;
            }
        }
        public static JLog GetLogger(string loggerName)
        {
            if (!Loggers.ContainsKey(loggerName.ToLower()))
            {
                Loggers.Add(loggerName.ToLower(), new JLog(loggerName));
            }

            return Loggers[loggerName.ToLower()];
        }

        public void Write(LogMode logMode, Exception ex)
        {
            if ((LogMode.Fatal & logMode) == LogMode.Fatal && Instance.IsFatalEnabled)
                Instance.Fatal(ex.ToString());

            if ((LogMode.Error & logMode) == LogMode.Error && Instance.IsErrorEnabled)
                Instance.Error(ex);

            if ((LogMode.Warn & logMode) == LogMode.Warn && Instance.IsWarnEnabled)
                Instance.Warn(ex.ToString());

            if ((LogMode.Info & logMode) == LogMode.Info && Instance.IsInfoEnabled)
                Instance.Info(ex.ToString());

            if ((LogMode.Debug & logMode) == LogMode.Debug && Instance.IsDebugEnabled)
                Instance.Debug(ex.ToString());

        }
        public void Write(LogMode logMode, Exception ex, string messageFormat, params object[] args)
        {
            string exMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
            string msg = args == null || args.Count() < 1 ? messageFormat : string.Format(messageFormat, args);
            Write(logMode, msg + string.Format("\r\n异常信息：{0}", exMessage));
        }

        public void Write(LogMode logMode, string messageFormat, params object[] args)
        {
            string message = args == null || args.Count() < 1 ? messageFormat : string.Format(messageFormat, args);

            if ((LogMode.Fatal & logMode) == LogMode.Fatal && Instance.IsFatalEnabled)
                Instance.Fatal(message);

            if ((LogMode.Error & logMode) == LogMode.Error && Instance.IsErrorEnabled)
                Instance.Error(message);

            if ((LogMode.Warn & logMode) == LogMode.Warn && Instance.IsWarnEnabled)
                Instance.Warn(message);

            if ((LogMode.Info & logMode) == LogMode.Info && Instance.IsInfoEnabled)
                Instance.Info(message);

            if ((LogMode.Debug & logMode) == LogMode.Debug && Instance.IsDebugEnabled)
                Instance.Debug(message);

        }
        public void Write(LogMode logMode, long elapsedMilliseconds, bool showSecondFormat, string messageFormat, params object[] args)
        {
            string message = args == null || args.Count() < 1 ? messageFormat : string.Format(messageFormat, args);
            if (showSecondFormat)
            {
                message += string.Format(" 耗时:{0}毫秒", elapsedMilliseconds);
            }
            else
            {
                message += string.Format(" 耗时:{0}秒", elapsedMilliseconds / 1000);
            }

            if ((LogMode.Fatal & logMode) == LogMode.Fatal && Instance.IsFatalEnabled)
                Instance.Fatal(message);

            if ((LogMode.Error & logMode) == LogMode.Error && Instance.IsErrorEnabled)
                Instance.Error(message);

            if ((LogMode.Warn & logMode) == LogMode.Warn && Instance.IsWarnEnabled)
                Instance.Warn(message);

            if ((LogMode.Info & logMode) == LogMode.Info && Instance.IsInfoEnabled)
                Instance.Info(message);

            if ((LogMode.Debug & logMode) == LogMode.Debug && Instance.IsDebugEnabled)
                Instance.Debug(message);

        }
    }
    [Flags]
    public enum LogMode
    {
        Debug = 0x10,
        Info = 0x100,
        Warn = 0x1000,
        Error = 0x10000,
        Fatal = 0x100000,
    }

}
