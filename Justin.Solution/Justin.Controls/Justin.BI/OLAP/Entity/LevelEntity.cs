using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class LevelEntity
    {
        public LevelEntity() : this("", "") { }
        public LevelEntity(string id)
            : this(id, id)
        {

        }
        public LevelEntity(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string SourceTable { get; set; }


        [XmlAttribute()]
        public string KeyColumn { get; set; }
        [XmlAttribute()]
        public string NameColumn { get; set; }
        [XmlAttribute()]
        public string OrderColumn { get; set; }
        [XmlAttribute()]
        public string ParentColumn { get; set; }


    }
}
