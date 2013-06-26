using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP
{
    public class SSASSolution : ISolution
    {
        public SSASSolution(string name)
        {
            this.Name = name;
            this.Cubes = new List<ICube>();
            this.Dims = new List<IDim>();
        }
        public string Name { get; set; }
        public List<ICube> Cubes { get; set; }
        public List<IDim> Dims { get; set; }
    }

    public class SSASDim : IDim
    {
        public SSASDim(string id)
            : this(id, id)
        {

        }
        public SSASDim(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public string ID { get; set; }
        public string Name { get; set; }


        public List<ILevel> Levels { get; set; }
        public List<IHierarchy> Hierarchies { get; set; }
    }

    public class SSASHierarchy : IHierarchy
    {
        public SSASHierarchy(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public string ID { get; set; }

        public string Name { get; set; }

        public List<ILevel> Levels { get; set; }
    }

    public class SSASLevel : ILevel
    {
        public SSASLevel(string id, string name)
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
