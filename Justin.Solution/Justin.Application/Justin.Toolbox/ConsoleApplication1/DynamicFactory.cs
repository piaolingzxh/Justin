using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace Slyzen.Utility
{
    /// <summary>
    /// 使用Emit动态调用的工厂
    /// <see cref="http://www.codeproject.com/Articles/14973/Dynamic-Code-Generation-vs-Reflection"/>
    /// <see cref="http://www.codeproject.com/Articles/14593/A-General-Fast-Method-Invoker"/>
    /// </summary>
    public class DynamicFactory
    {
        #region - Delegates -

        //public delegate object GetInvoker(object source);
        //public delegate void SetInvoker(object source, object value);
        //public delegate object InstantiateObjectInvoker();
        //public delegate object MethodInvoker(object target, object[] paramters);

        #endregion

        #region - Varibles/Properties -

        private static readonly MethodInfo getItem = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });

        //private bool _enableCache = true;

        ///// <summary>
        ///// 是否启用缓存。默认值:true
        ///// </summary>
        //public bool EnableCache
        //{
        //    get { return _enableCache; }
        //    set { _enableCache = value; }
        //}

        #endregion

        #region - Cache -

        private static Dictionary<string, Action<IDbCommand, object>> m_paramGenerator = new Dictionary<string, Action<IDbCommand, object>>();
        private static Dictionary<string, Func<object, Dictionary<string, object>>> m_prop = new Dictionary<string, Func<object, Dictionary<string, object>>>();

        private static Dictionary<Type, InstanceInvoker> m_instanceInvokerCache = new Dictionary<Type, InstanceInvoker>();
        private static Dictionary<string, GetInvoker> m_getInvokerCache = new Dictionary<string, GetInvoker>();
        private static Dictionary<string, SetInvoker> m_setInvokerCache = new Dictionary<string, SetInvoker>();
        private static Dictionary<string, MethodInvoker> m_methodInvokerCache = new Dictionary<string, MethodInvoker>();

        private static void SetInstanceCache(Type key, InstanceInvoker value)
        {
            lock (m_instanceInvokerCache) { m_instanceInvokerCache[key] = value; }
        }
        private static bool TryGetInstanceCache(Type key, out InstanceInvoker value)
        {
            lock (m_instanceInvokerCache) { return m_instanceInvokerCache.TryGetValue(key, out value); }
        }
        private static void SetGetCache(string key, GetInvoker value)
        {
            lock (m_getInvokerCache) { m_getInvokerCache[key] = value; }
        }
        private static bool TryGetGetCache(string key, out GetInvoker value)
        {
            lock (m_getInvokerCache) { return m_getInvokerCache.TryGetValue(key, out value); }
        }
        private static void SetSetCache(string key, SetInvoker value)
        {
            lock (m_setInvokerCache) { m_setInvokerCache[key] = value; }
        }
        private static bool TryGetSetCache(string key, out SetInvoker value)
        {
            lock (m_setInvokerCache) { return m_setInvokerCache.TryGetValue(key, out value); }
        }
        private static void SetMethodCache(string key, MethodInvoker value)
        {
            lock (m_getInvokerCache) { m_methodInvokerCache[key] = value; }
        }
        private static bool TryGetMethodCache(string key, out MethodInvoker value)
        {
            lock (m_getInvokerCache) { return m_methodInvokerCache.TryGetValue(key, out value); }
        }

        #endregion

        #region - Base Methods -

        /// <summary>
        /// 创建实例化对象的委托
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static InstanceInvoker CreateInstanceInvoker(Type type)
        {
            InstanceInvoker invoker;
            if (TryGetInstanceCache(type, out invoker))
            {
                return invoker;
            }

            ConstructorInfo constructorInfo = type.GetConstructor(BindingFlags.Public |
                   BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            if (constructorInfo == null)
            {
                throw new ApplicationException(string.Format(@"The type {0} must declare an
                                                        empty constructor (the constructor may be private, internal, 
                                                        protected, protected internal, or public).", type));
            }

            DynamicMethod dynamicMethod = new DynamicMethod("InstantiateObject",
                    MethodAttributes.Static | MethodAttributes.Public,
                    CallingConventions.Standard, typeof(object), null, type, true);

            ILGenerator generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Ret);

            invoker = (InstanceInvoker)dynamicMethod.CreateDelegate(typeof(InstanceInvoker));
            SetInstanceCache(type, invoker);
            return invoker;
        }

        /// <summary>
        /// 创建属性get方法的委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static GetInvoker CreateGetInvoker(Type type, PropertyInfo propertyInfo)
        {
            string key = type.FullName + propertyInfo.Name;
            GetInvoker invoker;
            if (TryGetGetCache(key, out invoker))
            {
                return invoker;
            }

            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator il = dynamicGet.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, getMethodInfo);
            BoxIfNeeded(getMethodInfo.ReturnType, il);
            il.Emit(OpCodes.Ret);

            invoker = (GetInvoker)dynamicGet.CreateDelegate(typeof(GetInvoker));
            SetGetCache(key, invoker);
            return invoker;
        }

        /// <summary>
        /// 创建属性set方法的委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static SetInvoker CreateSetInvoker(Type type, PropertyInfo propertyInfo)
        {
            string key = type.FullName + propertyInfo.Name;
            SetInvoker invoker;
            if (TryGetSetCache(key, out invoker))
            {
                return invoker;
            }

            MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
            DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
            ILGenerator il = dynamicSet.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(setMethodInfo.GetParameters()[0].ParameterType, il);
            il.Emit(OpCodes.Call, setMethodInfo);
            il.Emit(OpCodes.Ret);

            invoker = (SetInvoker)dynamicSet.CreateDelegate(typeof(SetInvoker));
            SetSetCache(key, invoker);
            return invoker;
        }

        /// <summary>
        /// 创建字段get方法的委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static GetInvoker CreateGetInvoker(Type type, FieldInfo fieldInfo)
        {
            string key = type.FullName + fieldInfo.Name;
            GetInvoker invoker;
            if (TryGetGetCache(key, out invoker))
            {
                return invoker;
            }

            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator il = dynamicGet.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldInfo);
            BoxIfNeeded(fieldInfo.FieldType, il);
            il.Emit(OpCodes.Ret);

            SetGetCache(key, invoker);
            invoker = (GetInvoker)dynamicGet.CreateDelegate(typeof(GetInvoker));
            return invoker;
        }

        /// <summary>
        /// 创建字段set方法的委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static SetInvoker CreateSetInvoker(Type type, FieldInfo fieldInfo)
        {
            string key = type.FullName + fieldInfo.Name;
            SetInvoker invoker;
            if (TryGetSetCache(key, out invoker))
            {
                return invoker;
            }

            DynamicMethod dm = CreateSetDynamicMethod(type);
            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(fieldInfo.FieldType, il);
            il.Emit(OpCodes.Stfld, fieldInfo);
            il.Emit(OpCodes.Ret);

            invoker = (SetInvoker)dm.CreateDelegate(typeof(SetInvoker));
            SetSetCache(key, invoker);
            return invoker;
        }

        /// <summary>
        /// 创建指定方法的委托
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static MethodInvoker CreateMethodInvoker(Type type, MethodInfo methodInfo)
        {
            string key = type.FullName + methodInfo.Name;
            MethodInvoker invoker;
            if (TryGetMethodCache(key, out invoker))
            {
                return invoker;
            }

            DynamicMethod dm = new DynamicMethod(string.Empty, typeof(object),
                new Type[] { typeof(object), typeof(object[]) }, type, true);
            ILGenerator il = dm.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = ps[i].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i]);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(paramTypes[i], il);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            il.Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldloc, locals[i]);
            }
            il.EmitCall(OpCodes.Call, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                BoxIfNeeded(methodInfo.ReturnType, il);
            il.Emit(OpCodes.Ret);

            invoker = (MethodInvoker)dm.CreateDelegate(typeof(MethodInvoker));
            SetMethodCache(key, invoker);
            return invoker;
        }

        #endregion

        #region - Extension Methods -

        /// <summary>
        /// 获取对象的属性的委托
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Func<object, Dictionary<string, object>> GetPropertiesHandler(Type type)
        {
            string key = type.FullName;
            Func<object, Dictionary<string, object>> func;
            if (m_prop.TryGetValue(key, out func))
            {
                return func;
            }
            Type retType = typeof(Dictionary<string, object>);
            DynamicMethod dm = new DynamicMethod("GetProperties" + m_prop.Count,
                retType, new[] { typeof(object) }, type);
            ILGenerator il = dm.GetILGenerator();

            LocalBuilder dic = il.DeclareLocal(typeof(Dictionary<string, object>));
            PropertyInfo[] props = type.GetProperties();
            EmitInt32(il, props.Length);                                    //[length]
            il.Emit(OpCodes.Newobj,                                         //[dic]
                retType.GetConstructor(new Type[] { typeof(int) }));
            il.Emit(OpCodes.Stloc, dic);                                    //stack is empty

            foreach (PropertyInfo prop in props)
            {
                il.Emit(OpCodes.Ldloc, dic);                                //[dic]
                il.Emit(OpCodes.Ldstr, prop.Name);                          //[dic][propName]

                MethodInfo getMethodInfo = prop.GetGetMethod(true);
                il.Emit(OpCodes.Ldarg_0);                                   //[dic][propName][object]
                il.Emit(OpCodes.Call, getMethodInfo);                       //[dic][propName][unboxed-value] 
                BoxIfNeeded(getMethodInfo.ReturnType, il);                  //[dic][propName][boxed-value]
                il.Emit(OpCodes.Callvirt, retType.GetMethod("Add"));        //stack is empty
            }

            il.Emit(OpCodes.Ldloc, dic); //[dic]
            il.Emit(OpCodes.Ret);
            func = (Func<object, Dictionary<string, object>>)dm.CreateDelegate(typeof(Func<object, Dictionary<string, object>>));
            m_prop.Add(key, func);
            return func;
        }

        /// <summary>
        /// 反射对象的属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetObjectProperties(object value)
        {
            Check.Argument(!(value is IEnumerable || value is IDictionary),
                "value", "不支持数组、集合、字典等类型，请检查。");

            Func<object, Dictionary<string, object>> getter = GetPropertiesHandler(value.GetType());
            return getter(value);
            //Type type = obj.GetType();
            //PropertyInfo[] props = type.GetProperties();
            //Dictionary<string, object> col = new Dictionary<string, object>(props.Length);
            //foreach (PropertyInfo prop in props)
            //{
            //    GetInvoker propGetInvoker = CreateGetInvoker(type, prop);
            //    col.Add(prop.Name, propGetInvoker(obj));
            //}
            //return col;
        }

        /// <summary>
        /// 反射对象的属性
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object SetObjectProperties(Dictionary<string, object> settings, object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            Dictionary<string, object> col = new Dictionary<string, object>(props.Length);
            foreach (PropertyInfo prop in props)
            {
                SetInvoker propSetInvoker = CreateSetInvoker(type, prop);
                propSetInvoker(obj, settings[prop.Name]);
            }
            return obj;
        }

        /// <summary>
        /// 反射对象的字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetObjectFileds(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            Dictionary<string, object> col = new Dictionary<string, object>(fields.Length);
            foreach (FieldInfo field in fields)
            {
                GetInvoker fieldGetInvoker = CreateGetInvoker(type, field);
                col.Add(field.Name, fieldGetInvoker(obj));
            }
            return col;
        }

        public static Action<IDbCommand, object> GetCommandParameter(string sql, CommandType commType, Type paramType)
        {
            string key = sql + paramType.FullName;
            Action<IDbCommand, object> action;
            if (m_paramGenerator.TryGetValue(key, out action))
            {
                return action;
            }

            #region Poco

            bool filterParams = (commType == CommandType.Text);
            DynamicMethod dm = new DynamicMethod("ParamInfo" + Guid.NewGuid().ToString(),
                null, new[] { typeof(IDbCommand), typeof(object) }, paramType, true);
            ILGenerator il = dm.GetILGenerator();

            il.DeclareLocal(paramType);            // 0
            bool haveInt32Arg1 = false;
            il.Emit(OpCodes.Ldarg_1);              // [untyped-parameter]
            il.Emit(OpCodes.Unbox_Any, paramType); // [typed-parameter]
            il.Emit(OpCodes.Stloc_0);              // stack is now empty

            il.Emit(OpCodes.Ldarg_0);              // [command]
            il.EmitCall(OpCodes.Callvirt, typeof(IDbCommand).GetProperty("Parameters").GetGetMethod(), null); //[parameters]

            IEnumerable<PropertyInfo> props = paramType.GetProperties();
            if (filterParams)
            {
                props = FilterProperties(props, sql);
            }
            foreach (PropertyInfo prop in props)
            {
                if (filterParams)
                {
                    if (sql.IndexOf("@" + prop.Name, StringComparison.InvariantCultureIgnoreCase) < 0
                        && sql.IndexOf(":" + prop.Name, StringComparison.InvariantCultureIgnoreCase) < 0)
                    { // can't see the parameter in the text (even in a comment, etc) - burn it with fire
                        continue;
                    }
                }
                DbType dbType = TypeSystem.GetDbType(prop.PropertyType);
                #region dbType == DbType.Xml

                //if (dbType == DbType.Xml)
                //{
                //    // this actually represents special handling for list types;
                //    il.Emit(OpCodes.Ldarg_0); // stack is now [parameters] [command]
                //    il.Emit(OpCodes.Ldstr, prop.Name); // stack is now [parameters] [command] [name]
                //    il.Emit(OpCodes.Ldloc_0); // stack is now [parameters] [command] [name] [typed-parameter]
                //    il.Emit(OpCodes.Callvirt, prop.GetGetMethod()); // stack is [parameters] [command] [name] [typed-value]
                //    if (prop.PropertyType.IsValueType)
                //    {
                //        il.Emit(OpCodes.Box, prop.PropertyType); // stack is [parameters] [command] [name] [boxed-value]
                //    }
                //    il.EmitCall(OpCodes.Call, typeof(Database).GetMethod("PackListParameters"), null); // stack is [parameters]
                //    continue;
                //}
                #endregion

                il.Emit(OpCodes.Dup);                           //[parameters] [parameters]
                il.Emit(OpCodes.Ldarg_0);                       //[parameters] [parameters] [command]
                il.EmitCall(OpCodes.Callvirt,                   //[parameters] [parameters] [parameter]
                    typeof(IDbCommand).GetMethod("CreateParameter"), null);
                il.Emit(OpCodes.Dup);                           //[parameters] [parameters] [parameter] [parameter]
                il.Emit(OpCodes.Ldstr, prop.Name);              //[parameters] [parameters] [parameter] [parameter] [name]
                il.EmitCall(OpCodes.Callvirt,                   //[parameters] [parameters] [parameter]
                    typeof(IDataParameter).GetProperty("ParameterName").GetSetMethod(), null);

                il.Emit(OpCodes.Dup);                           //[parameters] [parameters] [parameter] [parameter]
                EmitInt32(il, (int)dbType);                     //[parameters] [parameters] [parameter] [parameter] [db-type]

                il.EmitCall(OpCodes.Callvirt,                   //[parameters] [parameters] [parameter]
                    typeof(IDataParameter).GetProperty("DbType").GetSetMethod(), null);

                il.Emit(OpCodes.Dup);                           //[parameters] [parameters] [parameter] [parameter]
                EmitInt32(il, (int)ParameterDirection.Input);   //[parameters] [parameters] [parameter] [parameter] [dir]
                il.EmitCall(OpCodes.Callvirt,                   //[parameters] [parameters] [parameter]
                    typeof(IDataParameter).GetProperty("Direction").GetSetMethod(), null);

                il.Emit(OpCodes.Dup);                           //[parameters] [parameters] [parameter] [parameter]
                il.Emit(OpCodes.Ldloc_0);                       //[parameters] [parameters] [parameter] [parameter] [typed-parameter]
                il.Emit(OpCodes.Callvirt, prop.GetGetMethod()); //[parameters] [parameters] [parameter] [parameter] [typed-value]
                bool checkForNull = true;
                if (prop.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Box, prop.PropertyType);    //[parameters] [parameters] [parameter] [parameter] [boxed-value]
                    if (Nullable.GetUnderlyingType(prop.PropertyType) == null)
                    {   // struct but not Nullable<T>; boxed value cannot be null
                        checkForNull = false;
                    }
                }
                if (checkForNull)
                {
                    if (dbType == DbType.String && !haveInt32Arg1)
                    {
                        il.DeclareLocal(typeof(int));
                        haveInt32Arg1 = true;
                    }
                    // relative stack: [boxed value]
                    il.Emit(OpCodes.Dup);// relative stack: [boxed value] [boxed value]
                    Label notNull = il.DefineLabel();
                    Label? allDone = dbType == DbType.String ? il.DefineLabel() : (Label?)null;
                    il.Emit(OpCodes.Brtrue_S, notNull);
                    // relative stack [boxed value = null]
                    il.Emit(OpCodes.Pop); // relative stack empty
                    il.Emit(OpCodes.Ldsfld, typeof(DBNull).GetField("Value")); // relative stack [DBNull]
                    if (dbType == DbType.String)
                    {
                        EmitInt32(il, 0);
                        il.Emit(OpCodes.Stloc_1);
                    }
                    if (allDone != null) il.Emit(OpCodes.Br_S, allDone.Value);
                    il.MarkLabel(notNull);
                    if (prop.PropertyType == typeof(string))
                    {
                        il.Emit(OpCodes.Dup);           //[string] [string]
                        il.EmitCall(OpCodes.Callvirt,   //[string] [length]
                            typeof(string).GetProperty("Length").GetGetMethod(), null);
                        EmitInt32(il, 4000);            // [string] [length] [4000]
                        il.Emit(OpCodes.Cgt);           // [string] [0 or 1]
                        Label isLong = il.DefineLabel(), lenDone = il.DefineLabel();
                        il.Emit(OpCodes.Brtrue_S, isLong);
                        EmitInt32(il, 4000);            // [string] [4000]
                        il.Emit(OpCodes.Br_S, lenDone);
                        il.MarkLabel(isLong);
                        EmitInt32(il, -1);              // [string] [-1]
                        il.MarkLabel(lenDone);
                        il.Emit(OpCodes.Stloc_1);       // [string] 
                    }
                    if (allDone != null) il.MarkLabel(allDone.Value);
                    // relative stack [boxed value or DBNull]
                }
                il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("Value").GetSetMethod(), null);// stack is now [parameters] [parameters] [parameter]

                if (prop.PropertyType == typeof(string))
                {
                    var endOfSize = il.DefineLabel();
                    // don't set if 0
                    il.Emit(OpCodes.Ldloc_1);               //[parameters] [parameters] [parameter] [size]
                    il.Emit(OpCodes.Brfalse_S, endOfSize);  //[parameters] [parameters] [parameter]

                    il.Emit(OpCodes.Dup);                   //[parameters] [parameters] [parameter] [parameter]
                    il.Emit(OpCodes.Ldloc_1);               //[parameters] [parameters] [parameter] [parameter] [size]
                    il.EmitCall(OpCodes.Callvirt, typeof(IDbDataParameter).GetProperty("Size").GetSetMethod(), null);// stack is now [parameters] [parameters] [parameter]

                    il.MarkLabel(endOfSize);
                }

                il.EmitCall(OpCodes.Callvirt, typeof(IList).GetMethod("Add"), null); // stack is now [parameters]
                il.Emit(OpCodes.Pop); // IList.Add returns the new index (int); we don't care
            }
            // stack is currently [command]
            il.Emit(OpCodes.Pop); // stack is now empty
            il.Emit(OpCodes.Ret);

            #endregion

            action = (Action<IDbCommand, object>)dm.CreateDelegate(typeof(Action<IDbCommand, object>));
            m_paramGenerator.Add(key, action);
            return action;
        }
        private static IEnumerable<PropertyInfo> FilterProperties(IEnumerable<PropertyInfo> props, string sql)
        {
            List<PropertyInfo> ret = new List<PropertyInfo>();
            foreach (PropertyInfo prop in props)
            {
                if (Regex.IsMatch(sql, "[@:]" + prop.Name + "([^a-zA-Z0-9_]+|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline))
                {
                    yield return prop;
                }
            }
        }
        private static void EmitInt32(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                case 8: il.Emit(OpCodes.Ldc_I4_8); break;
                default:
                    if (value >= -128 && value <= 127)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
        }
        ///// <summary>
        ///// Handles variances in features per DBMS
        ///// </summary>
        //partial class FeatureSupport
        //{
        //    /// <summary>
        //    /// Dictionary of supported features index by connection type name
        //    /// </summary>
        //    private static readonly Dictionary<string, FeatureSupport> FeatureList =
        //        new Dictionary<string, FeatureSupport>(StringComparer.InvariantCultureIgnoreCase) {
        //        {"sqlserverconnection", new FeatureSupport { Arrays = false}},
        //        {"npgsqlconnection", new FeatureSupport {Arrays = true}}};

        //    /// <summary>
        //    /// Gets the featureset based on the passed connection
        //    /// </summary>
        //    public static FeatureSupport Get(IDbConnection connection)
        //    {
        //        string name = connection.GetType().Name;
        //        FeatureSupport features;
        //        return FeatureList.TryGetValue(name, out features) ? features : FeatureList.Values.First();
        //    }

        //    /// <summary>
        //    /// True if the db supports array columns e.g. Postgresql
        //    /// </summary>
        //    public bool Arrays { get; set; }
        //}
        ///// <summary>
        ///// Internal use only
        ///// </summary>
        //[Obsolete("This method is for internal usage only", false)]
        //internal static void PackListParameters(IDbCommand command, string namePrefix, object value)
        //{
        //    // initially we tried TVP, however it performs quite poorly.
        //    // keep in mind SQL support up to 2000 params easily in sp_executesql, needing more is rare

        //    var list = value as IEnumerable;
        //    var count = 0;

        //    if (list != null)
        //    {
        //        if (FeatureSupport.Get(command.Connection).Arrays)
        //        {
        //            var arrayParm = command.CreateParameter();
        //            arrayParm.Value = list;
        //            arrayParm.ParameterName = namePrefix;
        //            command.Parameters.Add(arrayParm);
        //        }
        //        else
        //        {
        //            bool isString = value is IEnumerable<string>;
        //            foreach (var item in list)
        //            {
        //                count++;
        //                var listParam = command.CreateParameter();
        //                listParam.ParameterName = namePrefix + count;
        //                listParam.Value = item ?? DBNull.Value;
        //                if (isString)
        //                {
        //                    listParam.Size = 4000;
        //                    if (item != null && ((string)item).Length > 4000)
        //                    {
        //                        listParam.Size = -1;
        //                    }
        //                }
        //                command.Parameters.Add(listParam);
        //            }

        //            if (count == 0)
        //            {
        //                command.CommandText = Regex.Replace(command.CommandText, @"[?@:]" + Regex.Escape(namePrefix), "(SELECT NULL WHERE 1 = 0)");
        //            }
        //            else
        //            {
        //                command.CommandText = Regex.Replace(command.CommandText, @"[?@:]" + Regex.Escape(namePrefix), match =>
        //                {
        //                    var grp = match.Value;
        //                    var sb = new StringBuilder("(").Append(grp).Append(1);
        //                    for (int i = 2; i <= count; i++)
        //                    {
        //                        sb.Append(',').Append(grp).Append(i);
        //                    }
        //                    return sb.Append(')').ToString();
        //                });
        //            }
        //        }
        //    }

        //}

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void PurgeCache()
        {
            m_getInvokerCache.Clear();
            m_setInvokerCache.Clear();
            m_methodInvokerCache.Clear();
        }

        #endregion

        #region - Private Static Methods -

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }

        private static DynamicMethod CreateGetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
        }

        private static DynamicMethod CreateSetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object) }, type, true);
        }

        private static void BoxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Box, type);
            }
        }

        private static void UnboxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Unbox_Any, type);
            }
        }

        private static void EmitCastToReference(System.Type type, ILGenerator il)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        #endregion
    }
}
