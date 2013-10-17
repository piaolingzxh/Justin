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
            this.Code = code;
            this.No = no;
            this.Name = name;

        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string No { get; set; }
        public string SpellingInShort { get; set; }

        public string CategroyDesc
        {
            get
            {
                string desc = "";
                string no = this.No;
                if (no.StartsWith("300"))
                { desc = "创业版"; }
                else if (no.StartsWith("600") || no.StartsWith("601"))
                { desc = "沪市A股"; }
                else if (no.StartsWith("900"))
                { desc = "沪市B股"; }
                else if (no.StartsWith("000"))
                { desc = "深市A股"; }
                else if (no.StartsWith("002"))
                { desc = "中小板"; }
                else if (no.StartsWith("200"))
                { desc = "沪市B股"; }
                return desc;
            }
        }


    }
}
