using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Justin.FrameWork
{
    public class CDATA : IXmlSerializable
    {
        private string _value;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CDATA() { }
        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="value"></param>
        public CDATA(string value)
        {
            this._value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            /***** 如果此节点中包含有多个节点须使用此方法。**/
            this._value = reader.ReadElementContentAsString();
            /* **********/
            //this.text = reader.ReadString();
            // reader.Read();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this._value);
        }
        /// <summary>
        /// 重写 获取CData节点的 内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._value;
        }
        /// <summary>
        /// 将 CDATA 对象隐式转换成 内容 字符串。
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static implicit operator string(CDATA element)
        {
            return (element == null) ? null : element._value;
        }
        /// <summary>
        /// 将 内容 对象隐式转换成 CDATA 字符串。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static implicit operator CDATA(string text)
        {
            return new CDATA(text);
        }
    }
}