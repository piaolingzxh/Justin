using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.BI.DBLibrary.Utility;
using Justin.FrameWork.Helper;
using System.Configuration;
using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Extensions;


namespace Justin.Toolbox.Tools
{
    public partial class TableConfigurator : JDBDcokForm
    {

        public TableConfigurator(JTable table)
        {
            InitializeComponent();
            this.tableConfigCtrl1.TableSetting = table;
            this.ConnStr = ConnStr;
            this.Text = table.TableName;
        }

        private void ConfigTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tableConfigCtrl1.TableSetting.Modified)
            {
                if (this.tableConfigCtrl1.ConnStr != this.tableConfigCtrl1.TableSetting.ConnStr)
                {
                    this.tableConfigCtrl1.TableSetting.ConnStr = this.tableConfigCtrl1.ConnStr;
                }
                DialogResult result = MessageBox.Show("是否保存设置？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {

                    this.tableConfigCtrl1.TableSetting.SaveSettings();
                    this.ShowMessage(string.Format("表【{0}】配置保存成功!", this.tableConfigCtrl1.TableSetting.TableName));
                }
            }
            else
            {
                if (this.tableConfigCtrl1.ConnStr != this.tableConfigCtrl1.TableSetting.ConnStr)
                {
                    this.tableConfigCtrl1.TableSetting.ConnStr = this.tableConfigCtrl1.ConnStr;
                    this.tableConfigCtrl1.TableSetting.SaveSettings();
                }
            }
        }


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.tableConfigCtrl1.TableSetting.TableName, this.tableConfigCtrl1.ConnStr);
        }

        protected override string ConnStr
        {
            get
            {
                return this.tableConfigCtrl1.ConnStr;
            }
            set
            {
                this.tableConfigCtrl1.ConnStr = value;
                base.ConnStr = value;
            }
        }
    }
}