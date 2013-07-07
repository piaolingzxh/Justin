using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices;

namespace Justin.BI.OLAP
{
    class Programs
    {
        public static void Main()
        {
            //string connStr =@"Provider=MSOLAP;Data Source=192.168.4.194;UserName=BISERVER194\BI;Password=123qwe!@#;";
            //string connStr = @"Provider=MSOLAP.4;Data Source=192.168.4.32;";

            //Server server = new Server();

            //try
            //{
            //    server.Connect(connStr);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            test1();
            Console.WriteLine("OK");
            Console.Read();

        }
        public static void test1()
        {


            string dwConnStr = "Provider=sqloledb;Data Source=.;Initial Catalog=MondrianDB;User Id=sa;Password=sa;";

            OleDbConnectionStringBuilder sb = new  OleDbConnectionStringBuilder(dwConnStr);
            sb.Remove("provider");
            SqlConnection sqlconn = new SqlConnection(sb.ConnectionString);
            sqlconn.Open();

            string olapConnectionString = "Data Source = .;Provider=msolap";
            //OleDbConnection con = new OleDbConnection(dwConnStr);
            //con.Open();

            SSASSolution solution = new SSASSolution("BITheme");
            var customerDim = new SSASDim("CustomerDim");
            customerDim.Levels = new List<ILevel>();
            customerDim.Levels.Add(new SSASLevel("customerlevel", "customerlevel") { SourceTable = "Customer", KeyColumn = "Id", NameColumn = "Name" });
            solution.Dims.Add(customerDim);


            var ProductDim = new SSASDim("ProductDim");
            ProductDim.Levels = new List<ILevel>();
            ProductDim.Levels.Add(new SSASLevel("Productlevel", "Productlevel") { SourceTable = "Product", KeyColumn = "Id", NameColumn = "Name" });
            solution.Dims.Add(ProductDim);


            //var DateDim = new SSASDim("DateDim");
            //DateDim.Levels = new List<ILevel>();
            //DateDim.Levels.Add(new Level("Datelevel", "Datelevel") { SourceTable = "" });
            //solution.Dims.Add(DateDim);


            SSASFactory factory = new SSASFactory(dwConnStr, olapConnectionString);
            factory.DeleteSolution(solution);
            factory.CreateSolution(solution);


            Console.WriteLine("OK");
        }
    }
}
