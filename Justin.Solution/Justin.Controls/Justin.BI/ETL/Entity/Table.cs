using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.FrameWork.Entities;

namespace Justin.BI.ETL.Entity
{
    public class Table : ETLSource
    {
        public Table()
        {
            this.PrimaryKeys = new List<PrimaryKey>();
            this.ForeKeys = new List<ForeKey>();
            this.Fields = new List<Field>();
            this.OrderBy = new List<Field>();
        }

        public List<PrimaryKey> PrimaryKeys { get; set; }
        public List<Field> Fields { get; set; }
        public List<ForeKey> ForeKeys { get; set; }

        public override SerializableDictionary<string, string> GetDeafultColumnMapping()
        {
            SerializableDictionary<string, string> columnMapping = new SerializableDictionary<string, string>();

            IEnumerable<Field> fields = this.PrimaryKeys.Union(this.Fields).Union(this.ForeKeys).Where(row => row.Enable);

            foreach (var item in fields.Select(row => row.Name).Distinct())
            {
                columnMapping.Add(item, item);
            }
            return columnMapping;
        }
        public override string ToQuerySQL(int pageSize, int pageIndex)
        {
            string pagedSQL = @"select {2} from
 (select t99999.*, Row_NUMBER() over(order by {1} desc) as num from {0} t99999) t99998
where num between {3} and {4}
order by {1} desc";
            StringBuilder sb = new StringBuilder();

            IEnumerable<Field> fields = this.PrimaryKeys.Union(this.Fields).Union(this.ForeKeys).Where(row => row.Enable);

            foreach (var item in fields.Select(row => row.Name).Distinct())
            {
                sb.AppendFormat("{0},", item);
            }
            if (this.OrderBy != null || this.OrderBy.Count == 0)
            {
                foreach (var item in this.PrimaryKeys)
                {
                    this.OrderBy.Add((Field)item);
                }
            }

            return string.Format(pagedSQL, this.Name, this.OrderBy[0].Name, sb.Remove(sb.Length - 1, 1).ToString(), pageSize * pageIndex + 1, pageSize * (pageIndex + 1));
        }
    }
}
