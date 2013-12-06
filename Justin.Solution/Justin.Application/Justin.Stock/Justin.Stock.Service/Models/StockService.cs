using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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



    }
}
