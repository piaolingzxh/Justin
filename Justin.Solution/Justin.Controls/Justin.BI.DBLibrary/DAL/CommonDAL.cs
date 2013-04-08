using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin.FrameWork.Helper;

namespace Justin.BI.DBLibrary.DAL
{
    public class CommonDAL
    {
        const string format_GetValueByTableNameAndColumnName = @"select distinct {1} as fieldValue from {0}";

        public static List<object> GetValues(SqlConnection conn, string tableName, string fieldName, string filter)
        {
            string sql = string.Format(format_GetValueByTableNameAndColumnName, tableName, fieldName);
            if (!string.IsNullOrEmpty(filter))
            {
                sql = sql + " where " + filter;
            }
            SqlDataReader rd = SqlHelper.ExecuteReader(conn, CommandType.Text, sql, null);
            List<object> list = new List<object>();
            while (rd.Read())
            {
                list.Add(rd["fieldValue"]);
            }

            rd.Close();
            rd.Dispose();
            return list;
        }
    }
}
