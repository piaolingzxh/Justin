using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class MeasureEntity
    {
        public MeasureEntity() : this("", "") { }
        public MeasureEntity(string id)
            : this(id, id)
        {

        }
        public MeasureEntity(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            Aggregator = Aggregator.None;
            this.Visable = true;
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string ColumnName { get; set; }
        [XmlAttribute()]
        public Aggregator Aggregator { get; set; }
        [XmlAttribute()]
        public bool Visable { get; set; }

    }
}
