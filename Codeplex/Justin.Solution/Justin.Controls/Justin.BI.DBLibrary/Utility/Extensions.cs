using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Justin.BI.DBLibrary.TestDataGenerate;

namespace Justin.BI.DBLibrary.Utility
{
    public static class Extensions
    {    
        public static string ToParameters(this string filter, List<string> paramNames)
        {
            if (string.IsNullOrEmpty(filter))
                return "";
            string format = "";

            bool beginParams = false;
            string paramName = "";
            foreach (char item in filter)
            {
                if (item == ':')
                {
                    beginParams = true;
                    format += " '{" + paramNames.Count + "}' ";
                }
                else if (item != ':' && !beginParams)
                {
                    format += item.ToString();
                }

                if (beginParams)
                {
                    paramName += item.ToString();
                }
                if (item == ' ' || item == ' ' && beginParams)
                {
                    beginParams = false;
                    if (paramName.Trim() != "")
                    {
                        paramNames.Add(paramName.TrimStart(':'));
                    }
                    paramName = "";
                }
            }
            if (paramName != "")
            {
                paramNames.Add(paramName.TrimStart(':'));
            }

            return format;
        }
         
        public static string GetDefaultFileExtension(this Enum instance)
        {
            FileInfoAttribute attribute = instance.GetFileInfoAttribute();

            if (attribute != null)
            {
                return attribute.DefaultFileExtension;
            }
            else
            {
                return null;
            }
        }
        public static string[] GetAllowFileExtensions(this Enum instance, bool ignoreDefault = false)
        {
            FileInfoAttribute attribute = instance.GetFileInfoAttribute();

            if (attribute != null )
            {
                return attribute.GetAllowFileExtensions(ignoreDefault);
            }
            else
            {
                return null;
            }
        }

        public static FileInfoAttribute GetFileInfoAttribute(this Enum instance)
        {
            FieldInfo fi = instance.GetType().GetField(instance.ToString());
            FileInfoAttribute[] attributes = (FileInfoAttribute[])fi.GetCustomAttributes(typeof(FileInfoAttribute), false);
            if (attributes != null && attributes.Length == 1)
            {
                return attributes[0];
            }
            else
            {
                return null;
            }
        }
        public static bool Contains<TSource>(this IEnumerable<TSource> source, string value, bool ignoreCase)
        {
            EqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;

            foreach (var item in source)
            {
                if (item is ValueType || item is String)
                {
                    if (string.Compare(item.ToString(), value.ToString(), ignoreCase) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }


}
