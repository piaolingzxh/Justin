using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP
{
    public interface ICube
    {
        string Name { get; set; }
    }

    public interface IDim
    {
        string ID { get; set; }
        string Name { get; set; }
        List<ILevel> Levels { get; set; }
        List<IHierarchy> Hierarchies { get; set; }

    }

    public interface IMeasure
    {
        string Name { get; set; }
    }

    public interface ISolution
    {
        string Name { get; set; }
        List<ICube> Cubes { get; set; }
        List<IDim> Dims { get; set; }
    }

    public interface IHierarchy
    {
        string ID { get; set; }
        string Name { get; set; }

        List<ILevel> Levels { get; set; }
    }

    public interface ILevel
    {
        string ID { get; set; }
        string Name { get; set; }

        string KeyColumn { get; set; }
        string NameColumn { get; set; }
        string OrderColumn { get; set; }



        String SourceTable { get; set; }
    }
}
