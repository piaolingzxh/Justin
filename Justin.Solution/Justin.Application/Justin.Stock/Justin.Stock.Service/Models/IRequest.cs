using System;
using System.Collections.Generic;
using Justin.Stock.Service.Entities;

namespace Justin.Stock.Service.Models
{
    public interface IRequest
    {
        void RefreshStockData(List<StockInfo> stocks);
        List<StockBaseInfo> GetAllStocks();

    }




}
