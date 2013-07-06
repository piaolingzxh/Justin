using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Justin.FrameWork;
using Justin.FrameWork.Helper;
using Oracle.DataAccess.Client;

namespace Justin.BI.ETL
{
    public class ETLService
    {
        public void Process(ETLInfo etlInfo, DbConnection sourceConnection, DbConnection DestinationConnection, bool clearDataBeforeETL = false, Action<int> callback = null, int pageSize = 10000)
        {
            int pageIndex = 0;
            int result = 1;
            IBulkCopy bcp = null;

            if (DestinationConnection is SqlConnection)
            {
                bcp = new BulkCopySQL();
            }
            else
            {
                bcp = new BulkCopyOracle();
            }
            if (clearDataBeforeETL)
                bcp.TruncateTable(DestinationConnection, etlInfo.DestinationTableName);
            int success = 0;
            while (result > 0)
            {
                result = BulkCopyByPage(etlInfo, pageSize, pageIndex, bcp, sourceConnection, DestinationConnection);
                pageIndex++;
                success += result;
                if (callback != null && result > 0)
                    callback(success);
            }
        }

        private int BulkCopyByPage(ETLInfo etlInfo, int pageSize, int pageIndex, IBulkCopy bcp, DbConnection sourceConnection, DbConnection DestinationConnection)
        {
            string sql = etlInfo.SourceTable.ToQuerySQL(pageSize, pageIndex); ;

            try
            {
                DataTable dt = DBHelper.ExecuteDataTable(sourceConnection,sql);
                if (dt == null || dt.Rows.Count == 0)
                    return 0;

                bcp.Insert(DestinationConnection, etlInfo.DestinationTableName, dt, etlInfo.ColumnMapping, DataRowState.Unchanged);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                string errorString = string.Format("转移失败,数据源SQL:{0}...", sql);
                File.AppendAllText(@"bulkcopy.log", errorString);
                throw;
            }
        }

        public void Process(string etlInfoFilePath, DbConnection sourceConnection, DbConnection DestinationConnection, bool clearDataBeforeETL = false, Action<int> callback = null, int pageSize = 10000)
        {
            ETLInfo etlInfo = SerializeHelper.XmlDeserializeFromFile<ETLInfo>(etlInfoFilePath);
            this.Process(etlInfo, sourceConnection, DestinationConnection, clearDataBeforeETL, callback, pageSize);
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
