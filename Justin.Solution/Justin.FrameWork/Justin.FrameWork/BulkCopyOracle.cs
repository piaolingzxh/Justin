using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Justin.FrameWork
{
    public class BulkCopyOracle : IBulkCopy
    {
        public void Insert(DbConnection conn, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null, DataRowState state = DataRowState.Added)
        {
            OracleConnection oracleConn = conn as OracleConnection;
            Check(oracleConn);

            using (OracleBulkCopyWrapper sqlBC = new OracleBulkCopyWrapper(oracleConn))
            {
                sqlBC.BatchSize = 10000;
                sqlBC.BulkCopyTimeout = 1200;
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
            OracleConnection oracleConn = new OracleConnection(connectionString);
            Insert(oracleConn, tableName, sourceData, columnMappings, state);
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
                throw new Exception(string.Format("连接数据库出错。ConnectionString:{0}", conn.ConnectionString), ex); ;
            }
        }
    }
}
