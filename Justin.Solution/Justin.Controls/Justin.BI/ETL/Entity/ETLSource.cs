using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Justin.FrameWork.Entities;

namespace Justin.BI.ETL.Entity
{
    [XmlInclude(typeof(Table)), XmlInclude(typeof(View))]
    public abstract class ETLSource
    {
        public ETLSource() { }

        [XmlAttribute()]
        public string Name { get; set; }
        public List<Field> OrderBy { get; set; }


        public abstract string ToQuerySQL(int pageSize, int pageIndex);
        public abstract SerializableDictionary<string, string> GetDeafultColumnMapping();
    }

}
