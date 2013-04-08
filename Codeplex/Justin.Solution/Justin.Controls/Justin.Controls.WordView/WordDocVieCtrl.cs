using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Xps.Packaging;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.IO;
 
namespace Justin.Controls.WordView
{
    public partial class WordDocVieCtrl : System.Windows.Forms.UserControl
    {
        public WordDocVieCtrl()
        {
            InitializeComponent();
        }

        public void LoadWordDoc(string docFileName)
        {
            if (string.IsNullOrEmpty(docFileName) || !File.Exists(docFileName))
            {
                MessageBox.Show("The file is invalid. Please select an existing file again.");
            }
            else
            {
                string convertedXpsDoc = string.Concat(Path.GetTempPath(), "\\", Guid.NewGuid().ToString(), ".xps");
                XpsDocument xpsDocument = ConvertWordToXps(docFileName, convertedXpsDoc);
                if (xpsDocument == null)
                {
                    return;
                }

                docView.Document = xpsDocument.GetFixedDocumentSequence();
            }
        }
        private XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        {
            // Create a WordApplication and host word document
            Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                wordApp.Documents.Open(wordFilename);

                // To Invisible the word document
                wordApp.Application.Visible = false;

                // Minimize the opened word document
                wordApp.WindowState = WdWindowState.wdWindowStateMinimize;

                Document doc = wordApp.ActiveDocument;

                doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);

                XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
                return xpsDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs, The error message is  " + ex.ToString());
                return null;
            }
            finally
            {
                wordApp.Documents.Close();
                ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
            
        }
        DocumentViewer docView = new DocumentViewer();


        private void WordDocVieCtrl_Load(object sender, EventArgs e)
        {
            this.elementHost1.Child = docView;
        }
    }
}
