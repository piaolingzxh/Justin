using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Justin.Stock.Service.Entities;

namespace Justin.Stock.Service.Models
{
    public class EastMoneyRequest : IRequest
    {
        public void RefreshStockData(List<StockInfo> stocks)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, string, string>> GetAllStocks()
        {
            string regexOfEastmoney = "<a target=\"_blank\" href=\"http://quote.eastmoney.com/(\\S+).html\">(\\S+)\\((\\S+)\\)</a>";

            WebRequest request = WebRequest.Create("http://quote.eastmoney.com/stocklist.html");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
            string htmlString = reader.ReadToEnd();
            reader.Close();
            response.Close();

            List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>();
            MatchCollection mc = Regex.Matches(htmlString, regexOfEastmoney);
            if (mc.Count < 1)
            {
                throw new Exception("请检查目标网站数据接口是否已经发生改变");
            }
            for (int i = 0; i < mc.Count; i++)
            {
                Match m = mc[i];
                list.Add(new Tuple<string, string, string>(m.Groups[1].Value, m.Groups[3].Value, m.Groups[2].Value));
            }

            return list;
        }
    }
}
