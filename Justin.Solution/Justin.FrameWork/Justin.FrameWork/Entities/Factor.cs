using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Entities
{
    public class Factor
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public Factor()
        { }
        public Factor(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
