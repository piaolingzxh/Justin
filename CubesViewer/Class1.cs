using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;
namespace MyAdomd
{
    public class CubesViewer
    {
        /// <summary>
        /// 
        /// </summary>
        private AdomdConnection con = null;


        private string conString = null;


        private bool isOpen = false;
        /// <summary>
        /// 获取或设置AdomdConnection
        /// </summary>
        public AdomdConnection Connection
        {
            get { return con; }
            set { con = value; }
        }
        /// <summary>
        /// 获取AdomdConnection连接状态
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
        }
        /// <summary>
        /// 获取或设置AdomdConnection连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return conString; }
            set
            {
                conString = value;
                con.ConnectionString = conString;

            }
        }
        /// <summary>
        /// 打开Adomd连接
        /// </summary>
        public void OpenConnection()
        {
            this.con.Open();
            this.isOpen = true;
        }
        /// <summary>
        /// 关闭Adomd连接
        /// </summary>
        public void CloseConnection()
        {
            this.con.Close();

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public CubesViewer()
        {
            con = new AdomdConnection();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public CubesViewer(String connectionString)
        {
            this.conString = connectionString;
            con = new AdomdConnection(conString);
        }
        /// <summary>
        /// 获取所有立方体的信息
        /// </summary>
        /// <param name="cubs">数据库所有立方休信息</param>
        /// <returns>所有立方体的信息</returns>
        public static DataTable Cubes(CubeCollection cubs)
        {

            DataTable table = new DataTable("Cubes");
            table.Columns.Add("Name");
            table.Columns.Add("Type");
            DataRow row;
            for (int i = 0; i < cubs.Count; i++)
            {
                row = table.NewRow();
                row["Name"] = cubs[i].Name;
                row["Type"] = cubs[i].Type.ToString();

                table.Rows.Add(row);
            }
            return table;

        }
        /// <summary>
        /// 获取多维数据集信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>多维数据集信息</returns>
        public static DataTable CubesCube(CubeCollection cubes)
        {
            DataTable table = new DataTable("Cubers");
            table.Columns.Add(new DataColumn("Name"));

            table.Columns.Add(new DataColumn("Caption"));
            DataRow row;
            foreach (CubeDef cub in cubes)
            {
                if (cub.Type.Equals(CubeType.Cube))
                {


                    row = table.NewRow();
                    row["Name"] = cub.Name;
                    row["Caption"] = cub.Caption;
                    table.Rows.Add(row);

                }

            }

            return table;
        }
        /// <summary>
        /// 获取所有度量值信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有度量值信息</returns>
        public static DataTable CubesMeasures(CubeCollection cubes)
        {
            DataTable table = new DataTable("Measurs");
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("CuberCaption"));


            DataRow row;

            foreach (CubeDef cub in cubes)
            {
                if (cub.Type.Equals(CubeType.Cube))
                {
                    foreach (Measure measure in cub.Measures)
                    {

                        row = table.NewRow();
                        row["CubeName"] = cub.Name;
                        row["CuberCaption"] = cub.Caption;
                        row["Name"] = measure.Name;
                        row["UniqueName"] = measure.UniqueName;
                        row["Caption"] = measure.Caption;
                        table.Rows.Add(row);
                    }
                }


            }

            return table;



        }
        /// <summary>
        /// 获取所有维度信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有维度信息</returns>
        public static DataTable CubesDimensions(CubeCollection cubes)
        {
            DataTable table = new DataTable("Dimensions");
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("CuberCaption"));

            DataRow row;
            string name = null;

            foreach (CubeDef cub in cubes)
            {

                DimensionCollection dimCollection = null;
                try { dimCollection = cub.Dimensions; }
                catch { }
                if (dimCollection == null) continue;
                if (cub.Type.Equals(CubeType.Dimension))
                {
                    name = cub.Name;
                    name = name.Replace("$", "");
                    Dimension dim = dimCollection[name];
                    row = table.NewRow();
                    row["CubeName"] = cub.Name;
                    row["CuberCaption"] = cub.Caption;
                    row["Name"] = dim.Name;
                    row["UniqueName"] = dim.UniqueName;
                    row["Caption"] = dim.Caption;
                    table.Rows.Add(row);

                }
                else
                {
                    foreach (var dim in dimCollection)
                    {
                        name = cub.Name;
                        name = name.Replace("$", "");
                        row = table.NewRow();
                        row["CubeName"] = cub.Name;
                        row["CuberCaption"] = cub.Caption;
                        row["Name"] = dim.Name;
                        row["UniqueName"] = dim.UniqueName;
                        row["Caption"] = dim.Caption;
                        table.Rows.Add(row);
                    }
                }


            }

            return table;
        }
        /// <summary>
        /// 获取所有层次结构信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有层次结构信息</returns>
        public static DataTable CubesHiberarchies(CubeCollection cubes)
        {
            DataTable table = new DataTable("CubeHiberachies");
            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("DimName"));

            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("Caption"));

            DataRow row = null;
            string name = null;
            foreach (CubeDef cub in cubes)
            {
                if (cub.Type.Equals(CubeType.Dimension))
                {
                    name = cub.Name;
                    name = name.Replace("$", "");
                    Dimension dim = cub.Dimensions[name];
                    foreach (Hierarchy hierarchy in dim.Hierarchies)
                    {
                        row = table.NewRow();
                        row["CubeName"] = cub.Name;
                        row["DimName"] = dim.UniqueName;
                        row["UniqueName"] = hierarchy.UniqueName;
                        row["Name"] = hierarchy.Name;
                        row["Caption"] = hierarchy.Caption;
                        table.Rows.Add(row);
                    }

                }
            }
            return table;


        }
        /// <summary>
        /// 获取维度的层次结构信息
        /// </summary>
        /// <param name="dim">维度</param>
        /// <returns>维度的层次结构信息</returns>

        public static DataTable DimHiberarchies(Dimension dim)
        {
            DataTable table = new DataTable("DimHiberachies");

            table.Columns.Add(new DataColumn("DimName"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("Caption"));

            DataRow row = null;


            foreach (Hierarchy hierarchy in dim.Hierarchies)
            {
                row = table.NewRow();
                row["DimName"] = dim.UniqueName;
                row["UniqueName"] = hierarchy.UniqueName;
                row["Name"] = hierarchy.Name;
                row["Caption"] = hierarchy.Caption;
                table.Rows.Add(row);
            }

            return table;


        }
        /// <summary>
        /// 获取所有级别信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有级别信息</returns>
        public static DataTable CubesLevels(CubeCollection cubes)
        {
            DataTable table = new DataTable("CubeLevels");
            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("DimName"));
            table.Columns.Add(new DataColumn("HierarchyName"));

            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("MemberCount"));

            DataRow row = null;
            string name = null;
            foreach (CubeDef cub in cubes)
            {
                if (cub.Type.Equals(CubeType.Dimension))
                {
                    name = cub.Name;
                    name = name.Replace("$", "");
                    Dimension dim = cub.Dimensions[name];


                    foreach (Hierarchy hierarchy in dim.Hierarchies)
                    {
                        foreach (Level level in hierarchy.Levels)
                        {
                            row = table.NewRow();
                            row["CubeName"] = cub.Name;
                            row["DimName"] = dim.UniqueName;
                            row["HierarchyName"] = hierarchy.UniqueName;
                            row["UniqueName"] = level.UniqueName;
                            row["Name"] = level.Name;
                            row["Caption"] = level.Caption;
                            row["MemberCount"] = level.MemberCount.ToString();
                            table.Rows.Add(row);
                        }
                    }

                }
            }
            return table;

        }
        /// <summary>
        /// 获取层次结构级别信息
        /// </summary>
        /// <param name="hiberarchy">层次结构</param>
        /// <returns>层次结构级别信息</returns>
        public static DataTable HierarchyLevels(Hierarchy hiberarchy)
        {
            DataTable table = new DataTable("HiberarchyLevels");
            table.Columns.Add(new DataColumn("HierarchyName"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("MemberCount"));

            DataRow row = null;


            foreach (Level level in hiberarchy.Levels)
            {
                row = table.NewRow();
                row["HierarchyName"] = hiberarchy.UniqueName;
                row["UniqueName"] = level.UniqueName;
                row["Name"] = level.Name;
                row["Caption"] = level.Caption;
                row["MemberCount"] = level.MemberCount.ToString();
                table.Rows.Add(row);
            }

            return table;

        }

        /// <summary>
        /// 获取所有级别成员的信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有级别成员的信息</returns>
        public static DataTable CubesMembers(CubeCollection cubes)
        {
            DataTable table = new DataTable("CubeMembers");
            table.Columns.Add(new DataColumn("DimName"));
            table.Columns.Add(new DataColumn("HierarchieName"));
            table.Columns.Add(new DataColumn("LevelName"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("LevelDepth"));
            table.Columns.Add(new DataColumn("ChildCount"));
            table.Columns.Add(new DataColumn("DrilledDown"));
            DataRow row = null;
            string name = null;
            foreach (CubeDef cub in cubes)
            {

                if (cub.Type.Equals(CubeType.Dimension))
                {
                    name = cub.Name;
                    name = name.Replace("$", "");
                    Dimension dim = cub.Dimensions[name];

                    foreach (Hierarchy hierarchy in dim.Hierarchies)
                    {

                        foreach (Level level in hierarchy.Levels)
                        {
                            foreach (Member member in level.GetMembers())
                            {


                                row = table.NewRow();
                                row["DimName"] = dim.UniqueName;
                                row["HierarchieName"] = hierarchy.UniqueName;
                                row["LevelName"] = member.LevelName;
                                row["UniqueName"] = member.UniqueName;
                                row["Name"] = member.Name;
                                row["LevelDepth"] = member.LevelDepth.ToString();
                                row["ChildCount"] = member.ChildCount.ToString();
                                row["DrilledDown"] = member.DrilledDown;
                                table.Rows.Add(row);

                            }
                        }

                    }
                }
            }

            return table;


        }
        /// <summary>
        /// 获取层次结构级别成员信息
        /// </summary>
        /// <param name="level">层次结构级别</param>
        /// <returns>层次结构级别成员信息</returns>
        public static DataTable LevelMembers(Level level)
        {
            DataTable table = new DataTable("CubeMembers");
            table.Columns.Add(new DataColumn("LevelName"));
            table.Columns.Add(new DataColumn("Caption"));
            table.Columns.Add(new DataColumn("UniqueName"));
            table.Columns.Add(new DataColumn("Name"));

            table.Columns.Add(new DataColumn("LevelDepth"));
            table.Columns.Add(new DataColumn("ChildCount"));

            DataRow row = null;

            foreach (Member member in level.GetMembers())
            {
                row = table.NewRow();
                row["LevelName"] = member.LevelName;
                row["UniqueName"] = member.UniqueName;
                row["Name"] = member.Name;
                row["Caption"] = member.Caption;
                row["LevelDepth"] = member.LevelDepth.ToString();
                row["ChildCount"] = member.ChildCount.ToString();

                table.Rows.Add(row);

            }
            return table;


        }
        /// <summary>
        /// 获取所有命名集信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有命名集信息</returns>
        public static DataTable CubesNameSet(CubeCollection cubes)
        {

            DataTable table = new DataTable("CubeNameSet");
            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("Description"));
            table.Columns.Add(new DataColumn("Expression"));
            DataRow row = null;

            foreach (CubeDef cub in cubes)
            {
                foreach (NamedSet name in cub.NamedSets)
                {
                    row = table.NewRow();
                    try
                    {
                        row["Name"] = name.Name;
                        row["CubeName"] = cub.Name;
                        row["Description"] = name.Description;
                        row["Expression"] = name.Expression;
                    }
                    catch { }
                    table.Rows.Add(row);

                }

            }
            return table;


        }
        /// <summary>
        /// 获取所有KPI信息
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <returns>所有KPI信息</returns>
        public static DataTable CubesKpiNames(CubeCollection cubes)
        {
            DataTable table = new DataTable("CubeKpis");
            table.Columns.Add("Name");
            table.Columns.Add("Caption");
            table.Columns.Add("StatusGraphic");
            table.Columns.Add("TrendGraphic");

            table.Columns.Add(new DataColumn("CubeName"));
            table.Columns.Add(new DataColumn("CuberCaption"));
            DataRow row = null;
            foreach (CubeDef cub in cubes)
            {
                KpiCollection kpis = null;
                try { kpis = cub.Kpis; }
                catch { }
                if (kpis == null) continue;
                foreach (Kpi kpi in cub.Kpis)
                {
                    row = table.NewRow();
                    row["Name"] = kpi.Name;
                    row["Caption"] = kpi.Caption;
                    row["StatusGraphic"] = kpi.StatusGraphic;
                    row["TrendGraphic"] = kpi.TrendGraphic;
                    row["CubeName"] = cub.Name;
                    row["CuberCaption"] = cub.Caption;
                    table.Rows.Add(row);

                }
            }
            return table;

        }
        /// <summary>
        /// 获取维度对象
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <param name="dimName">维度名称</param>
        /// <returns>维度对象</returns>
        public static Dimension GetDimByName(CubeCollection cubes, string dimName)
        {
            Dimension dim = null;

            string cubeName = "$" + dimName;
            if (cubes[cubeName] != null)
            {
                dim = cubes[cubeName].Dimensions[dimName];

            }
            return dim;
        }
        /// <summary>
        /// 获取层次结构对象
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <param name="dimName">维度名称</param>
        /// <param name="hiberarchyName">层次结构名称</param>
        /// <returns>层次结构对象</returns>
        public static Hierarchy GetHierarchyByName(CubeCollection cubes, string dimName, string hiberarchyName)
        {
            Hierarchy hiberarchy = null;
            string cubeName = "$" + dimName;
            if (cubes[cubeName] != null)
            {
                hiberarchy = cubes[cubeName].Dimensions[dimName].Hierarchies[hiberarchyName];

            }
            return hiberarchy;



        }
        /// <summary>
        /// 获取层次结构的级别
        /// </summary>
        /// <param name="cubes">数据库所有立方休信息</param>
        /// <param name="dimName">维度名称</param>
        /// <param name="hiberarchyName">层次结构名称</param>
        /// <param name="levelName">层次强构级别名称</param>
        /// <returns>层次强构级别对象</returns>
        public static Level GetLevelByName(CubeCollection cubes, string dimName, string hiberarchyName, string levelName)
        {
            Level level = null;
            string cubeName = "$" + dimName;
            if (cubes[cubeName] != null)
            {
                level = cubes[cubeName].Dimensions[dimName].Hierarchies[hiberarchyName].Levels[levelName];

            }
            return level;



        }




    }
}