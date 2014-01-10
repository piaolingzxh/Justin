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
            if (instance == null) return "";
            StringBuilder sb = new StringBuilder();

            while (instance != null)
            {
                sb.Append(instance.Message).AppendLine();
                instance = instance.InnerException;
            }

            return sb.ToString() + instance.ToString();
        }
    }
}
