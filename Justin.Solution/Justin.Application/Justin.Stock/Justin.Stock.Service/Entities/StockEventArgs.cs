using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Stock.Service.Entities
{
    public class StockEventArgs : EventArgs
    {
        public IEnumerable<StockInfo> Stocks { get; private set; }
        public StockEventArgs(IEnumerable<StockInfo> stocks)
        {
            this.Stocks = stocks;
        }

    }
}
