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
        public static DataTable ToDataTable(this CellSet cs, bool useFormattedValue = false)
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
                    //string str = " ";
                    //dt.Columns.Add(new DataColumn(str));
                    //col = 1;
                    if (cs.Axes[1].Positions.Count > 0 && cs.Axes[1].Positions[0] != null)
                    {
                        col = cs.Axes[1].Positions[0].Members.Count;
                        for (int i = 0; i < col; i++)
                        {
                            dt.Columns.Add(new DataColumn(cs.Axes[1].Positions[0].Members[i].UniqueName));
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
                            if (useFormattedValue)
                            {
                                dr[x] = cs[pos++].FormattedValue;
                            }
                            else
                            {
                                dr[x] = cs[pos++].Value;
                            }
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
                                if (useFormattedValue)
                                {
                                    dr[x] = cs[pos++].FormattedValue;
                                }
                                else
                                {
                                    dr[x] = cs[pos++].Value;
                                }

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

        public static DataTable ToDataTable2(this CellSet cs, bool useFormattedValue = false)
        {
            try
            {
                DataTable dt = new DataTable();

                int columnCountOfRowHeader = 0;

                #region 构造表头

                //当有行头时，添加行头占的列
                if (cs.Axes.Count > 1 && cs.Axes[1].Set.Tuples.Count > 0)
                {
                    columnCountOfRowHeader = cs.Axes[1].Set.Tuples[0].Members.Count;
                    for (int i = 0; i < columnCountOfRowHeader; i++)
                    {
                        Member member = cs.Axes[1].Set.Tuples[0].Members[i];
                        DataColumn column = new DataColumn(member.UniqueName);
                        column.ExtendedProperties["Data"] = member;

                        dt.Columns.Add(column);
                    }
                }

                //继续添加列头
                foreach (Microsoft.AnalysisServices.AdomdClient.Tuple tp in cs.Axes[0].Set.Tuples)
                {

                    string columnName = "";
                    foreach (Member member in tp.Members)
                    {
                        columnName = columnName + member.Caption + " ";
                    }
                    DataColumn dc = new DataColumn(columnName.Trim());

                    dt.Columns.Add(dc);
                }

                #endregion

                #region 构造数据

                int rowCount = cs.Axes.Count <= 1 ? 0 : cs.Axes[1].Set.Tuples.Count;
                int cellIndex = 0;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataRow dr = dt.NewRow();
                    if (cs.Axes.Count > 1)
                    {
                        for (int columnIndexOfRowHeader = 0; columnIndexOfRowHeader < columnCountOfRowHeader; columnIndexOfRowHeader++)
                        {
                            dr[columnIndexOfRowHeader] = cs.Axes[1].Set.Tuples[rowIndex].Members[columnIndexOfRowHeader];
                        }
                    }

                    //数据列
                    for (int columnIndexOfData = columnCountOfRowHeader; columnIndexOfData < cs.Axes[0].Positions.Count + columnCountOfRowHeader; columnIndexOfData++)
                    {
                        try
                        {
                            if (useFormattedValue)
                            {
                                dr[columnIndexOfData] = cs[cellIndex++].FormattedValue;
                            }
                            else
                            {
                                dr[columnIndexOfData] = cs[cellIndex++].Value;
                            }

                            if (dr[columnIndexOfData].ToString() == "null")
                            {
                                dr[columnIndexOfData] = "";
                            }
                        }
                        catch
                        {
                            dr[columnIndexOfData] = "";
                        }
                    }
                    dt.Rows.Add(dr);

                }

                #endregion

                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
