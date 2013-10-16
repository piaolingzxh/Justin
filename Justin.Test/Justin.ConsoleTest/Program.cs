using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string info = "";
            info += "当前用户电脑名称：" + System.Net.Dns.GetHostName() + Environment.NewLine;
            info += "当前电脑名：" + System.Environment.MachineName + Environment.NewLine;
            info += "当前电脑所属网域：" + System.Environment.UserDomainName + Environment.NewLine;
            info += "当前电脑用户：" + System.Environment.UserName + Environment.NewLine;
            IPAddress[] addresses = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            string ip = "";
            if (addresses != null && addresses.Count() > 0)
            {
                ip = addresses[0].ToString();
            }
            Console.WriteLine(ip);
            Console.Read();
        }
    }
}
