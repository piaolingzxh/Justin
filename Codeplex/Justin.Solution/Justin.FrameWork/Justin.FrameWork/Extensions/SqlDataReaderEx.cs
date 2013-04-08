using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace Justin.FrameWork.Extensions
{
    public static class SqlDataReaderEx
    {
        public static T GetInstanceWithProperty<T>(this SqlDataReader Instance)
        {
            Type type = typeof(T);
            //if (!Instance.HasRows)
            //{
            //    return null;
            //}
            //else
            //{
            object obj = Activator.CreateInstance(type);

            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                string name = prop.Name;
                try
                {
                    int fldOrdinal = Instance.GetOrdinal(name);
                    object val = Instance[fldOrdinal];
                    if (val == DBNull.Value)
                        val = null;
                    type.InvokeMember(name, BindingFlags.SetProperty, null, obj, new object[] { val });
                }
                catch { }//simply ignore
            }
            return (T)obj;
            //}
        }
        public static T GetInstanceWithField<T>(this SqlDataReader Instance)
        {
            Type type = typeof(T);
            //if (!Instance.HasRows)
            //{
            //    return null;
            //}
            //else
            //{
            object obj = Activator.CreateInstance(type);

            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                string name = field.Name;
                try
                {
                    int fldOrdinal = Instance.GetOrdinal(name);
                    object val = Instance[fldOrdinal];
                    if (val == DBNull.Value)
                        val = null;
                    type.InvokeMember(name, BindingFlags.SetField, null, obj, new object[] { val });
                }
                catch { }//simply ignore
            }
            return (T)obj;
            //}
        }
    }
}
