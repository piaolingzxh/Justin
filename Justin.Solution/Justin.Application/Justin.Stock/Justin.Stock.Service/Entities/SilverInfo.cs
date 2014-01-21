using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Stock.Service.Entities
{
    public class SilverInfo
    {
        public decimal HightPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal PriceNow { get; set; }

        public DateTime Now { get; set; }
    }
}
