using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Justin.FrameWork.Entities;

namespace Justin.BI.ETL.Entity
{
    public class ETLInfo
    {
        public ETLInfo()
        {
            this.ColumnMapping = new SerializableDictionary<string, string>();
        }
        public ETLInfo(ETLSource source, string dstTableName)
            : this()
        {
            this.SourceTable = source;
            this.DestinationTableName = dstTableName;

            this.ColumnMapping = SourceTable.GetDeafultColumnMapping();
        }

        [XmlAttribute()]
        public string DestinationTableName { get; set; }
        public ETLSource SourceTable { get; set; }
        public SerializableDictionary<string, string> ColumnMapping { get; set; }
    }
}
