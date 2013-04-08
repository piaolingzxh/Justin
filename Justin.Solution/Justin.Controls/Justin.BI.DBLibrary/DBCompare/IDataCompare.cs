using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public interface IDataCompare<in TSource, in TDestination>
    {
        int Compare(TSource x, TDestination y);
    }
}
