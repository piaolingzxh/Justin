using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Services;
using Justin.Log;
using Justin.Stock.Service.Entities;
namespace Justin.Stock.Service.Models
{
    public class TencentRequest : IRequest
    {
        WebRequest request;
        WebResponse rs;
        private Random rd = new Random(10);
        private int BufferSize = 64;
        public void RefreshStockData(List<StockInfo> stocks)
        {
            stocks = stocks.Where(r => !r.IsSilver).OrderByDescending(row => row.ShowInFolatWindow).ToList();
            int requestTimes = stocks.Count % BufferSize == 0 ? stocks.Count / BufferSize : stocks.Count / BufferSize + 1;

            for (int i = 0; i < requestTimes; i++)
            {
                List<StockInfo> stocksOfCurrentRequest = stocks.Skip(i * BufferSize).Take(BufferSize).ToList();

                RefreshStockDataInner(stocksOfCurrentRequest);
            }


        }
        public void RefreshStockDataInner(List<StockInfo> stocks)
        {
            string url = "";
            try
            {
                List<StockInfo> StockDatas = stocks;
                string stockCodes = "";
                foreach (var item in StockDatas)
                {
                    stockCodes += item.Code + ",";
                }
                stockCodes = stockCodes.Remove(stockCodes.Length - 1);
                url = "http://qt.gtimg.cn/r=" + rd.NextDouble() + "q=" + stockCodes;
                request = WebRequest.Create(url);
                request.Timeout = 500;
                rs = request.GetResponse();
                StreamReader reader = new StreamReader(rs.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                string stockMsg = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                rs.Close();
                rs = null;
                request = null;

                string[] stockItemString = stockMsg.Split(';');
                foreach (var item in stockItemString)
                {
                    int firstQuotationIndex = item.IndexOf('"');
                    int index = item.LastIndexOf("v_");
                    string msg = item.Substring(firstQuotationIndex + 1).Replace("\"", "").Replace(";", "");
                    string[] data = msg.Split('~');
                    if (data.Length >= 50)
                    {
                        string code = item.Substring(index + 2, 8);
                        var stockInfo = StockDatas.Where(row => row.Code == code).FirstOrDefault();

                        stockInfo.PriceTodayStart = data[5].Value<decimal>();
                        stockInfo.PriceYesterdayEnd = data[4].Value<decimal>();
                        stockInfo.PriceNow = data[3].Value<decimal>();
                        stockInfo.PriceTodayHigh = data[33].Value<decimal>();
                        stockInfo.PriceTodayLow = data[34].Value<decimal>();
                        stockInfo.DealsStockAmt = data[36].Value<long>();
                        stockInfo.DealsMoney = data[37].Value<decimal>();
                        stockInfo.TurnOver = data[38].Value<decimal>();
                        stockInfo.Amplitude = data[43].Value<decimal>();
                        stockInfo.Surged = data[31].Value<decimal>();
                        stockInfo.DateTime = data[30].Substring(0, 8) + " " + data[30].Substring(8, 6);
                        stockInfo.Now = DateTime.Now;

                        #region 五盘档口

                        stockInfo.Buy1Count = data[10].Value<int>();
                        stockInfo.Buy1Price = data[9].Value<decimal>();
                        stockInfo.Buy2Count = data[12].Value<int>();
                        stockInfo.Buy2Price = data[11].Value<decimal>();
                        stockInfo.Buy3Count = data[14].Value<int>();
                        stockInfo.Buy3Price = data[13].Value<decimal>();
                        stockInfo.Buy4Count = data[16].Value<int>();
                        stockInfo.Buy4Price = data[15].Value<decimal>();
                        stockInfo.Buy5Count = data[18].Value<int>();
                        stockInfo.Buy5Price = data[17].Value<decimal>();

                        stockInfo.Sell1Count = data[20].Value<int>();
                        stockInfo.Sell1Price = data[19].Value<decimal>();
                        stockInfo.Sell2Count = data[22].Value<int>();
                        stockInfo.Sell2Price = data[21].Value<decimal>();
                        stockInfo.Sell3Count = data[24].Value<int>();
                        stockInfo.Sell3Price = data[23].Value<decimal>();
                        stockInfo.Sell4Count = data[26].Value<int>();
                        stockInfo.Sell4Price = data[25].Value<decimal>();
                        stockInfo.Sell5Count = data[28].Value<int>();
                        stockInfo.Sell5Price = data[27].Value<decimal>();

                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex, "刷新个股信息失败，网址：{0}", url);
            }
        }

        public List<StockBaseInfo> GetAllStocks()
        {
            throw new NotImplementedException();
        }
    }
}
