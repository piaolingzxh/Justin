using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.Controls.TestDataGenerator.Entities
{
    public class DBTable
    {

        public string TableName { get; set; }
        public DBPrimaryKey PrimaryKey { get; private set; }
        public List<DBForeignKey> ForeignKeys { get; private set; }
        public List<DBColumn> Columns { get; set; }

        public DBTable(string tableName)
        {
            this.TableName = tableName;
            this.Columns = new List<DBColumn>();
            this.ForeignKeys = new List<DBForeignKey>();
        }

        public void SetPrimaryKey(DBColumn column, bool isIdentity, int seed, int currentValue, int step)
        {
            this.PrimaryKey = new DBPrimaryKey(column, isIdentity, seed, currentValue, step);
            this.Columns.Remove(column);
        }
        public void AddForeignKey(DBColumn column, string refTableName, string refColumnName)
        {
            this.ForeignKeys.Add(new DBForeignKey(column, refTableName, refColumnName));
            this.Columns.Remove(column);
        }
    }

    public class DBForeignKey : DBColumn
    {
        public string ReferenceTableName { get; private set; }
        public string ReferenceColumnName { get; private set; }

        public DBForeignKey(DBColumn column, string reftableName, string refColumnName)
            : this(column)
        {
            this.ReferenceColumnName = refColumnName;
            this.ReferenceTableName = reftableName;
        }
        private DBForeignKey(DBColumn column)
        {
            this.ColumnName = column.ColumnName;
            this.DbType = column.DbType;
            this.Length = column.Length;
            this.AllowNull = column.AllowNull;
        }

    }
    public class DBPrimaryKey : DBColumn
    {
        public bool IsIdentity { get; private set; }
        public int Seed { get; private set; }
        public int CurrentValue { get; private set; }
        public int Step { get; private set; }

        public DBPrimaryKey(DBColumn column, bool isIdentity, int seed, int currentValue, int step)
            : this(column)
        {
            this.IsIdentity = isIdentity;
            this.Seed = seed;
            this.CurrentValue = currentValue;
            this.Step = step;
        }
        private DBPrimaryKey(DBColumn column)
        {
            this.ColumnName = column.ColumnName;
            this.DbType = column.DbType;
            this.Length = column.Length;
        }

    }
    public class DBColumn
    {
        public string ColumnName { get; set; }
        public SqlDbType DbType { get; set; }
        public int Length { get; set; }
        public bool AllowNull { get; set; }

        public DBColumn(string columnName)
        {
            this.ColumnName = columnName;
        }
        public DBColumn()
        { }
    }
}
