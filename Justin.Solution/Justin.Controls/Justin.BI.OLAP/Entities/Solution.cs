using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entities
{
    public class Cube
    {
        public Cube() : this("", "") { }
        public Cube(string id)
            : this(id, id)
        {

        }
        public Cube(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Measures = new List<Measure>();
            this.Dimensions = new List<Dimension>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string TableName { get; set; }

        public List<Measure> Measures { get; set; }
        public List<Dimension> Dimensions { get; set; }

    }
    public class Measure
    {
        public Measure() : this("", "") { }
        public Measure(string id)
            : this(id, id)
        {

        }
        public Measure(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            AggregationFunction = Microsoft.AnalysisServices.AggregationFunction.Sum;
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string ColumnName { get; set; }
        public Microsoft.AnalysisServices.AggregationFunction AggregationFunction { get; set; }
    }
    public class Dimension
    {
        public Dimension() : this("", "") { }
        public Dimension(string id)
            : this(id, id)
        {

        }
        public Dimension(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Hierarchies = new List<Hierarchy>();
            this.Levels = new List<Level>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string FKColumn { get; set; }

        public List<Level> Levels { get; set; }
        public List<Hierarchy> Hierarchies { get; set; }

    }

    public class Hierarchy
    {
        public Hierarchy() : this("", "") { }
        public Hierarchy(string id)
            : this(id, id)
        {

        }
        public Hierarchy(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Levels = new List<Level>();
        }
        [XmlAttribute()]
        public string ID { get; set; }

        [XmlAttribute()]
        public string Name { get; set; }

        public List<Level> Levels { get; set; }
    }

    public class Level
    {
        public Level() : this("", "") { }
        public Level(string id)
            : this(id, id)
        {

        }
        public Level(string id, string name)
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

    }

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
            this.Cubes = new List<Cube>();
        }
        [XmlAttribute()]
        public string ID { get; set; }
        [XmlAttribute()]
        public string Name { get; set; }
        public List<Cube> Cubes { get; set; }

    }
}
