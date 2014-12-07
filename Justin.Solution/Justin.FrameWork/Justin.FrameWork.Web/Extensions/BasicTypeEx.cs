using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Justin.FrameWork.Web.Extensions
{
    public static class BasicTypeEx
    {
        #region String->HtmlEncode/HtmlDecode

        public static string HtmlEncode(this string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static string HtmlDecode(this string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        #endregion


        public static int[] GetInts(this String instance)
        {
            List<int> listValue = new List<int>();
            string value = "";
            foreach (char item in instance)
            {
                if (item >= 48 && item <= 58)
                {
                    value += item;
                }
                else
                {
                    if (!string.IsNullOrEmpty(value))
                        listValue.Add(int.Parse(value));
                    value = "";
                }
            }
            return listValue.ToArray();

        }
        public static string PadLeft(this Int32 instance, int totalWidth)
        {
            return instance.PadLeft(totalWidth, '0');
        }
        public static string PadLeft(this Int32 instance, int totalWidth, char paddingChar)
        {
            return instance.ToString().PadLeft(totalWidth, paddingChar);
        }
    }
}
