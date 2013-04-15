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
using Justin.Core;
using Justin.FrameWork.WinForm.Models;


namespace Justin.Toolbox
{
    public partial class TableConfigurator : JForm, IDB, IFile
    {
        private TableConfigurator()
        {
            InitializeComponent();
            this.tableConfigCtrl1.FileChanged += this.OnFileChanged;
            this.LoadAction = (fileName) => { this.tableConfigCtrl1.LoadFile(fileName); };
            this.SaveAction = (fileName) => { this.tableConfigCtrl1.SaveFile(fileName); };

        }
        public TableConfigurator(string[] args)
            : this()
        {
            this.FileName = args[0];
            this.ConnStr = args.Length > 1 ? args[1] : "";
        }
        public TableConfigurator(JTable table)
            : this()
        {
            this.tableConfigCtrl1.TableSetting = table;
            this.ConnStr = ConnStr;
            this.Text = table.TableName;
        }

        private void ConfigTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tableConfigCtrl1.TableSetting == null) return;
            if (this.tableConfigCtrl1.TableSetting.Modified)
            {
                if (this.tableConfigCtrl1.ConnStr != this.tableConfigCtrl1.TableSetting.ConnStr)
                {
                    this.tableConfigCtrl1.TableSetting.ConnStr = this.tableConfigCtrl1.ConnStr;
                }
                DialogResult result = MessageBox.Show("是否保存设置？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {

                    this.tableConfigCtrl1.TableSetting.SaveSettings(this.FileName);
                    this.ShowMessage(string.Format("表【{0}】配置保存成功!", this.tableConfigCtrl1.TableSetting.TableName));
                }
            }
            else
            {
                if (this.tableConfigCtrl1.ConnStr != this.tableConfigCtrl1.TableSetting.ConnStr)
                {
                    this.tableConfigCtrl1.TableSetting.ConnStr = this.tableConfigCtrl1.ConnStr;
                    this.tableConfigCtrl1.TableSetting.SaveSettings(this.FileName);
                }
            }
        }

        #region 继承

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName, this.ConnStr);
        }

        public override string ConnStr
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

        protected override string FileName
        {
            get
            {
                return this.tableConfigCtrl1.FileName;
            }
            set
            {
                this.tableConfigCtrl1.FileName = value;
            }
        }

        #endregion

        private void TableConfigurator_Load(object sender, EventArgs e)
        {
            if (this.tableConfigCtrl1.TableSetting == null)
            {
                this.LoadFile(this.FileName);
            }
            else
            { 
            
            }
        }


    }
}