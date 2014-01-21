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
    public class StockService
    {
        public static bool CheckTime = true;

        public static bool ForceDownLoadAllStock = false;
        public static int ReDoSecond = 5;

        static StockService()
        {
            timer = new System.Timers.Timer(ReDoSecond * 1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Time_Tick);
            timer.AutoReset = true;
            //timer.Enabled = true;
        }
        private StockService() { }

        public static EventHandler<StockEventArgs> ProcessEvent { get; set; }
        static System.Timers.Timer timer;
        public static bool IsRunning { get; private set; }

        public static decimal SumInvest { get; private set; }
        public static Func<decimal> QuerySumInvestFunc { get; set; }
        public static void ResetSumInvest()
        {
            if (QuerySumInvestFunc != null)
            {
                SumInvest = QuerySumInvestFunc();
            }
        }

        public static Func<List<StockInfo>> GetAllMyStockFunc { get; set; }
        public static void ResetMyStock()
        {
            if (GetAllMyStockFunc != null)
            {
                MyStock = GetAllMyStockFunc();
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

        #region 刷新股票信息

        private static bool RefreshDataBeyondTime = false;
        private static void Time_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            if (CheckTimeIsOpen())
            {
                RefreshCurrentStockDataFromWeb();
            }
            else
            {
                if (!RefreshDataBeyondTime)
                {
                    RefreshCurrentStockDataFromWeb();
                    RefreshDataBeyondTime = true;
                }
            }
            RefreshSilver();
        }
        private static void RefreshCurrentStockDataFromWeb()
        {

            try
            {
                if (ProcessEvent.GetInvocationList().Count() > 0)
                {
                    List<StockInfo> StockDatas = MyStock;
                    if (StockDatas != null && StockDatas.Count > 0)
                    {
                        RequestFactory.CurrentRequest.RefreshStockData(StockDatas);
                        OnStockDataChanged(new StockEventArgs(StockDatas));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
            }

        }
        protected static void OnStockDataChanged(StockEventArgs e)
        {
            if (ProcessEvent != null)
            {
                foreach (EventHandler<StockEventArgs> stockEvent in ProcessEvent.GetInvocationList())
                {
                    stockEvent(null, e);
                }
            }
        }

        #endregion

        #region 股票代码名称大全

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

        public static void AddEvent(EventHandler<StockEventArgs> value)
        {
            if (ProcessEvent != null)
            {
                if (!ProcessEvent.GetInvocationList().Select(row => row.Method.Name).Contains(value.Method.Name))
                {
                    ProcessEvent += value;
                }
            }
            else
            {
                ProcessEvent += value;
            }
        }
        public static void RemoveEvent(EventHandler<StockEventArgs> value)
        {
            if (ProcessEvent != null)
            {
                if (ProcessEvent.GetInvocationList().Select(row => row.Method.Name).Contains(value.Method.Name))
                {
                    ProcessEvent -= value;
                }
            }
        }

        #region 启动、停止、重启

        public static void Start(int second = 0)
        {
            ResetSumInvest();
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

        private static bool CheckTimeIsOpen()
        {
            if (!CheckTime)
            {
                return true;
            }
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


        #region 现货白银


        public static Action<SilverInfo> SilverInfoChanged { get; set; }

        private static SilverInfo _silverInfo = new SilverInfo();
        public static SilverInfo SilverInfo { get { return _silverInfo; } }


        private static void RefreshSilver()
        {

            RefreshSilverData(SilverInfo);
            if (SilverInfoChanged != null)
            {
                SilverInfoChanged(SilverInfo);
            }
        }

        private static void RefreshSilverData(SilverInfo silverInfo)
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
                silverInfo.OpenPrice = matche.Groups[4].Value.Value<decimal>();
                silverInfo.ClosePrice = matche.Groups[5].Value.Value<decimal>();
                silverInfo.HightPrice = matche.Groups[6].Value.Value<decimal>();
                silverInfo.PriceNow = matche.Groups[7].Value.Value<decimal>();
                silverInfo.LowPrice = matche.Groups[8].Value.Value<decimal>();

            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex, "刷新个股信息失败，网址：{0}", url);
            }
        }


        #endregion



    }
}
