using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.BI.ETL.Entity
{
    public class ForeKey : Field
    {
        public Table Referencetable { get; set; }
    }

}
