using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Justin.FrameWork.Helper;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            DataTable table = PrepareDataTable();

            Console.WriteLine(table.Columns.Count + "OK");
            Console.Read();
        }


        public static DataTable PrepareDataTable()
        {
            DataTable tempData = new DataTable();
            string[] columnNames = new string[] { "DimColumnName", "UniqueKey", "Drilled", "ca854c76ca47ce4e&维修↑维修", "d24282b7b2e72fc3&改扩建↑改扩建", "eec893efc10f1db9&新建↑新建" };
            Type[] columnTypes = new Type[] { typeof(string), typeof(string), typeof(bool), typeof(double), typeof(double), typeof(double) };

            #region 创建结构

            for (int i = 0; i < columnNames.Length; i++)
            {
                tempData.Columns.Add(columnNames[i], columnTypes[i]);
            }

            #endregion

            #region 填充数据



            AddRow(tempData, "侧挂", "[GCPS.BI.BI.InstallMeterModeDim.hieInfo].[3463d06de0ca9a15]", false, 176916, 223911, 187853);
            AddRow(tempData, "后挂", "[GCPS.BI.BI.InstallMeterModeDim.hieInfo].[7a37563370ded05f]", false, 171252, 187321, 189338);
            AddRow(tempData, "混合", "[GCPS.BI.BI.InstallMeterModeDim.hieInfo].[aa0efb9e19094440]", false, 203935, 183251, 185596);
            AddRow(tempData, "前挂", "[GCPS.BI.BI.InstallMeterModeDim.hieInfo].[4005f8da13f42943]", false, 185959, 190658, 173093);
            AddRow(tempData, "左侧挂", "[GCPS.BI.BI.InstallMeterModeDim.hieInfo].[e0327ad868d138f2]", false, 172331, 203227, 209642);




            #endregion
            return tempData;
        }

        public static void AddRow(DataTable table, string dimValueText, string dimValueUniqueKey, bool drilled, double weixiuValue, double gaikuojianValue, double newValue)
        {
            DataRow row = table.NewRow();
            row[0] = dimValueText;
            row[1] = dimValueUniqueKey;
            row[2] = drilled;
            row[3] = weixiuValue;
            row[4] = gaikuojianValue;
            row[5] = newValue;
            table.Rows.Add(row);
        }
    }


}
