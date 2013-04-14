using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.WinForm.Helper
{
    public class FileHelper
    {
        public static void OverWrite(string fileName,string content)
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write(content);
            sw.Close();
        }
    }
}
