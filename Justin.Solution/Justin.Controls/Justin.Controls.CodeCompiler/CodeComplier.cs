using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Utility;
using Justin.FrameWork.WinForm.Utility;

namespace Justin.Controls.CodeCompiler
{
    public delegate void ComplierMsgReceivehandler(string msg);
    

    #region 其他
    public enum NetDialect
    {
        CSharp,
        VB,
    }
    

    public enum Target
    {
        Exe,
        WinExe,
        Library,
        Module,
        AppContainerExe,
        WinmdObj,
    }
    #endregion

    public abstract class CodeComplierBase
    {
        public CodeComplierBase(bool async = false)
        {
            this.Async = async;
        }
        public CodeComplierBase(string sourceFileName, bool async = false)
        {
            this.SourceFileName = sourceFileName;
            this.Async = async;
        }
        private bool Async { get; set; }
        public string ComplieToolPath { get; set; }
        public string ExecuteToolPath { get; set; }
        public string ILDeComplieToolPath { get; set; }

        protected string SourcePath
        {
            get
            {
                return Path.GetDirectoryName(SourceFileName);
            }
        }
        public string SourceFileName { get; set; }
        protected abstract string ComplierResultFileName { get; }
        protected abstract string ILFileName { get; }
        protected abstract string GetComplieArguments();
        protected abstract string GetRunArguments();
        protected abstract string GetILArguments();


        private string processInfoFormat = @"参数：【Tool:{0}】【Arguments:{1}】";
        public string Complier()
        {
            try
            {
                ShowMessage("编译中......");
                string args = this.GetComplieArguments();
                ShowMessage(string.Format(processInfoFormat, ComplieToolPath, args));
                if (File.Exists(this.ComplierResultFileName))
                {
                    File.Delete(this.ComplierResultFileName);
                }
                ProcessBackground pbg = null;
                if (this.Async)
                {
                    pbg = new AsyncProcessBackground(ComplieToolPath);
                }
                else
                {
                    pbg = new SyncProcessBackground(ComplieToolPath);
                }
                pbg.MsgReceivedEvent = this.ShowMessage;
                //pbg.ShowErrorData = true;
                //pbg.ShowOutputData = true;
                pbg.ExecuteCommand(args);
                ShowMessage("编译结束。");

                if (this.Async)
                    Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
            return ComplierResultFileName;
        }
        public void Run()
        {
            try
            {
                Complier();

                if (File.Exists(this.ComplierResultFileName))
                {
                    ShowMessage("运行中......");
                    string exeToolPath = string.IsNullOrEmpty(this.ExecuteToolPath) ? this.ComplierResultFileName : this.ExecuteToolPath;
                    string args = this.GetRunArguments();
                    ShowMessage(string.Format(processInfoFormat, exeToolPath, args));


                    ProcessBackground pbg = null;
                    if (this.Async)
                    {
                        pbg = new AsyncProcessBackground(exeToolPath);
                    }
                    else
                    {
                        pbg = new SyncProcessBackground(exeToolPath);
                    }
                    pbg.MsgReceivedEvent = this.ShowMessage;
                    //pbg.ShowOutputData = true;
                    //pbg.ShowErrorData = true;

                    pbg.ExecuteCommand(args);
                    ShowMessage("运行结束。");
                    if (this.Async)
                        Thread.Sleep(2000);
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
        }
        public string IL()
        {
            try
            {
                Complier();
                if (File.Exists(this.ComplierResultFileName))
                {
                    ShowMessage("中间语言生成中......");
                    string args = this.GetILArguments();
                    ShowMessage(string.Format(processInfoFormat, this.ILDeComplieToolPath, args));
                    //ProcessBackground pbg = new ProcessBackground(@"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\ildasm.exe");
                    //ProcessBackground pbg = new ProcessBackground(@"d:\test\ildasm.exe");     
                    ProcessBackground pbg = null;
                    if (this.Async)
                    {
                        pbg = new AsyncProcessBackground(this.ILDeComplieToolPath);
                    }
                    else
                    {
                        pbg = new SyncProcessBackground(this.ILDeComplieToolPath);
                    }
                    pbg.MsgReceivedEvent = this.ShowMessage;
                    //pbg.ShowOutputData = true;
                    pbg.ExecuteCommand(args);
                    if (this.Async)
                        Thread.Sleep(2000);
                    ShowMessage("中间语言生成结束。");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
            return ILFileName;
        }

        protected virtual void ShowMessage(string msg)
        {
            if (this.MsgReceivedEvent != null && !string.IsNullOrEmpty(msg))
            {
                this.MsgReceivedEvent(msg);
            }
        }
        public ComplierMsgReceivehandler MsgReceivedEvent;
    }

    public class NetCodeComplier : CodeComplierBase
    {
        public NetCodeComplier(NetDialect dialect) : this(dialect, FrameworkVersion.VersionLatest, Target.Exe) { }
        public NetCodeComplier(NetDialect dialect, FrameworkVersion version, Target target)
            : base(false)
        {
            string complieFile = "";
            if (dialect == NetDialect.CSharp)
            {
                complieFile = "csc.exe";
            }
            else
            {
                complieFile = "vbc.exe";
            }
            CSCDictioncry = new Dictionary<FrameworkVersion, string>();
            CSCDictioncry.Add(FrameworkVersion.Version11, Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v1.0.3705\", complieFile));
            CSCDictioncry.Add(FrameworkVersion.Version20, Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\", complieFile));
            CSCDictioncry.Add(FrameworkVersion.Version30, Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v3.0\", complieFile));
            CSCDictioncry.Add(FrameworkVersion.Version35, Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v3.5\", complieFile));
            CSCDictioncry.Add(FrameworkVersion.Version40, Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\", complieFile));

            this.Version = version;
            this.Target = Target.Exe;
            this.ComplieToolPath = CSCDictioncry[FrameworkVersion.VersionLatest];
            this.ExecuteToolPath = "";
            this.ILDeComplieToolPath = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\ildasm.exe";
        }

        private Dictionary<FrameworkVersion, string> CSCDictioncry { get; set; }
        public FrameworkVersion Version { get; set; }
        public Target Target { get; set; }

        protected override string ComplierResultFileName
        {
            get
            {
                return Path.Combine(SourcePath, Path.GetFileNameWithoutExtension(SourceFileName) + "." + Target.ToString().ToLower());
            }
        }
        protected override string ILFileName
        {
            get
            {
                return Path.Combine(SourcePath, Path.GetFileNameWithoutExtension(SourceFileName) + "." + "il");
            }
        }
        protected override string GetComplieArguments()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" /target:{0}", Target.ToString().ToLower());
            sb.AppendFormat(" /out:{0}", this.ComplierResultFileName);
            sb.AppendFormat(" {0}", SourceFileName);

            return sb.ToString();
        }
        protected override string GetRunArguments()
        {
            return "";
        }
        protected override string GetILArguments()
        {
            return string.Format("/out:{0} {1} ", ILFileName, this.ComplierResultFileName);
        }
        protected override void ShowMessage(string msg)
        {
            if (msg.StartsWith("Microsoft", true, CultureInfo.CurrentCulture))
            {
                string temp = "All rights reserved.";
                int index = msg.LastIndexOf(temp);
                if (index > 0)
                {
                    msg = msg.Substring(index + temp.Length).TrimEndWhiteSpaceAndNewLine();
                }
            }
            base.ShowMessage(msg);
        }
    }
    public class JavaCodeComplier : CodeComplierBase
    {
        public JavaCodeComplier()
        {
            //this.ComplieToolPath = @"C:\Windows\system32\cmd.exe";
            // this.ExecuteToolPath = @"C:\Windows\system32\cmd.exe";
            //this.ILDeComplieToolPath = @"C:\Windows\system32\cmd.exe";
            this.ComplieToolPath = Path.Combine(JDKPath, "javac.exe");
            this.ExecuteToolPath = Path.Combine(JDKPath, "java.exe");
            this.ILDeComplieToolPath = Path.Combine(JDKPath, "javap.exe");

        }

        public static string JDKPath { get; set; }
        protected override string ComplierResultFileName
        {
            get
            {
                return Path.Combine(this.ClassPath, Path.GetFileNameWithoutExtension(SourceFileName) + ".class");
            }
        }
        protected override string ILFileName
        {
            get
            {
                return Path.Combine(this.ClassPath, Path.GetFileNameWithoutExtension(SourceFileName) + "." + "jil");
            }
        }
        private string ClassPath
        {
            get
            {
                return Path.Combine(this.SourcePath, @"Class\");
            }
        }
        protected override string GetComplieArguments()
        {
            DirectoryInfo classDir = new DirectoryInfo(ClassPath);
            if (!classDir.Exists)
            {
                classDir.Create();
            }
            string result = string.Format(@" -sourcepath {0} -d {1} {2}", this.SourcePath, this.ClassPath, this.SourceFileName);
            return result;
        }
        protected override string GetRunArguments()
        {
            string result = string.Format(@"-classpath {0}; {1}", this.ClassPath, Path.GetFileNameWithoutExtension(this.ComplierResultFileName));
            return result;
        }
        protected override string GetILArguments()
        {
            //if (!File.Exists(this.ILFileName))
            //{
            //    //File.AppendAllText(this.ILFileName, "");
            //    //File.Create(this.ILFileName);
            //}
            return string.Format(" -classpath {0} -c {1}", this.ClassPath, Path.GetFileNameWithoutExtension(this.ComplierResultFileName), this.ILFileName);
        }

        protected override void ShowMessage(string msg)
        {
            //if (msg.StartsWith("Microsoft", true, CultureInfo.CurrentCulture))
            //{
            //    return;
            //}
            base.ShowMessage(msg);
        }
    }
}
