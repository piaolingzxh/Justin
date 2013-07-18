using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Justin.BI.OLAP.Entities;

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
            string dwOleDbConnStr = "Provider=sqloledb;Data Source=.;Initial Catalog=OLAPDW;User Id=sa;Password=sa;";
            string olapConnString = "Data Source = .;Provider=msolap";

            Solution solution = new Solution("SSAS_OLAP", "SSAS 测试");
            Cube salesCube = new Cube("Sales", "销售");
            salesCube.TableName = "SaleHistory";
            solution.Cubes.Add(salesCube);

            var customerDim = new Dimension("CustomerDim", "客户维") { FKColumn = "CustomerId" };
            customerDim.Levels.Add(new Level("customerlevel", "客户") { SourceTable = "Customer", KeyColumn = "Id", NameColumn = "Name" });
            salesCube.Dimensions.Add(customerDim);


            var ProductDim = new Dimension("ProductDim", "产品维") { FKColumn = "ProductId" };
            ProductDim.Levels.Add(new Level("Productlevel", "产品") { SourceTable = "Product", KeyColumn = "Id", NameColumn = "Name" });
            salesCube.Dimensions.Add(ProductDim);


            //var DateDim = new SSASDim("DateDim");
            //DateDim.Levels = new List<ILevel>();
            //DateDim.Levels.Add(new Level("Datelevel", "Datelevel") { SourceTable = "" });
            //solution.Dims.Add(DateDim);

            salesCube.Measures.Add(new Measure("ProductCount", "产品数量") { ColumnName = "ProductCount" });
            salesCube.Measures.Add(new Measure("UnitPrice", "产品单价") { ColumnName = "UnitPrice" });

            SSASFactory factory = new SSASFactory(dwOleDbConnStr, olapConnString);
            factory.DeleteSolution(solution);
            factory.CreateSolution(solution);


            Console.WriteLine("OK");
        }
    }
}
