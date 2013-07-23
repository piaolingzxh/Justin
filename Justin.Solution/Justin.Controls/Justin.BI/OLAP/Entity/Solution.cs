using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity
{
    public class Solution
    {
        public Solution() : this("", "") { }
        public Solution(string id)
            : this(id, id)
        {

        }
        public Solution(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Cubes = new List<CubeEntity>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        public List<CubeEntity> Cubes { get; set; }

    }
}
