using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP.Entity
{
    public enum Aggregator
    {
        Sum = 0,
        Count = 1,
        Min = 2,
        Max = 3,
        DistinctCount = 4,
        None = 5,
        ByAccount = 6,
        AverageOfChildren = 7,
        FirstChild = 8,
        LastChild = 9,
        FirstNonEmpty = 10,
        LastNonEmpty = 11,
    }
}
