using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.OLAP
{
    public interface IOLAPFactory
    {
        void CreateSolution(Justin.BI.OLAP.Entities.Cube cube);
        void DeleteSolution(Justin.BI.OLAP.Entities.Cube cube);

    }
}
