using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP.Entities
{
    public class Cube
    {
        public Cube()
        {
            this.Measures = new List<Measure>();
            this.Dimensions = new List<Dimension>();
        }
        public Cube(string name)
            : this()
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public List<Measure> Measures { get; set; }

        public List<Dimension> Dimensions { get; set; }
    }
    public class Measure
    {
        public Measure() { }
        public string Name { get; set; }
        public string ColumnName { get; set; }
    }
    public class Dimension
    {
        public Dimension() { }
        public Dimension(string id)
            : this(id, id)
        {

        }
        public Dimension(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public string ID { get; set; }
        public string Name { get; set; }


        public List<Level> Levels { get; set; }
        public List<Hierarchy> Hierarchies { get; set; }
    }

    public class Hierarchy
    {
        public Hierarchy() { }
        public Hierarchy(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public string ID { get; set; }

        public string Name { get; set; }

        public List<Level> Levels { get; set; }
    }

    public class Level
    {
        public Level() { }
        public Level(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string SourceTable { get; set; }


        public string KeyColumn { get; set; }
        public string NameColumn { get; set; }
        public string OrderColumn { get; set; }

    }
}
