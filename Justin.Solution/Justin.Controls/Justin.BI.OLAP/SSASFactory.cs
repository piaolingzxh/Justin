using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AnalysisServices;

namespace Justin.BI.OLAP
{
    public class SSASFactory : IOLAPFactory
    {
        private static string DefaultHierarchy = "DefaultHierarchy";
        private string dwConnectionString = "Provider=sqloledb;Data Source=.;Initial Catalog=DwDemo;User Id=sa;Password=sa;";
        private string olapConnectionString = "Data Source = .;Provider=msolap";//"Provider=MSOLAP.4;Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=Adventureworks;Data Source=(local);MDX Compatibility=1;Safety Options=2;MDX Missing Member Mode=Error";

        private Server server;


        public SSASFactory(string dwConnectionString, string olapConnectionString)
        {
            this.dwConnectionString = dwConnectionString;
            this.olapConnectionString = olapConnectionString;
            server = new Server();
        }

        public SSASFactory()
        {
            server = new Server();
        }

        private void Connect()
        {
            if (server.Connected)
                server.Disconnect();
            server.Connect(olapConnectionString);
        }
        private void CheckConnect()
        {
            this.Connect();
        }
        private bool IsDatabaseExist(string databaseName)
        {
            this.CheckConnect();
            return server.Databases.ContainsName(databaseName);
        }


        private Database CreateDatabaseIfNotExist(string databaseName)
        {
            Database database = null;
            if (server.Databases.ContainsName(databaseName))
            {
                database = server.Databases[databaseName];
            }
            else
            {
                database = server.Databases.Add(databaseName, databaseName);
                database.Update();
            }
            return database;
        }
        private DataSource CreateDataSourceIfNotExist(Database database, String dataSourceName)
        {
            DataSource dataSource = null;
            if (!database.DataSources.ContainsName(dataSourceName))
            {
                dataSource = database.DataSources.Add(dataSourceName);
            }
            else
            {
                dataSource = database.DataSources[dataSourceName];
            }
            //if (dwConnectionString.ToUpper().Contains("PROVIDER=SQLOLEDB;"))
            //{
            dataSource.ConnectionString = dwConnectionString;// Regex.Replace(dwConnectionString, "PROVIDER=SQLOLEDB;", "", RegexOptions.IgnoreCase);
            dataSource.ManagedProvider = "System.Data.OleDb";// "System.Data.SqlClient";
            //}
            //else
            //{
            //    dataSource.ConnectionString = dwConnectionString;
            //}
            dataSource.Update();
            return dataSource;
        }
        private DataSourceView CreateDataSourceViewIfNotExist(Database database, string viewName, List<String> tableNames)
        {
            DataSourceView dataSourceView;
            if (!database.DataSourceViews.ContainsName(viewName))
            {
                DataSet dataSet = GenerateDWSchema(tableNames);
                dataSourceView = database.DataSourceViews.Add(viewName, viewName);
                dataSourceView.DataSourceID = viewName;
                dataSourceView.Schema = dataSet;
                dataSourceView.Update();
            }
            else
            {
                dataSourceView = database.DataSourceViews.GetByName(viewName);
            }
            return dataSourceView;
        }

        private void DeleteDatabase(string databaseName)
        {
            if (server.Databases.ContainsName(databaseName))
            {
                server.Databases[databaseName].Drop();
            }
        }
        private void CreateDim(Database database, DataSourceView dataSourceView, Justin.BI.OLAP.Entities.Dimension dim)
        {
            Dimension dimension;
            if (!database.Dimensions.ContainsName(dim.Name))
            {
                dimension = database.Dimensions.Add(dim.Name, dim.ID);
                dimension.UnknownMember = UnknownMemberBehavior.Hidden;
                dimension.AttributeAllMemberName = "all";
                dimension.StorageMode = DimensionStorageMode.Molap;
                dimension.Source = new DataSourceViewBinding(database.Name);
                dimension.Type = DimensionType.Regular;
            }
            else
            {
                dimension = database.Dimensions[dim.Name];
            }
            if ((dim.Hierarchies == null || dim.Hierarchies.Count == 0) && (dim.Levels != null && dim.Levels.Count != 0))
            {
                if (!dimension.Hierarchies.ContainsName(DefaultHierarchy))
                {
                    Hierarchy hierarchy = dimension.Hierarchies.Add(DefaultHierarchy);
                    hierarchy.AllMemberName = "all";
                    foreach (var level in dim.Levels)
                    {
                        if (!hierarchy.Levels.ContainsName(level.Name))
                        {
                            this.CreateDimensionAttributeForLevel(
                                    dimension
                                    , dataSourceView
                                    , level.Name
                                    , level.ID
                                    , level.SourceTable
                                    , level.KeyColumn
                                    , level.NameColumn
                                    , AttributeUsage.Key
                                    , OrderBy.Key
                                    , AttributeType.Regular
                                    , true);
                            Level lv = hierarchy.Levels.Add(level.Name);
                            lv.SourceAttributeID = dimension.Attributes.GetByName(level.Name).ID;
                        }

                    }
                }
            }
            else
            {
                foreach (var tempHierarchy in dim.Hierarchies)
                {
                    Hierarchy hierarchy;
                    if (!dimension.Hierarchies.ContainsName(tempHierarchy.Name))
                    {
                        hierarchy = dimension.Hierarchies.Add(tempHierarchy.Name);
                        hierarchy.AllMemberName = "all";
                    }
                    else
                    {
                        hierarchy = dimension.Hierarchies[tempHierarchy.Name];
                    }
                    if (tempHierarchy.Levels != null && tempHierarchy.Levels.Count > 0)
                    {
                        foreach (var tempLevel in tempHierarchy.Levels)
                        {
                            Level level;
                            if (hierarchy.Levels.ContainsName(tempLevel.Name))
                            {
                                level = hierarchy.Levels.Add(tempLevel.Name);
                                level.SourceAttributeID = dimension.Attributes.GetByName(tempLevel.Name).ID;
                            }
                            else
                            {
                                level = hierarchy.Levels[tempLevel.Name];
                            }
                        }
                    }
                }
            }
            dimension.Update();

        }
        public bool CreateDimensionAttributeForLevel(Dimension dimension, DataSourceView dataSourceView, String levelName, String id, String table, String keyColumn, String nameColumn, AttributeUsage attributeUsage, OrderBy orderBy, AttributeType attribType, bool visible)
        {
            if (dimension.Attributes == null)
            {
                return false;
            }

            DimensionAttribute dimAttrib;
            if (!dimension.Attributes.ContainsName(levelName))
            {
                dimAttrib = dimension.Attributes.Add(levelName, id);
                dimAttrib.Usage = attributeUsage;
                dimAttrib.OrderBy = orderBy;
                dimAttrib.Type = attribType;
                dimAttrib.AttributeHierarchyVisible = true;// visible;
                dimAttrib.AttributeHierarchyOptimizedState = OptimizationType.NotOptimized;
                dimAttrib.AttributeHierarchyOrdered = true;
                dimAttrib.IsAggregatable = true;
                //设置MembersWithDataCaption会导致当前粒度在子粒度中存在，形如"XXXX(All)"，因此注释掉
                //if (attributeUsage == AttributeUsage.Parent)
                //{
                //    dimAttrib.MembersWithDataCaption = "*(All)";
                //}
                dimAttrib.MembersWithData = MembersWithData.NonLeafDataHidden;

                DataItem diKey = CreateDataItem(dataSourceView, table, keyColumn);
                dimAttrib.KeyColumns.Add(diKey);
                if (!String.IsNullOrEmpty(nameColumn))
                {
                    DataItem diName = CreateDataItem(dataSourceView, table, nameColumn);
                    dimAttrib.NameColumn = diName;

                    DataItem diValue = CreateDataItem(dataSourceView, table, nameColumn);
                    dimAttrib.ValueColumn = diValue;
                }
                return true;
            }
            return true;
        }

        private DataSet GenerateDWSchema(List<String> tableNames, string dbSchema = "dbo")
        {
            OleDbConnection con = new OleDbConnection(dwConnectionString);

            DataSet dataSet = new DataSet();
            foreach (String tableName in tableNames)
            {
                string sql = "Select * From [" + dbSchema + "].[" + tableName + "] Where 1=0";
                OleDbDataAdapter sqlDataAdapter = new OleDbDataAdapter(sql, con);
                sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataTable[] dataTables = sqlDataAdapter.FillSchema(dataSet, SchemaType.Mapped, tableName);
                DataTable dataTable = dataTables[0];
                dataTable.ExtendedProperties.Add("TableType", "Table"); //为表增加扩展属性
                dataTable.ExtendedProperties.Add("DbSchemaName", dbSchema);
                dataTable.ExtendedProperties.Add("DbTableName", tableName);
                dataTable.ExtendedProperties.Add("FriendlyName", tableName);
            }
            return dataSet;
        }
        private DataItem CreateDataItem(DataSourceView parDsv, string parTableName, string parColumnName)
        {
            DataTable dataTable = ((DataSourceView)parDsv).Schema.Tables[parTableName];
            DataColumn dataColumn = dataTable.Columns[parColumnName];
            return new DataItem(parTableName, parColumnName, OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));
        }

        public void CreateSolution(Justin.BI.OLAP.Entities.Cube cube)
        {
            this.CheckConnect();
            Database database = this.CreateDatabaseIfNotExist(cube.Name);

            DataSource dataSource = this.CreateDataSourceIfNotExist(database, cube.Name);

            List<string> allTableNames = this.GetSchemaNames(cube);

            DataSourceView dataSourceView = this.CreateDataSourceViewIfNotExist(database, cube.Name, allTableNames);

            foreach (var item in cube.Dimensions)
            {
                this.CreateDim(database, dataSourceView, item);
            }
            Cube ssasCube = database.Cubes.FindByName(cube.Name);
            if (ssasCube != null)
                ssasCube.Drop();
            ssasCube = database.Cubes.Add(cube.Name, cube.Name);
            database.Update();
            MeasureGroup group = ssasCube.MeasureGroups.FindByName(cube.Name);
            if (group != null)
                group.Drop();
            group = ssasCube.MeasureGroups.Add(cube.Name, cube.Name);

            group.Measures.Clear();
            foreach (var item in cube.Measures)
            {
                Measure measure = new Measure(item.Name, item.Name);
                group.Measures.Add(measure);

            }
            group.Update();
            ssasCube.Update();


        }

        public void CreateCubeInner(Database db, Justin.BI.OLAP.Entities.Cube cube)
        {

            Cube ssasCube = db.Cubes.FindByName(cube.Name);
            if (ssasCube != null)
                ssasCube.Drop();
            ssasCube = db.Cubes.Add(cube.Name, cube.Name);

            ssasCube.Source = new DataSourceViewBinding(db.Name);
            ssasCube.StorageMode = StorageMode.Molap;

            foreach (var item in cube.Dimensions)
            {
                CubeDimension dim = ssasCube.Dimensions.FindByName(item.Name);
                if (dim == null)
                    dim = ssasCube.Dimensions.Add(item.Name);
            }

            CreateInternetSalesMeasureGroup(ssasCube, cube);



            ssasCube.Update(UpdateOptions.ExpandFull);
        }

        public void CreateInternetSalesMeasureGroup(Microsoft.AnalysisServices.Cube ssasCube, Justin.BI.OLAP.Entities.Cube cube)
        {
            // Create the Internet Sales measure group
            Database db = ssasCube.Parent;
            MeasureGroup mg = ssasCube.MeasureGroups.Add("Internet Sales");
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

            cubeDim = ssasCube.Dimensions.GetByName("Customer");
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

            ssasCube.Update(UpdateOptions.ExpandFull);
        }
        public void DeleteSolution(Justin.BI.OLAP.Entities.Cube cube)
        {
            this.DeleteDatabase(cube.Name);
        }

        public List<string> GetSchemaNames(Justin.BI.OLAP.Entities.Cube cube)
        {
            List<string> tables = new List<string>();
            if (cube.Dimensions == null || cube.Dimensions.Count < 1)
                return tables;
            foreach (var dim in cube.Dimensions)
            {
                if (dim.Levels != null && dim.Levels.Count > 0)
                {
                    foreach (var level in dim.Levels)
                    {
                        if (!tables.Contains(level.SourceTable))
                            tables.Add(level.SourceTable);
                    }
                }
                if (dim.Hierarchies != null && dim.Hierarchies.Count > 0)
                {
                    foreach (var hierarchy in dim.Hierarchies)
                    {
                        if (hierarchy.Levels != null || hierarchy.Levels.Count > 0)
                        {
                            foreach (var level in hierarchy.Levels)
                            {
                                if (!tables.Contains(level.SourceTable))
                                    tables.Add(level.SourceTable);
                            }
                        }
                    }
                }
            }

            return tables;
        }

    }
}
