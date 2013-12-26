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
            List<int> list = new List<int>();

            for (int i = 0; i < 50; i++)
            {
                list.Add(i);
            }

            var mList = list.Skip(0).Take(10).ToList();
        }
    }


}
