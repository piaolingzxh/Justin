using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Justin.FrameWork.Extensions;
namespace Justin.Controls.Mondrian1
{
    //    [Serializable]
    //    public partial class SQL : IXmlSerializable
    //    {

    //        private string Script { get; set; }
    //        public SQL(SQLDialect dialect, string script)
    //        {
    //            this.Dialect = dialect;
    //            this.Script = script;
    //        }

    //        public System.Xml.Schema.XmlSchema GetSchema()
    //        {
    //            throw new NotImplementedException();
    //        }

    //        public void ReadXml(XmlReader reader)
    //        {
    //            this.Dialect = reader.GetAttribute("Dialect").GetEnum<SQLDialect>(SQLDialect.generic);
    //            reader.ReadStartElement("SQL");
    //            this.Script = reader.Value;
    //            reader.mo();
    //        }

    //        public void WriteXml(XmlWriter writer)
    //        {
    //            writer.WriteStartAttribute("Dialect");
    //            writer.WriteValue(this.Dialect.ToString());
    //            writer.WriteEndAttribute();
    //            writer.WriteCData(this.Script);
    //        }
    //    }
}
