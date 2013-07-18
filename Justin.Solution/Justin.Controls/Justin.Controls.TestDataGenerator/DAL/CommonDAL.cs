using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin.FrameWork.Helper;

namespace Justin.Controls.TestDataGenerator.DAL
{
    public class CommonDAL
    {
        const string format_MSSQL_GetValueByTableNameAndColumnName = "select top 1 {1} as fieldValue from {0} where {2} order by newid()";

        const string format_Oracle_GetValueByTableNameAndColumnName = @"select distinct {1}  from (select * from {0} order by dbms_random.random) where rownum =1 and {2}";
        public static object GetValue(OleDbConnection conn, string tableName, string fieldName, string filter)
        {
            string sql = string.Format(conn.GetDataBaseType() == DataBaseType.MSSQL ? format_MSSQL_GetValueByTableNameAndColumnName : format_Oracle_GetValueByTableNameAndColumnName
                , tableName, fieldName, string.IsNullOrEmpty(filter) ? "1=1" : filter);
            object fieldValue = DBHelper.ExecuteScalar(conn, sql);
            return fieldValue;
        }
    }
}
