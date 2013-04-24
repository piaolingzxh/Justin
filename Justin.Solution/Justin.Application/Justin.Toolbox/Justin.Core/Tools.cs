using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Core
{
    public class Tools
    {
        public const string OpenFileDialogFilterFormart = "{1}文件(*.{0})|*.{0}|";
        public static string GetOpenFileDialogFilter(string extensionsString)
        {

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(extensionsString))
            {
                string[] extensions = extensionsString.Split(',');
                if (extensions != null)
                {
                    foreach (var item in extensions)
                    {
                        sb.AppendFormat(OpenFileDialogFilterFormart, item.TrimStart('.'), item.TrimStart('.'));
                    }
                }
            }
            return sb.ToString().TrimEnd('|');

        }
    }
}
