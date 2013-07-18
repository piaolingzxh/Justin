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
    public class OleDbHelper
    {
        #region OleDb Operation

        public static OleDbConnection GetOleDbConnection(string oledbConnectionString)
        {
            OleDbConnection conn = new OleDbConnection(oledbConnectionString);
            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(string oleDbConnString, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(oleDbConnString))
            {
                return ExecuteNonQuery(conn, cmdText);
            }
        }
        public static int ExecuteNonQuery(OleDbConnection connection, string cmdText)
        {
            OleDbCommand cmd = PrepareOleDbCommand(connection, null, cmdText);
            return cmd.ExecuteNonQuery();
        }
        public static int ExecuteNonQuery(OleDbTransaction trans, string cmdText)
        {
            OleDbCommand cmd = PrepareOleDbCommand(trans.Connection, trans, cmdText);
            return cmd.ExecuteNonQuery();

        }
        public static object ExecuteScalar(string oleDbConnString, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(oleDbConnString))
            {
                return ExecuteScalar(conn, cmdText);
            }
        }
        public static object ExecuteScalar(OleDbConnection connection, string cmdText)
        {
            OleDbCommand cmd = PrepareOleDbCommand(connection, null, cmdText);
            object val = cmd.ExecuteScalar();
            return val;
        }
        public static DataTable ExecuteDataTable(string oleDbConnString, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(oleDbConnString))
            {
                return ExecuteDataTable(conn, cmdText);
            }
        }
        public static DataTable ExecuteDataTable(OleDbConnection connection, string cmdText)
        {
            OleDbCommand cmd = PrepareOleDbCommand(connection, null, cmdText);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public static OleDbDataReader ExecuteReader(string oleDbConnString, string cmdText)
        {
            OleDbConnection conn = new OleDbConnection(oleDbConnString);
            return ExecuteReader(conn, cmdText);
        }
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, string cmdText)
        {
            try
            {
                OleDbCommand cmd = PrepareOleDbCommand(connection, null, cmdText);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
        private static OleDbCommand PrepareOleDbCommand(OleDbConnection connection, OleDbTransaction trans, string cmdText)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            OleDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 600;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public static void BulkInsert(OleDbConnection conn, string tableName, DataTable sourceData, DataRowState state, Dictionary<string, string> columnMappings = null)
        {
            BulkCopy bulkCopy = new BulkCopy(conn);
            bulkCopy.Insert(tableName, sourceData, state, columnMappings);

        }

        public static void BulkInsert(OleDbConnection conn, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null)
        {
            BulkCopy bulkCopy = new BulkCopy(conn);
            bulkCopy.Insert(tableName, sourceData, columnMappings);

        }

        #endregion
    }
}
