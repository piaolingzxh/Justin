using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class InterfaceProgram
    {
        public static void Main()
        {


        }
    }
    public interface IName
    {
        string Name { private get; set; }
    }

    public class Schema : IName
    {
        public Schema(string name)
        {
            if (this is IName)
            {
                IName iName = this as IName;
                iName.Name = name;
            }
        }


        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
