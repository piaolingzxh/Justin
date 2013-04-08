using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public delegate DataTable ExecuteDataTableHandler(string sql);
    public class Table2TableDataCompare<TSource, TDestination> : IDataCompare<TSource, TDestination>
        where TSource : Table
        where TDestination : Table
    {
        private StringComparison ComparisonType { get; set; }
        private Dictionary<string, string> MappingProperties { get; set; }

        public Table2TableDataCompare(ExecuteDataTableHandler executeDataTable, Dictionary<string, string> mappingProperties, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            this.ComparisonType = comparisonType;
            this.ExecuteDataTable = executeDataTable;
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
            //!++问题：源和目标大多数情况下，不会再一个DB中，此问题尚待解决
            //!select * from Student where str(id)+'-'+name not in(select str(id)+'-'+name from Student2 )
            StringBuilder sourceSelectBuilder = new StringBuilder();
            StringBuilder sourceWhereBuilder = new StringBuilder();
            StringBuilder destionationSelectBuilder = new StringBuilder();
            foreach (var item in MappingProperties)
            {
                sourceSelectBuilder.AppendFormat("{0},", item.Key);
                sourceWhereBuilder.AppendFormat(" str({0})+'-'", item.Key);
                destionationSelectBuilder.AppendFormat(" str({0})+'-'", item.Value);
            }
            string sql = string.Format("select {2} from {0} where {3} not in(select {4} from {1} )"
                , source.TableName
                , destionation.TableName
                , sourceSelectBuilder.ToString().TrimEnd(',')
                , sourceWhereBuilder.ToString().TrimEnd('-')
                , destionationSelectBuilder.ToString().TrimEnd('-'));
            DataTable table = ExecuteDataTable(sql);
            return table.Rows.Count > 0 ? -1 : 0;
        }

        public ExecuteDataTableHandler ExecuteDataTable { get; set; }
    }
}
