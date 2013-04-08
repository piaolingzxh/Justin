using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.TestDataGenerate
{
    public class SourceFieldData
    {
        public string TableName { get; set; }
        public string fieldName { get; set; }
        public object[] Values { get; set; }

    }
}
