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
                { desc = "创业"; }
                else if (no.StartsWith("600") || no.StartsWith("601"))
                { desc = "沪A"; }
                else if (no.StartsWith("900"))
                { desc = "沪B"; }
                else if (no.StartsWith("000"))
                { desc = "深A"; }
                else if (no.StartsWith("002"))
                { desc = "中小"; }
                else if (no.StartsWith("200"))
                { desc = "沪B"; }
                else if (no.StartsWith("730"))
                { desc = "新购"; }
                else if (no.StartsWith("700"))
                { desc = "沪配股"; }
                else if (no.StartsWith("080"))
                { desc = "深配股"; }
                else if (no.StartsWith("580"))
                { desc = "沪权证"; }
                else if (no.StartsWith("031"))
                { desc = "深权证"; }
                //else if (no.StartsWith("603") || no.StartsWith("60"))
                //{ desc = "沪A"; }

                return desc;
            }
        }

        public string Description { get; set; }


    }
}
