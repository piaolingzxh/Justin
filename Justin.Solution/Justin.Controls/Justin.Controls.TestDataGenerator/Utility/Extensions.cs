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
using Justin.Controls.TestDataGenerator.Entities;

namespace Justin.Controls.TestDataGenerator.Utility
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
    }


}
