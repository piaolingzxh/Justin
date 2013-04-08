using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.FrameWork.Entities;
using System.Reflection;
using System.ComponentModel;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using System.Xml;

namespace Justin.FrameWork.Extensions
{
    public static class BasicTypeEx
    {

        public static string TrimEndWhiteSpaceAndNewLine(this string instance)
        {
            StringBuilder sb = new StringBuilder(instance);
            for (int i = instance.Length - 1; i >= 0; i--)
            {
                if (instance[i].ToString().Equals(" ") || instance[i].Equals(' ') || instance[i].Equals('\r') || instance[i].Equals('\n'))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
        #region Obj->Int32

        /// <summary>
        /// 将Object类型的数字，转换为Int32
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(this Object instance, Int32 defaultValue = 0)
        {
            return instance.Value<Int32>(defaultValue);
        }

        #endregion

        #region Obj->Double

        /// <summary>
        /// 将Object类型的数字，转换为Double
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="digits">保留几位小数位</param>
        /// <returns></returns>
        public static Double GetDouble(this Object instance, Double defaultValue = 0.0, int digits = -1)
        {
            Double result = instance.Value<Double>(defaultValue);
            if (digits > -1)
                result = Math.Round(result, digits);
            return result;
        }

        #endregion

        #region Obj->Decimal

        /// <summary>
        /// 将Object类型的数字，转换为Decimal
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="digits">保留几位小数位</param>
        /// <returns></returns>
        public static Decimal GetDouble(this Object instance, Decimal defaultValue = 0, int digits = -1)
        {
            Decimal result = instance.Value<Decimal>(defaultValue);
            if (digits > -1)
                result = Math.Round(result, digits);
            return result;
        }

        #endregion

        #region Obj->float

        /// <summary>
        /// 将Object类型的数字，转换为float
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float GetDouble(this Object instance, float defaultValue = 0)
        {
            return instance.Value<float>(defaultValue);
        }

        #endregion

        #region Obj->Boolean

        /// <summary>
        /// 将Object类型的数字，转换为Boolean
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Boolean GetBoolean(this Object instance, Boolean defaultValue = false)
        {
            return instance.Value<Boolean>(defaultValue);
        }

        #endregion

        #region String->Enum

        /// <summary>
        /// 将String类型，转换为枚举类型T
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetEnum<T>(this string instance, T defaultValue) where T : struct
        {
            T result = defaultValue;
            try
            {
                Enum.TryParse<T>(instance, true, out result);
            }
            catch { }
            return result;
        }

        #endregion

        #region Int32->Enum

        /// <summary>
        /// 将Int32类型，转换为枚举类型T
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetEnum<T>(this Int32 instance, T defaultValue) where T : struct
        {
            object m = instance;
            try
            {
                return (T)m;
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        public static string ToJString(this object obj, string defaultValue = "")
        {
            if (obj == null || obj == "")
            {
                return defaultValue;
            }
            return obj.ToString();
        }

        #region Enum->Description

        public static string GetDescription(this Enum instance)
        {
            string result = string.Empty;

            FieldInfo fieldInfo = instance.GetType().GetField(instance.ToString());
            if (fieldInfo != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length == 1)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return null;
                }
            }
            return result;
        }

        #endregion

        #region Enum->DisplayName

        public static string GetDisplayName(this Enum instance)
        {
            string result = string.Empty;

            FieldInfo fieldInfo = instance.GetType().GetField(instance.ToString());
            if (fieldInfo != null)
            {
                DisplayNameAttribute[] attributes = (DisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);

                if (attributes.Length > 0)
                {
                    result = attributes[0].DisplayName;
                }
            }
            return result;
        }

        #endregion

        #region Enum->DisplayName

        public static string GetDisplay(this Enum instance)
        {
            string result = string.Empty;

            FieldInfo fieldInfo = instance.GetType().GetField(instance.ToString());
            if (fieldInfo != null)
            {
                DisplayAttribute[] attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attributes.Length > 0)
                {
                    result = attributes[0].Name;
                }
            }
            return result;
        }

        #endregion

        #region String->DateTime

        /// <summary>
        /// 将String类型，转换为枚举类型T
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this string instance, DateTime defaultValue)
        {
            return instance.Value<DateTime>(defaultValue);
        }

        #endregion

        #region String->DateTime

        /// <summary>
        /// 将String类型，转换为枚举类型T
        /// 转换失败后，返回默认值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this string instance)
        {
            return instance.Value<DateTime>(new DateTime(1900, 1, 1));
        }

        #endregion

        #region StringBuilder   ->format

        public static void AppendLine(this StringBuilder builder, string format, params Object[] parameters)
        {
            builder.AppendLine(string.Format(format, parameters));
        }

        #endregion

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

        #region 反射

        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName);

            if (method == null)
                throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);

            var value = method.Invoke(obj, parameters);
            return (value is T ? (T)value : default(T));
        }
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);

            var value = property.GetValue(obj, null);
            return (value is T ? (T)value : defaultValue);
        }
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            if (!property.CanWrite)
                throw new ArgumentException(string.Format("Property '{0}' does not allow writes.", propertyName), propertyName);
            property.SetValue(obj, value, null);
        }

        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            if ((attributes.Length > 0))
                return (attributes[0] as T);
            return null;
        }
        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited).OfType<T>().Select(attribute => attribute);
        }

        #endregion

        #region 其他

        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
                action(value);
        }

        public static string ToCSV<T>(this IEnumerable<T> source, char separator)
        {
            if (source == null)
                return string.Empty;

            var csv = new StringBuilder();
            source.ForEach(value => csv.AppendFormat("{0}{1}", value, separator));
            return csv.ToString(0, csv.Length - 1);
        }

        #endregion


        #region XmlNode

        public static T GetValue<T>(this XmlNode node, string attributeName)
        {
            string value = "";
            if (node != null && node.Attributes[attributeName] != null)
                value = node.Attributes[attributeName].Value;
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region DataRowView

        public static T Get<T>(this DataRowView row, string field, T defaultValue)
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            return value.Value<T>(defaultValue);
        }
        public static T GetEnum<T>(this DataRowView row, string field, T defaultValue) where T : struct
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            int temp;
            if (Int32.TryParse(value.ToString(), out temp))
            {
                return temp.GetEnum<T>(defaultValue);
            }
            else
            {
                return value.ToString().GetEnum<T>(defaultValue);
            }
        }

        #endregion

        #region DataRow

        public static T Get<T>(this DataRow row, string field, T defaultValue)
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            return value.Value<T>(defaultValue);
        }
        public static T GetEnum<T>(this DataRow row, string field, T defaultValue) where T : struct
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            int temp;
            if (Int32.TryParse(value.ToString(), out temp))
            {
                return temp.GetEnum<T>(defaultValue);
            }
            else
            {
                return value.ToString().GetEnum<T>(defaultValue);
            }
        }

        #endregion

        #region Dataset Conversion

        /// <summary>
        /// Converts the provided list to a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="T">Concrete type of the objects in the list.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <returns>A <see cref="DataSet"/> containing the converted records.</returns>
        public static DataSet GetDataSet<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            DataSet ds = new DataSet();

            if (list.Count <= 0)
            {
                return ds;
            }

            DataTable dt = new DataTable(typeof(T).Name);

            PropertyInfo[] propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }

                DataRow row = dt.NewRow();
                for (int i = 0, j = propertyInfo.Length; i < j; i++)
                {
                    PropertyInfo pInfo = propertyInfo[i];
                    string name = pInfo.Name;
                    if (dt.Columns[name] == null)
                    {
                        dt.Columns.Add(new DataColumn(name, pInfo.PropertyType));
                    }
                    row[name] = pInfo.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// Converts the provided list to a <see cref="DataTable"/>.
        /// </summary>
        /// <typeparam name="T">Concrete type of the objects in the list.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <returns>A <see cref="DataTable"/> containing the converted records.</returns>
        public static DataTable GetDataTable<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            DataSet ds = GetDataSet(list);
            DataTable dt = null;

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        #endregion
    }
}
