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
using GTP.BI.ETL.Utility;
using Justin.FrameWork.Helper;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Process p = new Process();
            //    string cmd = "sqlcmd -S \"192.168.130.111\" -U \"sa\" -P \"bisa\" -d \"gcps_DW\" -i  \"D:\\temp\\JDataGenerate\\F_GCZJ_JMYH_1W.Sql\"";
            //    //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：

            //    p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //    p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            //    p.StartInfo.FileName = "cmd.exe";           //設定程序名
            //    p.StartInfo.Arguments = "/c " + cmd;    //設定程式執行參數
            //    p.StartInfo.UseShellExecute = false;        //關閉Shell的使用
            //    p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
            //    p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
            //    p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
            //    p.StartInfo.CreateNoWindow = false;          //設置不顯示窗口

            //    p.Start();   //啟動
            //    p.BeginOutputReadLine();
            //    p.BeginErrorReadLine();
            //    //p.WaitForExit();

            //    //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令
            //    //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機

            //    //string outString = p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果
            //    //Console.WriteLine("[{0}]", outString);
            //    Console.WriteLine("OK");

            //DbConnectionStringBuilder dcb = new OleDbConnectionStringBuilder("Data Source=.;Initial Catalog=AdventureWorks;Integrated Security=True");
            //DbConnectionStringBuilder ecb = new OleDbConnectionStringBuilder("Data Source=.;Initial Catalog=AdventureWorks;Persist Security Info=True;User ID=sa;Password=sa");


            double d = 6380099472108200.0000;
            string s = d.ToString("F4");
            Console.WriteLine(s);



            Console.Read();
        }

        static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        static void p_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("over");
        }

        public void test()
        {
            string connStr = "Data Source = 192.168.130.111;Initial Catalog = gcps_dw;User Id = sa;Password = bisa;";

            string selectSQL = string.Format("select * from {0} where {1}", "D_INSTALL_METER_MODE", "LEN(SYS_KEY)<4"); ;
            DataTable table = SqlHelper.ExecuteDataTable(connStr, CommandType.Text, selectSQL, null);

            StringBuilder sb = new StringBuilder();

            foreach (var item in table.Rows.Cast<DataRow>())
            {
                sb.AppendFormat("update {0} set {1} ='{2}' where {1}='{3}'",
                  "D_INSTALL_METER_MODE",
                  "SYS_KEY",
                  MD5Encrypt.EncryptAlgorithm(item["INSTALL_METER_MODE_KEY"]),
                  item["SYS_KEY"].ToString()).AppendLine();
            }

            SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, sb.ToString(), null);
        }
    }


}
