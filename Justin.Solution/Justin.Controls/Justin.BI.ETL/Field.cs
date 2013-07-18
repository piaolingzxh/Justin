using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Justin.FrameWork.Entities;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Oracle.DataAccess.Client;
namespace Justin.BI.ETL
{
    public class Field
    {
        public Field()
        {
            this.Enable = true;
        }
        public Field(string name, DbType dbType, int length)
        {
            this.Name = name;
            this.Length = length;
            this.FieldType = dbType;
            this.Enable = true;
        }
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public DbType FieldType { get; set; }
        [XmlAttribute()]
        public int Length { get; set; }
        [XmlAttribute()]
        public bool Enable { get; set; }
    }

    public class PrimaryKey : Field
    {

    }
    public class ForeKey : Field
    {
        public Table Referencetable { get; set; }
    }

    [XmlInclude(typeof(Table)),
    XmlInclude(typeof(View))]
    public abstract class ETLSource
    {
        public ETLSource() { }

        [XmlAttribute()]
        public string Name { get; set; }
        public List<Field> OrderBy { get; set; }


        public abstract string ToQuerySQL(int pageSize, int pageIndex);
        public abstract SerializableDictionary<string, string> GetDeafultColumnMapping();
    }

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

        private DbConnection Connection { get; set; }
        [XmlElement()]
        public string SQL { get; set; }
        public override SerializableDictionary<string, string> GetDeafultColumnMapping()
        {
            SerializableDictionary<string, string> columnMapping = new SerializableDictionary<string, string>();
            string sql = this.SQL + " where 1=0";
            DataTable table = DBHelper.ExecuteDataTable(this.Connection, sql);

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

    public class ETLInfo
    {
        public ETLInfo()
        {
            this.ColumnMapping = new SerializableDictionary<string, string>();
        }
        public ETLInfo(ETLSource source, string dstTableName)
            : this()
        {
            this.SourceTable = source;
            this.DestinationTableName = dstTableName;

            this.ColumnMapping = SourceTable.GetDeafultColumnMapping();
        }

        [XmlAttribute()]
        public string DestinationTableName { get; set; }
        public ETLSource SourceTable { get; set; }
        public SerializableDictionary<string, string> ColumnMapping { get; set; }
    }
}
