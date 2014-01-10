using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Extensions
{
    public static class ExceptionEx
    {
        public static string GetAllMessage(this Exception instance)
        {
            Exception ex = instance;
            if (ex == null) return "";
            StringBuilder sb = new StringBuilder();

            while (ex != null)
            {
                sb.Append(ex.Message).AppendLine();
                ex = ex.InnerException;
            }

            return sb.ToString() + instance.ToString();
        }
    }
}
