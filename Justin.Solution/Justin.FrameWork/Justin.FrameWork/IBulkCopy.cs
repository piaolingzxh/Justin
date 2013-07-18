using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Justin.FrameWork
{
    public interface IBulkCopy 
    {                                                                                                                
        void Insert(DbConnection conn, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings=null, DataRowState state = DataRowState.Added);
        void Insert(string connectionString, string tableName, DataTable sourceData, Dictionary<string, string> columnMappings = null, DataRowState state = DataRowState.Added);
        void Check(DbConnection conn);         
    }
}
