using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Services;
using Justin.Log;
using Justin.Stock.Service.Entities;

namespace Justin.Stock.Service.Models
{
    public delegate void EventHandler<JEventArgs>(object sender, JEventArgs e) where JEventArgs : EventArgs;

    [Serializable]
    public class DataService
    {
        public static int ReDoSecond = 5;
        public static bool EnableStock = true;
        public static bool EnableSilver = true;
        public static bool IsRunning { get; private set; }
        private static System.Timers.Timer timer;
        public static EventHandler<DataEventArgs> DataChangedEvent { get; set; }

        static DataService()
        {
            timer = new System.Timers.Timer(ReDoSecond * 1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Time_Tick);
            timer.AutoReset = true;
            //timer.Enabled = true;
        }
        private DataService() { }

        public static void AddEvent(EventHandler<DataEventArgs> value)
        {
            if (DataChangedEvent != null)
            {
                if (!DataChangedEvent.GetInvocationList().Select(row => row.Method.Name).Contains(value.Method.Name))
                {
                    DataChangedEvent += value;
                }
            }
            else
            {
                DataChangedEvent += value;
            }
        }
        public static void RemoveEvent(EventHandler<DataEventArgs> value)
        {
            if (DataChangedEvent != null)
            {
                if (DataChangedEvent.GetInvocationList().Select(row => row.Method.Name).Contains(value.Method.Name))
                {
                    DataChangedEvent -= value;
                }
            }
        }

        private static void Time_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            if (DataChangedEvent.GetInvocationList().Count() <= 0) return;

            RefreshStockPriceFromWeb(MyStock.Where(r => !r.IsSilver).ToList());
            RefreshSilverPrice(MyStock.Where(r => r.IsSilver).FirstOrDefault());

            OnDataChanged(new DataEventArgs() { Stocks = MyStock });
        }

        #region 启动、停止、重启

        public static void Start(int second = 0)
        {
            ResetStockSumInvest();
            ResetMyStock();
            if (second != 0)
            {
                timer.Interval = second * 1000;
            }
            timer.Enabled = IsRunning = true;
        }
        public static void ReStart(int second = 0)
        {
            timer.Enabled = false;
            if (second != 0)
            {
                timer.Interval = second * 1000;
            }
            Thread.Sleep(1000);
            timer.Enabled = IsRunning = true;
        }
        public static void Stop()
        {
            timer.Enabled = IsRunning = false;
        }

        #endregion

        #region 股票

        public static bool CheckStockTime = true;
        private static bool CheckTimeIsOpen()
        {
            if (!CheckStockTime) return true;
            if (!EnableStock) return false;

            DateTime now = DateTime.Now;

            DateTime firstOpenTime = new DateTime(now.Year, now.Month, now.Day, 9, 10, 0);
            DateTime firstCloseTime = new DateTime(now.Year, now.Month, now.Day, 11, 35, 0);

            DateTime secondOpenTime = new DateTime(now.Year, now.Month, now.Day, 12, 55, 0);
            DateTime secondCloseTime = new DateTime(now.Year, now.Month, now.Day, 15, 05, 0);

            if (now > firstOpenTime && now < firstCloseTime)
            {
                return true;
            }
            if (now > secondOpenTime && now < secondCloseTime)
            {
                return true;
            }
            return false;
        }

        #region 刷新股票信息

        private static bool RefreshStockBeyondTime = false;
        private static void RefreshStockPriceFromWeb(List<StockInfo> Stocks)
        {
            try
            {
                if (Stocks == null || Stocks.Count <= 0) return;

                if (CheckTimeIsOpen())
                {
                    RequestFactory.CurrentRequest.RefreshStockData(Stocks);
                }
                else
                {
                    if (!RefreshStockBeyondTime)
                    {
                        RequestFactory.CurrentRequest.RefreshStockData(Stocks);
                        RefreshStockBeyondTime = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
            }

        }

        #endregion

        #region 重置总投入和自选股委托

        public static decimal StockSumInvest { get; private set; }
        public static Func<decimal> QuerySumInvestFunc { get; set; }
        public static void ResetStockSumInvest()
        {
            if (QuerySumInvestFunc != null)
            {
                StockSumInvest = QuerySumInvestFunc();
            }
        }

        #region 自选股

        private static object syncMyStockData = new Object();

        private static List<StockInfo> myStock = null;
        public static List<StockInfo> MyStock
        {
            get
            {
                lock (syncMyStockData)
                {
                    return myStock;
                }

            }
            set
            {
                lock (syncMyStockData)
                {
                    myStock = value;
                }

            }
        }

        #endregion

        public static Func<List<StockInfo>> GetAllMyStockFunc { get; set; }
        public static void ResetMyStock()
        {
            if (GetAllMyStockFunc != null)
            {
                MyStock = GetAllMyStockFunc();
                RefreshStockBeyondTime = false;
            }
        }

        #endregion

        #region 股票代码名称大全

        public static bool ForceDownLoadAllStock = false;

        private static List<StockBaseInfo> _allStocks = null;
        private static List<StockBaseInfo> GetAllStocks()
        {
            IRequest allStockRequest = RequestFactory.GetRequest(ServiceProvider.EastMoney);
            return allStockRequest.GetAllStocks();
        }
        /// <summary>
        /// item1：Code Item2:no,Item3:Name
        /// </summary>
        public static List<StockBaseInfo> AllStocks
        {
            get
            {
                if (_allStocks == null || ForceDownLoadAllStock)
                {
                    _allStocks = GetAllStocks();
                    if (ForceDownLoadAllStock)
                    {
                        ForceDownLoadAllStock = false;
                    }
                }
                return _allStocks;
            }

        }

        #endregion

        #endregion

        #region 现货白银

        private static void RefreshSilverPrice(StockInfo silverInfo)
        {
            string url = "";
            try
            {
                if (silverInfo == null)
                    return;


                url = "http://data.91jin.com/data.aspx?a=quotation&code=AG&callback=jsonp1390282593069";
                WebRequest request = WebRequest.Create(url);
                WebResponse rs = request.GetResponse();
                StreamReader reader = new StreamReader(rs.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                string silverMessage = reader.ReadToEnd();
                reader.Close();
                rs.Close();

                Regex regex = new Regex("\"UpdateTime\":\"(.*)\",\"SellPrice\":\"(.*)\",\"BuyPrice\":\"(.*)\",\"OpenPrice\":\"(.*)\",\"ClosePrice\":\"(.*)\",\"MaxPrice\":\"(.*)\",\"LastPrice\":\"(.*)\",\"MinPrice\":\"(.*)\"");
                Match matche = regex.Matches(silverMessage)[0];

                silverInfo.Now = matche.Groups[1].Value.Value<DateTime>();
                silverInfo.PriceTodayStart = matche.Groups[4].Value.Value<decimal>();
                silverInfo.PriceYesterdayEnd = matche.Groups[5].Value.Value<decimal>();
                silverInfo.PriceTodayHigh = matche.Groups[6].Value.Value<decimal>();
                silverInfo.PriceNow = matche.Groups[7].Value.Value<decimal>();
                silverInfo.PriceTodayLow = matche.Groups[8].Value.Value<decimal>();

            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex, "刷新个股信息失败，网址：{0}", url);
            }
        }

        #endregion

        protected static void OnDataChanged(DataEventArgs e)
        {
            if (DataChangedEvent != null)
            {
                foreach (EventHandler<DataEventArgs> dataChangedEvent in DataChangedEvent.GetInvocationList())
                {
                    dataChangedEvent(null, e);
                }
            }
        }

    }
}
