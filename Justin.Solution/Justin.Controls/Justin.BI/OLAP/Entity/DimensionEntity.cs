using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class DimensionEntity
    {
        public DimensionEntity() : this("", "") { }
        public DimensionEntity(string id)
            : this(id, id)
        {

        }
        public DimensionEntity(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Hierarchies = new List<HierarchyEntity>();
            this.Levels = new List<LevelEntity>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string FKColumn { get; set; }

        public List<LevelEntity> Levels { get; set; }
        public List<HierarchyEntity> Hierarchies { get; set; }

    }

}
