using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Helper
{
    public class ExcelHelper
    {
        public  static void Output(DataTable dt, string fileName)
        {
            Stream myStream = File.Open(fileName,FileMode.OpenOrCreate,FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string columnTitle = "";
            try
            {
                //写入列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        columnTitle += "\t";
                    }
                    columnTitle += dt.Columns[i].ColumnName;
                }
                sw.WriteLine(columnTitle);

                //写入列内容
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string columnValue = "";
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            columnValue += "\t";
                        }
                        if (dt.Rows[j][k] == null)
                            columnValue += "";
                        else
                        {
                            if (dt.Rows[j][k].GetType() == typeof(string) && dt.Rows[j][k].ToString().StartsWith("0"))
                            {
                                columnValue += "'" + dt.Rows[j][k].ToString();
                            }
                            else
                                columnValue += dt.Rows[j][k].ToString();
                        }
                    }
                    sw.WriteLine(columnValue);
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
    }
}