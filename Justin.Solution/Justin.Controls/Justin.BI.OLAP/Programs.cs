using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Justin.BI.OLAP.Entities;
using Justin.FrameWork.Helper;

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
            string dwConnStr = "Provider=sqloledb;Data Source=.;Initial Catalog=OLAPDW;User Id=sa;Password=sa;";
            string olapConnectionString = "Data Source = .;Provider=msolap";
            //OleDbConnection con = new OleDbConnection(dwConnStr);
            //con.Open();
            Cube cube = new Cube("OLAP_SSAS");
            var customerDim = new Dimension("CustomerDim");
            customerDim.Levels = new List<Level>();
            customerDim.Levels.Add(new Level("customerlevel", "customerlevel") { SourceTable = "Customers", KeyColumn = "Id", NameColumn = "Name" });
            cube.Dimensions.Add(customerDim);


            var ProductDim = new Dimension("ProductDim");
            ProductDim.Levels = new List<Level>();
            ProductDim.Levels.Add(new Level("Productlevel", "Productlevel") { SourceTable = "Products", KeyColumn = "Id", NameColumn = "Name" });
            cube.Dimensions.Add(ProductDim);


            var employeeDim = new Dimension("EmployeeDim");
            employeeDim.Levels = new List<Level>();
            employeeDim.Levels.Add(new Level("EmployeeLevel", "EmployeeLevel") { SourceTable = "Employees", KeyColumn = "Id", NameColumn = "Name" });
            cube.Dimensions.Add(employeeDim);

            cube.Measures.Add(new Measure() { Name = "ProductCount", ColumnName = "ProductCount" });
            cube.Measures.Add(new Measure() { Name = "UnitPrice", ColumnName = "UnitPrice" });

            //var DateDim = new SSASDim("DateDim");
            //DateDim.Levels = new List<ILevel>();
            //DateDim.Levels.Add(new Level("Datelevel", "Datelevel") { SourceTable = "" });
            //solution.Dims.Add(DateDim);

           

            SerializeHelper.XmlSerializeToFile(cube, "solution.xml", true);
            Cube s = SerializeHelper.XmlDeserializeFromFile<Cube>("solution.xml");

            SSASFactory factory = new SSASFactory(dwConnStr, olapConnectionString);
            factory.DeleteSolution(cube);
            factory.CreateSolution(cube);


            Console.WriteLine("OK");
        }
    }
}
