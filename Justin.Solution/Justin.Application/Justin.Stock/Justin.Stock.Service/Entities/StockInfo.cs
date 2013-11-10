using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Stock.Service.Entities
{
    public class StockInfo : StockBaseInfo
    {
        #region 非实时数据

        public decimal WarnPrice_Min { get; set; }
        public decimal WarnPrice_Max { get; set; }
        public decimal WarnPercent_Max { get; set; }
        public decimal WarnPercent_Min { get; set; }
        public decimal BuyPrice { get; set; }
        public int BuyCount { get; set; }
        /// <summary>
        /// 是否显示在桌面
        /// </summary>
        public bool ShowInFolatWindow { get; set; }
        public bool Warn { get; set; }
        /// <summary>
        /// 显示排序
        /// </summary>
        public decimal Order { get; set; }
        /// <summary>
        /// 历史盈亏
        /// </summary>
        public decimal HasProfit
        {
            get
            {
                if (this.HasStock && this.PriceNow > 0)
                {
                    return this.CurrentProfit + this.ProfitHistoryData.Sum();
                }
                else
                {
                    return this.HasProfitBefore + this.ProfitHistoryData.Sum();
                }
            }
        }

        public string ProfitHistory { get; set; }

        public List<decimal> ProfitHistoryData
        {
            get
            {
                return GetProfitHistoryData(this.ProfitHistory);
            }
        }



        public decimal HasProfitBefore { get; set; }

        #endregion

        public bool HasStock
        {
            get { return this.BuyCount > 0; }
            set { }
        }
        #region 实时数据
        /// <summary>
        /// 总成本价=当前成本+历史盈亏
        /// </summary>
        public decimal SumCost
        {
            get
            {
                if (this.HasStock )
                {
                    return Math.Round(this.BuyPrice * this.BuyCount - this.ProfitHistoryData.Sum(), 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal SumProfitPercent
        {
            get
            {
                if (this.SumCost != 0)
                {
                    return this.SumProfit / this.SumCost;
                }
                return 0;
            }
        }
        public decimal SumProfit
        {
            get
            {
                if (this.HasStock &&this.PriceNow>0)
                {
                    return Math.Round(this.CurrentProfit + this.ProfitHistoryData.Sum());
                }
                else
                {
                    return Math.Round(this.HasProfitBefore + this.ProfitHistoryData.Sum());
                }
            }
        }

        /// <summary>
        /// 当前盈亏
        /// </summary>
        public decimal CurrentProfit
        {
            get
            {
                decimal temp;
                if (BuyCount >= 0)
                {
                    if (PriceNow != 0)
                    {
                        temp = (PriceNow - BuyPrice) * BuyCount;
                    }
                    else
                    {
                        temp = (PriceYesterdayEnd - BuyPrice) * BuyCount;
                    }
                }
                else
                {
                    temp = 0;
                }
                return Math.Round(temp, 0);
            }
        }
        /// <summary>
        /// 当前市值
        /// </summary>
        public decimal MarketValue
        {
            get
            {
                decimal marketValue;
                if (BuyCount >= 0)
                {
                    if (PriceNow != 0)
                    {
                        marketValue = PriceNow * BuyCount;
                    }
                    else
                    {
                        marketValue = PriceYesterdayEnd * BuyCount;
                    }
                }
                else
                {
                    marketValue = 0;
                }
                return Math.Round(marketValue, 0);
            }

        }
        public decimal PriceTodayStart { get; set; }
        public decimal PriceYesterdayEnd { get; set; }
        public decimal PriceNow { get; set; }
        public decimal PriceTodayHigh { get; set; }
        public decimal PriceTodayLow { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public long DealsStockAmt { get; set; }
        /// <summary>
        /// 成交额
        /// </summary>
        public decimal DealsMoney { get; set; }
        public string DateTime { get; set; }
        public DateTime Now { get; set; }
        /// <summary>
        /// 今日涨幅
        /// </summary>
        public decimal SurgedRange
        {
            get
            {
                if (PriceYesterdayEnd != 0)
                {
                    return Math.Round((PriceNow - PriceYesterdayEnd) / PriceYesterdayEnd * 100, 2);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 今日涨跌
        /// </summary>
        public decimal Surged { get; set; }
        /// <summary>
        /// 今日振幅
        /// </summary>
        public decimal Amplitude { get; set; }
        /// <summary>
        /// 今日换手率
        /// </summary>
        public decimal TurnOver { get; set; }

        #region 五档盘口

        #region 买

        public int Buy1Count { get; set; }
        public int Buy2Count { get; set; }
        public int Buy3Count { get; set; }
        public int Buy4Count { get; set; }
        public int Buy5Count { get; set; }

        public decimal Buy1Price { get; set; }
        public decimal Buy2Price { get; set; }
        public decimal Buy3Price { get; set; }
        public decimal Buy4Price { get; set; }
        public decimal Buy5Price { get; set; }

        #endregion

        #region 卖

        public int Sell1Count { get; set; }
        public int Sell2Count { get; set; }
        public int Sell3Count { get; set; }
        public int Sell4Count { get; set; }
        public int Sell5Count { get; set; }

        public decimal Sell1Price { get; set; }
        public decimal Sell2Price { get; set; }
        public decimal Sell3Price { get; set; }
        public decimal Sell4Price { get; set; }
        public decimal Sell5Price { get; set; }

        #endregion

        #endregion


        #endregion


        public static List<decimal> GetProfitHistoryData(string profitOrLossHistory)
        {
            List<decimal> datas = new List<decimal>();
            if (string.IsNullOrEmpty(profitOrLossHistory) || string.IsNullOrEmpty(profitOrLossHistory.Trim()))
            {
                return datas;
            }

            string[] dataStrs = profitOrLossHistory.Split(',');
            if (dataStrs == null || dataStrs.Length < 1)
                return datas;
            foreach (var item in dataStrs)
            {
                datas.Add(decimal.Parse(item));
            }
            return datas;
        }

    }
}
