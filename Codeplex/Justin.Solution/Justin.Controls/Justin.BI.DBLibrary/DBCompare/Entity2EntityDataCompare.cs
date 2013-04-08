using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public class Entity2EntityDataCompare<TSource, TDestination> : IDataCompare<TSource, TDestination>
        where TSource : Entity
        where TDestination : Entity
    {
        private StringComparison ComparisonType { get; set; }
        private Dictionary<string, string> MappingProperties { get; set; }

        public Entity2EntityDataCompare(Dictionary<string, string> mappingProperties, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            this.MappingProperties = mappingProperties;
            this.ComparisonType = comparisonType;

        }
      
        public int Compare(List<TSource> sources, List<TDestination> destinations, Dictionary<string, string> mappingProperties, List<TSource> sourceRedundant, List<TSource> destinationRedundant)
        {
            if (sourceRedundant != null)
            {
                sourceRedundant.Clear();
            }
            else
            {
                sourceRedundant = new List<TSource>();
            }
            foreach (var source in sources)
            {
                bool exist = false;
                foreach (var dst in destinations)
                {
                    if (this.Compare(source, dst) == 0)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    sourceRedundant.Add(source);
                }
            }
            return sourceRedundant.Count > 0 ? -1 : 0;
        }
        public int Compare(TSource source, TDestination destination)
        {
            object[] sourcePropertyValues = source.GetByPropertiesValue(MappingProperties.Keys.ToArray()).ToArray();
            object[] destinationPropertyValues = destination.GetByPropertiesValue(MappingProperties.Keys.ToArray()).ToArray();

            for (int i = 0; i < sourcePropertyValues.Length; i++)
            {
                if (string.Compare(sourcePropertyValues[i].ToString(), destinationPropertyValues[i].ToString(), ComparisonType) != 0)
                {
                    return -1;
                }
            }
            return 0;
        }
    }
}
