using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Controls.CubeView
{
    public class CubeOperate
    {
        public CubeOperate(String adomdConnectionString)
        {
            this.ConnectionString = adomdConnectionString;
            Conn = new AdomdConnection(adomdConnectionString);

            Conn.Open();
            CubeDefs = Conn.Cubes.Cast<CubeDef>().ToList();
            Cubes = GetCubes();
            Dimensions = GetDimensions();
        }
        public string ConnectionString { get; private set; }
        public AdomdConnection Conn { private set; get; }
        public List<CubeDef> CubeDefs { get; private set; }
        public IEnumerable<CubeDef> Cubes { get; private set; }
        public IEnumerable<CubeDef> Dimensions { get; private set; }

        public CubeDef GetCube(string cubeName)
        {
            return Cubes.Where(r => r.Name.Replace("$", "").Equals(cubeName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }
        public IEnumerable<CubeDef> GetCubes()
        {
            var list = CubeDefs.Where(r => r.Type.Equals(CubeType.Cube));
            return list;
        }
        public CubeDef GetDimension(string dimensionName)
        {
            return Dimensions.Where(r => r.Name.Equals(dimensionName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }
        public IEnumerable<CubeDef> GetDimensions()
        {
            var list = CubeDefs.Where(r => r.Type.Equals(CubeType.Dimension));
            return list;
        }

        public IEnumerable<Measure> GetMeasures(string cubeName)
        {
            return GetCube(cubeName).Measures.Cast<Measure>();
        }

    }
}
