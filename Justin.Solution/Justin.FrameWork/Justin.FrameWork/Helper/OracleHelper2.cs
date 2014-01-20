using System;
using System.Collections;
using System.Configuration;
using System.Data;
using Justin.FrameWork.Extensions;
using Oracle.DataAccess.Client;

namespace Justin.FrameWork.Helper
{
    public abstract class OracleHelper2
    {

        // Read the connection strings from the configuration file
        public static readonly string ConnStr;//"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.80.204)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=IIC_DC_NC;Password=iic*dctabs201b;min pool size=4;max pool size=4";
        public static int CommandTimeout = 600;

        //Create a hashtable for the parameter cached
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a database query which does not include a select
        /// </summary>
        /// <param name="connectionString">Connection string to database</param>
        /// <param name="cmdType">Command type either stored procedure or SQL</param>
        /// <param name="cmdText">Acutall SQL Command</param>
        /// <param name="commandParameters">Parameters to bind to the command</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            // Create a new Oracle command
            OracleCommand cmd = new OracleCommand();

            //Create a connection
            using (OracleConnection connection = new OracleConnection(connectionString))
            {

                //Prepare the command
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the command
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against an existing database transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing database transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }



        /// <summary>
        /// Execute an OracleCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        ///	<summary>
        ///	Execute	a OracleCommand (that returns a 1x1 resultset)	against	the	specified SqlTransaction
        ///	using the provided parameters.
        ///	</summary>
        ///	<param name="transaction">A	valid SqlTransaction</param>
        ///	<param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        ///	<param name="commandText">The stored procedure name	or PL/SQL command</param>
        ///	<param name="commandParameters">An array of	OracleParamters used to execute the command</param>
        ///	<returns>An	object containing the value	in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked	or commited, please	provide	an open	transaction.", "transaction");

            // Create a	command	and	prepare	it for execution
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            // Execute the command & return	the	results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters	from the command object, so	they can be	used again
            cmd.Parameters.Clear();
            return retval;
        }
        /// <summary>
        /// Execute an OracleCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// Execute a select query that will return a result set
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        ///<param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {

            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = new OracleConnection(connectionString);

            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the query, stating that the connection should close when the resulting datareader has been read
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch
            {
                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                connection.Close();
                throw;
            }
        }
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the query, stating that the connection should close when the resulting datareader has been read
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                throw;
            }
        }



        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand();

                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    OracleDataAdapter MyAdapter = new OracleDataAdapter();
                    MyAdapter.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    MyAdapter.Fill(ds);
                    cmd.Parameters.Clear();
                    DataTable table = ds.Tables[0];
                    ds.Dispose();
                    connection.Close();
                    return table;
                }
                catch
                {
                    connection.Close();
                    throw;
                }
            }
        }
        public static DataTable ExecuteDataTable(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            OracleDataAdapter MyAdapter = new OracleDataAdapter();
            MyAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            MyAdapter.Fill(ds);
            cmd.Parameters.Clear();
            DataTable table = ds.Tables[0];
            ds.Dispose();

            return table;
        }



        /// <summary>
        /// Add a set of parameters to the cached
        /// </summary>
        /// <param name="cacheKey">Key value to look up the parameters</param>
        /// <param name="commandParameters">Actual parameters to cached</param>
        public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Fetch parameters from the cache
        /// </summary>
        /// <param name="cacheKey">Key to look up the parameters</param>
        /// <returns></returns>
        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            // If the parameters are in the cache
            OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

            // return a copy of the parameters
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="connection">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection connection, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (connection.State != ConnectionState.Open)
                connection.Open();

            //Set up the command
            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = CommandTimeout;
            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static string OraBit(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static bool OraBool(string value)
        {
            if (value.Equals("Y"))
                return true;
            else
                return false;
        }

        public static DataSet ExecutePageing(string connectionString, string queryDataString, int pageSize, int currentPageIndex, ref int totalRows, ref int totalPages)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand oracleCommand;
                DataSet dataSet = new DataSet();
                try
                {
                    oracleCommand = new OracleCommand();
                    oracleCommand.Connection = connection;
                    connection.Open();
                    oracleCommand.CommandText = "ExecutePageing";
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paras = new OracleParameter[5];
                    paras[0] = new OracleParameter("sqlStr", System.Data.OracleClient.OracleType.VarChar);
                    paras[1] = new OracleParameter("pageSize", System.Data.OracleClient.OracleType.Number);
                    paras[2] = new OracleParameter("currentPageIndex", System.Data.OracleClient.OracleType.Number);
                    paras[3] = new OracleParameter("totalRows", System.Data.OracleClient.OracleType.Number);
                    paras[4] = new OracleParameter("totalPages", System.Data.OracleClient.OracleType.Number);

                    paras[0].Value = queryDataString;
                    paras[1].Value = pageSize;
                    paras[2].Value = currentPageIndex;
                    paras[3].Value = totalRows;
                    paras[4].Value = totalPages;

                    paras[0].Direction = ParameterDirection.Input;
                    paras[1].Direction = ParameterDirection.Input;
                    paras[2].Direction = ParameterDirection.Input;
                    paras[3].Direction = ParameterDirection.Output;
                    paras[4].Direction = ParameterDirection.Output;
                    oracleCommand.Parameters.AddRange(paras);

                    OracleDataAdapter adapter = new OracleDataAdapter(oracleCommand);

                    adapter.Fill(dataSet);
                    totalRows = paras[3].Value.GetInt();
                    totalPages = paras[4].Value.GetInt();

                    return dataSet;
                }
                catch (Exception ex)
                {
                    System.Console.Write(ex.Message);
                    throw ex;
                }
            }
        }

        public static OracleConnection GetConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }
    }
}
