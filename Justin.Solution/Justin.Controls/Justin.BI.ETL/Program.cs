using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL
{
    public class Program
    {
        public static void Main()
        {
            OleDbConnection conn = new OleDbConnection("Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.4.194)(PORT=1521))(CONNECT_DATA=(SID=bi)));User Id=orcl;Password=orcl;");
                  
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select * from d_app";
            OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
           DataTable table=new DataTable();
           ada.Fill(table);

            //Test();
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
            Stopwatch watch = Stopwatch.StartNew();
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
            view.OrderBy.Add(new Field() { Name = "SYS_KEY", FieldType = DbType.String });

            ETLInfo etlInfo = new ETLInfo(table, "Client");

            SerializeHelper.XmlSerializeToFile(etlInfo, "table.xml", true);
            ETLInfo e2 = SerializeHelper.XmlDeserializeFromFile<ETLInfo>("table.xml");
            SerializeHelper.XmlSerializeToFile(e2, "table2.xml", true);

            new ETLService().Process("table.xml", sourceConn, dstConn, true, new Program().ShowMsg);

            watch.Stop();
            Console.WriteLine("耗时{0}毫秒", watch.ElapsedMilliseconds);
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

        public static void Test()
        {
            var invertedIndex = File.ReadAllLines("input.txt").
                SelectMany(line =>
                {
                    var fs = line.Split();
                    return Enumerable
                            .Range(1, fs.Length - 1)
                            .Select(i => new { Num = int.Parse(fs[i]), Word = fs[0] });
                })
                .Distinct()
                .ToLookup(x => x.Num, x => x.Word);

            foreach (var item in invertedIndex)
                Console.WriteLine(item);

            var results = new int[][] {
                                        new int[] { 3, 4, 10 },
                                        new int[] { 12, 3, 4 }, 
                                        new int[] { 3, 9 }, 
                                        new int[] { 3, 4, 12 } 
                                       }
                                        .Select(x => x.SelectMany(xx => invertedIndex[xx])
                //.GroupBy(xx => xx)
                //.OrderByDescending(xx => xx.Count())
                //.First()
                //.Key
                                                );
        }
    }
}
