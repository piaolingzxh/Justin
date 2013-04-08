using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using Justin.BI.DBLibrary.Mondrian;
namespace ConsoleApplication1
{

    public class Program
    {
        #region string

        static string mssqlOfMonthView = @"
SELECT D.DAY_ID
          , DAY_DESC
          , DAY_DATE
          , DAY_WEEK_ID
          , M.MONTH_ID
          , MONTH_DESC
          , Q.QUARTER_ID
          , QUARTER_DESC
          , Y.YEAR_ID
          , YEAR_DESC
          FROM
          D_DATE_DAY D
          LEFT JOIN D_DATE_MONTH M
          ON D.MONTH_ID = M.MONTH_ID
          LEFT JOIN D_DATE_QUARTER Q
          ON M.QUARTER_ID = Q.QUARTER_ID
          LEFT JOIN D_DATE_YEAR Y
          ON Q.YEAR_ID = Y.YEAR_ID";

        static string oracleOfMonthView = @"
 SELECT D.DAY_ID
          , DAY_DESC
          , DAY_DATE
          , DAY_WEEK_ID
          , M.MONTH_ID
          , MONTH_DESC
          , Q.QUARTER_ID
          , QUARTER_DESC
          , Y.YEAR_ID
          , YEAR_DESC
          FROM
          D_DATE_DAY D
          LEFT JOIN D_DATE_MONTH M
          ON D.MONTH_ID = M.MONTH_ID
          LEFT JOIN D_DATE_QUARTER Q
          ON M.QUARTER_ID = Q.QUARTER_ID
          LEFT JOIN D_DATE_YEAR Y
          ON Q.YEAR_ID = Y.YEAR_ID";

        static string mssqlOfWeekHie = @"
 SELECT D.DAY_ID
          , DAY_DESC
          , W.WEEK_ID
          , W.WEEK_DESC
          , Y.YEAR_ID
          , YEAR_DESC
          FROM
          D_DATE_DAY D
          LEFT JOIN D_DATE_WEEK W
          ON D.WEEK_ID = W.WEEK_ID
          LEFT JOIN D_DATE_YEAR Y
          ON W.YEAR_ID = Y.YEAR_ID
";
        static string oracleOfWeekHie = @"
SELECT D.DAY_ID
          , DAY_DESC
          , W.WEEK_ID
          , W.WEEK_DESC
          , Y.YEAR_ID
          , YEAR_DESC
          FROM
          D_DATE_DAY D
          LEFT JOIN D_DATE_WEEK W
          ON D.WEEK_ID = W.WEEK_ID
          LEFT JOIN D_DATE_YEAR Y
          ON W.YEAR_ID = Y.YEAR_ID
";

        #endregion
        public static void Main()
        {
            SchemaTest();
            Console.WriteLine("序列化完成");
            var schema = Schema.Deserialize("schema.xml");
            schema.Serializer("schema_D.xml");
            Console.WriteLine("反序列化完成");
        }


        public static void SchemaTest()
        {



            string pkName = "SYS_KEY";

            Schema schema = new Schema("GTPBI_Oracle_Schema");

            #region projectDim

            Dimension projectDim = new Dimension("ProjectDim")
            {
                Description = "项目维度：实体类型维度",
                Caption = "项目维度",
            };
            Hierarchy projectHierarchy = new Hierarchy("hieInfo")
            {
                HasAll = true,
                PrimaryKey = pkName,
                Caption = "项目列表",
            };
            projectHierarchy.Table = new Table("D_PROJECT") { Schema = "dbo" };

            projectHierarchy.Levels.Add(new Level("Project")
            {
                Description = "项目列表",
                Column = pkName,
                Caption = "项目列表",
                CaptionColumn = "PROJECT_VAL",
            });
            projectDim.Hierarchies.Add(projectHierarchy);
            schema.Dimensions.Add(projectDim);

            #endregion

            #region BillStateDim

            Dimension BillStateDim = new Dimension("BillStateDim")
            {
                Description = "单据状态：枚举类型",
                Caption = "单据状态",
            };
            Hierarchy billStateDimHierarchy = new Hierarchy("hieInfo")
            {
                HasAll = true,
                PrimaryKey = pkName,
            };
            billStateDimHierarchy.Table = new Table("D_BILL_STATE") { Schema = "dbo" };

            billStateDimHierarchy.Levels.Add(new Level("BillState")
            {
                Description = "单据状态",
                Column = pkName,
                Caption = "单据状态",
                CaptionColumn = "SYS_DISPLAY",
            });
            BillStateDim.Hierarchies.Add(billStateDimHierarchy);

            schema.Dimensions.Add(BillStateDim);

            #endregion

            #region ContractBillDim

            Dimension ContractBillDim = new Dimension("ContractBillDim")
            {
                Description = "采购合同类型：数据字典类型",
                Caption = "采购合同类型",
            };
            Hierarchy ContractBillDimHierarchy = new Hierarchy("hieInfo")
            {
                HasAll = true,
                PrimaryKey = pkName,
            };
            ContractBillDimHierarchy.Table = new Table("D_CONTRACT_BILL_TYPE") { Schema = "dbo" };
            Level ContractBillLevel = new Level("BillType")
            {
                Table = "D_CONTRACT_BILL_TYPE",
                Description = "采购合同类型",
                Column = pkName,
                NameColumn = "SYS_DISPLAY",
                ParentColumn = "SYS_PARENTKEY",
                NullParentValue = "0",
                ColumnType = ColumnType.Numeric,
                UniqueMembers = true,
                Caption = "采购合同类型",
            };

            ContractBillLevel.Closure = new Closure() { ParentColumn = "SYS_PARENTKEY", ChildColumn = "SYS_KEY" };
            ContractBillLevel.Closure.Table = new Table("D_CONTRACT_BILL_TYPE_CLOSURE") { Schema = "dbo" };
            ContractBillDimHierarchy.Levels.Add(ContractBillLevel);
            ContractBillDim.Hierarchies.Add(ContractBillDimHierarchy);

            schema.Dimensions.Add(ContractBillDim);

            #endregion

            #region DeptDim

            Dimension DeptDim = new Dimension("DeptDim")
            {
                Description = "部门维：实体类型（父子结构）",
                Caption = "部门维",
            };
            Hierarchy DeptDimHierarchy = new Hierarchy("hieInfo")
            {
                HasAll = true,
                PrimaryKey = pkName,
            };
            DeptDimHierarchy.Table = new Table("D_DEPT") { Schema = "dbo" };
            Level deptLevel = new Level("Dept")
            {
                Description = "部门",
                Column = pkName,
                NameColumn = "DEPT_VAL",
                ParentColumn = "SYS_PARENTKEY",
                NullParentValue = "0",
                ColumnType = ColumnType.Numeric,
                UniqueMembers = true,
                Caption = "部门",
            };

            deptLevel.Closure = new Closure() { ParentColumn = "SYS_PARENTKEY", ChildColumn = "SYS_KEY" };
            deptLevel.Closure.Table = new Table("D_DEPT_CLOSURE") { Schema = "dbo" };
            deptLevel.Properties.Add(new Property("DEPT_Name") { Column = "DEPT_VAL" });

            DeptDimHierarchy.Levels.Add(deptLevel);
            DeptDim.Hierarchies.Add(DeptDimHierarchy);
            schema.Dimensions.Add(DeptDim);

            #endregion

            #region DateDim

            Dimension DateDim = new Dimension("DateDim")
            {
                Description = "日期维：日期类型",
                Caption = "日期维",
            };

            #region MonthHie

            Hierarchy MonthHie = new Hierarchy("MonthHie")
            {
                Description = "月层次",
                HasAll = true,
                PrimaryKey = "DAY_ID",
            };

            MonthHie.View = new View() { Alias = "MonthHieView" };

            MonthHie.View.SQLList.Add(
                new SQL()
                {
                    Dialect = SQLDialect.Mssql,
                    Text = mssqlOfMonthView,
                });
            MonthHie.View.SQLList.Add(new SQL()
            {
                Dialect = SQLDialect.Oracle,
                Text = oracleOfMonthView,
            });

            MonthHie.Levels.Add(new Level("YearLevel") { Description = "年", Column = "YEAR_ID", OrdinalColumn = "YEAR_ID", CaptionColumn = "YEAR_DESC", });
            MonthHie.Levels.Add(new Level("QuarterLevel") { Description = "季度", Column = "QUARTER_ID", OrdinalColumn = "QUARTER_ID", CaptionColumn = "QUARTER_DESC", });
            MonthHie.Levels.Add(new Level("MonthLevel") { Description = "月", Column = "MONTH_ID", OrdinalColumn = "MONTH_ID", CaptionColumn = "MONTH_DESC", });
            MonthHie.Levels.Add(new Level("DayLevel") { Description = "日", Column = "DAY_ID", CaptionColumn = "DAY_DESC", });




            #endregion


            #region WeekHie

            Hierarchy WeekHie = new Hierarchy("WeekHie")
            {
                Description = "周层次",
                HasAll = true,
                PrimaryKey = "DAY_ID",
            };

            WeekHie.View = new View() { Alias = "WeekHieView" };

            WeekHie.View.SQLList.Add(
                new SQL()
                {
                    Dialect = SQLDialect.Mssql,
                    Text = mssqlOfWeekHie,
                });
            WeekHie.View.SQLList.Add(new SQL()
            {
                Dialect = SQLDialect.Oracle,
                Text = oracleOfWeekHie,
            }
         );

            WeekHie.Levels.Add(new Level("YearLevel") { Description = "年", Column = "YEAR_ID", OrdinalColumn = "YEAR_ID", CaptionColumn = "YEAR_DESC", });
            WeekHie.Levels.Add(new Level("WeekLevel") { Description = "周", Column = "WEEK_ID", OrdinalColumn = "WEEK_ID", CaptionColumn = "WEEK_DESC", });
            WeekHie.Levels.Add(new Level("DayLevel") { Description = "日", Column = "DAY_ID", OrdinalColumn = "DAY_ID", CaptionColumn = "DAY_DESC", });




            #endregion


            DateDim.Hierarchies.Add(MonthHie);
            DateDim.Hierarchies.Add(WeekHie);

            schema.Dimensions.Add(DateDim);

            #endregion

            #region ZCHT_BsJe

            Cube zchtCube = new Cube("ZCHT_BsJe") { Description = "支出_笔数和金额", Caption = "支出_笔数和金额" };
            zchtCube.Table = new Table("F_ZCHT_BSJE") { Schema = "dbo" };
            zchtCube.DimensionUsages.Add(new DimensionUsage("ProjectDim") { Source = "ProjectDim", ForeignKey = "PROJECT_KEY", });
            zchtCube.DimensionUsages.Add(new DimensionUsage("BillStateDim") { Source = "BillStateDim", ForeignKey = "BILL_STATE_KEY", });
            zchtCube.DimensionUsages.Add(new DimensionUsage("ContractBillDim") { Source = "ContractBillDim", ForeignKey = "CONTRACT_BILL_TYPE_KEY", });
            zchtCube.DimensionUsages.Add(new DimensionUsage("DateDim") { Source = "DateDim", ForeignKey = "DAY_ID", });
            zchtCube.DimensionUsages.Add(new DimensionUsage("DeptDim") { Source = "DeptDim", ForeignKey = "DEPT_ID_KEY", });

            zchtCube.Measures.Add(new Measure("ZCHTCount") { Description = "支出合同笔数", Aggregator = Aggregator.DistinctCount, Column = "M_F_ID", FormatString = FormatString.Standard, DataType = ColumnType.Integer, Caption = "支出合同笔数" });
            zchtCube.Measures.Add(new Measure("ZCHTAmount") { Description = "支出合同金额", Aggregator = Aggregator.Sum, Column = "M_F_AMOUNT", FormatString = FormatString.Standard, Caption = "支出合同金额" });

            schema.Cubes.Add(zchtCube);

            #endregion

            #region SRHT_BsJe

            Cube srhtCube = new Cube("SRHT_BsJe") { Description = "收入_笔数和金额", Caption = "收入_笔数和金额" };
            srhtCube.Table = new Table("F_SRHT_BSJE") { Schema = "dbo" };
            srhtCube.DimensionUsages.Add(new DimensionUsage("ProjectDim") { Source = "ProjectDim", ForeignKey = "PROJECT_KEY", });

            srhtCube.Measures.Add(new Measure("SRHTCount") { Description = "收入合同笔数", Aggregator = Aggregator.DistinctCount, Column = "M_F_ID", FormatString = FormatString.Standard, DataType = ColumnType.Integer, Caption = "收入合同笔数" });
            srhtCube.Measures.Add(new Measure("SRHTAmount") { Description = "收入合同金额", Aggregator = Aggregator.Sum, Column = "M_F_AMOUNT", FormatString = FormatString.Standard, Caption = "收入合同金额" });

            schema.Cubes.Add(srhtCube);

            #endregion


            #region VirtualCube

            VirtualCube vcube = new VirtualCube("Virtual Cube") { Caption = "虚拟多为立方体" };

            vcube.VirtualCubeDimensions.Add(new VirtualCubeDimension("ProjectDim"));
            vcube.VirtualCubeDimensions.Add(new VirtualCubeDimension("BillStateDim"));
            vcube.VirtualCubeDimensions.Add(new VirtualCubeDimension("ContractBillDim"));

            vcube.VirtualCubeMeasures.Add(new VirtualCubeMeasure("[Measures].[ZCHTCount]") { CubeName = "ZCHT_BsJe" });
            vcube.VirtualCubeMeasures.Add(new VirtualCubeMeasure("[Measures].[ZCHTAmount]") { CubeName = "ZCHT_BsJe" });
            vcube.VirtualCubeMeasures.Add(new VirtualCubeMeasure("[Measures].[SRHTCount]") { CubeName = "SRHT_BsJe" });
            vcube.VirtualCubeMeasures.Add(new VirtualCubeMeasure("[Measures].[SRHTAmount]") { CubeName = "SRHT_BsJe" });

            vcube.Caption = "";
            schema.VirtualCubes.Add(vcube);

            #endregion

            schema.Serializer("schema.xml");
        }
    }
}