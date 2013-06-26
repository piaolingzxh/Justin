using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Justin.FrameWork.Entities
{

    /// <summary>
    /// 支持XML序列化的泛型 Dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [XmlRoot("SerializableDictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {

        #region 构造函数
        public SerializableDictionary()
            : base()
        {
        }
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public SerializableDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public SerializableDictionary(int capacity)
            : base(capacity)
        {
        }
        public SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }
        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// 从对象的 XML 表示形式生成该对象
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                //reader.ReadStartElement("item");
                //reader.ReadStartElement("key");
                //TKey key = (TKey)keySerializer.Deserialize(reader);
                //reader.ReadEndElement();
                //reader.ReadStartElement("value");
                //TValue value = (TValue)valueSerializer.Deserialize(reader);
                //reader.ReadEndElement();
                //this.Add(key, value);
                //reader.ReadEndElement();
                //reader.MoveToContent();

                reader.MoveToAttribute("key");
                TKey key = (TKey)Convert.ChangeType((object)reader.Value, typeof(TKey));
                reader.MoveToAttribute("value");
                TValue value = (TValue)Convert.ChangeType((object)reader.Value, typeof(TValue));

                this.Add(key, value);
                reader.Read();
            }
            reader.ReadEndElement();
        }

        /**/
        /// <summary>
        /// 将对象转换为其 XML 表示形式
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                //writer.WriteStartElement("item");
                //writer.WriteStartElement("key");
                //keySerializer.Serialize(writer, key);
                //writer.WriteEndElement();
                //writer.WriteStartElement("value");
                //TValue value = this[key];
                //valueSerializer.Serialize(writer, value);
                //writer.WriteEndElement();
                //writer.WriteEndElement();


                writer.WriteStartElement("item");
                writer.WriteStartAttribute("key");
                writer.WriteString(key.ToString());
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("value");
                writer.WriteString(this[key].ToString());
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
