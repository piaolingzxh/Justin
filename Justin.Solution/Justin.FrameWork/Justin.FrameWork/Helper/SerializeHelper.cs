using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Justin.FrameWork.Helper
{
    public class SerializeHelper
    {
        /// <summary>
        /// XML系列化
        /// </summary>
        /// <typeparam name="T">需要序列化对象的类型</typeparam>
        /// <param name="obj"> 需要序列化的对象</param>
        /// <returns>Xml Data String</returns>
        public static string XmlSerialize<T>(T obj, bool removeNamespace = false)
        {
            if (obj == null) return null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            //try
            //{
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add("", "");
            if (removeNamespace)
            {
                serializer.Serialize(stream, obj, ns);
            }
            else
            {
                serializer.Serialize(stream, obj);
            }
            //}
            //catch { return null; }
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
        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">反序列化目标类型</typeparam>
        /// <param name="data">需要反序列化的String</param>
        /// <returns>得到的反序列化对象</returns>
        public static T XmlDeserialize<T>(string data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
                {
                    sw.Write(data);
                    sw.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    //try
                    //{
                        var obj = serializer.Deserialize(stream);
                        return ((T)obj);
                    //}
                    //catch { return default(T); }
                }
            }
        }

        public static string XmlSerializeToFile<T>(T obj, string fileName, bool removeNamespace = false)
        {
            string content = XmlSerialize<T>(obj, removeNamespace);
            StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
            writer.Write(content);
            writer.Close();
            return content;
        }

        public static T XmlDeserializeFromFile<T>(string fileName)
        {
            string content = File.ReadAllText(fileName, Encoding.UTF8);
            T t = XmlDeserialize<T>(content);
            return t;
        }
    }
}
