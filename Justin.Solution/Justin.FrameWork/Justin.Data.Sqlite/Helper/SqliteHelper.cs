using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.SQLite;
using System.Data;

namespace Justin.FrameWork.Helper
{
   /// <summary>
    /// Sqlite访问数据库帮助类
   /// </summary>
    public abstract class SqliteHelper
    {

        public static string ConnStr;//"Data Source=myDBName;Version=3;Pooling=true;Max Pool Size=100;"
       
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
         
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(SQLiteConnection connection, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static int ExecuteNonQuery(SQLiteTransaction trans, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandTimeout = 600;
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static  SQLiteDataReader ExecuteReader(SQLiteConnection conn, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                 SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }

            catch
            {
                conn.Close();
                throw;
            }
        }
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(); using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }

        }
        public static object ExecuteScalar(SQLiteConnection connection, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static DataTable ExecuteDataTable(SQLiteConnection connection, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {

            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            SQLiteDataAdapter MyAdapter = new SQLiteDataAdapter();
            MyAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            MyAdapter.Fill(ds);
            cmd.Parameters.Clear();
            DataTable table = ds.Tables[0];
            ds.Dispose();
            connection.Close();
            return table;
        }
        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SQLiteDataAdapter MyAdapter = new SQLiteDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                MyAdapter.Fill(ds);
                cmd.Parameters.Clear();
                DataTable table = ds.Tables[0];
                ds.Dispose();
                conn.Close();
                return table;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }


        public static void CacheParameters(string cacheKey, params SQLiteParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        public static SQLiteParameter[] GetCachedParameters(string cacheKey)
        {

            SQLiteParameter[] cachedParms = (SQLiteParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SQLiteParameter[] clonedParms = new SQLiteParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SQLiteParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, CommandType cmdType, string cmdText, SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);

            }

        }

    }
}
