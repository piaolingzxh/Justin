using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;

namespace Justin.Controls.Mondrian
{
    public partial class SQL : IXmlSerializable
    {
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        private static Dictionary<string, SQLDialect> _SQLDialectList = new Dictionary<string, SQLDialect>();
        private static Dictionary<string, SQLDialect> SQLDialectList
        {
            get
            {
                if (_SQLDialectList.Count == 0)
                {
                    foreach (SQLDialect item in Enum.GetValues(typeof(SQLDialect)))
                    {
                        XmlEnumAttribute[] attributes = typeof(SQLDialect).GetField(item.ToString()).GetCustomAttributes(typeof(XmlEnumAttribute), true) as XmlEnumAttribute[];
                        string dialectAlias = attributes.Count() > 0 ? attributes[0].Name : item.ToString();
                        if (!_SQLDialectList.Keys.Contains(dialectAlias))
                            _SQLDialectList.Add(dialectAlias, item);
                    }
                }
                return _SQLDialectList;
            }
        }
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string dialectAlias = reader.GetAttribute(this.dialectAlias);

            this.Dialect = SQLDialectList.Keys.Contains(dialectAlias) ? _SQLDialectList[dialectAlias] : SQLDialect.Generic;

            string text = reader.ReadInnerXml();
            if (text.StartsWith(Constants.CDataStartTag) && text.EndsWith(Constants.CDataEndTag))
            {
                text = text.Substring(Constants.CDataStartTag.Length, text.LastIndexOf(Constants.CDataEndTag) - Constants.CDataStartTag.Length);
            }
            this.Text = text;

        }
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            XmlEnumAttribute[] xmlEnumAttributes = typeof(SQLDialect).GetField(this.Dialect.ToString()).GetCustomAttributes(typeof(XmlEnumAttribute), true) as XmlEnumAttribute[];
            string dialectValue = xmlEnumAttributes.Count() > 0 ? xmlEnumAttributes[0].Name : this.Dialect.ToString();
            writer.WriteAttributeString(this.dialectAlias, dialectValue);
            writer.WriteCData(this.Text);
        }
    }
    public partial class Element
    {
        protected void PrepareChildrenElements()
        {
            PropertyInfo[] pInfos = this.GetType().GetProperties();
            this.Items = null;
            this.ItemTypes = null;

            List<Tuple<int, IEnumerable<Element>>> childElementList = new List<Tuple<int, IEnumerable<Element>>>();
            foreach (PropertyInfo pInfo in pInfos)
            {
                ChildElementAttribute[] attributes = pInfo.GetCustomAttributes(typeof(ChildElementAttribute), true) as ChildElementAttribute[];
                if (attributes.Count() > 0)
                {
                    ChildElementAttribute attribute = attributes[0];

                    object pValue = pInfo.GetValue(this, null);

                    if (pValue != null)
                    {
                        if (attribute.ChildCategory == ChildCategory.Element)
                        {
                            var childElement = pValue as Element;
                            if (childElement != null)
                                childElement.PrepareChildrenElements();
                        }
                        else if (attribute.ChildCategory == ChildCategory.ChildrenColection || attribute.ChildCategory == ChildCategory.ChildrenList)
                        {
                            IEnumerable<Element> elements = pValue as IEnumerable<Element>;

                            if (elements != null)
                            {
                                if (elements.Count() == 0)
                                {
                                    pInfo.SetValue(this, null, null);
                                }
                                else
                                {
                                    foreach (var element in elements)
                                    {
                                        element.PrepareChildrenElements();
                                    }
                                    if (attribute.ChildCategory == ChildCategory.ChildrenColection)
                                    {
                                        childElementList.Add(new Tuple<int, IEnumerable<Element>>(attribute.Order, elements));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (childElementList.Count > 0)
            {
                List<Element> tElements = new List<Element>();
                foreach (var item in childElementList.OrderBy(row => row.Item1))
                {
                    tElements.AddRange(item.Item2);
                }
                this.Items = tElements.ToArray();
                this.ItemTypes = tElements.Select(row => row.ElementType).ToArray();
            }
        }

        protected void RevertChildrenElements()
        {
            Dictionary<string, List<Element>> dic = new Dictionary<string, List<Element>>();
            if (this.Items != null)
            {
                var group = this.Items.GroupBy(row => row.GetType().FullName);
                foreach (var item in group)
                {
                    dic.Add(item.Key, item.ToList<Element>());
                }
            }
            PropertyInfo[] pInfos = this.GetType().GetProperties();

            foreach (var pInfo in pInfos)
            {
                object pValue = pInfo.GetValue(this, null);
                ChildElementAttribute[] attributes = pInfo.GetCustomAttributes(typeof(ChildElementAttribute), true) as ChildElementAttribute[];
                if (attributes.Count() > 0)
                {
                    ChildElementAttribute attribute = attributes[0];

                    if (attribute.ChildCategory == ChildCategory.Element)
                    {
                        var childElement = pValue as Element;
                        if (childElement != null)
                            childElement.RevertChildrenElements();
                    }
                    else
                    {
                        if (dic.Keys.Contains(attribute.ChildType.ToString()))
                        {
                            Type t = attribute.ChildType;
                            IList list = Element.CreateGetInvoker(this.GetType(), pInfo)(this) as IList; //; pInfo.GetGetMethod().Invoke(this, null) as IList;
                            if (list == null)
                                continue;
                            IList values = dic[attribute.ChildType.ToString()] as IList;
                            if (values == null)
                                continue;
                            foreach (var item in values)
                            {
                                Element.ListAdd(list, item, t);
                            }

                            IEnumerable<Element> elements = pValue as IEnumerable<Element>;

                            if (elements != null)
                            {
                                foreach (var element in elements)
                                {
                                    element.RevertChildrenElements();
                                }
                            }

                        }
                    }
                }
            }

        }

        private static Dictionary<string, Func<object, object>> dic = new Dictionary<string, Func<object, object>>();

        /// <summary>
        /// 创建属性get方法的委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private static Func<object, object> CreateGetInvoker(Type type, PropertyInfo propertyInfo)
        {
            string key = type.FullName + "_" + propertyInfo.Name;
            Func<object, object> invoker;

            if (dic.TryGetValue(key, out invoker))
                return invoker;

            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            DynamicMethod dynamicGet = new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
            ILGenerator il = dynamicGet.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, getMethodInfo);
            BoxIfNeeded(getMethodInfo.ReturnType, il);
            il.Emit(OpCodes.Ret);

            invoker = (Func<object, object>)dynamicGet.CreateDelegate(typeof(Func<object, object>));
            dic.Add(key, invoker);
            return invoker;
        }

        private static void BoxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Box, type);
            }
        }

        private static MethodInfo List_Add = typeof(IList).GetMethod("Add");

        private static void ListAdd(IList list, object item, Type itemType)
        {
            var action = GetListAddAction(itemType);
            int index = action(list, item);
        }

        private static Func<IList, object, int> GetListAddAction(Type itemType)
        {
            string key = "list_" + itemType.FullName;
            DynamicMethod dm = new DynamicMethod("list_add", typeof(int), new[] { typeof(IList), typeof(object) }, true);
            ILGenerator il = dm.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);                                           //list
            il.Emit(OpCodes.Ldarg_1);                                           //list,value
            EmitCastToReference(itemType, il);                                  //list,[cast/unbox]value
            il.Emit(OpCodes.Callvirt, List_Add);                                //stack is empty
            il.Emit(OpCodes.Ret);
            Func<IList, object, int> action = (Func<IList, object, int>)dm.CreateDelegate(typeof(Func<IList, object, int>));
            return action;
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

        public string Serializer()
        {
            this.PrepareChildrenElements();
            return SerializeHelper.XmlSerialize(this, true);
        }
    }

    public partial class Schema
    {
        public new string Serializer()
        {
            this.PrepareChildrenElements();
            return SerializeHelper.XmlSerialize<Schema>(this, true);
        }
        public void Serializer(string fileName)
        {
            this.PrepareChildrenElements();
            SerializeHelper.XmlSerializeToFile<Schema>(this, fileName, true);
        }
        public static Schema Deserialize(string fileName)
        {
            Schema schema = SerializeHelper.XmlDeserializeFromFile<Schema>(fileName);
            schema.RevertChildrenElements();
            return schema;
        }

    }


}
