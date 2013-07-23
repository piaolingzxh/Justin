using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.BI.OLAP.Entity;
using Justin.FrameWork.Helper;

namespace Justin.BI.OLAP
{
    public class OLAPTest
    {
        public static Solution PrepareSolution()
        {
            Solution solution = new Solution("SSAS_OLAP", "SSAS 测试");
            CubeEntity salesCube = new CubeEntity("SalesCube", "Sales");
            salesCube.TableName = "SaleHistory";
            solution.Cubes.Add(salesCube);

            var customerDim = new DimensionEntity("CustomerDim", "Customer") { FKColumn = "CustomerId" };
            customerDim.Levels.Add(new LevelEntity("customerlevel", "Customer") { SourceTable = "Customer", KeyColumn = "Id", NameColumn = "Name" });
            salesCube.Dimensions.Add(customerDim);


            var ProductDim = new DimensionEntity("ProductDim", "Product") { FKColumn = "ProductId" };
            ProductDim.Levels.Add(new LevelEntity("Productlevel", "Product") { SourceTable = "Product", KeyColumn = "Id", NameColumn = "Name" });
            salesCube.Dimensions.Add(ProductDim);


            //var DateDim = new SSASDim("DateDim");
            //DateDim.Levels = new List<ILevel>();
            //DateDim.Levels.Add(new Level("Datelevel", "Datelevel") { SourceTable = "" });
            //solution.Dims.Add(DateDim);

            salesCube.Measures.Add(new MeasureEntity("ProductCount", "ProductCount") { ColumnName = "ProductCount", Aggregator = Aggregator.Sum });
            salesCube.Measures.Add(new MeasureEntity("UnitPrice", "UnitPrice") { ColumnName = "UnitPrice", Aggregator = Aggregator.Sum });

            return solution;
        }
        public static void SSAS_OLAP()
        {
            var solution = PrepareSolution();

            string dwOleDbConnStr = "Provider=sqloledb;Data Source=.;Initial Catalog=OLAPDW;User Id=sa;Password=sa;";
            string olapConnString = "Data Source = .;Provider=msolap";


            //SerializeHelper.XmlSerializeToFile(solution, "solution.xml", true);
            //var newSolution = SerializeHelper.XmlDeserializeFromFile<Solution>("solution.xml");
            //SerializeHelper.XmlSerializeToFile(newSolution, "solution_new.xml", true);

            SSASFactory factory = new SSASFactory(dwOleDbConnStr, olapConnString);
            factory.DeleteSolution(solution);
            factory.CreateSolution(solution);


            Console.WriteLine("OK");
        }
        public static void Mondrian_OLAP(string fileName)
        {
            var solution = PrepareSolution();

            MondrianFactory factory = new MondrianFactory(fileName);

            factory.DeleteSolution(solution);
            factory.CreateSolution(solution);
        }
    }
}
