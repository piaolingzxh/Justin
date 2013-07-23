using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.BI.OLAP.Entity;

namespace Justin.BI.OLAP
{
    public interface IOLAPFactory
    {
        void CreateSolution(Solution solution);
        void DeleteSolution(Solution solution);  
    }
}
