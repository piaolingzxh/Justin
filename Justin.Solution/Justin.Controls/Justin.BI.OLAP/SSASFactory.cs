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
            //this.dwConnectionString = dwConnectionString;
            //  this.olapConnectionString = olapConnectionString;
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
            if (dwConnectionString.ToUpper().Contains("PROVIDER=SQLOLEDB;"))
            {
                dataSource.ConnectionString = Regex.Replace(dwConnectionString, "PROVIDER=SQLOLEDB;", "", RegexOptions.IgnoreCase);
                dataSource.ManagedProvider = "System.Data.SqlClient";
            }
            else
            {
                dataSource.ConnectionString = dwConnectionString;
            }
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

        public void CreateSolution(ISolution solution)
        {
            this.CheckConnect();
            Database database = this.CreateDatabaseIfNotExist(solution.Name);

            DataSource dataSource = this.CreateDataSourceIfNotExist(database, solution.Name);

            List<string> allTableNames = this.GetSchemaNames(solution);

            DataSourceView dataSourceView = this.CreateDataSourceViewIfNotExist(database, solution.Name, allTableNames);

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

    }
}
