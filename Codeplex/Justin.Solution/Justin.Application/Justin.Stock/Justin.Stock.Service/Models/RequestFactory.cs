using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Justin.Stock.Service.Entities;

namespace Justin.Stock.Service.Models
{
    public class RequestFactory
    {
        public static IRequest GetRequest(ServiceProvider sp)
        {
            if (sp == ServiceProvider.Sina)
            {
                return new SinaRequest();
            }
            else if (sp == ServiceProvider.Tencent)
            {
                return new TencentRequest();
            }
            else if (sp == ServiceProvider.EastMoney)
            {
                return new EastMoneyRequest();
            }
            return new SinaRequest();
        }

        public static ServiceProvider ServiceProvider { get; set; }
        public static int ServiceProviderValue
        {
            set
            {
                ServiceProvider = (ServiceProvider)(value);
            }
        }
        private static object syncRequest = new Object();
        private static IRequest _currentRequest;
        public static IRequest CurrentRequest
        {
            get
            {
                if (_currentRequest == null)
                {
                    lock (syncRequest)
                    {
                        if (_currentRequest == null)
                        {
                            _currentRequest = RequestFactory.GetRequest(ServiceProvider);
                        }
                    }
                }
                return _currentRequest;
            }
        }

    }
}
