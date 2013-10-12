using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "刘燕子";
            string s = @"\u5218\u71d5\u5b50";
            string s1 = @"521871d55b50";
          //  Console.WriteLine(Encoding.Unicode.GetString(.)); 

            Console.WriteLine(UnicodeToString(s1));
            Console.Read();
        }
        private static string UnicodeToString(string inputs)
        {
            StringBuilder sb = new StringBuilder();
            int len = inputs.Length / 4;
            for (int i = 0; i <= len - 1; i++)
            {
                string strT = "";
                strT = inputs.Substring(0, 4);//;.Substring(2);
                inputs = inputs.Substring(4);
                sb.Append(Convert.ToChar(int.Parse(strT, NumberStyles.HexNumber)));
            }
           
            return sb.ToString();
        }
    }
}
