using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Helper
{
    public class DBHelper
    {
        public static int ExecuteNonQuery(DbConnection connection, string cmdText)
        {
            DbCommand cmd = PrepareCommand(connection, null, cmdText);
            return cmd.ExecuteNonQuery();
        }
        public static int ExecuteNonQuery(DbTransaction trans, string cmdText)
        {
            DbCommand cmd = PrepareCommand(trans.Connection, trans, cmdText);
            return cmd.ExecuteNonQuery();

        }
        public static object ExecuteScalar(DbConnection connection, string cmdText)
        {
            DbCommand cmd = PrepareCommand(connection, null, cmdText);
            object val = cmd.ExecuteScalar();
            return val;
        }

        public static DataTable ExecuteDataTable(DbConnection connection, string cmdText)
        {
            DbDataReader reader = ExecuteReader(connection, cmdText);
            DataTable table = ConvertDataReaderToDataTable(reader);
            return table;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="connection" type="System.Data.Common.DbConnection">
        ///     <para>
        ///            connection为SqlConnection时，此方法需要独占一个Connection，直到Reader.CLose()
        ///     </para>
        /// </param>
        /// <param name="cmdText" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.Common.DbDataReader value...
        /// </returns>
        public static DbDataReader ExecuteReader(DbConnection connection, string cmdText)
        {
            try
            {
                DbCommand cmd = PrepareCommand(connection, null, cmdText);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
                                                                                              
        private static DbCommand PrepareCommand(DbConnection connection, DbTransaction trans, string cmdText)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 600;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public static DataTable ConvertDataReaderToDataTable(DbDataReader reader)
        {
            try
            {
                DataTable table = new DataTable();
                int fieldCount = reader.FieldCount;
                for (int fieldIndex = 0; fieldIndex < fieldCount; ++fieldIndex)
                {
                    table.Columns.Add(reader.GetName(fieldIndex), reader.GetFieldType(fieldIndex));
                }

                table.BeginLoadData();

                object[] rowValues = new object[fieldCount];
                while (reader.Read())
                {
                    reader.GetValues(rowValues);
                    table.LoadDataRow(rowValues, true);
                }
                reader.Close();
                table.EndLoadData();

                return table;

            }
            catch (Exception ex)
            {
                throw new Exception("DataReader转换为DataTable时出错!", ex);
            }

        }

        public static void Insert(DbConnection conn, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null, DataRowState state = DataRowState.Added)
        {
            IBulkCopy bulkCopy = null;

            if (conn is SqlConnection)
            {
                bulkCopy = new BulkCopySQL();
            }
            else if (conn is Oracle.DataAccess.Client.OracleConnection)
            {
                bulkCopy = new BulkCopyOracle();
            }
            if (bulkCopy == null)
            {
                throw new Exception("不支持该Connection");
            }
            bulkCopy.Insert(conn, tableName, sourceData, columnMappings, state);
        }
        public static void InsertTruncateTable(DbConnection conn, string tableName)
        {
            IBulkCopy bulkCopy = null;

            if (conn is SqlConnection)
            {
                bulkCopy = new BulkCopySQL();
            }
            else if (conn is Oracle.DataAccess.Client.OracleConnection)
            {
                bulkCopy = new BulkCopyOracle();
            }
            if (bulkCopy == null)
            {
                throw new Exception("不支持该Connection");
            }
            bulkCopy.TruncateTable(conn, tableName);
        }


    }
}
