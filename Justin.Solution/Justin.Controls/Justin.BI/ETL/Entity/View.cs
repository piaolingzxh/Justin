using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Justin.FrameWork.Entities;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL.Entity
{
    public class View : ETLSource
    {
        public View()
        {
            this.OrderBy = new List<Field>();
        }
        public View(string sql, string oleDbConnString)
            : this()
        {
            this.SQL = sql;
            this.Connection = new OleDbConnection(oleDbConnString);
        }

        private OleDbConnection Connection { get; set; }
        [XmlElement()]
        public string SQL { get; set; }
        public override SerializableDictionary<string, string> GetDeafultColumnMapping()
        {
            SerializableDictionary<string, string> columnMapping = new SerializableDictionary<string, string>();
            string sql = this.SQL + " where 1=0";
            DataTable table = OleDbHelper.ExecuteDataTable(this.Connection, sql);

            IEnumerable<DataColumn> fields = table.Columns.Cast<DataColumn>().Where(col => col.ColumnName != "_row_num");

            foreach (var item in fields.Select(row => row.ColumnName).Distinct())
            {
                columnMapping.Add(item, item);
            }
            return columnMapping;
        }
        public override string ToQuerySQL(int pageSize, int pageIndex)
        {

            string pagedSQL = @"select * from
 (select t99999.*, Row_NUMBER() over(order by {1} desc) as _row_num from {0} t99999) t99998
where _row_num between {2} and {3}
order by {1} desc";

            return string.Format(pagedSQL, "(" + this.SQL + ")", this.OrderBy[0].Name, pageSize * pageIndex + 1, pageSize * (pageIndex + 1));
        }
    }
}
