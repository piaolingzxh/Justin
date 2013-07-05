using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL
{
    #region Args

    public class Args
    {
        public string ConnStringName { get; set; }
        public OleDbConnection Conn { get; set; }
    }

    public class QueryArgs : Args
    {
        public string CommandText { get; set; }
        public bool IsQuery { get; set; }
    }

    public class BulkCopyArgs : Args
    {
        public string TableName { get; set; }

    }

    #endregion

    public interface IOperate
    {

    }



    public class QueryCommand : CmdBase
    {
        public QueryCommand() { }
        public QueryCommand(string connStringName, string sql)
        {
            this.QueryCommandArgs = new QueryArgs()
            {
                ConnStringName = connStringName,
                CommandText = sql,
                IsQuery = true
            };
        }
        public QueryArgs QueryCommandArgs { get; set; }


        public DataCommand Transform(Func<DataRow, DataRow> transformAction)
        {
            DataCommand dataCmd = new DataCommand(transformAction);
            dataCmd.InnerCommand = this;
            return dataCmd;
        }

    }

    public class DataCommand : CmdBase
    {
        public DataCommand(Func<DataRow, DataRow> transformAction)
        {
            this.TransformAction = transformAction;
        }
        public Func<DataRow, DataRow> TransformAction;

        public DataCommand Transform(Func<DataRow, DataRow> transformAction)
        {
            DataCommand dataCmd = new DataCommand(transformAction);
            dataCmd.InnerCommand = this;
            return dataCmd;
        }
    }

    #region MyRegion
    //public class Command
    //{

    //}
    //public class BulkCopyCommand
    //{
    //    public BulkCopyArgs BulkCopyCommandArgs { get; set; }

    //    public BulkCopyCommand(BulkCopyArgs bulkCopyArgs)
    //    {
    //        this.BulkCopyCommandArgs = bulkCopyArgs;
    //    }
    //    public BulkCopyCommand(string connString, string tableName)
    //    {
    //        this.BulkCopyCommandArgs = new BulkCopyArgs() { ConnStringName = connString, TableName = tableName };
    //    }

    //}

    //public class DbCommand : CmdBase
    //{
    //    public Args DbCommandArgs { get; set; }
    //    private Action<OleDbCommand, DataRow> DbCommandAction;

    //    public override void Execute()
    //    {
    //        if (QueryCommandArgs != null)
    //        {
    //            InnerQuery(QueryCommandArgs);
    //        }


    //    }

    //    private void InnerQuery(QueryArgs args)
    //    {
    //        QueryByPage(args);
    //    }
    //    private string PrepareSQLByPage()
    //    {
    //        return "";
    //    }
    //    private int QueryByPage(QueryArgs args)
    //    {
    //        string sql = PrepareSQLByPage();
    //        DataTable table = DBHelper.ExecuteDataTable(null, sql);
    //        if (table != null && table.Rows.Count > 0)
    //        {

    //            if (this.TransformAction != null)
    //            {
    //                for (int i = 0; i < table.Rows.Count; i++)
    //                {
    //                    TransformAction(table.Rows[i]);
    //                }
    //            }
    //            if (this.DbCommandAction != null)
    //            {
    //                for (int i = 0; i < table.Rows.Count; i++)
    //                {
    //                    OleDbCommand cmd = this.DbCommandArgs.Conn.CreateCommand();
    //                    DbCommandAction(cmd, table.Rows[i]);
    //                }
    //            }
    //            if (BulkCopyCommandArgs != null)
    //            {
    //                table.BulkCopy(this.BulkCopyCommandArgs);
    //            }


    //            return table.Rows.Count;
    //        }

    //        else
    //        {
    //            return 0;
    //        }

    //    }

    //    public static QueryCommand Query(string connStringName, string sql)
    //    {
    //        return new QueryCommand(connStringName, sql);
    //    }

    //    public Command DbCommand(string connString, Action<OleDbCommand, DataRow> dbCommandAction)
    //    {
    //        Args opa = new Args() { ConnStringName = connString };
    //        this.DbCommandArgs = opa;
    //        this.DbCommandAction = dbCommandAction;
    //        return this;
    //    }
    //    public Command BulkCopy(string connString, string tableName)
    //    {
    //        BulkCopyArgs bca = new BulkCopyArgs()
    //        {
    //            ConnStringName = connString,
    //            TableName = tableName
    //        };

    //        this.BulkCopyCommandArgs = bca;
    //        return this;
    //    }

    //}
    #endregion


    public static class Input
    {
        public static Command GetNewCommand()
        {
            return new Command();
        }
        public static void BulkCopy(this DataTable table, string connStringName, string dstTableName)
        {

        }
        public static void BulkCopy(this DataTable table, BulkCopyArgs bulkCopyArgs)
        {

        }

        public static OleDbCommand DbCommand(this OleDbConnection conn)
        {
            return conn.CreateCommand();
        }
        public static void AddParameter(this OleDbCommand command, string name, object val)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = val ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        public static void Main()
        {
            Input
                .GetNewCommand()
                .Query("input", "SELECT * FROM D_CLIENT")
                .Transform(row => { return row; })
                .DbCommand("output", (cmd, row) =>
                    {
                        cmd.CommandText = @"
    INSERT INTO 
    Client (CLIENT_KEY,SYS_DISPLAY,SYS_LOAD_TIME,SYS_END_TIME,SYS_START_TIME,SYS_KEY) 
    VALUES (@CLIENT_KEY,@SYS_DISPLAY,@SYS_LOAD_TIME,@SYS_END_TIME,@SYS_START_TIME,@SYS_KEY)";
                        cmd.AddParameter("CLIENT_KEY", row["CLIENT_KEY"]);
                        cmd.AddParameter("SYS_DISPLAY", row["SYS_DISPLAY"]);
                        cmd.AddParameter("SYS_LOAD_TIME", row["SYS_LOAD_TIME"]);
                        cmd.AddParameter("SYS_END_TIME", row["SYS_END_TIME"]);
                        cmd.AddParameter("SYS_START_TIME", row["SYS_START_TIME"]);
                        cmd.AddParameter("SYS_KEY", row["SYS_KEY"]);
                    }
                ).BulkCopy("output", "client")
                .Execute();


        }
    }



}
