using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace Justin.FrameWork.WinForm.Utility
{
    public class DBConnectionInfo
    {
        public DBConnectionInfo()
        {
            this.Dialog = new DataConnectionDialog();
            this.Dialog.DataSources.Add(DataSource.SqlDataSource);
            this.Dialog.DataSources.Add(DataSource.OracleDataSource);
            this.DataSource = DataSource.SqlDataSource;
            this.Provider = DataProvider.SqlDataProvider;
        }

        public DataConnectionDialog Dialog { get; set; }
        public DataSource DataSource { get; set; }
        public string ConnString { get; set; }
        public DataProvider Provider { get; set; }

        public string Change(string connString = "")
        {

            string tempConnString = !string.IsNullOrEmpty(connString) ? connString : this.ConnString;
            try
            {
                if (string.IsNullOrEmpty(tempConnString))
                    return this.GetConnString();
                if (tempConnString.Equals(this.Dialog.ConnectionString))
                    return this.GetConnString();

                if (tempConnString.ToLower().Contains("provider"))
                {
                    if (tempConnString.ToLower().Contains("msdaora"))
                    {
                        this.DataSource = DataSource.OracleDataSource;
                    }
                    else
                    {
                        this.DataSource = DataSource.SqlDataSource;
                    }
                    this.Provider = DataProvider.OleDBDataProvider;
                }
                else
                {
                    this.DataSource = tempConnString.ToLower().Contains("unicode") ? DataSource.OracleDataSource : DataSource.SqlDataSource;
                    this.Provider = tempConnString.ToLower().Contains("unicode") ? DataProvider.OracleDataProvider : DataProvider.SqlDataProvider;
                }

                if (!Dialog.DataSources.Contains(this.DataSource))
                    Dialog.DataSources.Add(this.DataSource);
                Dialog.SelectedDataSource = this.DataSource;




                foreach (DataProvider objDataProvider in Dialog.SelectedDataSource.Providers)
                {

                    if (objDataProvider.Name == "System.Data.OleDb" && this.Provider.Equals(DataProvider.OleDBDataProvider))
                    {
                        Dialog.SelectedDataProvider = objDataProvider;
                        break;
                    }

                    if (objDataProvider.Name == "System.Data.OracleClient" && this.Provider.Equals(DataProvider.OracleDataProvider))
                    {
                        Dialog.SelectedDataProvider = objDataProvider;
                        break;
                    }
                    if (objDataProvider.Name == "System.Data.SqlClient" && this.Provider.Equals(DataProvider.SqlDataProvider))
                    {
                        Dialog.SelectedDataProvider = objDataProvider;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(tempConnString))
                {
                    Dialog.ConnectionString = tempConnString;
                }


            }
            catch
            {

            }
            return this.GetConnString();

        }

        public string GetConnString()
        {
            if (DataConnectionDialog.Show(this.Dialog) == DialogResult.OK)
            {
                this.ConnString = Dialog.ConnectionString;
                this.DataSource = Dialog.SelectedDataSource;
                this.Provider = Dialog.SelectedDataProvider;
            }
            return this.ConnString;
        }

    }
}
