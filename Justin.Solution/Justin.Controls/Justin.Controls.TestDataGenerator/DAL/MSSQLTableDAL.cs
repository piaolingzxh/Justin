using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin.FrameWork.Helper;
using Justin.Controls.TestDataGenerator.Entities;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace Justin.Controls.TestDataGenerator.DAL
{
    public class MSSQLTableDAL : TableDAL
    {
        #region  format

        string sql_Get_All_Tables = "select name from sysobjects where type= 'u' and name !='sysdiagrams'";
        string format_Get_All_Fields = @"select c.name as fieldName, t.name as fieldType, c.length as fieldLength ,c.isnullable as allowNull
from 
syscolumns c inner join 
sysobjects o on c.id = o.id and o.xtype = 'u' inner join 
systypes t on c.xtype = t.xtype 
where o.name='{0}' and t.name!='sysname'";
        string format_Get_All_PrimaryKeys = @"EXEC sp_pkeys @table_name='{0}'";
        string format_GetAllIdentityColumn = @"
declare @Table_name varchar(60)
set @Table_name = '{0}';
Select so.name Table_name, --表名字
sc.name Iden_Column_name, --自增字段名字
ident_current(so.name) curr_value, --自增字段当前值
ident_incr(so.name) incr_value, --自增字段增长值
ident_seed(so.name) seed_value --自增字段种子值
from sysobjects so 
Inner Join syscolumns sc
on so.id = sc.id
and columnproperty(sc.id, sc.name, 'IsIdentity') = 1
Where upper(so.name) = upper(@Table_name)
";

        string format_Get_All_ForeignKeys = @"
select
a.name as refName,
object_name(b.parent_object_id) as tableName,
c.name as columnName,
object_name(b.referenced_object_id) as  refTableName,
d.name as refColumnName
from sys.foreign_keys A
inner join sys.foreign_key_columns B on A.object_id=b.constraint_object_id
inner join sys.columns C on B.parent_object_id=C.object_id and B.parent_column_id=C.column_id 
inner join sys.columns D on B.referenced_object_id=d.object_id and B.referenced_column_id=D.column_id 
where object_name(B.parent_object_id)='{0}';

";

        #endregion

        public MSSQLTableDAL(string oleDbConnstr) : base(oleDbConnstr) { }

        protected override string SQL_GET_ALL_Tables
        {
            get { return this.sql_Get_All_Tables; }
        }
        protected override string Format_GET_ALL_FIELDS
        {
            get { return this.format_Get_All_Fields; }
        }
        protected override string Format_GET_ALL_PrimaryKeys
        {
            get { return this.format_Get_All_PrimaryKeys; }
        }
        protected override string Format_GET_ALL_ForeignKeys
        {
            get { return this.format_Get_All_ForeignKeys; }
        }

        protected override void FillPrimaryKey(DBTable table)
        {
            OleDbDataReader rdPK = OleDbHelper.ExecuteReader(this.OleDbConnStr, string.Format(this.Format_GET_ALL_PrimaryKeys, table.TableName));
            //List<DBColumn> fields = new List<DBColumn>();
            OleDbDataReader rdIdentity = OleDbHelper.ExecuteReader(this.OleDbConnStr, string.Format(format_GetAllIdentityColumn, table.TableName));
            string identityColumnName = "";
            if (rdIdentity.HasRows)
            {
                identityColumnName = rdIdentity["Iden_Column_name"].ToString();
            }
            rdIdentity.Close();
            rdIdentity.Dispose();
            while (rdPK.Read())
            {
                string columnName = rdPK["COLUMN_NAME"].ToString();
                DBColumn column = table.Columns.Where(row => row.ColumnName == columnName).FirstOrDefault();
                if (column != null)
                {
                    if (columnName == identityColumnName)
                    {
                        table.SetPrimaryKey(
                            column
                            , true
                            , int.Parse(rdIdentity["seed_value"].ToString())
                            , int.Parse(rdIdentity["curr_value"].ToString())
                            , int.Parse(rdIdentity["incr_value"].ToString())
                            );
                    }
                    else
                    {
                        table.SetPrimaryKey(column, false, 0, 0, 1);
                    }

                }
            }
            rdPK.Close();
            rdPK.Dispose();
        }
    }

    public abstract class TableDAL
    {
        public TableDAL(string oleDbConnstr)
        {
            this.OleDbConnStr = oleDbConnstr;
            OleDbConnectionStringBuilder sb = new OleDbConnectionStringBuilder(this.OleDbConnStr);
            //if (sb.Provider.ToLower() == "sqloledb" || sb.Provider.ToLower() == "oraoledb")
            //{
            //    this.DataBaseType = sb.Provider.ToLower() == "sqloledb" ? DataBaseType.MSSQL : DataBaseType.ORACLE;

            //}
            //else
            //{
            //    throw new Exception("oleDbConnstr格式错误");
            //}
        }

        public DataBaseType DataBaseType { private set; get; }

        public string OleDbConnStr { get; set; }
        protected abstract string SQL_GET_ALL_Tables { get; }
        protected abstract string Format_GET_ALL_FIELDS { get; }
        protected abstract string Format_GET_ALL_PrimaryKeys { get; }
        protected abstract string Format_GET_ALL_ForeignKeys { get; }

        public List<string> GetAllTables()
        {
            OleDbDataReader rd = OleDbHelper.ExecuteReader(this.OleDbConnStr, SQL_GET_ALL_Tables);
            List<string> tableNames = new List<string>();
            while (rd.Read())
            {
                tableNames.Add(rd["name"].ToString());
            }
            rd.Close();
            rd.Dispose();
            return tableNames.OrderBy(row => row).ToList();
        }
        public List<DBTable> GetAllTableSchema(List<string> tableNames)
        {
            //第一步，获取所有表的基本信息：表名

            List<DBTable> tables = new List<DBTable>();
            foreach (var tableName in tableNames)
            {
                tables.Add(new DBTable(tableName));
            }

            foreach (var table in tables)
            {
                //第二步：获取每个表的所有字段信息，并填充
                FillFields(table);

                //第三步:获取每个表的主键信息，并填充
                FillPrimaryKey(table);


                //第四步:获取每个表的外键信息，并填充
                FillForeignKey(table);
            }


            return tables;
        }

        protected virtual void FillFields(DBTable table)
        {
            OleDbDataReader rd = OleDbHelper.ExecuteReader(this.OleDbConnStr, string.Format(this.Format_GET_ALL_FIELDS, table.TableName));
            List<DBColumn> fields = new List<DBColumn>();

            while (rd.Read())
            {
                try
                {
                    DBColumn column = new DBColumn(rd["fieldName"].ToString());
                    string fieldType = rd["fieldType"].ToString();
                    if (this.DataBaseType == DataBaseType.MSSQL)
                    {
                        column.DbType = ((SqlDbType)Enum.Parse(typeof(SqlDbType), fieldType, true)).ToJFielType();
                    }
                    else
                    {
                        column.DbType = ((OracleType)Enum.Parse(typeof(OracleType), fieldType, true)).ToJFielType();
                    }

                    column.Length = int.Parse(rd["fieldLength"].ToString());
                    column.AllowNull = rd["allowNull"].ToString() == "1" ? true : false;
                    fields.Add(column);
                }
                catch (Exception ex)
                {
                }
            }
            rd.Close();
            rd.Dispose();
            table.Columns = fields;
        }
        protected virtual void FillPrimaryKey(DBTable table)
        {
            OleDbDataReader rdPK = OleDbHelper.ExecuteReader(this.OleDbConnStr, string.Format(this.Format_GET_ALL_PrimaryKeys, table.TableName));

            while (rdPK.Read())
            {
                string columnName = rdPK["COLUMN_NAME"].ToString();
                DBColumn column = table.Columns.Where(row => row.ColumnName == columnName).FirstOrDefault();
                if (column != null)
                {
                    table.SetPrimaryKey(column, false, 0, 0, 1);
                }
            }
            rdPK.Close();
            rdPK.Dispose();
        }
        protected virtual void FillForeignKey(DBTable table)
        {
            OleDbDataReader rdfk = OleDbHelper.ExecuteReader(this.OleDbConnStr, string.Format(this.Format_GET_ALL_ForeignKeys, table.TableName));

            while (rdfk.Read())
            {
                string columnName = rdfk["columnName"].ToString();
                string refTableName = rdfk["refTableName"].ToString();
                string refColumnName = rdfk["refColumnName"].ToString();

                DBColumn column = table.Columns.Where(row => row.ColumnName == columnName).FirstOrDefault();
                table.AddForeignKey(column, refTableName, refColumnName);
            }
            rdfk.Close();
            rdfk.Dispose();
        }

    }

    public class OracleTableDAL : TableDAL
    {
        public OracleTableDAL(string oleDbConnstr) : base(oleDbConnstr) { }

        #region format

        string sql_Get_All_Tables = "select table_name as name from user_tables;";
        string format_Get_All_Fields = "select column_name fieldName,data_type fieldType,data_length fieldLength,nullable allowNull from user_tab_columns where Table_Name='{0}';";
        string format_Get_All_PrimaryKeys = @"select   *   from   user_cons_columns 
where   constraint_name   =   (
        select   constraint_name   from   user_constraints  
                 where   table_name   =   '{0}'  and   constraint_type   ='P'
                 );";
        string format_Get_All_ForeignKeys = @"select a.table_name tableName ,
       a.constraint_name  refName,
       (select c.column_name
          from user_cons_columns c
         where c.constraint_name = a.constraint_name) columnName,
       a.r_constraint_name fk_cons_name,
       (select c.table_name
          from user_cons_columns c
         where c.constraint_name = a.r_constraint_name) refTableName,
       (select c.column_name
          from user_cons_columns c
         where c.constraint_name = a.r_constraint_name) refColumnName
  from user_constraints a
where constraint_type in ( 'R')
 and table_name in ('{0}') ";

        #endregion

        protected override string SQL_GET_ALL_Tables
        {
            get { return this.sql_Get_All_Tables; }
        }
        protected override string Format_GET_ALL_FIELDS
        {
            get { return this.format_Get_All_Fields; }
        }
        protected override string Format_GET_ALL_PrimaryKeys
        {
            get { return this.format_Get_All_PrimaryKeys; }
        }
        protected override string Format_GET_ALL_ForeignKeys
        {
            get { return this.format_Get_All_ForeignKeys; }
        }
    }

    public enum DataBaseType
    {
        MSSQL,
        ORACLE
    }

    public static class Extensions
    {
        public static JFieldType ToJFielType(this SqlDbType dbFieldType)
        {
            switch (dbFieldType)
            {
                case SqlDbType.BigInt: return JFieldType.Numeric;
                case SqlDbType.Binary: return JFieldType.String;
                case SqlDbType.Bit: return JFieldType.String;
                case SqlDbType.Char: return JFieldType.String;
                case SqlDbType.DateTime: return JFieldType.DateTime;
                case SqlDbType.Decimal: return JFieldType.Numeric;
                case SqlDbType.Float: return JFieldType.Numeric;
                case SqlDbType.Image: return JFieldType.String;
                case SqlDbType.Int: return JFieldType.Numeric;
                case SqlDbType.Money: return JFieldType.String;
                case SqlDbType.NChar: return JFieldType.String;
                case SqlDbType.NText: return JFieldType.String;
                case SqlDbType.NVarChar: return JFieldType.String;
                case SqlDbType.Real: return JFieldType.String;
                case SqlDbType.UniqueIdentifier: return JFieldType.String;
                case SqlDbType.SmallDateTime: return JFieldType.DateTime;
                case SqlDbType.SmallInt: return JFieldType.Numeric;
                case SqlDbType.SmallMoney: return JFieldType.String;
                case SqlDbType.Text: return JFieldType.String;
                case SqlDbType.Timestamp: return JFieldType.DateTime;
                case SqlDbType.TinyInt: return JFieldType.Numeric;
                case SqlDbType.VarBinary: return JFieldType.String;
                case SqlDbType.VarChar: return JFieldType.String;
                case SqlDbType.Variant: return JFieldType.String;
                case SqlDbType.Xml: return JFieldType.String;
                case SqlDbType.Udt: return JFieldType.String;
                case SqlDbType.Structured: return JFieldType.String;
                case SqlDbType.Date: return JFieldType.DateTime;
                case SqlDbType.Time: return JFieldType.DateTime;
                case SqlDbType.DateTime2: return JFieldType.DateTime;
                case SqlDbType.DateTimeOffset: return JFieldType.DateTime;

            }


            return JFieldType.String;
        }
        public static JFieldType ToJFielType(this OracleType dbFieldType)
        {
            switch (dbFieldType)
            {
                case OracleType.BFile: return JFieldType.String;
                case OracleType.Blob: return JFieldType.String;
                case OracleType.Char: return JFieldType.String;
                case OracleType.Clob: return JFieldType.String;
                case OracleType.Cursor: return JFieldType.String;
                case OracleType.DateTime: return JFieldType.DateTime;
                case OracleType.IntervalDayToSecond: return JFieldType.String;
                case OracleType.IntervalYearToMonth: return JFieldType.String;
                case OracleType.LongRaw: return JFieldType.String;
                case OracleType.LongVarChar: return JFieldType.String;
                case OracleType.NChar: return JFieldType.String;
                case OracleType.NClob: return JFieldType.String;
                case OracleType.Number: return JFieldType.Numeric;
                case OracleType.NVarChar: return JFieldType.String;
                case OracleType.Raw: return JFieldType.String;
                case OracleType.RowId: return JFieldType.String;
                case OracleType.Timestamp: return JFieldType.DateTime;
                case OracleType.TimestampLocal: return JFieldType.DateTime;
                case OracleType.TimestampWithTZ: return JFieldType.DateTime;
                case OracleType.VarChar: return JFieldType.String;
                case OracleType.Byte: return JFieldType.String;
                case OracleType.UInt16: return JFieldType.Numeric;
                case OracleType.UInt32: return JFieldType.Numeric;
                case OracleType.SByte: return JFieldType.String;
                case OracleType.Int16: return JFieldType.Numeric;
                case OracleType.Int32: return JFieldType.Numeric;
                case OracleType.Float: return JFieldType.Numeric;
                case OracleType.Double: return JFieldType.Numeric;

            }
            return JFieldType.String;

        }
        public static DataBaseType GetDataBaseType(this OleDbConnection conn)
        {
            switch (conn.Provider.ToLower())
            {
                case "sqloledb": return DataBaseType.MSSQL; 
                case "sqlncli": return DataBaseType.MSSQL;
                case "msdaora": return DataBaseType.ORACLE; 
                case "oraoledb": return DataBaseType.ORACLE;
            }
            return DataBaseType.MSSQL;

        }
    }
}


