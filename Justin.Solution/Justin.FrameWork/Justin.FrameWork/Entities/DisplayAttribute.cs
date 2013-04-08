using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Entities
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class DisplayAttribute : Attribute
    {

        public string Name { get; set; }
        public DisplayAttribute(string name)
        {
            this.Name = name;
        }
    }
}
