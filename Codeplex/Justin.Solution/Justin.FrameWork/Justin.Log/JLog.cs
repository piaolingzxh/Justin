using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Justin.Log
{

    public class JLog
    {
        private static volatile NLog.Logger nLogInstance;
        private static object syncObject = new Object();
        private static object writeObject = new object();
        private JLog() { }
        private static NLog.Logger Log
        {
            get
            {
                if (nLogInstance == null)
                {
                    lock (syncObject)
                    {
                        if (nLogInstance == null)
                        {
                            nLogInstance = LogManager.GetCurrentClassLogger();
                        }
                    }
                }
                return nLogInstance;
            }
        }

        public static void Write(LogMode logMode, Exception ex)
        {
            lock (writeObject)
            {
                if ((LogMode.Fatal & logMode) == LogMode.Fatal && Log.IsFatalEnabled)
                    Log.Fatal(ex.ToString());

                if ((LogMode.Error & logMode) == LogMode.Error && Log.IsErrorEnabled)
                    Log.Error(ex.ToString());

                if ((LogMode.Warn & logMode) == LogMode.Warn && Log.IsWarnEnabled)
                    Log.Warn(ex.ToString());

                if ((LogMode.Info & logMode) == LogMode.Info && Log.IsInfoEnabled)
                    Log.Info(ex.ToString());

                if ((LogMode.Debug & logMode) == LogMode.Debug && Log.IsDebugEnabled)
                    Log.Debug(ex.ToString());
                if ((LogMode.Data & logMode) == LogMode.Data && IsDataEnabled)
                    Log.Debug(ex.ToString());

            }
        }
        public static void Write(LogMode logMode, Exception ex, string messageFormat, params object[] args)
        {
            string exMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
            Write(logMode, string.Format(messageFormat, args) + string.Format("\r\n异常信息：{0}", exMessage));
        }

        public static void Write(LogMode logMode, string messageFormat, params object[] args)
        {
            string message = string.Format(messageFormat, args);
            lock (writeObject)
            {
                if ((LogMode.Fatal & logMode) == LogMode.Fatal && Log.IsFatalEnabled)
                    Log.Fatal(message);

                if ((LogMode.Error & logMode) == LogMode.Error && Log.IsErrorEnabled)
                    Log.Error(message);

                if ((LogMode.Warn & logMode) == LogMode.Warn && Log.IsWarnEnabled)
                    Log.Warn(message);

                if ((LogMode.Info & logMode) == LogMode.Info && Log.IsInfoEnabled)
                    Log.Info(message);

                if ((LogMode.Debug & logMode) == LogMode.Debug && Log.IsDebugEnabled)
                    Log.Debug(message);

                if ((LogMode.Data & logMode) == LogMode.Data && IsDataEnabled)
                    Log.Debug(message);

            }
        }
        public static void Write(LogMode logMode, long elapsedMilliseconds, bool showSecondFormat, string messageFormat, params object[] args)
        {
            string message = string.Format(messageFormat, args);
            if (showSecondFormat)
            {
                message += string.Format(" 耗时:{0}毫秒", elapsedMilliseconds);
            }
            else
            {
                message += string.Format(" 耗时:{0}秒", elapsedMilliseconds / 1000);
            }
            lock (writeObject)
            {
                if ((LogMode.Fatal & logMode) == LogMode.Fatal && Log.IsFatalEnabled)
                    Log.Fatal(message);

                if ((LogMode.Error & logMode) == LogMode.Error && Log.IsErrorEnabled)
                    Log.Error(message);

                if ((LogMode.Warn & logMode) == LogMode.Warn && Log.IsWarnEnabled)
                    Log.Warn(message);

                if ((LogMode.Info & logMode) == LogMode.Info && Log.IsInfoEnabled)
                    Log.Info(message);

                if ((LogMode.Debug & logMode) == LogMode.Debug && Log.IsDebugEnabled)
                    Log.Debug(message);

                if ((LogMode.Data & logMode) == LogMode.Data && IsDataEnabled)
                    Log.Debug(message);

            }
        }
        private static bool isDataEnabled = false;
        public static bool IsDataEnabled
        {
            get
            {
                return isDataEnabled;
            }
        }

    }

    /// <summary>
    /// 日志级别
    /// </summary>
    [Flags]
    public enum LogMode
    {
        Debug = 0x10,
        Info = 0x100,
        Warn = 0x1000,
        Error = 0x10000,
        Fatal = 0x100000,
        Data = 0x100001,
    }

    #region 注释
     //public class JustinLog
    //{
        //private static volatile NLog.Logger nLogInstance;
        //private static volatile log4net.ILog log4Instance;
        //private static object syncObject = new Object();
        //public static int logType = 0;
        //private static object writeObject = new object();
        //private JustinLog() { }
        //private static NLog.Logger NLog
        //{
        //    get
        //    {
        //        if (nLogInstance == null)
        //        {
        //            lock (syncObject)
        //            {
        //                if (nLogInstance == null)
        //                {
        //                    nLogInstance = LogManager.GetCurrentClassLogger();
        //                }
        //            }
        //        }
        //        return nLogInstance;
        //    }
        //}
        //private static log4net.ILog Log4
        //{
        //    get
        //    {
        //        if (log4Instance == null)
        //        {
        //            lock (syncObject)
        //            {
        //                if (log4Instance == null)
        //                {
        //                    log4Instance = log4net.LogManager.GetLogger("BenzLog");
        //                }
        //            }
        //        }
        //        return log4Instance;
        //    }
        //}

        //public static void Write(Exception ex, LogMode logMode)
        //{
        //    lock (writeObject)
        //    {
        //        if (logType == 0)
        //        {
        //            if ((LogMode.Fatal & logMode) == LogMode.Fatal && JustinLog.NLog.IsFatalEnabled)
        //                JustinLog.NLog.Fatal(ex.ToString());

        //            if ((LogMode.Error & logMode) == LogMode.Error && JustinLog.NLog.IsErrorEnabled)
        //                JustinLog.NLog.Error(ex);

        //            if ((LogMode.Warn & logMode) == LogMode.Warn && JustinLog.NLog.IsWarnEnabled)
        //                JustinLog.NLog.Warn(ex.ToString());

        //            if ((LogMode.Info & logMode) == LogMode.Info && JustinLog.NLog.IsInfoEnabled)
        //                JustinLog.NLog.Info(ex.ToString());

        //            if ((LogMode.Debug & logMode) == LogMode.Debug && JustinLog.NLog.IsDebugEnabled)
        //                JustinLog.NLog.Debug(ex.ToString());
        //            if ((LogMode.ScenarioData & logMode) == LogMode.ScenarioData && JustinLog.IsDataEnabled)
        //                JustinLog.NLog.Debug(ex.ToString());
        //        }
        //        else
        //        {
        //            if ((LogMode.Fatal & logMode) == LogMode.Fatal && JustinLog.Log4.IsFatalEnabled)
        //                JustinLog.Log4.FatalFormat("{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);

        //            if ((LogMode.Error & logMode) == LogMode.Error && JustinLog.Log4.IsErrorEnabled)
        //                JustinLog.Log4.ErrorFormat("{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);

        //            if ((LogMode.Warn & logMode) == LogMode.Warn && JustinLog.Log4.IsWarnEnabled)
        //                JustinLog.Log4.WarnFormat("{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);

        //            if ((LogMode.Info & logMode) == LogMode.Info && JustinLog.Log4.IsInfoEnabled)
        //                JustinLog.Log4.InfoFormat("{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);

        //            if ((LogMode.Debug & logMode) == LogMode.Debug && JustinLog.Log4.IsDebugEnabled)
        //                JustinLog.Log4.DebugFormat("{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);
        //        }
        //    }
        //}
        //public static void Write(string methrodName, Exception ex, LogMode logMode)
        //{
        //    string exMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
        //    Write(string.Format("{0} Exception,Details:{1}", methrodName, exMessage), logMode); ;
        //}
        //public static void Write(string methrodName, long elapsedMilliseconds, LogMode logMode)
        //{
        //    Write(string.Format("{0},Elapsed:{1}ms", methrodName, elapsedMilliseconds), logMode);
        //}
        //public static void Write(string message, LogMode logMode)
        //{
        //    lock (writeObject)
        //    {
        //        if (logType == 0)
        //        {
        //            if ((LogMode.Fatal & logMode) == LogMode.Fatal && JustinLog.NLog.IsFatalEnabled)
        //                JustinLog.NLog.Fatal(message);

        //            if ((LogMode.Error & logMode) == LogMode.Error && JustinLog.NLog.IsErrorEnabled)
        //                JustinLog.NLog.Error(message);

        //            if ((LogMode.Warn & logMode) == LogMode.Warn && JustinLog.NLog.IsWarnEnabled)
        //                JustinLog.NLog.Warn(message);

        //            if ((LogMode.Info & logMode) == LogMode.Info && JustinLog.NLog.IsInfoEnabled)
        //                JustinLog.NLog.Info(message);

        //            if ((LogMode.Debug & logMode) == LogMode.Debug && JustinLog.NLog.IsDebugEnabled)
        //                JustinLog.NLog.Debug(message);

        //            if ((LogMode.ScenarioData & logMode) == LogMode.ScenarioData && JustinLog.IsDataEnabled)
        //                JustinLog.NLog.Debug(message);
        //        }
        //        else
        //        {
        //            if ((LogMode.Fatal & logMode) == LogMode.Fatal && JustinLog.Log4.IsFatalEnabled)
        //                JustinLog.Log4.FatalFormat("{0}\r\n ", message);

        //            if ((LogMode.Error & logMode) == LogMode.Error && JustinLog.Log4.IsErrorEnabled)
        //                JustinLog.Log4.ErrorFormat("{0}\r\n ", message);

        //            if ((LogMode.Warn & logMode) == LogMode.Warn && JustinLog.Log4.IsWarnEnabled)
        //                JustinLog.Log4.WarnFormat("{0}\r\n ", message);

        //            if ((LogMode.Info & logMode) == LogMode.Info && JustinLog.Log4.IsInfoEnabled)
        //                JustinLog.Log4.InfoFormat("{0}\r\n ", message);

        //            if ((LogMode.Debug & logMode) == LogMode.Debug && JustinLog.Log4.IsDebugEnabled)
        //                JustinLog.Log4.DebugFormat("{0}\r\n ", message);
        //        }
        //    }
        //}
        //private static bool? isDataEnabled;
        //public static bool IsDataEnabled
        //{
        //    get
        //    {
        //        if (!isDataEnabled.HasValue)
        //        {
        //            return isDataEnabled.Value;
        //        }
        //        else
        //            return false;
        //    }
        //    set
        //    {
        //        isDataEnabled = value;
        //    }
        //}

    //}
    #endregion
}
