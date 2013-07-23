using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.ETL.Entity
{
    public class Field
    {
        public Field()
        {
            this.Enable = true;
        }
        public Field(string name, DbType dbType, int length)
        {
            this.Name = name;
            this.Length = length;
            this.FieldType = dbType;
            this.Enable = true;
        }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public DbType FieldType { get; set; }
        [XmlAttribute()]
        public int Length { get; set; }
        [XmlAttribute()]
        public bool Enable { get; set; }
    }

}
