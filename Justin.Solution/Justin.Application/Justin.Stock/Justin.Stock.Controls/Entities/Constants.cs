using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.Stock.DAL;
using Justin.Stock.Service.Models;

namespace Justin.Stock.Controls.Entities
{
    public class Constants
    {
        public static string SettingFileName = "JStock.xml";
        public static string SettingFilePath = "";
        public static string DefaultDBPath = Path.Combine(Application.StartupPath, "JStock.db3");
        public static string DefaultDeskDisplayFormat = "{0}:{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}";
        public static string DefaultDeskDisplayFormatTips = @"0：简称
1：当前价格
2：当日涨幅
3：总盈亏
4：总盈亏比例
5：成本价
6：股数
7：换手率
8：当前市值
9：总成本";

        public static JSettings Setting { get; set; }

        public static void ResetDBConnString(string dbPath)
        {
            SqliteHelper.ConnStr = String.Format("Data Source={0};Version=3;Pooling=true;Max Pool Size=100;", dbPath);
            ResetMyStock();
        }

        public static void ResetMyStock()
        {
            StockService.MyStock = new StockDAL().getAllMyStock();
        }

    }
}
