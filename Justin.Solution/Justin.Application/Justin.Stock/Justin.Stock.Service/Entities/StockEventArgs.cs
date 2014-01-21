using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Stock.Service.Entities
{
    public class DataEventArgs : EventArgs
    {
        public List<StockInfo> Stocks { get; set; }
        //public SilverInfo SilverInfo { get; set; }

    }
}
