using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.Stock.Service.Entities;
using Justin.Stock.Service.Models;

namespace Justin.Stock.DAL
{
    public class StockDAL
    {
        public StockDAL()
        {
            InitDB();
        }

        public List<StockInfo> getAllMyStock()
        {
            List<StockInfo> list = new List<StockInfo>();
            using (SQLiteConnection conn = new SQLiteConnection(SqliteHelper.ConnStr))
            {
                conn.Open();
                SQLiteDataReader reader = SqliteHelper.ExecuteReader(conn, CommandType.Text, "select * from MyStocks", null);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(GetStock(reader));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            return list;
        }

        private StockInfo GetStock(SQLiteDataReader reader)
        {
            StockInfo stock = new StockInfo()
            {
                Code = reader["Code"].Value<string>(),
                No = reader["No"].Value<string>(),
                SpellingInShort = reader["SpellingInShort"].Value<string>(),
                Name = reader["Name"].Value<string>(),
                WarnPercent_Max = reader["WarnPercent_Max"].Value<decimal>(),
                WarnPercent_Min = reader["WarnPercent_Min"].Value<decimal>(),
                WarnPrice_Max = reader["WarnPrice_Max"].Value<decimal>(),
                WarnPrice_Min = reader["WarnPrice_Min"].Value<decimal>(),
                BuyCount = reader["BuyCount"].Value<int>(),
                BuyPrice = reader["BuyPrice"].Value<decimal>(),
                ShowInFolatWindow = reader["ShowInFolatWindow"].Value<bool>(),
                Order = reader["Order"].Value<int>(),
                ProfitOrLossHistory = reader["ProfitOrLossHistory"].Value<string>(),
                Warn = reader["Warn"].Value<bool>(),
            };
            return stock;
        }

        #region INIT DB

        private static string INIT_SQL = @"
CREATE TABLE [MyStocks] (
  [Code] NVARCHAR(15) NOT NULL UNIQUE, 
  [No] NVARCHAR(15) NOT NULL, 
  [Name] NVARCHAR(15) NOT NULL, 
  [SpellingInShort] NVARCHAR(15) NOT NULL, 
  [WarnPrice_Min] FLOAT, 
  [WarnPrice_Max] FLOAT, 
  [WarnPercent_Min] FLOAT, 
  [WarnPercent_Max] FLOAT, 
  [BuyPrice] FLOAT, 
  [BuyCount] INTEGER, 
  [ShowInFolatWindow] BOOLEAN, 
  [Order] INTEGER, 
  [ProfitOrLossHistory] TEXT, 
  [Warn] BOOLEAN DEFAULT 1);";

        public static void InitDB()
        {
            //if (!File.Exists(Constants.DBPath))
            //{
            //    SQLiteConnection.CreateFile(Constants.DBPath);
            //    SqliteHelper.ExecuteNonQuery(Constants.ConnStr, System.Data.CommandType.Text, INIT_SQL, null);
            //}
        }

        #endregion

        #region 添加自选

        public void InsertStock(string code, string no, string name, string shortName)
        {
            string CHECK_SQL_FORMAT = "select count(*) from MyStocks where Code='{0}'";
            string INSERT_SQL_FORMAT = @"insert into MyStocks(Code,No,Name,SpellingInShort,ShowInFolatWindow)values('{0}','{1}','{2}','{3}','false')";

            int count = int.Parse(SqliteHelper.ExecuteScalar(SqliteHelper.ConnStr, CommandType.Text, String.Format(CHECK_SQL_FORMAT, code), null).ToString());
            if (count < 1)
            {
                string insertSQL = string.Format(INSERT_SQL_FORMAT, code, no, name, shortName);
                SqliteHelper.ExecuteNonQuery(SqliteHelper.ConnStr, CommandType.Text, insertSQL, null);
            }

        }
        public void DeleteStock(string code)
        {

            string sql = string.Format("delete from MyStocks where Code='{0}'", code);

            SqliteHelper.ExecuteNonQuery(SqliteHelper.ConnStr, CommandType.Text, sql, null);
        }
        //        public void UpdateStock(string code, string name, string inShort, decimal warnprice_Min, decimal warnprice_Max, decimal warnpercent_Min, decimal warnpercent_Max, decimal buyPrice, int buyCount, bool showInFolatWindow, int order, string profitOrLossHistory)
        //        {

        //            string UPDATE_SQL_FORMAT = @"
        //update MyStocks set
        //SpellingInShort    ='{1}'
        //,Warnprice_Min     ={2}
        //,Warnprice_Max     ={3}
        //,Warnpercent_Min   ={4}
        //,Warnpercent_Max   ={5}
        //,BuyPrice          ={6}
        //,BuyCount          ={7}
        //,Name              ='{8}'
        //,ShowInFolatWindow={9}
        //,[Order]={10}
        //,ProfitOrLossHistory='{11}'  
        //where Code='{0}'";

        //            string updateSQL = string.Format(UPDATE_SQL_FORMAT, code, inShort, warnprice_Min, warnprice_Max, warnpercent_Min, warnpercent_Max, buyPrice, buyCount, name, showInFolatWindow ? 1 : 0, order, profitOrLossHistory);
        //            SqliteHelper.ExecuteScalar(SqliteHelper.ConnStr, CommandType.Text, updateSQL, null);
        //        }


        public int UpdateByDataSet(DataTable table)
        {
            string UPDATE_SQL_FORMAT = @"
            update MyStocks set
            SpellingInShort          =@SpellingInShort
            ,Warnprice_Min           =@Warnprice_Min
            ,Warnprice_Max           =@Warnprice_Max
            ,Warnpercent_Min         =@Warnpercent_Min
            ,Warnpercent_Max         =@Warnpercent_Max
            ,BuyPrice                =@BuyPrice
            ,BuyCount                =@BuyCount
            ,ShowInFolatWindow       =@ShowInFolatWindow
            ,[Order]                 =@Order
            ,ProfitOrLossHistory     =@ProfitOrLossHistory
            ,Warn                    =@Warn
            where Code               =@Code";

            SQLiteConnection conn = new SQLiteConnection(SqliteHelper.ConnStr);

            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter();
            SQLiteCommand myCommand = new SQLiteCommand(("select * from " + "MyStocks"), conn);
            myAdapter.SelectCommand = myCommand;

            SQLiteCommand updateCmd = new SQLiteCommand();
            updateCmd.CommandType = CommandType.Text;
            updateCmd.CommandText = UPDATE_SQL_FORMAT;
            updateCmd.Connection = conn;
            updateCmd.Parameters.Add(new SQLiteParameter("@SpellingInShort", DbType.String, "SpellingInShort", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Warnprice_Min", DbType.String, "Warnprice_Min", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Warnprice_Max", DbType.String, "Warnprice_Max", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Warnpercent_Min", DbType.String, "Warnpercent_Min", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Warnpercent_Max", DbType.String, "Warnpercent_Max", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@BuyPrice", DbType.String, "BuyPrice", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@BuyCount", DbType.String, "BuyCount", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@ShowInFolatWindow", DbType.Boolean, "ShowInFolatWindow", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Order", DbType.String, "Order", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@ProfitOrLossHistory", DbType.String, "ProfitOrLossHistory", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Warn", DbType.Boolean, "Warn", DataRowVersion.Current));
            updateCmd.Parameters.Add(new SQLiteParameter("@Code", DbType.String, "Code", DataRowVersion.Original));

            myAdapter.UpdateCommand = updateCmd;
            //SQLiteCommandBuilder myCommandBuilder = new SQLiteCommandBuilder(myAdapter);
            IEnumerable<DataRow> rows = table.Rows.Cast<DataRow>().Where(row => row.RowState == DataRowState.Modified);

            if (rows != null && rows.Count() > 0)
            {
                var tempRows = rows.ToArray();
                myAdapter.Update(tempRows);
            }
            return 0;

        }
        #endregion

        public DataTable Query(string sql)
        {
            return SqliteHelper.ExecuteDataTable(SqliteHelper.ConnStr, CommandType.Text, sql, null);
        }


    }
}
