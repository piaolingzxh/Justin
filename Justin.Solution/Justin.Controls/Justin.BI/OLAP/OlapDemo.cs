using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using Microsoft.AnalysisServices;
using analysis = Microsoft.AnalysisServices;

namespace BuildOLAP
{
    public class Form1
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private const string strConnectionString = "Provider=SQLNCLI.1;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=AdventureWorksDW";
        /// <summary>
        /// 服务器名称
        /// </summary>
        private const string strServerName = "DC";
        /// <summary>
        /// 数据库名称
        /// </summary>
        private const string strDataBaseName = "OlapSample";

        /// <summary>
        /// 名称
        /// 该名称用于数据源，数据源视图，CUBE
        /// </summary>
        private const string strName = "OlapSample";


        /// <summary>
        /// 服务器的实例
        /// </summary>
        static analysis.Server serverAnalysis;

        /// <summary>
        /// 创建对象的类
        /// </summary>
        static Olap olap = new Olap();

        public static void Main1()
        {

            serverAnalysis = new Microsoft.AnalysisServices.Server();

            try
            {
                serverAnalysis.Connect("Data Source = " + strServerName);

                olap = new Olap();

                analysis.Database db = serverAnalysis.Databases.FindByName(strDataBaseName);

                if (db != null)
                {
                    db.Drop();
                }
                else
                {

                    db = serverAnalysis.Databases.Add(strDataBaseName);

                    db.Update();

                    olap.CreateDataSource(db, strName, strConnectionString);

                    olap.CreateDataSourceView(db, strName, strName);

                    olap.CreateGeographyDimension(db, strName);

                    olap.CreateCustomerDimension(db, strName);

                    olap.CreateCube(db, strName);
                }
            }

            catch (analysis.AmoException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }

    public class Olap
    {
        public Olap()
        {

        }

        #region 创建多维数据库

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="parDatabase">数据库</param>
        /// <param name="parDataSourceName">数据源名</param>
        /// <param name="parConnString">连接字符</param>
        public void CreateDataSource(Database parDatabase, string parDataSourceName, string parConnString)
        {
            try
            {
                DataSource ds = parDatabase.DataSources.Add(parDataSourceName);

                ds.ConnectionString = parConnString;

                ds.Update();
            }
            catch
            {
                throw new AmoException("error!");
            }
        }

        /// <summary>
        /// 创建数据源视图
        /// </summary>
        /// <param name="parDatabase">数据库</param>
        /// <param name="parDataSourceName">数据源名</param>
        /// <param name="parDataSourceViewName">数据源视图名</param>
        public void CreateDataSourceView(Database parDatabase, string parDataSourceName, string parDataSourceViewName)
        {
            DataSourceView dsv = parDatabase.DataSourceViews.Add(parDataSourceViewName);
            dsv.DataSourceID = parDataSourceName;
            dsv.Schema = new DataSet();

            //Open a connection to the data source
            OleDbConnection connection = new OleDbConnection(dsv.DataSource.ConnectionString);
            connection.Open();

            //增加 DimGeography表
            AddTable(dsv, connection, "DimGeography");
            //AddComputedColumn(dsv, connection, "DimCustomer", "FullName", "CASE WHEN MiddleName IS NULL THEN FirstName + ' ' + LastName ELSE FirstName + ' ' + MiddleName + ' ' + LastName END");
            //AddComputedColumn(dsv, connection, "DimCustomer", "GenderDesc", "CASE WHEN Gender = 'M' THEN 'Male' ELSE 'Female' END");
            //AddComputedColumn(dsv, connection, "DimCustomer", "MaritalStatusDesc", "CASE WHEN MaritalStatus = 'S' THEN 'Single' ELSE 'Married' END");



            //增加 DimCustomer表
            AddTable(dsv, connection, "DimCustomer");
            AddRelation(dsv, "DimCustomer", "GeographyKey", "DimGeography", "GeographyKey");

            //增加 FactInternetSales表
            AddTable(dsv, connection, "FactInternetSales");
            AddRelation(dsv, "FactInternetSales", "CustomerKey", "DimCustomer", "CustomerKey");

            dsv.Update();
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="parDsv">数据源视图</param>
        /// <param name="parConn">连接到数据仓库</param>
        /// <param name="parTable">表名</param>
        public void AddTable(DataSourceView dsv, OleDbConnection connection, String tableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT * FROM [dbo].[" + tableName + "] WHERE 1=0",
                connection);
            DataTable[] dataTables = adapter.FillSchema(dsv.Schema, //将表的结构映射到数据源视图
                SchemaType.Mapped, tableName);
            DataTable dataTable = dataTables[0];

            dataTable.ExtendedProperties.Add("TableType", "Table"); //为表增加扩展属性
            dataTable.ExtendedProperties.Add("DbSchemaName", "dbo");
            dataTable.ExtendedProperties.Add("DbTableName", tableName);
            dataTable.ExtendedProperties.Add("FriendlyName", tableName);
        }

        /// <summary>
        /// 创建关系
        /// </summary>
        /// <param name="parDsv">数据源视图</param>
        /// <param name="parFkTableName">外键表名</param>
        /// <param name="parFkColumnName">外键列名</param>
        /// <param name="parPkTableName">主键表名</param>
        /// <param name="parPkColumnName">主键列名</param>
        public void AddRelation(DataSourceView parDsv, string parFkTableName, string parFkColumnName, string parPkTableName, string parPkColumnName)
        {
            DataColumn fkColumn = parDsv.Schema.Tables[parFkTableName].Columns[parFkColumnName];
            DataColumn pkColumn = parDsv.Schema.Tables[parPkTableName].Columns[parPkColumnName];
            parDsv.Schema.Relations.Add("FK_" + parFkTableName + "_" + parPkColumnName, pkColumn, fkColumn);
        }

        /// <summary>
        /// 创建项
        /// </summary>
        /// <param name="parDsv">数据源视图</param>
        /// <param name="parTableName">表名</param>
        /// <param name="parColumnName">列名</param>
        /// <returns></returns>
        public DataItem CreateDataItem(DataSourceView parDsv, string parTableName, string parColumnName)
        {
            DataTable dataTable = ((DataSourceView)parDsv).Schema.Tables[parTableName];
            DataColumn dataColumn = dataTable.Columns[parColumnName];
            return new DataItem(parTableName, parColumnName, OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));
        }

        public void AddComputedColumn(DataSourceView dsv, OleDbConnection connection, String tableName, String computedColumnName, String expression)
        {
            DataSet tmpDataSet = new DataSet();
            tmpDataSet.Locale = CultureInfo.CurrentCulture;
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ("
                + expression + ") AS [" + computedColumnName + "] FROM [dbo].["
                + tableName + "] WHERE 1=0", connection);
            DataTable[] dataTables = adapter.FillSchema(tmpDataSet,
                SchemaType.Mapped, tableName);
            DataTable dataTable = dataTables[0];
            DataColumn dataColumn = dataTable.Columns[computedColumnName];

            dataTable.Constraints.Clear();
            dataTable.Columns.Remove(dataColumn);

            dataColumn.ExtendedProperties.Add("DbColumnName", computedColumnName);
            dataColumn.ExtendedProperties.Add("ComputedColumnExpression",
                expression);
            dataColumn.ExtendedProperties.Add("IsLogical", "True");

            dsv.Schema.Tables[tableName].Columns.Add(dataColumn);
        }

        /// <summary>
        /// 创建Geography维度
        /// </summary>
        /// <param name="db"></param>
        public void CreateGeographyDimension(Database db, string parDataSourceName)
        {
            // Create the Geography dimension
            Dimension dim = db.Dimensions.Add("Geography");
            dim.Type = DimensionType.Geography;
            dim.UnknownMember = UnknownMemberBehavior.Hidden;
            dim.AttributeAllMemberName = "All Geographies";
            dim.Source = new DataSourceViewBinding(parDataSourceName);
            dim.StorageMode = DimensionStorageMode.Molap;

            #region Create attributes

            DimensionAttribute attr;

            attr = dim.Attributes.Add("Geography Key");
            attr.Usage = AttributeUsage.Key;
            attr.OrderBy = OrderBy.Name;
            attr.AttributeHierarchyVisible = false;
            attr.AttributeHierarchyOptimizedState = OptimizationType.NotOptimized;
            attr.AttributeHierarchyOrdered = false;
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "GeographyKey"));

            attr = dim.Attributes.Add("Country-Region");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "EnglishCountryRegionName"));

            attr = dim.Attributes.Add("State-Province");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "CountryRegionCode"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceCode"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceName");
            attr.AttributeRelationships.Add(new AttributeRelationship("Country-Region"));

            attr = dim.Attributes.Add("City");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceCode"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "City"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "City");
            attr.AttributeRelationships.Add(new AttributeRelationship("State-Province"));

            attr = dim.Attributes.Add("Postal Code");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "City"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "PostalCode"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "PostalCode");
            attr.AttributeRelationships.Add(new AttributeRelationship("City"));

            #endregion

            #region Create hierarchies

            Hierarchy hier;

            hier = dim.Hierarchies.Add("Geography");
            hier.AllMemberName = "All Geographies";
            hier.Levels.Add("Country-Region").SourceAttributeID = "Country-Region";
            hier.Levels.Add("State-Province").SourceAttributeID = "State-Province";
            hier.Levels.Add("City").SourceAttributeID = "City";
            hier.Levels.Add("Postal Code").SourceAttributeID = "Postal Code";

            #endregion

            dim.Update();
        }

        /// <summary>
        /// 创建维度
        /// </summary>
        /// <param name="db"></param>
        public void CreateCustomerDimension(Database db, string parDataSourceName)
        {
            // Create the Customer dimension
            Dimension dim = db.Dimensions.Add("Customer");
            dim.Type = DimensionType.Customers;
            dim.UnknownMember = UnknownMemberBehavior.Hidden;
            dim.AttributeAllMemberName = "All Customers";
            dim.Source = new DataSourceViewBinding(parDataSourceName);
            dim.StorageMode = DimensionStorageMode.Molap;

            #region Create attributes

            DimensionAttribute attr;

            attr = dim.Attributes.Add("Full Name");
            attr.Usage = AttributeUsage.Key;
            attr.Type = AttributeType.Customers;
            attr.OrderBy = OrderBy.Name;
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "CustomerKey"));



            //增加国家区域属性，从DimGeography表映射该字段
            attr = dim.Attributes.Add("Country-Region");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "EnglishCountryRegionName"));


            //增加国家，省，从DimGeography表映射该字段
            //增加一个命名列
            attr = dim.Attributes.Add("State-Province");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "CountryRegionCode"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceCode"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceName");
            attr.AttributeRelationships.Add(new AttributeRelationship("Country-Region"));

            attr = dim.Attributes.Add("City");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "StateProvinceCode"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "City"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "City");
            attr.AttributeRelationships.Add(new AttributeRelationship("State-Province"));

            attr = dim.Attributes.Add("Postal Code");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "City"));
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimGeography", "PostalCode"));
            attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimGeography", "PostalCode");
            attr.AttributeRelationships.Add(new AttributeRelationship("City"));

            attr = dim.Attributes.Add("Education");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "EnglishEducation"));

            attr = dim.Attributes.Add("Email Address");

            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "EmailAddress"));

            //attr = dim.Attributes.Add("Gender");
            //attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "GenderDesc"));

            //attr = dim.Attributes.Add("Marital Status");
            //attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "MaritalStatusDesc"));

            attr = dim.Attributes.Add("Occupation");
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "EnglishOccupation"));

            attr = dim.Attributes.Add("Phone");
            attr.AttributeHierarchyEnabled = false;
            attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimCustomer", "Phone"));

            #endregion

            #region Create hierarchies

            Hierarchy hier;

            hier = dim.Hierarchies.Add("Customer Geography");
            hier.AllMemberName = "All Customers";
            hier.Levels.Add("Country-Region").SourceAttributeID = "Country-Region";
            hier.Levels.Add("State-Province").SourceAttributeID = "State-Province";
            hier.Levels.Add("City").SourceAttributeID = "City";
            hier.Levels.Add("Postal Code").SourceAttributeID = "Postal Code";
            hier.Levels.Add("Full Name").SourceAttributeID = "Full Name";

            #endregion

            dim.Update();
        }
        /// <summary>
        /// 创建Cube
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="parDataSourceName">数据源</param>
        public void CreateCube(Database db, string parDataSourceName)
        {
            // Create the Adventure Works cube
            Cube cube = db.Cubes.Add("Adventure Works");

            cube.Source = new DataSourceViewBinding(parDataSourceName);
            cube.StorageMode = StorageMode.Molap;

            #region 将维度添加到Cube中

            Dimension dim;

            dim = db.Dimensions.GetByName("Customer");
            cube.Dimensions.Add(dim.ID);

            #endregion

            #region 创建度量值组

            CreateInternetSalesMeasureGroup(cube);

            #endregion

            cube.Update(UpdateOptions.ExpandFull);
        }

        public void CreateInternetSalesMeasureGroup(Cube cube)
        {
            // Create the Internet Sales measure group
            Database db = cube.Parent;
            MeasureGroup mg = cube.MeasureGroups.Add("Internet Sales");
            mg.StorageMode = StorageMode.Molap;
            mg.ProcessingMode = ProcessingMode.LazyAggregations;
            mg.Type = MeasureGroupType.Sales;

            #region Create measures

            Measure meas;

            //meas = mg.Measures.Add("Internet Sales Amount");                                         //加一个维度
            //meas.AggregateFunction = AggregationFunction.Sum;                                        //维度的计算方法
            //meas.MeasureExpression = "[Internet Sales Amount] * [Average Rate]";                     //维度表达式
            //meas.FormatString = "Currency";                                                          //维度格式字符
            //meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesAmount"); //

            meas = mg.Measures.Add("Internet Order Quantity");
            meas.AggregateFunction = AggregationFunction.Sum;
            meas.FormatString = "#,#";
            meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "OrderQuantity");

            meas = mg.Measures.Add("Internet Unit Price");
            meas.AggregateFunction = AggregationFunction.Sum;
            meas.FormatString = "Currency";
            meas.Visible = false;
            meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "UnitPrice");

            //meas = mg.Measures.Add("Internet Total Product Cost");
            //meas.AggregateFunction = AggregationFunction.Sum;
            //meas.MeasureExpression = "[Internet Total Product Cost] * [Average Rate]";
            //meas.FormatString = "Currency";
            //meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "TotalProductCost");

            meas = mg.Measures.Add("Internet Order Count");
            meas.AggregateFunction = AggregationFunction.Count;
            meas.FormatString = "#,#";
            meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ProductKey");

            #endregion

            #region Create measure group dimensions

            CubeDimension cubeDim;
            RegularMeasureGroupDimension regMgDim;
            ManyToManyMeasureGroupDimension mmMgDim;
            MeasureGroupAttribute mgAttr;

            //cubeDim = cube.Dimensions.GetByName("Date");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "OrderDateKey"));

            //cubeDim = cube.Dimensions.GetByName("Ship Date");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ShipDateKey"));

            //cubeDim = cube.Dimensions.GetByName("Delivery Date");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "DueDateKey"));

            cubeDim = cube.Dimensions.GetByName("Customer");
            regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            mg.Dimensions.Add(regMgDim);
            mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Full Name").ID);
            mgAttr.Type = MeasureGroupAttributeType.Granularity;
            mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "CustomerKey"));

            //cubeDim = cube.Dimensions.GetByName("Product");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Product Name").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ProductKey"));

            //cubeDim = cube.Dimensions.GetByName("Source Currency");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Currency").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "CurrencyKey"));

            //cubeDim = cube.Dimensions.GetByName("Sales Reason");
            //mmMgDim = new ManyToManyMeasureGroupDimension();
            //mmMgDim.CubeDimensionID = cubeDim.ID;
            //mmMgDim.MeasureGroupID = cube.MeasureGroups.GetByName("Sales Reasons").ID;
            //mg.Dimensions.Add(mmMgDim);

            //cubeDim = cube.Dimensions.GetByName("Internet Sales Order Details");
            //regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);
            //mg.Dimensions.Add(regMgDim);
            //mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Sales Order Key").ID);
            //mgAttr.Type = MeasureGroupAttributeType.Granularity;
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesOrderNumber"));
            //mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesOrderLineNumber"));

            #endregion

            #region Create partitions

            Partition part;

            part = mg.Partitions.Add("Internet_Sales_184");
            part.StorageMode = StorageMode.Molap;
            part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey <= '184'");
            part.Annotations.Add("LastOrderDateKey", "184");

            part = mg.Partitions.Add("Internet_Sales_549");
            part.StorageMode = StorageMode.Molap;
            part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey > '184' AND OrderDateKey <= '549'");
            part.Annotations.Add("LastOrderDateKey", "549");

            part = mg.Partitions.Add("Internet_Sales_914");
            part.StorageMode = StorageMode.Molap;
            part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey > '549' AND OrderDateKey <= '914'");
            part.Annotations.Add("LastOrderDateKey", "914");

            #endregion

            cube.Update(UpdateOptions.ExpandFull);
        }
        #endregion
    }
}



