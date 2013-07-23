using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Justin.BI.ETL.Entity;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL
{
    public class ETLTest
    {
        public static void ShowMsg(int successCount)
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

            string sourceOleDbConnString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            string dstOleDbConnString = ConfigurationManager.ConnectionStrings["oracle"].ConnectionString;

            string sql = "select CLIENT_KEY,SYS_DISPLAY,SYS_LOAD_TIME,SYS_END_TIME,SYS_START_TIME,SYS_KEY from D_CLIENT";
            View view = new View(sql, sourceOleDbConnString);
            view.OrderBy.Add(new Field() { Name = "SYS_KEY", FieldType = DbType.String });

            ETLInfo etlInfo = new ETLInfo(table, "Client");

            SerializeHelper.XmlSerializeToFile(etlInfo, "table.xml", true);
            ETLInfo e2 = SerializeHelper.XmlDeserializeFromFile<ETLInfo>("table.xml");
            SerializeHelper.XmlSerializeToFile(e2, "table2.xml", true);

            new ETLService().Process("table.xml", sourceOleDbConnString, dstOleDbConnString, true, ShowMsg);

            watch.Stop();
            Console.WriteLine("耗时{0}毫秒", watch.ElapsedMilliseconds);
        }

    }


}
