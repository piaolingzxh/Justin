using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Justin.FrameWork.Helper;
using Microsoft.AnalysisServices;

namespace Justin.BI.OLAP
{
    public interface IOLAPFactory
    {
        void CreateSolution(Justin.BI.OLAP.Entities.Solution solution);
        void DeleteSolution(Justin.BI.OLAP.Entities.Solution solution);

    }
    public class SSASFactory : IOLAPFactory
    {
        private static string DefaultHierarchy = "DefaultHierarchy";
        private string dwOleDbConnectionString = "Provider=sqloledb;Data Source=.;Initial Catalog=DwDemo;User Id=sa;Password=sa;";
        private string olapConnectionString = "Data Source = .;Provider=msolap";//"Provider=MSOLAP.4;Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=Adventureworks;Data Source=(local);MDX Compatibility=1;Safety Options=2;MDX Missing Member Mode=Error";

        private Server server;


        public SSASFactory(string dwOleDbConnectionString, string olapConnectionString)
        {
            this.dwOleDbConnectionString = dwOleDbConnectionString;
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

        #region Database

        private Database CreateDatabase(string dataBaseId, string dataBaseName)
        {
            Database db = null;
            if ((server != null) && (server.Connected))
            {
                db = server.Databases.FindByName(dataBaseName);
                if (db != null)
                {
                    db.Drop();
                }

                db = server.Databases.Add(dataBaseName, dataBaseId);
                db.Update();
            }
            return db;
        }
        private void DeleteDatabase(string databaseName)
        {
            if (server.Databases.ContainsName(databaseName))
            {
                server.Databases[databaseName].Drop();
            }
        }
        private Database ProcessDatabase(Database database, ProcessType processType)
        {
            database.Process(processType);
            return database;
        }

        #endregion

        #region DataSource

        private DataSource CreateDataSource(Database dataBase, string dataSourceId, String dataSourceName)
        {
            DataSource dataSource = dataBase.DataSources.FindByName(dataSourceName);
            if (dataSource != null)
                dataSource.Drop();
            dataSource = dataBase.DataSources.Add(dataSourceName, dataSourceId);
            dataSource.ConnectionString = this.dwOleDbConnectionString;
            //OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder(dwOleDbConnectionString);
            //dataSource.ManagedProvider = builder.Provider;
            dataSource.Update();

            return dataSource;
        }

        private DataSourceView CreateDataSourceView(Database database, string dataSourceViewId, string dataSourceViewName, List<String> tableNames, List<ForeignKeyInfo> foreignKeys)
        {
            DataSourceView dataSourceView;
            if (!database.DataSourceViews.ContainsName(dataSourceViewName))
            {
                DataSet dataSet = GenerateDWSchema(tableNames);
                dataSourceView = database.DataSourceViews.Add(dataSourceViewName, dataSourceViewId);
                dataSourceView.DataSourceID = dataSourceViewId;
                dataSourceView.Schema = dataSet;


                foreach (ForeignKeyInfo foreignKey in foreignKeys)
                {
                    this.CreateViewRelation(dataSourceView, foreignKey.ForeignKeyTable, foreignKey.ForeignKeyColumn, foreignKey.PrimaryKeyTable, foreignKey.PrimaryKeyColumn);
                }

                dataSourceView.Update();
            }
            else
            {
                dataSourceView = database.DataSourceViews.GetByName(dataSourceViewName);
            }


            return dataSourceView;
        }
        private DataSet GenerateDWSchema(List<String> tableNames, string dbSchema = "dbo")
        {
            DataSet dataSet = new DataSet();
            OleDbConnection oleDbConn = new OleDbConnection(this.dwOleDbConnectionString);

            foreach (var tableName in tableNames)
            {
                string sql = string.Format("select * from {0} where 0=1", tableName);
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, oleDbConn);
                DataTable[] dataTables = adapter.FillSchema(dataSet, SchemaType.Mapped, tableName);
                DataTable dataTable = dataTables[0];
                dataTable.ExtendedProperties.Add("TableType", "Table"); //为表增加扩展属性
                dataTable.ExtendedProperties.Add("DbSchemaName", dbSchema);
                dataTable.ExtendedProperties.Add("DbTableName", tableName);
                dataTable.ExtendedProperties.Add("FriendlyName", tableName);
            }
            return dataSet;
        }
        public void CreateViewRelation(DataSourceView dataSourceView, String parFkTableName, String parFkColumnName, String parPkTableName, String parPkColumnName)
        {
            DataColumn fkColumn = dataSourceView.Schema.Tables[parFkTableName].Columns[parFkColumnName];
            DataColumn pkColumn = dataSourceView.Schema.Tables[parPkTableName].Columns[parPkColumnName];
            string fkName = "FK_" + parFkTableName + "_" + parPkTableName;
            fkName = GetFKName(dataSourceView, fkName, 0);
            dataSourceView.Schema.Relations.Add(fkName, pkColumn, fkColumn);
        }
        private string GetFKName(DataSourceView dataSourceView, string name, int idx)
        {
            string temp = name;
            foreach (DataRelation dr in dataSourceView.Schema.Relations)
            {
                if (dr.RelationName == name)
                {
                    idx++;
                    temp += idx.ToString();
                    return GetFKName(dataSourceView, temp, idx);
                }
            }
            return name;
        }

        //例子
        static DataSourceView CreateDataSourceView(Database db, string strDataSourceName)
        {
            // Create the data source view
            DataSourceView dsv = db.DataSourceViews.FindByName(strDataSourceName);
            if (dsv != null)
                dsv.Drop();
            dsv = db.DataSourceViews.Add(strDataSourceName, strDataSourceName);
            dsv.DataSourceID = strDataSourceName;
            dsv.Schema = new DataSet();
            dsv.Schema.Locale = CultureInfo.CurrentCulture;

            // Open a connection to the data source
            OleDbConnection connection = new OleDbConnection(dsv.DataSource.ConnectionString);
            connection.Open();

            #region Create tables

            // Add the DimTime table
            AddTable(dsv, connection, "DimTime");
            AddComputedColumn(dsv, connection, "DimTime", "SimpleDate", "DATENAME(mm, FullDateAlternateKey) + ' ' + DATENAME(dd, FullDateAlternateKey) + ',' + ' ' + DATENAME(yy, FullDateAlternateKey)");
            AddComputedColumn(dsv, connection, "DimTime", "CalendarYearDesc", "'CY' + ' ' + CalendarYear");
            AddComputedColumn(dsv, connection, "DimTime", "CalendarSemesterDesc", "CASE WHEN CalendarSemester = 1 THEN 'H1'+' '+ 'CY' +' '+ CONVERT(CHAR (4), CalendarYear) ELSE 'H2'+' '+ 'CY' +' '+ CONVERT(CHAR (4), CalendarYear) END");
            AddComputedColumn(dsv, connection, "DimTime", "CalendarQuarterDesc", "'Q' + CONVERT(CHAR (1), CalendarQuarter) +' '+ 'CY' +' '+ CONVERT(CHAR (4), CalendarYear)");
            AddComputedColumn(dsv, connection, "DimTime", "MonthName", "EnglishMonthName+' '+ CONVERT(CHAR (4), CalendarYear)");
            AddComputedColumn(dsv, connection, "DimTime", "FiscalYearDesc", "'FY' + ' ' + FiscalYear");
            AddComputedColumn(dsv, connection, "DimTime", "FiscalSemesterDesc", "CASE WHEN FiscalSemester = 1 THEN 'H1'+' '+ 'FY' +' '+ CONVERT(CHAR (4), FiscalYear) ELSE 'H2'+' '+ 'FY' +' '+ CONVERT(CHAR (4), FiscalYear) END");
            AddComputedColumn(dsv, connection, "DimTime", "FiscalQuarterDesc", "'Q' + CONVERT(CHAR (1), FiscalQuarter) +' '+ 'FY' +' '+ CONVERT(CHAR (4), FiscalYear)");
            AddComputedColumn(dsv, connection, "DimTime", "FiscalMonthNumberOfYear", "CASE WHEN MonthNumberOfYear = '1'  THEN CONVERT(int,'7') WHEN MonthNumberOfYear = '2'  THEN CONVERT(int,'8') WHEN MonthNumberOfYear = '3'  THEN CONVERT(int,'9') WHEN MonthNumberOfYear = '4'  THEN CONVERT(int,'10') WHEN MonthNumberOfYear = '5'  THEN CONVERT(int,'11') WHEN MonthNumberOfYear = '6'  THEN CONVERT(int,'12') WHEN MonthNumberOfYear = '7'  THEN CONVERT(int,'1') WHEN MonthNumberOfYear = '8'  THEN CONVERT(int,'2') WHEN MonthNumberOfYear = '9'  THEN CONVERT(int,'3') WHEN MonthNumberOfYear = '10' THEN CONVERT(int,'4') WHEN MonthNumberOfYear = '11' THEN CONVERT(int,'5') WHEN MonthNumberOfYear = '12' THEN CONVERT(int,'6') END");
            dsv.Update();

            // Add the DimGeography table
            AddTable(dsv, connection, "DimGeography");

            // Add the DimProductCategory table
            AddTable(dsv, connection, "DimProductCategory");

            // Add the DimProductSubcategory table
            AddTable(dsv, connection, "DimProductSubcategory");
            AddRelation(dsv, "DimProductSubcategory", "ProductCategoryKey", "DimProductCategory", "ProductCategoryKey");

            // Add the DimProduct table
            AddTable(dsv, connection, "DimProduct");
            AddComputedColumn(dsv, connection, "DimProduct", "ProductLineName", "CASE ProductLine WHEN 'M' THEN 'Mountain' WHEN 'R' THEN 'Road' WHEN 'S' THEN 'Accessory' WHEN 'T' THEN 'Touring' ELSE 'Components' END");
            AddRelation(dsv, "DimProduct", "ProductSubcategoryKey", "DimProductSubcategory", "ProductSubcategoryKey");
            dsv.Update();

            // Add the DimCustomer table
            AddTable(dsv, connection, "DimCustomer");
            AddComputedColumn(dsv, connection, "DimCustomer", "FullName", "CASE WHEN MiddleName IS NULL THEN FirstName + ' ' + LastName ELSE FirstName + ' ' + MiddleName + ' ' + LastName END");
            AddComputedColumn(dsv, connection, "DimCustomer", "GenderDesc", "CASE WHEN Gender = 'M' THEN 'Male' ELSE 'Female' END");
            AddComputedColumn(dsv, connection, "DimCustomer", "MaritalStatusDesc", "CASE WHEN MaritalStatus = 'S' THEN 'Single' ELSE 'Married' END");
            AddRelation(dsv, "DimCustomer", "GeographyKey", "DimGeography", "GeographyKey");

            // Add the DimReseller table
            AddTable(dsv, connection, "DimReseller");
            AddComputedColumn(dsv, connection, "DimReseller", "OrderFrequencyDesc", "CASE WHEN OrderFrequency = 'A' THEN 'Annual' WHEN OrderFrequency = 'S' THEN 'Bi-Annual' ELSE 'Quarterly' END");
            AddComputedColumn(dsv, connection, "DimReseller", "OrderMonthDesc", "CASE WHEN OrderMonth = '1' THEN 'January' WHEN OrderMonth = '2' THEN 'February' WHEN OrderMonth = '3' THEN 'March' WHEN OrderMonth = '4' THEN 'April' WHEN OrderMonth = '5' THEN 'May' WHEN OrderMonth = '6' THEN 'June' WHEN OrderMonth = '7' THEN 'July' WHEN OrderMonth = '8' THEN 'August' WHEN OrderMonth = '9' THEN 'September' WHEN OrderMonth = '10' THEN 'October' WHEN OrderMonth = '11' THEN 'November' WHEN OrderMonth = '12' THEN 'December' ELSE 'Never Ordered' END");

            // Add the DimCurrency table
            AddTable(dsv, connection, "DimCurrency");
            dsv.Update();

            // Add the DimSalesReason table
            AddTable(dsv, connection, "DimSalesReason");

            // Add the FactInternetSales table
            AddTable(dsv, connection, "FactInternetSales");
            AddRelation(dsv, "FactInternetSales", "ProductKey", "DimProduct", "ProductKey");
            AddRelation(dsv, "FactInternetSales", "CustomerKey", "DimCustomer", "CustomerKey");
            AddRelation(dsv, "FactInternetSales", "OrderDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactInternetSales", "ShipDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactInternetSales", "DueDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactInternetSales", "CurrencyKey", "DimCurrency", "CurrencyKey");
            dsv.Update();

            // Add the FactResellerSales table
            AddTable(dsv, connection, "FactResellerSales");
            AddRelation(dsv, "FactResellerSales", "ProductKey", "DimProduct", "ProductKey");
            AddRelation(dsv, "FactResellerSales", "ResellerKey", "DimReseller", "ResellerKey");
            AddRelation(dsv, "FactResellerSales", "OrderDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactResellerSales", "ShipDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactResellerSales", "DueDateKey", "DimTime", "TimeKey");
            AddRelation(dsv, "FactResellerSales", "CurrencyKey", "DimCurrency", "CurrencyKey");

            // Add the FactInternetSalesReason table
            AddTable(dsv, connection, "FactInternetSalesReason");
            AddCompositeRelation(dsv, "FactInternetSalesReason", "FactInternetSales", "SalesOrderNumber", "SalesOrderLineNumber");
            dsv.Update();

            // Add the FactCurrencyRate table
            AddTable(dsv, connection, "FactCurrencyRate");
            AddRelation(dsv, "FactCurrencyRate", "CurrencyKey", "DimCurrency", "CurrencyKey");
            AddRelation(dsv, "FactCurrencyRate", "TimeKey", "DimTime", "TimeKey");

            #endregion

            // Send the data source view definition to the server
            dsv.Update();

            return dsv;
        }
        static void AddTable(DataSourceView dsv, OleDbConnection connection, String tableName)
        {
            string strSelectText = "SELECT * FROM [dbo].[" + tableName + "] WHERE 1=0";
            OleDbDataAdapter adapter = new OleDbDataAdapter(strSelectText, connection);
            DataTable[] dataTables = adapter.FillSchema(dsv.Schema, SchemaType.Mapped, tableName);
            DataTable dataTable = dataTables[0];

            dataTable.ExtendedProperties.Add("TableType", "Table");
            dataTable.ExtendedProperties.Add("DbSchemaName", "dbo");
            dataTable.ExtendedProperties.Add("DbTableName", tableName);
            dataTable.ExtendedProperties.Add("FriendlyName", tableName);


            dataTable = null;
            dataTables = null;
            adapter = null;
        }
        static void AddComputedColumn(DataSourceView dsv, OleDbConnection connection, String tableName, String computedColumnName, String expression)
        {
            DataSet tmpDataSet = new DataSet();
            tmpDataSet.Locale = CultureInfo.CurrentCulture;
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ("
                + expression + ") AS [" + computedColumnName + "] FROM [dbo].["
                + tableName + "] WHERE 1=0", connection);
            DataTable[] dataTables = adapter.FillSchema(tmpDataSet, SchemaType.Mapped, tableName);
            DataTable dataTable = dataTables[0];
            DataColumn dataColumn = dataTable.Columns[computedColumnName];

            dataTable.Constraints.Clear();
            dataTable.Columns.Remove(dataColumn);

            dataColumn.ExtendedProperties.Add("DbColumnName", computedColumnName);
            dataColumn.ExtendedProperties.Add("ComputedColumnExpression", expression);
            dataColumn.ExtendedProperties.Add("IsLogical", "True");

            dsv.Schema.Tables[tableName].Columns.Add(dataColumn);

            dataColumn = null;
            dataTable = null;
            dataTables = null;
            adapter = null;
            tmpDataSet = null;
        }
        static void AddRelation(DataSourceView dsv, String fkTableName, String fkColumnName, String pkTableName, String pkColumnName)
        {
            DataColumn fkColumn = dsv.Schema.Tables[fkTableName].Columns[fkColumnName];
            DataColumn pkColumn = dsv.Schema.Tables[pkTableName].Columns[pkColumnName];
            dsv.Schema.Relations.Add("FK_" + fkTableName + "_" + fkColumnName, pkColumn, fkColumn, true);
        }
        static void AddCompositeRelation(DataSourceView dsv, String fkTableName, String pkTableName, String columnName1, String columnName2)
        {
            DataColumn[] fkColumns = new DataColumn[2];
            fkColumns[0] = dsv.Schema.Tables[fkTableName].Columns[columnName1];
            fkColumns[1] = dsv.Schema.Tables[fkTableName].Columns[columnName2];

            DataColumn[] pkColumns = new DataColumn[2];
            pkColumns[0] = dsv.Schema.Tables[pkTableName].Columns[columnName1];
            pkColumns[1] = dsv.Schema.Tables[pkTableName].Columns[columnName2];

            dsv.Schema.Relations.Add("FK_" + fkTableName + "_" + columnName1 + "_" + columnName2, pkColumns, fkColumns, true);
        }

        #endregion

        #region 维度

        private void CreateDim(Database database, DataSourceView dataSourceView, Justin.BI.OLAP.Entities.Dimension dim)
        {
            Dimension ssasDimension;
            if (!database.Dimensions.Contains(dim.ID))
            {
                ssasDimension = database.Dimensions.Add(dim.Name, dim.ID);
                ssasDimension.UnknownMember = UnknownMemberBehavior.Hidden;
                ssasDimension.AttributeAllMemberName = "all";
                ssasDimension.StorageMode = DimensionStorageMode.Molap;
                ssasDimension.Source = new DataSourceViewBinding(dataSourceView.ID);
                ssasDimension.Type = DimensionType.Regular;
            }
            else
            {
                ssasDimension = database.Dimensions[dim.ID];
            }
            if ((dim.Hierarchies == null || dim.Hierarchies.Count == 0) && (dim.Levels != null && dim.Levels.Count != 0))
            {
                if (!ssasDimension.Hierarchies.Contains(DefaultHierarchy))
                {
                    Hierarchy ssasHierarchy = ssasDimension.Hierarchies.Add(DefaultHierarchy, DefaultHierarchy);
                    ssasHierarchy.AllMemberName = "all";
                    CreateLevels(ssasDimension, ssasHierarchy, dataSourceView, dim.Levels);
                }
            }
            else
            {
                foreach (var outerHierarchy in dim.Hierarchies)
                {
                    Hierarchy ssasHierarchy;
                    if (!ssasDimension.Hierarchies.Contains(outerHierarchy.ID))
                    {
                        ssasHierarchy = ssasDimension.Hierarchies.Add(outerHierarchy.Name, outerHierarchy.ID);
                        ssasHierarchy.AllMemberName = "all";
                    }
                    else
                    {
                        ssasHierarchy = ssasDimension.Hierarchies[outerHierarchy.ID];
                    }

                    if (outerHierarchy.Levels != null && outerHierarchy.Levels.Count > 0)
                    {
                        CreateLevels(ssasDimension, ssasHierarchy, dataSourceView, outerHierarchy.Levels);
                    }
                }
            }
            ssasDimension.Update();

        }
        private void CreateLevels(Dimension ssasDimension, Hierarchy ssasHierarchy, DataSourceView dataSourceView, List<Justin.BI.OLAP.Entities.Level> outerLevels)
        {
            var miniOuterLevel = outerLevels[outerLevels.Count - 1];
            this.CreateDimensionAttributeForLevel(
                        ssasDimension
                        , dataSourceView
                        , miniOuterLevel.ID
                        , miniOuterLevel.Name
                        , miniOuterLevel.SourceTable
                        , miniOuterLevel.KeyColumn
                        , miniOuterLevel.NameColumn
                        , AttributeUsage.Key
                        , OrderBy.Key
                        , AttributeType.Regular
                        , true);

            foreach (var outerLevel in outerLevels)
            {
                Level ssasLevel;
                if (!ssasHierarchy.Levels.Contains(outerLevel.ID))
                {
                    this.CreateDimensionAttributeForLevel(
                        ssasDimension
                        , dataSourceView
                        , outerLevel.ID
                        , outerLevel.Name
                        , outerLevel.SourceTable
                        , outerLevel.KeyColumn
                        , outerLevel.NameColumn
                        , AttributeUsage.Regular
                        , OrderBy.Key
                        , AttributeType.Regular
                        , true);
                    ssasLevel = ssasHierarchy.Levels.Add(outerLevel.Name);
                    ssasLevel.SourceAttributeID = ssasDimension.Attributes.GetByName(outerLevel.Name).ID;
                }
                else
                {
                    ssasLevel = ssasHierarchy.Levels[outerLevel.ID];
                }
            }
        }
        //--------------------
        public bool CreateDimensionAttributeForLevel(Dimension dimension, DataSourceView dataSourceView,
            String levelId, String levelName, String tableName, String keyColumn, String nameColumn,
            AttributeUsage attributeUsage, OrderBy orderBy, AttributeType attribType, bool visible)
        {
            if (dimension.Attributes == null)
            {
                return false;
            }

            DimensionAttribute dimAttrib;
            if (!dimension.Attributes.Contains(levelId))
            {
                dimAttrib = dimension.Attributes.Add(levelName, levelId);
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

                DataItem diKey = CreateDataItem(dataSourceView, tableName, keyColumn);
                dimAttrib.KeyColumns.Add(diKey);
                if (!String.IsNullOrEmpty(nameColumn))
                {
                    DataItem diName = CreateDataItem(dataSourceView, tableName, nameColumn);
                    dimAttrib.NameColumn = diName;

                    DataItem diValue = CreateDataItem(dataSourceView, tableName, nameColumn);
                    dimAttrib.ValueColumn = diValue;
                }
                return true;
            }
            return true;
        }
        private DataItem CreateDataItem(DataSourceView parDsv, string parTableName, string parColumnName)
        {
            DataTable dataTable = ((DataSourceView)parDsv).Schema.Tables[parTableName];
            DataColumn dataColumn = dataTable.Columns[parColumnName];
            return new DataItem(parTableName, parColumnName, OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));
        }

        #endregion

        #region Cube
        private void CreateCube(Database database,DataSource dataSource, DataSourceView dataSourceView, Justin.BI.OLAP.Entities.Cube cube)
        {

            Cube ssasCube = database.Cubes.Find(cube.ID);
            if (ssasCube != null)
                ssasCube.Drop();

            ssasCube = database.Cubes.Add(cube.Name, cube.ID);
            ssasCube.Source = new DataSourceViewBinding(dataSourceView.ID);
            ssasCube.StorageMode = StorageMode.Molap;
            ssasCube.Visible = true;
            foreach (var item in cube.Dimensions)
            {
                ssasCube.Dimensions.Add(item.ID);
            }

            var defaultGroup = ssasCube.MeasureGroups.Add("Default");
            defaultGroup.StorageMode = StorageMode.Molap;
            defaultGroup.ProcessingMode = ProcessingMode.LazyAggregations;


            foreach (var measure in cube.Measures)
            {
                var ssasMeasure = defaultGroup.Measures.Add(measure.Name, measure.ID);
                ssasMeasure.Source = this.CreateDataItem(dataSourceView, cube.TableName, measure.ColumnName);
                ssasMeasure.AggregateFunction = measure.AggregationFunction;
                //measure.FormatString = format;// "000,000.##";
                //measure.Description = description;
                //measure.DisplayFolder = folderName;
            }
            foreach (var dim in cube.Dimensions)
            {
                var cubeDim = ssasCube.Dimensions.Find(dim.ID);
                var groupDim = defaultGroup.Dimensions.Add(dim.ID);
                string levelId = "";
                for (int i = 0; i < cubeDim.Attributes.Count; i++)
                {
                    if (cubeDim.Dimension.Attributes[i].Usage == AttributeUsage.Key)
                    {
                        levelId = cubeDim.Dimension.Attributes[i].ID;
                        break;
                    }
                }
                var measureGroupAttribute = groupDim.Attributes.Add(levelId);
                measureGroupAttribute.Type = MeasureGroupAttributeType.Granularity;

                measureGroupAttribute.KeyColumns.Add(CreateDataItem(dataSourceView, cube.TableName, dim.FKColumn));

            }
            CreatePartition(defaultGroup, dataSource, cube.TableName, DateTime.Now);
            ssasCube.Update(UpdateOptions.ExpandFull);
            ssasCube.Process();

        }

        #endregion
        public void CreatePartition(MeasureGroup measureGroup, DataSource relationalDataSource, String tableName, DateTime dateTime)
        {
            string partitionName = tableName + DateTime.Now.ToString("yyyyMM");
            if (measureGroup.Partitions.IndexOfName(partitionName) < 0)
            {
                var partition = measureGroup.Partitions.Add(partitionName);
                partition.Source = new QueryBinding(relationalDataSource.ID, "Select * From " + tableName);
                partition.StorageMode = StorageMode.Molap;
            }
        }
        public List<string> GetAllTableNames(Justin.BI.OLAP.Entities.Solution solution, out  List<ForeignKeyInfo> ForeignKeys)
        {
            List<string> tables = new List<string>();
            ForeignKeys = new List<ForeignKeyInfo>();

            if (solution.Cubes == null || solution.Cubes.Count < 1)
            {
                return tables;
            }

            foreach (var cube in solution.Cubes)
            {
                if (!tables.Contains(cube.TableName))
                {
                    tables.Add(cube.TableName);
                }

                if (cube.Dimensions == null || cube.Dimensions.Count < 1)
                    continue;
                foreach (var dim in cube.Dimensions)
                {
                    if (dim.Levels != null && dim.Levels.Count > 0)
                    {
                        foreach (var level in dim.Levels)
                        {
                            if (!tables.Contains(level.SourceTable))
                            {
                                tables.Add(level.SourceTable);
                                ForeignKeys.Add(new ForeignKeyInfo() { ForeignKeyTable = cube.TableName, ForeignKeyColumn = dim.FKColumn, PrimaryKeyTable = level.SourceTable, PrimaryKeyColumn = level.KeyColumn });
                            }
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
                                    {
                                        tables.Add(level.SourceTable);
                                        ForeignKeys.Add(new ForeignKeyInfo() { ForeignKeyTable = cube.TableName, ForeignKeyColumn = dim.FKColumn, PrimaryKeyTable = level.SourceTable, PrimaryKeyColumn = level.KeyColumn });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tables;
        }

        public void CreateSolution(Justin.BI.OLAP.Entities.Solution solution)
        {
            List<ForeignKeyInfo> foreignKeys = new List<ForeignKeyInfo>();
            List<string> allTableNames = this.GetAllTableNames(solution, out foreignKeys);

            this.Connect();
            Database database = this.CreateDatabase(solution.ID, solution.Name);
            DataSource dataSource = this.CreateDataSource(database, solution.ID, solution.Name);
            DataSourceView dataSourceView = this.CreateDataSourceView(database, solution.ID, solution.Name, allTableNames, foreignKeys);

            foreach (var cube in solution.Cubes)
            {
                foreach (var dim in cube.Dimensions)
                {
                    this.CreateDim(database, dataSourceView, dim);
                }
                this.CreateCube(database,dataSource ,dataSourceView, cube);

            }
        }
        public void DeleteSolution(Justin.BI.OLAP.Entities.Solution solution)
        {
            this.DeleteDatabase(solution.Name);
        }




    }

    public class ForeignKeyInfo
    {
        public string ForeignKeyTable { get; set; }
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyTable { get; set; }
        public string PrimaryKeyColumn { get; set; }
    }
}
