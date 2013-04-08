using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.FrameWork.Extensions
{
    public static class CellSetEx
    {
        public static DataTable ToDataTable(this CellSet cs)
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn();
                DataRow dr = null;
                //第一列：必有为维度描述（行头）
                int col = 0;
                if (cs.Axes.Count > 1)
                {
                    string str = " ";
                    dt.Columns.Add(new DataColumn(str));
                    col = 1;
                    if (cs.Axes[1].Positions.Count > 0 && cs.Axes[1].Positions[0] != null)
                    {
                        col = cs.Axes[1].Positions[0].Members.Count;
                        for (int i = 1; i < col; i++)
                        {
                            str += " ";
                            dt.Columns.Add(new DataColumn(str));
                        }
                    }
                }
                //生成数据列对象
                string name;
                foreach (Position p in cs.Axes[0].Positions)
                {
                    dc = new DataColumn();
                    name = "";
                    foreach (Member m in p.Members)
                    {
                        name = name + m.Caption + " ";
                    }
                    dc.ColumnName = name.Trim();
                    dt.Columns.Add(dc);
                }
                //添加行数据
                int pos = 0;
                if (cs.Axes.Count == 1)
                {
                    dr = dt.NewRow();
                    //数据列
                    for (int x = col; x < cs.Axes[0].Positions.Count + col; x++)
                    {
                        try
                        {
                            dr[x] = cs[pos++].FormattedValue;
                            if (dr[x].ToString() == "null")
                            {
                                dr[x] = "";
                            }
                        }
                        catch
                        {
                            dr[x] = "";
                        }
                    }
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (Position py in cs.Axes[1].Positions)
                    {
                        dr = dt.NewRow();
                        //维度描述列数据（行头）
                        for (int i = 0; i < py.Members.Count; i++)
                        {
                            dr[i] = py.Members[i].Caption;
                        }
                        //数据列
                        for (int x = col; x < cs.Axes[0].Positions.Count + col; x++)
                        {
                            try
                            {
                                dr[x] = cs[pos++].FormattedValue;
                                if (dr[x].ToString() == "null")
                                {
                                    dr[x] = "";
                                }
                            }
                            catch
                            {
                                dr[x] = "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
