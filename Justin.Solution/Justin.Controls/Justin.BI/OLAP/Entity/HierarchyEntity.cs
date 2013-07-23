using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class HierarchyEntity
    {
        public HierarchyEntity() : this("", "") { }
        public HierarchyEntity(string id)
            : this(id, id)
        {

        }
        public HierarchyEntity(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Levels = new List<LevelEntity>();
        }
        [XmlAttribute()]
        public string ID { get; set; }

        [XmlAttribute()]
        public string Name { get; set; }

        public List<LevelEntity> Levels { get; set; }
    }
}
