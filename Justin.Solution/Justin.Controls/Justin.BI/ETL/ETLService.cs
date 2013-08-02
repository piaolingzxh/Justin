using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using Justin.BI.ETL.Entity;
using Justin.FrameWork;
using Justin.FrameWork.Helper;

namespace Justin.BI.ETL
{
    public class ETLService
    {
        public void Process(ETLInfo etlInfo, string oleDbConnstring, string destinationOleDbConnectionString, bool clearDataBeforeETL = false, Action<int> callback = null, int pageSize = 10000)
        {
            int pageIndex = 0;
            int result = 1;
            BulkCopy bcp = new BulkCopy(oleDbConnstring);
            OleDbConnection sourceConn = new OleDbConnection(oleDbConnstring);
            OleDbConnection dstConnection = new OleDbConnection(destinationOleDbConnectionString);
            if (clearDataBeforeETL)
                OleDbHelper.TruncateTable(dstConnection, etlInfo.DestinationTableName);
            int success = 0;
            while (result > 0)
            {
                result = BulkCopyByPage(etlInfo, pageSize, pageIndex, bcp, sourceConn, dstConnection);
                if (result > 0)
                {
                    pageIndex++;
                    success += result;
                    if (callback != null)
                        callback(success);
                }    
            }
        }

        private int BulkCopyByPage(ETLInfo etlInfo, int pageSize, int pageIndex, BulkCopy bcp, OleDbConnection sourceConnection, DbConnection DestinationConnection)
        {
            string sql = etlInfo.SourceTable.ToQuerySQL(pageSize, pageIndex); ;

            try
            {
                DataTable dt = OleDbHelper.ExecuteDataTable(sourceConnection, sql);
                if (dt == null || dt.Rows.Count == 0)
                    return 0;

                bcp.Insert(etlInfo.DestinationTableName, dt, etlInfo.ColumnMapping);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                string errorString = string.Format("转移失败,数据源SQL:{0}...", sql);
                File.AppendAllText(@"bulkcopy.log", errorString);
                throw;
            }
        }

        public void Process(string etlInfoFilePath, string oleDbConnstring, string destinationOleDbConnectionString, bool clearDataBeforeETL = false, Action<int> callback = null, int pageSize = 10000)
        {
            ETLInfo etlInfo = SerializeHelper.XmlDeserializeFromFile<ETLInfo>(etlInfoFilePath);
            this.Process(etlInfo, oleDbConnstring, destinationOleDbConnectionString, clearDataBeforeETL, callback, pageSize);
        }

        public void CheckConn(DbConnection sourceConnection, DbConnection DestinationConnection)
        {
            if (sourceConnection.State != ConnectionState.Open)
            {
                sourceConnection.Open();
            }
            if (DestinationConnection.State != ConnectionState.Open)
            {
                DestinationConnection.Open();
            }
        }
    }
}
