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

        private Database CreateDatabase(string databaseName)
        {
            Database db = null;
            if ((server != null) && (server.Connected))
            {
                db = server.Databases.FindByName(databaseName);
                if (db != null)
                {
                    db.Drop();
                }

                db = server.Databases.Add(databaseName);
                db.Update();
            }
            return db;
        }
        private Database ProcessDatabase(Database db, ProcessType pt)
        {
            db.Process(pt);
            return db;
        }


        private DataSource CreateDataSource(Database dataBase, String strDataSourceName)
        {
            DataSource dataSource = dataBase.DataSources.FindByName(strDataSourceName);
            if (dataSource != null)
                dataSource.Drop();
            dataSource = dataBase.DataSources.Add(strDataSourceName, strDataSourceName);
            dataSource.ConnectionString = this.dwOleDbConnectionString;
            //OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder(dwOleDbConnectionString);
            //dataSource.ManagedProvider = builder.Provider;
            dataSource.Update();

            return dataSource;
        }


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


        #region  liumr
        private DataSourceView CreateDataSourceView(Database database, string viewName, List<String> tableNames)
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
        private void CreateDim(Database database, DataSourceView dataSourceView, IDim dim)
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
            //OleDbConnection con = new OleDbConnection(dwOleDbConnectionString);

            //DataSet dataSet = new DataSet();
            //foreach (String tableName in tableNames)
            //{
            //    string sql = "Select * From [" + dbSchema + "].[" + tableName + "] Where 1=0";
            //    OleDbDataAdapter sqlDataAdapter = new OleDbDataAdapter(sql, con);
            //    sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //    DataTable[] dataTables = sqlDataAdapter.FillSchema(dataSet, SchemaType.Mapped, tableName);
            //    DataTable dataTable = dataTables[0];
            //    dataTable.ExtendedProperties.Add("TableType", "Table"); //为表增加扩展属性
            //    dataTable.ExtendedProperties.Add("DbSchemaName", dbSchema);
            //    dataTable.ExtendedProperties.Add("DbTableName", tableName);
            //    dataTable.ExtendedProperties.Add("FriendlyName", tableName);
            //}
            DataSet dataSet = new DataSet();
            OleDbConnection oleDbConn = new OleDbConnection(this.dwOleDbConnectionString);

            foreach (var tableName in tableNames)
            {
                string sql = string.Format("select * from {0} where 0=1", tableName);
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, oleDbConn);
                adapter.FillSchema(dataSet, SchemaType.Mapped, tableName);
            }
            return dataSet;
        }
        private DataItem CreateDataItem(DataSourceView parDsv, string parTableName, string parColumnName)
        {
            DataTable dataTable = ((DataSourceView)parDsv).Schema.Tables[parTableName];
            DataColumn dataColumn = dataTable.Columns[parColumnName];
            return new DataItem(parTableName, parColumnName, OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));
        }

        public void CreateSolution(ISolution solution)
        {
            this.Connect();
            Database database = this.CreateDatabase(solution.Name);

            DataSource dataSource = this.CreateDataSource(database, solution.Name);

            List<string> allTableNames = this.GetSchemaNames(solution);

            DataSourceView dataSourceView = this.CreateDataSourceView(database, solution.Name, allTableNames);

            foreach (var item in solution.Dims)
            {
                this.CreateDim(database, dataSourceView, item);
            }

            foreach (var item in solution.Cubes)
            {

            }
        }
        public void DeleteSolution(ISolution solution)
        {
            this.DeleteDatabase(solution.Name);
        }



        public List<string> GetSchemaNames(ISolution solution)
        {
            List<string> tables = new List<string>();
            if (solution.Dims == null || solution.Dims.Count < 1)
                return tables;
            foreach (var dim in solution.Dims)
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

        #endregion


    }
}
