using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.FrameWork.Entities;
using System.ComponentModel;
using System.Reflection;

namespace Justin.FrameWork.Helper
{
    public class EnumHelper
    {
        public static List<Factor> GetDataSource<T>()
        {
            Type tEnum = typeof(T);
            List<Factor> list = new List<Factor>();

            Array values = System.Enum.GetValues(tEnum);

            foreach (var i in values)
            {
                list.Add(new Factor((int)i, System.Enum.GetName(tEnum, i)));
            }

            return list;
        }

        public static List<Factor> GetDescriptionDataSource<T>()
        {
            Type tEnum = typeof(T);
            List<Factor> list = new List<Factor>();

            Array values = System.Enum.GetValues(tEnum);

            foreach (var i in values)
            {
                string name = System.Enum.GetName(tEnum, i);
                string descr = string.Empty;

                FieldInfo fieldInfo = tEnum.GetField(name);
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    descr = attributes[0].Description;
                }
                else
                {
                    descr = i.ToString();
                }
                list.Add(new Factor((int)i, descr));
            }

            return list;
        }


        ///// <summary>
        ///// 获取一个枚举的所有属性
        ///// </summary>
        ///// <param name="tEnum"></param>
        ///// <returns></returns>
        //public static List<Factor> EnumDescriptionsByte<T>()
        //{
        //    Type tEnum = typeof(T);
        //    Array values = System.Enum.GetValues(tEnum);
        //    List<Factor> list = new List<Factor>();
        //    foreach (var i in values)
        //    {
        //        string name = System.Enum.GetName(tEnum, i);
        //        string descr = string.Empty;

        //        FieldInfo fieldInfo = tEnum.GetField(name);
        //        DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        if (attributes.Length > 0)
        //        {
        //            descr = attributes[0].Description;
        //        }
        //        else
        //        {
        //            descr = i.ToString();
        //        }
        //        list.Add(new Factor((int)((byte)i), descr));
        //    }

        //    return list;
        //}
    }
}
