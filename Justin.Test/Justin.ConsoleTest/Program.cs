using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Justin.FrameWork.Helper;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            list.Add("1='1'");
            list.Add("2='2'");
            list.Add("3='3'");
            list.Add("4='4'");
            list.Add("5='5'");

            list = list.Distinct().ToList();

            string s = string.Join(" and ", list);
            Console.Read();
        }

        private static string Serialize<T>(T obj, bool removeNamespace = true)
        {
            if (obj == null) return null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;

            try
            {
                if (removeNamespace)
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(stream, obj, ns);
                }
                else
                {
                    serializer.Serialize(stream, obj);
                }
            }
            catch { return null; }
            stream.Position = 0;
            string returnStr = string.Empty;
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    returnStr += line + Environment.NewLine;
                }
            }
            return returnStr;
        }


    }
    public class Student
    {
        public Student() { }
        public Student(int id)
        {
            this.Id = id;
        }
        [XmlAttribute()]
        public int Id { get; set; }
        [DefaultValue(null)]
        [XmlAttribute()]
        public string Birthday { get; set; }

    }
}
