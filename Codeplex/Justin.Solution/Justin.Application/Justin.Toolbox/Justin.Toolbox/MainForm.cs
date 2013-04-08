using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Settings;
using Justin.Toolbox.Customization;
using Justin.Toolbox.Tools;
using WeifenLuo.WinFormsUI.Docking;
using Justin.FrameWork.Extensions;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.BI.DBLibrary.Utility;

namespace Justin.Toolbox
{
    public partial class MainForm : Form
    {
        #region 加载上次打开的文件

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        /// <summary>
        /// 加载上次打开的文件
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        #endregion

        #region 右键直接打开文件

        string rightContextMenuFileName = "";
        bool specialFile = false;
        /// <summary>
        ///     实现右键文件进行打开
        /// </summary>
        /// <param name="fileName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public MainForm(string fileName)
        {
            InitializeComponent();
            this.specialFile = true;
            this.rightContextMenuFileName = fileName;
            this.m_bSaveLayout = false;
        }

        #endregion
        bool forceClose = false;
        private OutPutWindow OutPutWin = new OutPutWindow();
        public DockPanel DockPanel
        {
            get
            {
                return this.dockPanel;
            }
        }

        #region Form事件

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!specialFile) //加载上次打开的文件
            {
                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
                try
                {
                    if (File.Exists(configFile))
                        dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                }
                catch
                {
                    File.Delete(configFile);
                }
            }
            else   //右键文件名，进行打开
            {
                OpenFileAccordingToFile(rightContextMenuFileName);
            }
            dockPanel.ShowDocumentIcon = true;

            OutPutWin.Show(dockPanel, DockState.DockBottom);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {

                if (m_bSaveLayout && !specialFile)
                {
                    string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
                    dockPanel.SaveAsXml(configFile);
                }
            }
        }

        #endregion

        #region File菜单

        #region New
        //TestDataGenerator
        private void TestDataGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestDataGenerator tdgtool = new TestDataGenerator("");
            tdgtool.MdiParent = this;
            tdgtool.Show(dockPanel);
        }
        //sqlExecuteor      
        private void sqlExecuteorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlExecuteor form = new SqlExecuteor("", "");
            form.MdiParent = this;
            form.Show(dockPanel);
        }
        //mdxExecutor
        private void mdxExecutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxExecutor executor = new MdxExecutor();
            executor.MdiParent = this;
            executor.Show(dockPanel);
        }
        //CodeCompiler
        private void CodeCompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JCodeCompiler executor = new JCodeCompiler();
            executor.MdiParent = this;
            executor.Show(dockPanel);
        }
        //mondrianSchemaWorkbench
        private void mondrianSchemaWorkbenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MondrianSchemaWorkbench fa = new MondrianSchemaWorkbench();
            fa.MdiParent = this;
            fa.Show();
        }
        //jsonViewer
        private void jsonViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonViewer jv = new JsonViewer();
            jv.MdiParent = this;
            jv.Show();
        }
        //codeSnippet
        private void codeSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeSnippetMgr form = new CodeSnippetMgr();
            form.MdiParent = this;
            form.Show();
        }

        #endregion

        #region Open

        private void menuItemOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Constants.ConfigFileFolder;
            fileDialog.RestoreDirectory = true;
            StringBuilder filterBuilder = new StringBuilder();
            foreach (FileType fileType in Enum.GetValues(typeof(FileType)))
            {
                FileInfoAttribute fia = fileType.GetFileInfoAttribute();
                filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, fia.DefaultFileExtension, fia.DefaultDisplayName);
                foreach (string fileExtension in fia.GetAllowFileExtensions(true))
                {
                    filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, fileExtension, fileExtension);
                }
            }
            filterBuilder.AppendFormat(Constants.OpenFileDialogFilterFormart, "*", "所有");
            fileDialog.Filter = filterBuilder.ToString().TrimEnd('|');
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                OpenFileAccordingToFile(fileName);
            }
        }

        #endregion

        //Close----------------------------
        private void menuItemCloseCurent_Click(object sender, EventArgs e)
        {
            this.CloseCurrent();
        }

        private void menuItemCloseAll_Click(object sender, EventArgs e)
        {
            this.CloseAllDocuments();
        }

        private void menuItemCloseAllButThisOne_Click(object sender, EventArgs e)
        {
            this.CloseAllDocumentButCurrent();
        }

        //Exit------------------------------------------

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitWithOutSavingLayout_Click(object sender, EventArgs e)
        {
            m_bSaveLayout = false;
            Close();
            m_bSaveLayout = true;
        }





        #endregion

        #region View菜单


        //--------------------------
        private void menuItemOutputWindow_Click(object sender, EventArgs e)
        {
            if (OutPutWin.IsHidden)
            {
                OutPutWin.Show(dockPanel, DockState.DockBottom);
            }
            else
            {
                OutPutWin.Hide();
            }
        }

        private void menuItemToolBar_Click(object sender, EventArgs e)
        {
            toolBar.Visible = menuItemToolBar.Checked = !toolBar.Visible;

        }

        private void menuItemStatusBar_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = menuItemStatusBar.Checked = !statusStrip1.Visible;
        }


        #endregion

        #region Style菜单

        private void menuItemTools_DropDownOpening(object sender, EventArgs e)
        {
            menuItemLockLayout.Checked = !this.dockPanel.AllowEndUserDocking;
            menuItemShowDocumentIcon.Checked = this.dockPanel.ShowDocumentIcon;
        }

        private void menuItemLockLayout_Click(object sender, EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }

        private void menuItemShowDocumentIcon_Click(object sender, EventArgs e)
        {
            dockPanel.ShowDocumentIcon = menuItemShowDocumentIcon.Checked = !menuItemShowDocumentIcon.Checked;
        }

        private void SetSchema(object sender, System.EventArgs e)
        {
            CloseAllContents();

            if (sender == menuItemSchemaVS2005)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2005);
            else if (sender == menuItemSchemaVS2003)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2003);

            menuItemSchemaVS2005.Checked = (sender == menuItemSchemaVS2005);
            menuItemSchemaVS2003.Checked = (sender == menuItemSchemaVS2003);
        }

        private void SetDocumentStyle(object sender, System.EventArgs e)
        {
            DocumentStyle oldStyle = dockPanel.DocumentStyle;
            DocumentStyle newStyle;
            if (sender == menuItemDockingMdi)
                newStyle = DocumentStyle.DockingMdi;
            else if (sender == menuItemDockingWindow)
                newStyle = DocumentStyle.DockingWindow;
            else if (sender == menuItemDockingSdi)
                newStyle = DocumentStyle.DockingSdi;
            else
                newStyle = DocumentStyle.SystemMdi;

            if (oldStyle == newStyle)
                return;

            if (oldStyle == DocumentStyle.SystemMdi || newStyle == DocumentStyle.SystemMdi)
                CloseAllDocuments();

            dockPanel.DocumentStyle = newStyle;
            menuItemDockingMdi.Checked = (newStyle == DocumentStyle.DockingMdi);
            menuItemDockingWindow.Checked = (newStyle == DocumentStyle.DockingWindow);
            menuItemDockingSdi.Checked = (newStyle == DocumentStyle.DockingSdi);
            menuItemSystemMdi.Checked = (newStyle == DocumentStyle.SystemMdi);
            menuItemLayoutByCode.Enabled = (newStyle != DocumentStyle.SystemMdi);
            menuItemLayoutByXml.Enabled = (newStyle != DocumentStyle.SystemMdi);
            toolBarButtonLayoutByCode.Enabled = (newStyle != DocumentStyle.SystemMdi);
            toolBarButtonLayoutByXml.Enabled = (newStyle != DocumentStyle.SystemMdi);
        }

        #endregion

        #region Tool菜单

        //FileAssociation
        private void FileAssociationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileAssociation fa = new FileAssociation();
            fa.MdiParent = this;
            fa.Show();
        }

        #endregion

        #region Window菜单

        private void menuItemNewWindow_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
        }

        private void newInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
            //this.GetType().Assembly.Location;
            //System.Windows.Forms.Application.ExecutablePath;
            //System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string fileName = this.GetType().Assembly.Location;
            pExecuteEXE.StartInfo.FileName = fileName;
            pExecuteEXE.Start();
            pExecuteEXE.WaitForExit();//无限期等待完成
            //pExecuteEXE.WaitForExit(10000);//等待最长10秒钟完成。
        }

        #endregion

        #region Help菜单

        //About
        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            Abount abount = new Abount();
            abount.ShowDialog();

        }

        #endregion

        #region 工具栏

        private void toolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolBarButtonNew)
            {
                //menuItemNewTestDataConfigFile_Click(null, null);
            }
            else if (e.ClickedItem == toolBarButtonOpen)
            {
                menuItemOpenFile_Click(null, null);
            }
            else if (e.ClickedItem == toolBarButtonOutputWindow)
            {
                menuItemOutputWindow_Click(null, null);
            }
            else if (e.ClickedItem == toolBarButtonLayoutByCode)
            { }
            else if (e.ClickedItem == toolBarButtonLayoutByXml)
            { }
        }

        #endregion

        #region 函数 (业务相关)


        private IDockContent NewToolAccrodingPersistString(string[] parsedStrings)
        {
            if (parsedStrings[0] == typeof(TableConfigurator).ToString())
            {
                #region TableConfigurator

                if (parsedStrings.Length != 3)
                    return null;

                string tableName = parsedStrings[1];
                string fileName = JTools.GetFileName(tableName, FileType.TableConfig);

                if (!File.Exists(fileName))
                {
                    this.ShowMessage("文件不存在", string.Format("文件{0}不存在", fileName));
                }
                else
                {
                    JTable table = JTools.ReadTableSettingByFile(fileName);
                    TableConfigurator tableConfig = new TableConfigurator(table);
                    return tableConfig;
                }

                #endregion
            }
            else if (parsedStrings[0] == typeof(TestDataGenerator).ToString())
            {
                #region TestDataGenerator

                if (parsedStrings.Length != 2)
                    return null;

                TestDataGenerator testDataGenerator = new TestDataGenerator(parsedStrings[1]);
                return testDataGenerator;

                #endregion
            }
            else if (parsedStrings[0] == typeof(SqlExecuteor).ToString())
            {
                #region SqlExecuteor

                if (parsedStrings.Length != 3)
                    return null;

                string fileName = parsedStrings[1];

                if (!File.Exists(fileName))
                {
                    this.ShowMessage("文件不存在", string.Format("文件{0}不存在", fileName));
                }
                SqlExecuteor sqlExecutor = new SqlExecuteor(fileName, parsedStrings[2]);
                return sqlExecutor;

                #endregion
            }
            else if (parsedStrings[0] == typeof(MdxExecutor).ToString())
            {
                #region MdxExecutor

                if (parsedStrings.Length != 3)
                    return null;

                MdxExecutor executor = new MdxExecutor(parsedStrings[1], parsedStrings[2]);
                return executor;

                #endregion
            }
            else if (parsedStrings[0] == typeof(MondrianSchemaWorkbench).ToString())
            {
                #region MondrianSchemaWorkbench

                if (parsedStrings.Length != 3)
                    return null;

                MondrianSchemaWorkbench executor = new MondrianSchemaWorkbench(parsedStrings[1], parsedStrings[2]);
                return executor;

                #endregion
            }
            else if (parsedStrings[0] == typeof(JCodeCompiler).ToString())
            {
                #region JCodeCompiler

                //if (parsedStrings.Length != 3)
                //    return null;

                JCodeCompiler executor = new JCodeCompiler();
                return executor;

                #endregion
            }
            else if (parsedStrings[0] == typeof(JsonViewer).ToString())
            {
                #region JsonViewer

                JsonViewer executor = new JsonViewer();
                return executor;

                #endregion
            }
            else if (parsedStrings[0] == typeof(CodeSnippetMgr).ToString())
            {
                #region CodeSnippetMgr

                CodeSnippetMgr executor = new CodeSnippetMgr();
                return executor;

                #endregion
            }
            return null;
        }

        //右键打开不同类型的文件
        public void OpenFileAccordingToFile(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName).ToLower().TrimStart('.');
            if (FileType.TableConfig.GetAllowFileExtensions().Contains(fileExtension, true))
            {
                OpenTableConfigFile(fileName);
            }
            else if (FileType.SQL.GetAllowFileExtensions().Contains(fileExtension, true))
            {
                OpenSQLFile(fileName);
            }
            else if (FileType.MDX.GetAllowFileExtensions().Contains(fileExtension, true))
            {
                OpenMDXFile(fileName);
            }
            else
            {
                this.ShowMessage("不支持此文件类型", "不支持此文件类型");
            }
        }
        private void OpenTableConfigFile(string fileName)
        {
            JTable table = JTools.ReadTableSettingByFile(fileName);
            if (table != null)
            {
                TableConfigurator configForm = new TableConfigurator(table);
                configForm.MdiParent = this;
                configForm.Show();
            }
            else
            {
                this.ShowMessage("配置文件不可识别", "所读文件不符合表配置文件规则");
            }
        }
        private void OpenSQLFile(string fileName)
        {
            SqlExecuteor sqlExecutor = new SqlExecuteor(fileName, "");
            sqlExecutor.MdiParent = this;
            sqlExecutor.Show();
        }
        private void OpenMDXFile(string fileName)
        {
            string mdx = File.ReadAllText(fileName);
            MdxExecutor executor = new MdxExecutor("", mdx);
            executor.MdiParent = this;
            executor.Show();
        }

        #endregion

        #region 业务无关
        //关闭窗体 (不关闭OutPutWindow)   
        public void CloseAllDocumentBut(JDockForm exceptForm)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form != exceptForm && !(form is OutPutWindow))
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (document != exceptForm && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        public void CloseAllDocumentButCurrent()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi && !(form is OutPutWindow))
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        public void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (!(form is OutPutWindow))
                    {
                        form.Close();
                    }
                }
            }
            else
            {
                for (int index = dockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (dockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)dockPanel.Contents[index];
                        if (!(content is OutPutWindow))
                        {
                            content.DockHandler.Close();
                        }
                    }
                }
            }
        }
        public void CloseCurrent()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi && !(ActiveMdiChild is OutPutWindow))
            {
                ActiveMdiChild.Close();
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (document.DockHandler.IsActivated && !(document is OutPutWindow))
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }
        private void CloseAllContents()
        {
            OutPutWin.DockPanel = null;
            CloseAllDocuments();
        }

        //根据保存文件加载子窗体信息
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(OutPutWindow).ToString())
            {
                return OutPutWin;
            }
            else
            {
                string[] parsedStrings = persistString.Split(Constants.Splitor.ToCharArray());
                if (parsedStrings.Count() >= 1)
                {
                    IDockContent content = NewToolAccrodingPersistString(parsedStrings);
                    if (content != null)
                    {
                        return content;
                    }
                }
                return null;
            }
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forceClose = true;
            this.Close();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
                this.Show();
            else
                this.Hide();
        }



    }
}
