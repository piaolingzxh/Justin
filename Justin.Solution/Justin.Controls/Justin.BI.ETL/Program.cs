using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL
{
    public class Program
    {
        public static void Main()
        {
            ETLServiceTest();
            Console.WriteLine("OK");
            Console.Read();
        }

        public void ShowMsg(int successCount)
        {
            Console.WriteLine(successCount);
        }

        public static void ETLServiceTest()
        {
            #region table

            Table table = new Table() { Name = "D_CLIENT" };

            table.PrimaryKeys.Add(new PrimaryKey() { Name = "SYS_KEY", FieldType = DbType.String });
            table.Fields.Add(new Field() { Name = "CLIENT_KEY", FieldType = DbType.String });
            table.Fields.Add(new Field() { Name = "SYS_DISPLAY", FieldType = DbType.String });
            table.Fields.Add(new Field() { Name = "SYS_LOAD_TIME", FieldType = DbType.DateTime });
            table.Fields.Add(new Field() { Name = "SYS_END_TIME", FieldType = DbType.DateTime });
            table.Fields.Add(new Field() { Name = "SYS_START_TIME", FieldType = DbType.DateTime });

            #endregion

            DbConnection sourceConn = DBHelper.GetConnection(ConfigurationManager.ConnectionStrings["mssql"]);
            DbConnection dstConn = DBHelper.GetConnection(ConfigurationManager.ConnectionStrings["oracle"]);

            string sql = "select CLIENT_KEY,SYS_DISPLAY,SYS_LOAD_TIME,SYS_END_TIME,SYS_START_TIME,SYS_KEY from D_CLIENT";
            View view = new View(sql, sourceConn);
            view.PrimaryKeys.Add(new PrimaryKey() { Name = "SYS_KEY", FieldType = DbType.String });

            ETLInfo etlInfo = new ETLInfo(view, "Client");

            SerializeHelper.XmlSerializeToFile(etlInfo, "table.xml", true);

            new ETLService().Process("table.xml", sourceConn, dstConn, true, new Program().ShowMsg);
        }


        public static void DBHelperTest()
        {
            DbConnection sourceConn = DBHelper.GetConnection(ConfigurationManager.ConnectionStrings["mssql"]);
            DbConnection dstConn = DBHelper.GetConnection(ConfigurationManager.ConnectionStrings["oracle"]);
            DbConnection sqlite = DBHelper.GetConnection(ConfigurationManager.ConnectionStrings["sqlite"]);

            DbConnection testConn = sqlite;

            DbDataReader dr1 = DBHelper.ExecuteReader(testConn, "select * from Client where sys_key='00004302f8af896d'");
            //dr1.Close();
            int e1 = DBHelper.ExecuteNonQuery(testConn, "update Client set sys_display='00004302f8af896d' where sys_key='00004302f8af896d'");

            DataTable dt1 = DBHelper.ExecuteDataTable(testConn, "select * from Client where sys_key='00004302f8af896d'");
            object o1 = DBHelper.ExecuteScalar(testConn, "select count(*) from Client");

        }
    }
}
