using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Extensions
{
    public static class EnumParser<T>
    {
        private static readonly Dictionary<string, T> _dictionary = new Dictionary<string, T>();

        static EnumParser()
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException("Type " + typeof(T).FullName + " is not an enum.");

            string[] names = Enum.GetNames(typeof(T));
            T[] values = (T[])Enum.GetValues(typeof(T));

            int count = names.Length;
            for (int i = 0; i < count; i++)
                _dictionary.Add(names[i], values[i]);
        }

        public static bool TryParse(string name, out T value)
        {
            return _dictionary.TryGetValue(name, out value);
        }

        public static T Parse(string name)
        {
            return _dictionary[name];
        }

        #region 使用

        //enum Color
        //{
        //    Red = 0,
        //    Blue = 1,
        //    Green = 2
        //}

        //Color redColorEnum = EnumParser<Color>.Parse("Red");
        #endregion
    }
}
