using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork
{
    public static class IEnumerableEx
    {

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
