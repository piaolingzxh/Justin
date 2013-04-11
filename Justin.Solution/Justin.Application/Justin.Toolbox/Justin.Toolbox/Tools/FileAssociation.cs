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
using AssociationManager;
using Justin.BI.DBLibrary.TestDataGenerate;
using Microsoft.Win32;
using Justin.BI.DBLibrary.Utility;
using Justin.Core;

namespace Justin.Toolbox.Tools
{
    public partial class FileAssociation : JForm
    {
        public FileAssociation()
        {
            InitializeComponent();
        }
         

        private void Form1_Load(object sender, EventArgs e)
        {

            this.cListBoxFileExtension.Items.Clear();

            foreach (FileType item in Enum.GetValues(typeof(FileType)))
            {
                this.cListBoxFileExtension.Items.Add(item.GetDefaultFileExtension());
            }
        }

        private void btnDoFileAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileAssociationManager mgr = new FileAssociationManager())
                {
                    foreach (object item in cListBoxFileExtension.CheckedItems)
                    {
                        using (ApplicationAssociation ext = mgr.RegisterFileAssociation(item.ToString()))
                        {
                            ext.DefaultIcon = new ApplicationIcon(Application.ExecutablePath);
                            ext.ShellOpenCommand = Application.ExecutablePath;
                            ext.Associated = true;
                        }
                    }
                }
                this.ShowMessage("已关联文件");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, ex.ToString());
            }
        }

        private void btnUoDoFileAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileAssociationManager mgr = new FileAssociationManager())
                {
                    mgr.UnregisterApplicationAssociation();
                }
                this.ShowMessage("已撤销文件关联");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, ex.ToString());
            }
        }
    }
}
