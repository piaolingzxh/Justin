using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Drawing;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Justin.FrameWork.Helper
{
    /// <summary>
    /// FileName:
    /// 
    /// Description:
    /// Excel工具类，提供对Excel的读取，导出等操作。
    /// 
    /// Created by liuzm at 2011-04-07
    /// </summary>
    public sealed class ExcelHelper
    {
        public static string excelFolder = @"tmp\export\";
        public static string excelFullFolder;

        private static string ext = ".xls";
        static ExcelHelper()
        {
            System.Web.UI.Page page = System.Web.HttpContext.Current.Handler as System.Web.UI.Page;
            excelFullFolder = Path.Combine(page.Request.PhysicalApplicationPath, excelFolder);
            DirectoryInfo excelDir = new DirectoryInfo(excelFullFolder);
            if (!excelDir.Exists)
            {
                excelDir.Create();
            }
        }

        public static string Export(System.Data.DataTable dt, string fileName)
        {
            System.Web.UI.Page page = System.Web.HttpContext.Current.Handler as System.Web.UI.Page;

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            DataGrid dGrid = new DataGrid();

            TableItemStyle alternatingStyle = new TableItemStyle();
            TableItemStyle headerStyle = new TableItemStyle();
            TableItemStyle itemStyle = new TableItemStyle();

            alternatingStyle.BackColor = Color.LightGray;

            headerStyle.BackColor = Color.LightGray;
            headerStyle.Font.Bold = true;
            headerStyle.HorizontalAlign = HorizontalAlign.Center;

            itemStyle.HorizontalAlign = HorizontalAlign.Center;

            dGrid.GridLines = GridLines.Both;

            dGrid.HeaderStyle.MergeWith(headerStyle);
            dGrid.HeaderStyle.Font.Bold = true;

            dGrid.AlternatingItemStyle.MergeWith(alternatingStyle);
            dGrid.ItemStyle.MergeWith(itemStyle);


            dGrid.DataSource = dt.DefaultView;
            dGrid.DataBind();
            dGrid.RenderControl(htmlWriter);


            string filePath = Path.Combine(excelFullFolder, fileName + ext);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            sw.Write(stringWriter.ToString());
            sw.Close();

            int pos = page.Request.Url.ToString().LastIndexOf(page.Request.Path);

            string fileUrl = page.Request.Url.ToString().Substring(0, pos);
            fileUrl += page.Request.ApplicationPath + excelFolder.Replace("\\", "/") + fileName + ext;
            HttpContext.Current.Response.Redirect(fileUrl);

            return fileUrl;
        }

    }
    

}
