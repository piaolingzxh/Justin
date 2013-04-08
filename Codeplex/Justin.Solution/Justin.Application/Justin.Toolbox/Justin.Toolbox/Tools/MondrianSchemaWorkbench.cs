using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.FormUI.SharpCodeTextEditor;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.FormUI.PropertyGrid;

namespace Justin.Toolbox.Tools
{
    public partial class MondrianSchemaWorkbench : JDockForm
    {
        public MondrianSchemaWorkbench()
        {
            InitializeComponent();
        }
        public MondrianSchemaWorkbench(string fileName, string dstFileName)
            : this()
        {
            schemaViewerCtrl1.SchemaFileName = fileName;
            schemaViewerCtrl1.SaveSchemaFileName = dstFileName;
        }

        #region 继承


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), schemaViewerCtrl1.SchemaFileName, schemaViewerCtrl1.SaveSchemaFileName);
        }

        #endregion
    }



}
