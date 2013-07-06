using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Extensions
{
    public static class IEnumerableEx
    {

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, bool ignoreCase)
        {
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
