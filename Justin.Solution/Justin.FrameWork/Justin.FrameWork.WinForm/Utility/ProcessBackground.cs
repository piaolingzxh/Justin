using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Justin.FrameWork.Extensions;


namespace Justin.FrameWork.WinForm.Utility
{
    public delegate void ProcessMsgReceivehandler(string msg);
    public abstract class ProcessBackground
    {
        public abstract void ExecuteCommand(string args);

        public ProcessMsgReceivehandler MsgReceivedEvent;
    }

    public class AsyncProcessBackground : ProcessBackground
    {

        private ProcessStartInfo StartInfo { get; set; }
        public AsyncProcessBackground(string fileName)
        {
            this.ShowErrorData = this.ShowOutputData = true;
            StartInfo = new ProcessStartInfo();

            StartInfo.FileName = fileName;

            StartInfo.CreateNoWindow = true;
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;

        }

        private void ProcessBackground_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.MsgReceivedEvent != null && !string.IsNullOrEmpty(e.Data))
            {
                this.MsgReceivedEvent(e.Data);
            }
        }
        private void ProcessBackground_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.MsgReceivedEvent != null && !string.IsNullOrEmpty(e.Data))
            {
                this.MsgReceivedEvent(e.Data);
            }
        }
        public bool ShowOutputData { get; set; }
        public bool ShowErrorData { get; set; }

        public override void ExecuteCommand(string args)
        {
            this.StartInfo.Arguments = args;
            Process p = new Process();// Process.Start(StartInfo);
            p.StartInfo = StartInfo;
            if (this.ShowOutputData)
                p.OutputDataReceived += ProcessBackground_OutputDataReceived;
            if (this.ShowErrorData)
                p.ErrorDataReceived += ProcessBackground_ErrorDataReceived;
            p.Start();
            if (this.ShowErrorData || this.ShowOutputData)
                p.BeginOutputReadLine();
            if (!this.ShowErrorData && !this.ShowOutputData)
                p.WaitForExit();
        }
    }

    public class SyncProcessBackground : ProcessBackground
    {

        private ProcessStartInfo StartInfo { get; set; }
        public SyncProcessBackground(string fileName)
        {
            this.ShowErrorData = this.ShowOutputData = true;
            StartInfo = new ProcessStartInfo();
            StartInfo.FileName = fileName;
            StartInfo.CreateNoWindow = true;
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;

        }

        public bool ShowOutputData { get; set; }
        public bool ShowErrorData { get; set; }


        public override void ExecuteCommand(string args)
        {
            this.StartInfo.Arguments = args;
            Process p = new Process();// Process.Start(StartInfo);
            p.StartInfo = StartInfo;

            p.Start();

            string output = p.StandardOutput.ReadToEnd().Trim().TrimEndWhiteSpaceAndNewLine();
            string errorMsg = p.StandardError.ReadToEnd().Trim().TrimEndWhiteSpaceAndNewLine();
            //if (errorMsg.StartsWith("ERROR:Could not find >"))
            //{
            //    errorMsg = "";
            //}

            if ((!String.IsNullOrEmpty(output) || !string.IsNullOrEmpty(errorMsg)) && this.MsgReceivedEvent != null)
            {
                string result = errorMsg;
                if (!string.IsNullOrEmpty(output))
                {
                    if (string.IsNullOrEmpty(errorMsg))
                    {
                        result = output;
                    }
                    else
                    {
                        result = output + Environment.NewLine + errorMsg;
                    }
                }

                if (result != Environment.NewLine)
                    this.MsgReceivedEvent(result);
            }

            p.WaitForExit();
            p.Close();
        }

    }
}
