using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Justin.FrameWork.Extensions;
using Justin.Log;
using Justin.Stock.Service.Entities;
namespace Justin.Stock.Service.Models
{
    public class SinaRequest : IRequest
    {
        public void RefreshStockData(List<StockInfo> stocks)
        {
            try
            {
                List<StockInfo> StockDatas = stocks;
                string stockCodes = "";
                foreach (var item in StockDatas)
                {
                    stockCodes += item.Code + ",";
                }
                stockCodes = stockCodes.Remove(stockCodes.Length - 1);
                WebRequest request = WebRequest.Create("http://hq.sinajs.cn/list=" + stockCodes);
                WebResponse rs = request.GetResponse();
                StreamReader reader = new StreamReader(rs.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                string stockMsg = reader.ReadToEnd();
                reader.Close();
                rs.Close();


                string[] stockItemString = stockMsg.Split(';');
                foreach (var item in stockItemString)
                {
                    int firstQuotationIndex = item.IndexOf('"');
                    int index = item.LastIndexOf("hq_str_");
                    string msg = item.Substring(firstQuotationIndex + 1).Replace("\"", "").Replace(";", "");
                    string[] data = msg.Split(',');
                    if (data.Length >= 32)
                    {
                        string code = item.Substring(index + 7, 8);
                        var stockInfo = StockDatas.Where(row => row.Code == code).FirstOrDefault();

                        stockInfo.PriceTodayStart = data[1].Value<decimal>();
                        stockInfo.PriceYesterdayEnd = data[2].Value<decimal>();
                        stockInfo.PriceNow = data[3].Value<decimal>();
                        stockInfo.PriceTodayHigh = data[4].Value<decimal>();
                        stockInfo.PriceTodayLow = data[5].Value<decimal>();
                        stockInfo.DealsStockAmt = data[8].Value<long>() / 100;
                        stockInfo.DealsMoney = data[9].Value<decimal>() / 10000;
                        stockInfo.DateTime = data[30] + " " + data[31];
                        stockInfo.Now = DateTime.Now;

                        #region 五盘档口

                        stockInfo.Buy1Count = data[10].Value<int>() / 100;
                        stockInfo.Buy1Price = data[11].Value<decimal>();
                        stockInfo.Buy2Count = data[12].Value<int>() / 100;
                        stockInfo.Buy2Price = data[13].Value<decimal>();
                        stockInfo.Buy3Count = data[14].Value<int>() / 100;
                        stockInfo.Buy3Price = data[15].Value<decimal>();
                        stockInfo.Buy4Count = data[16].Value<int>() / 100;
                        stockInfo.Buy4Price = data[17].Value<decimal>();
                        stockInfo.Buy5Count = data[18].Value<int>() / 100;
                        stockInfo.Buy5Price = data[19].Value<decimal>();

                        stockInfo.Sell1Count = data[20].Value<int>() / 100;
                        stockInfo.Sell1Price = data[21].Value<decimal>();
                        stockInfo.Sell2Count = data[22].Value<int>() / 100;
                        stockInfo.Sell2Price = data[23].Value<decimal>();
                        stockInfo.Sell3Count = data[24].Value<int>() / 100;
                        stockInfo.Sell3Price = data[25].Value<decimal>();
                        stockInfo.Sell4Count = data[26].Value<int>() / 100;
                        stockInfo.Sell4Price = data[27].Value<decimal>();
                        stockInfo.Sell5Count = data[28].Value<int>() / 100;
                        stockInfo.Sell5Price = data[29].Value<decimal>();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                JLog.Write(LogMode.Error, ex);
            }
        }


        public List<StockBaseInfo> GetAllStocks()
        {
            throw new NotImplementedException();
        }
    }
}
