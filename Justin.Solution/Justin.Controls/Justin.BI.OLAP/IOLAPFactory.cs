using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP
{
    public interface IOLAPFactory
    {
        void CreateSolution(ISolution solution);
        void DeleteSolution(ISolution solution);

    }
}
