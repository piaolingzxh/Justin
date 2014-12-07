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
            XmlDocument doc = new XmlDocument();
            doc.Load(@"E:\temp\cnblogs\cnblogs_category.xml");
            XmlNode xn = doc.SelectSingleNode("items");


            foreach (var item in xn.ChildNodes)
            {
                XmlElement xe = (XmlElement)item;
                string id = xe.GetAttribute("id").ToString();
                string text = xe.GetAttribute("name").ToString();
                string url = xe.GetAttribute("url").ToString();
                string tp = xe.GetAttribute("tp").ToString();
                string pid = xe.GetAttribute("pid").ToString();

                File.AppendAllText(@"E:\temp\cnblogs\categroy.text",
                    String.Format("categories.add(new BlogCategory(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"));" + Environment.NewLine
                    , id, text,url,tp,pid));
            }
            Console.WriteLine("OK");
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
