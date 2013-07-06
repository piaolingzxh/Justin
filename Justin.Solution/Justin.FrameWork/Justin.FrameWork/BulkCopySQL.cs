using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Justin.FrameWork
{
    public class BulkCopySQL : IBulkCopy
    {
        public void Insert(DbConnection conn, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null, DataRowState state = DataRowState.Added)
        {
            SqlConnection sqlConn = conn as SqlConnection;
            Check(conn);

            using (SqlBulkCopy sqlBC = new SqlBulkCopy(sqlConn))
            {
                sqlBC.BatchSize = 10000;
                sqlBC.BulkCopyTimeout = 600;
                sqlBC.DestinationTableName = tableName;
                if (columnMappings != null && columnMappings.Count > 0)
                {
                    foreach (var item in columnMappings)
                    {
                        sqlBC.ColumnMappings.Add(item.Value, item.Key);
                    }
                }
                else
                {
                    foreach (var item in sourceData.Columns.Cast<DataColumn>())
                    {
                        sqlBC.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                    }

                }
                sqlBC.WriteToServer(sourceData, state);
            }

        }


        public void Insert(string connectionString, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null, DataRowState state = DataRowState.Added)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            Insert(sqlConn, tableName, sourceData, columnMappings, state);
        }

        public void TruncateTable(DbConnection conn, string tableName)
        {
            Check(conn);
            SqlConnection sqlConn = conn as SqlConnection;
            SqlCommand cmd = new SqlCommand(string.Format("truncate table {0}", tableName), sqlConn);
            cmd.CommandTimeout = 600;
            cmd.ExecuteNonQuery();
        }

        public void Check(DbConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
