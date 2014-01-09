using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Justin.FrameWork.Helper;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = GetProcessByPort(8894);
            Console.WriteLine("");
        }

        public static Process GetProcessByPort(int port)
        {
            Process result = null;

            Process proTool = new Process();
            proTool.StartInfo.FileName = "cmd.exe";
            proTool.StartInfo.UseShellExecute = false;
            proTool.StartInfo.RedirectStandardInput = true;
            proTool.StartInfo.RedirectStandardOutput = true;
            proTool.StartInfo.RedirectStandardError = true;
            proTool.StartInfo.CreateNoWindow = true;
            proTool.Start();
            proTool.StandardInput.WriteLine("netstat -ano");
            proTool.StandardInput.WriteLine("exit");

            Regex reg = new Regex("\\s+", RegexOptions.Compiled);
            string line = null;
            while ((line = proTool.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");

                    string[] arr = line.Split(',');
                    if (arr[1].EndsWith(":" + port))
                    {
                        result = Process.GetProcessById(int.Parse(arr[4]));
                        break;
                    }
                }
            }

            proTool.Close();
            return result;
        }
    }
}
