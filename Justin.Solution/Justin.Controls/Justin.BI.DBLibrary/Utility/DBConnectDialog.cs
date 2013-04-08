using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
namespace Justin.BI.DBLibrary.Utility
{
    public class DBConnectDialog
    {
        public enum DataSourceType { SqlDataSource, AccessDataSource, OdbcDataSouce, OracleDataSource, SqlFileDataSource };
        /// <summary>
        /// 手动选择数据源和数据提供程序
        /// </summary>
        /// <returns>返回连接字符串</returns>
        public static string GetConnectionString()
        {
            string connection = null;
            DataConnectionDialog dialog = new DataConnectionDialog();
            DataSource.AddStandardDataSources(dialog);
            if (DataConnectionDialog.Show(dialog) == DialogResult.OK)
            {
                connection = dialog.ConnectionString;
            }
            return connection;
        }

        /// <summary>
        /// 可以直接传入字符格式的数据源名称，然后用转换成枚举格式
        /// </summary>
        /// <param name="str">SqlDataSource, AccessDataSource, OdbcDataSouce, OracleDataSource, SqlFileDataSource</param>
        /// <returns>返回连接字符串</returns>
        public static string GetConnectionString(string str)
        {
            Type DTS = typeof(DataSourceType);

            //从ComboBox中选择的数据源已经转换成字符格式
            DataSourceType DS = (DataSourceType)Enum.Parse(DTS, str);

            return GetConnectionString(DS);
        }


        /// <summary>
        /// 枚举格式
        /// </summary>
        /// <param name="DS">数据提供类型 VS2008Dlg.DataSourceType</param>
        /// <returns>返回连接字符串</returns>
        public static string GetConnectionString(DataSourceType DS)
        {
            string connection = null;
            DataConnectionDialog dialog = new DataConnectionDialog();

            DataSource.AddStandardDataSources(dialog);

            dialog.SelectedDataSource = ConvertToDaSource(DS);
            if (ConvertToDaProvider(DS) != null)
            {
                dialog.SelectedDataProvider = ConvertToDaProvider(DS);
            }
            if (DataConnectionDialog.Show(dialog) == DialogResult.OK)
            {
                connection = dialog.ConnectionString;
            }
            return connection;

        }

        private static DataSource ConvertToDaSource(DataSourceType DST)
        {
            DataSource r = null;
            switch (DST)
            {
                case DataSourceType.AccessDataSource:
                    r = DataSource.AccessDataSource;
                    break;
                case DataSourceType.OdbcDataSouce:
                    r = DataSource.OdbcDataSource;
                    break;
                case DataSourceType.OracleDataSource:
                    r = DataSource.OracleDataSource;
                    break;
                case DataSourceType.SqlDataSource:
                    r = DataSource.SqlDataSource;
                    break;
                case DataSourceType.SqlFileDataSource:
                    r = DataSource.SqlFileDataSource;
                    break;
                default:
                    r = DataSource.SqlDataSource;
                    break;
            }
            return r;
        }


        private static DataProvider ConvertToDaProvider(DataSourceType DST)
        {
            DataProvider r = null;
            switch (DST)
            {
                case DataSourceType.AccessDataSource:
                    r = null;// DataProvider.OleDBDataProvider;
                    break;
                case DataSourceType.OdbcDataSouce:
                    r = DataProvider.OdbcDataProvider;
                    break;
                case DataSourceType.OracleDataSource:
                    r = DataProvider.OracleDataProvider;
                    break;
                case DataSourceType.SqlDataSource:
                    r = DataProvider.SqlDataProvider;
                    break;
                case DataSourceType.SqlFileDataSource:
                    r = null;// DataProvider.SqlDataProvider;
                    break;
                default:
                    r = null;
                    break;
            }
            return r;
        }
    }
}
