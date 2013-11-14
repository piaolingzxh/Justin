using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Settings
{
    public class Constants
    {
        //public static string ConfigFileFolder = ConfigurationManager.AppSettings["ConfigFileFolder"];
        //public static string OuputSQLFileFolder = ConfigurationManager.AppSettings["OuputSQLFileFolder"];

        public static string NotImplemented = "尚在开发中！";
        public const string SQLParagraphStartFlag = "--[->";
        public const string SQLParagraphEndFlag = "--<-]";
        public static int SqlBufferSize = 250000;
        public static int SqlLineSize = 5000;
        public const string OpenFileDialogFilterFormart = "{1}文件(*.{0})|*.{0}|";
        public const string Splitor = "↑";

        public const string CDataStartTag = "<![CDATA[";
        public const string CDataEndTag = "]]>";

    }
}
