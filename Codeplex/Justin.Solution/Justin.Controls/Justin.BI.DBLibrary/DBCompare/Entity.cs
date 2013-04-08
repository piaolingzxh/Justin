using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public abstract class Entity : IData
    {
        public abstract object GetPropertyValue(string propertyName);
        public abstract List<object> GetByPropertiesValue(string[] propertyNames);

    }
}
