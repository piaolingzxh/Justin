using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "[BIMetadataTest.Analysis.DeptDim.hieInfo].[2777933194de2b7d&组织]";

            string s2 = "[BIMetadataTest.Analysis.DeptDim.hieInfo].[2777933194de2b7d&组织].[296f1ed642e35ac2&分类1]";


            object res = s2.StartsWith(s1);

            Console.Read();
        }




    }
    public static class extensions
    {
        #region Obj->T

        /// <summary>
        /// 1、拆箱：将装箱后的T(任意类型，包括系统定义的值类型和引用类型和自定义的各种类型)，转换为你所想要的T
        /// 2、类型转换：将一种简单类型转换为另外一种简单类型,例如 将string类型的"123"转换为数字类型的123，
        /// 简单类型包括；bool、byte、char、decimal、double、float、int、long、sbyte、short、uint、ulong、ushort，类型转换时注意溢出问题
        /// 转换失败,则抛出异常
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(this Object instance)
        {
            T result = default(T);
            try
            {
                result = (T)Convert.ChangeType(instance, typeof(T));
            }
            catch { };
            return result;
        }

        #endregion
        #region Obj->T

        /// <summary>
        /// 1、拆箱：将装箱后的T(任意类型，包括系统定义的值类型和引用类型和自定义的各种类型)，转换为你所想要的T
        /// 2、类型转换：将一种简单类型转换为另外一种简单类型,例如 将string类型的"123"转换为数字类型的123，
        /// 简单类型包括；bool、byte、char、decimal、double、float、int、long、sbyte、short、uint、ulong、ushort，类型转换时注意溢出问题
        /// 转换失败后，不抛出异常，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(this Object instance, T defaultValue)
        {
            T result = defaultValue;
            try
            {
                result = (T)Convert.ChangeType(instance, typeof(T));
            }
            catch { }
            return result;

        }

        #endregion
    }
}
