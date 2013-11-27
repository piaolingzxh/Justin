using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Justin.FrameWork.Extensions;
using System.Configuration;
namespace Justin.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Json s = new Json("123");
            Json s2 = new Json("123","vbin");

            Console.WriteLine("Ok");
        }

        public class Json
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public Json(string id)
            {
                this.Id = id;
            }
            public Json(string id,string name)
            {
                this.Id = id;
                this.Name = name;
            }
        }
    }
}
