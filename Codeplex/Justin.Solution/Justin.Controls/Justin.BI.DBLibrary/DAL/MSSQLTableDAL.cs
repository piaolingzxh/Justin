using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin.FrameWork.Helper;
using Justin.BI.DBLibrary.TestDataGenerate;

namespace Justin.BI.DBLibrary.DAL
{
    public class MSSQLTableDAL
    {
        #region  format

        string format_getAllTables = "select name from sysobjects where type= 'u'";
        string format_getAllFields = @"select c.name as fieldName, t.name as fieldType, c.length as fieldLength ,c.isnullable as allowNull
from 
syscolumns c inner join 
sysobjects o on c.id = o.id and o.xtype = 'u' inner join 
systypes t on c.xtype = t.xtype 
where o.name='{0}'";
        string format_getAllPrimaryKey = @"EXEC sp_pkeys @table_name='{0}'";
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

        string format_getAllForeignKeyByTable = @"
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

        private string ConnStr { get; set; }
        public MSSQLTableDAL(string connStr)
        {
            this.ConnStr = connStr;
        }
        public List<string> GetAllTables()
        {
            SqlDataReader rd = SqlHelper.ExecuteReader(new SqlConnection(this.ConnStr), CommandType.Text, format_getAllTables, null);
            List<string> tableNames = new List<string>();
            while (rd.Read())
            {
                string tableName = rd["name"].ToString();
                if (tableName != "sysdiagrams")
                {
                    tableNames.Add(tableName);
                }
            }
            rd.Close();
            rd.Dispose();
            return tableNames.OrderBy(row => row).ToList();
        }
        public List<DBTable> GetAllTableSchema(List<string> tableNames)
        {
            //第一步，获取所有表的基本信息：表明
            //SqlDataReader rd = SqlHelper.ExecuteReader(new SqlConnection(ConnStr), CommandType.Text, format_getAllTables, null);
            //List<DBTable> tables = new List<DBTable>();
            //while (rd.Read())
            //{
            //    string tableName = rd["name"].ToString();
            //    if (tableName != "sysdiagrams")
            //    {
            //        tables.Add(new DBTable(tableName));
            //    }
            //}

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
        private void FillFields(DBTable table)
        {
            SqlDataReader rd = SqlHelper.ExecuteReader(new SqlConnection(this.ConnStr), CommandType.Text, string.Format(format_getAllFields, table.TableName), null);
            List<DBColumn> fields = new List<DBColumn>();

            while (rd.Read())
            {
                try
                {
                    DBColumn column = new DBColumn(rd["fieldName"].ToString());
                    string sqlDBType = rd["fieldType"].ToString();
                    column.DbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), sqlDBType, true);
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

        private void FillPrimaryKey(DBTable table)
        {
            SqlDataReader rdPK = SqlHelper.ExecuteReader(new SqlConnection(this.ConnStr), CommandType.Text, string.Format(format_getAllPrimaryKey, table.TableName), null);
            //List<DBColumn> fields = new List<DBColumn>();
            SqlDataReader rdIdentity = SqlHelper.ExecuteReader(new SqlConnection(this.ConnStr), CommandType.Text, string.Format(format_GetAllIdentityColumn, table.TableName), null);
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
            //table.Columns = fields;
        }
        private void FillForeignKey(DBTable table)
        {
            SqlDataReader rdfk = SqlHelper.ExecuteReader(new SqlConnection(this.ConnStr), CommandType.Text, string.Format(format_getAllForeignKeyByTable, table.TableName), null);

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
}


