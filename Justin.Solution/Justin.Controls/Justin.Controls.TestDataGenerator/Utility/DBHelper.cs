using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Justin.Controls.TestDataGenerator.Entities;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Controls.TestDataGenerator.Utility
{
    public class DBHelper
    {
        public static string ConnStr = "";
        public static void ExecuteNonQuery(IDbConnection conn, string sql, SqlSyntax syntax)
        {
            switch (syntax)
            {
                case SqlSyntax.MSSQL:
                    SqlHelper.ExecuteNonQuery((SqlConnection)conn, CommandType.Text, sql, null);
                    break;
                case SqlSyntax.Oracle:

                    break;
                case SqlSyntax.Mdx:
                    MdxHelper.ExecuteNonQuery((AdomdConnection)conn, sql);
                    break;
            }

        }
        public static object ExecuteScalar(IDbConnection conn, string sql, SqlSyntax syntax)
        {
            switch (syntax)
            {
                case SqlSyntax.MSSQL:
                    return SqlHelper.ExecuteScalar((SqlConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.Oracle:
                    return OracleHelper.ExecuteScalar((OracleConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.Mdx:

                    return MdxHelper.ExecuteScalar((AdomdConnection)conn, sql);
                default: return null;
            }


        }
        public static DataTable ExecuteDataTable(IDbConnection conn, string sql, SqlSyntax syntax)
        {
            switch (syntax)
            {
                case SqlSyntax.MSSQL:
                    return SqlHelper.ExecuteDataTable((SqlConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.Oracle:
                    return OracleHelper.ExecuteDataTable((OracleConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.Mdx:
                    CellSet cellSet = MdxHelper.ExecuteCellSet((AdomdConnection)conn, sql);
                    if (cellSet != null)
                    {
                        return cellSet.ToDataTable();
                    }
                    return null;
                default: return null;
            }
        }
        public static IDataReader ExecuteReader(IDbConnection conn, string sql, SqlSyntax syntax)
        {
            switch (syntax)
            {
                case SqlSyntax.MSSQL:
                    return SqlHelper.ExecuteReader((SqlConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.Oracle:
                    return OracleHelper.ExecuteReader((OracleConnection)conn, CommandType.Text, sql, null);
                case SqlSyntax.GSQL:
                    return null;
                case SqlSyntax.Mdx:
                    return MdxHelper.ExecuteReader((AdomdConnection)conn, sql);
                default: return null;
            }
        }

        public static IDbConnection GetConnection(SqlSyntax syntax, string connstr)
        {
            switch (syntax)
            {
                case SqlSyntax.MSSQL:
                    return SqlHelper.GetConnection(connstr);
                case SqlSyntax.Oracle:
                    return OracleHelper.GetConnection(connstr);
                case SqlSyntax.Mdx:
                    return MdxHelper.GetConnection(connstr);
                default: return null;
            }
        }
    }
}
