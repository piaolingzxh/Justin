using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Justin.BI.OLAP;

namespace Justin.BI
{
    public class Program
    {
        public static void Main()
        {

            OLAPTest.Mondrian_OLAP(@"D:\Programs\MondrianServer\webapps\mondrian\WEB-INF\queries\Justin_Mondrian.xml");
            Console.WriteLine("OK");
            Console.Read();
        }

    }
}
