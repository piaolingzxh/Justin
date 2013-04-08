using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.DBCompare
{
    public class Table : IData
    {
        public Table(string tableName)
        {
            TableName = tableName;
            CommonFields = new List<string>();
            TranslateFields = new List<TranslateMapping>();
        }
        public string TableName { get; set; }
        public List<TranslateMapping> TranslateFields { get; set; }
        public List<string> CommonFields { get; set; }

        public KeyValuePair<string, TranslateMapping> GetMappingField(string fieldName)
        {
            KeyValuePair<string, TranslateMapping> keyMapping = new KeyValuePair<string, TranslateMapping>(fieldName, null);
            foreach (TranslateMapping item in TranslateFields)
            {
                if (item.FieldName == fieldName)
                {
                    keyMapping = new KeyValuePair<string, TranslateMapping>(fieldName, item);

                }
            }
            foreach (var item in CommonFields)
            {
                if (item == fieldName)
                {
                    keyMapping = new KeyValuePair<string, TranslateMapping>(fieldName, null);

                }
            }
            return keyMapping;
        }

    }
    public class TranslateMapping
    {
        public string FieldName { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceFieldName { get; set; }
        public string DestinationFieldName { get; set; }

    }
}
