using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class CubeEntity
    {
        public CubeEntity() : this("", "") { }
        public CubeEntity(string id)
            : this(id, id)
        {

        }
        public CubeEntity(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Measures = new List<MeasureEntity>();
            this.Dimensions = new List<DimensionEntity>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string TableName { get; set; }

        public List<MeasureEntity> Measures { get; set; }
        public List<DimensionEntity> Dimensions { get; set; }

    }
}
