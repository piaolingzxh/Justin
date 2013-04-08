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
    }
}
