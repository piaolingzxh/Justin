using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.Stock.Service
{
    public class StockBaseInfo
    {
        public StockBaseInfo() { }
        public StockBaseInfo(string code, string no, string name)
        {
            this.StockCode = code;
            this.StockNo = no;
            this.StockName = name;

        }
        public string StockName { get; set; }
        public string StockCode { get; set; }
        public string StockNo { get; set; }
        public string SpellingInShort { get; set; }



    }
}
