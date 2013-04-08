using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public delegate object ExecuteScalarHandler(string sql);
    public class Entity2TableDataCompare<TSource, TDestination> : IDataCompare<TSource, TDestination>
        where TSource : Entity
        where TDestination : Table
    {
        private StringComparison ComparisonType { get; set; }
        private Dictionary<string, string> MappingProperties { get; set; }

        public Entity2TableDataCompare(ExecuteScalarHandler executeScalar, Dictionary<string, string> mappingProperties, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            this.ComparisonType = comparisonType;
            this.ExecuteScalar = executeScalar;
            this.MappingProperties = mappingProperties;

        }

        public int Compare(List<TSource> sources, TDestination destionation, List<TSource> sourceRedundant)
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
                if (this.Compare(source, destionation) != 0)
                {
                    sourceRedundant.Add(source);
                }
            }
            return sourceRedundant.Count > 0 ? -1 : 0;
        }
        public int Compare(TSource source, TDestination destionation)
        {
            object[] sourcePropertyValues = source.GetByPropertiesValue(MappingProperties.Keys.ToArray()).ToArray();

            StringBuilder joinBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder("");
            //select count(*) from table t0 join t1 on t0.  where field1=svalue1 and field2=svalue2

            string[] keys = MappingProperties.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                string dstFieldName = MappingProperties[keys[i]];
                KeyValuePair<string, TranslateMapping> dstReference = destionation.GetMappingField(dstFieldName);
                if (dstReference.Value != null)
                {
                    TranslateMapping fk = dstReference.Value;
                    joinBuilder.AppendFormat(" join {0} t{1} on t0.{2}=t{1}.{3} ", fk.ReferenceTableName, i, fk.FieldName, fk.ReferenceFieldName);
                    whereBuilder.AppendFormat(" and t{0}.{1}='{2}'", i, fk.DestinationFieldName, sourcePropertyValues[i]);
                }
                else
                {
                    whereBuilder.AppendFormat(" and t0.{0}='{1}'", dstFieldName, sourcePropertyValues[i]);
                }
            }
            string sql = string.Format("select count(*) from {0} t0 {1}  where 1=1 {2}", destionation.TableName, joinBuilder.ToString(), whereBuilder.ToString());

            int result = (int)ExecuteScalar(sql);
            if (result > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public ExecuteScalarHandler ExecuteScalar { get; set; }
    }
}
