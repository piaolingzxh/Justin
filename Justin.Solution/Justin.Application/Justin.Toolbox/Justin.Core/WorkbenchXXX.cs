using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Justin.Core.Customization;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Core
{
    public partial class WorkbenchXXX : Form
    {
        #region 加载上次打开的文件

        //加载上次打开的窗口
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        public WorkbenchXXX()
        {
            InitializeComponent();
            //m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        #endregion

        #region 右键直接打开文件

        bool specialFile = false;
        //右键菜单打开文件
        string rightContextMenuFileName = "";

        public WorkbenchXXX(string fileName)
        {
            InitializeComponent();
            //this.specialFile = true;
            //this.rightContextMenuFileName = fileName;
            //this.m_bSaveLayout = false;
        }

        #endregion

        #region Property

        //其他属性
        private OutPutWindow OutPutWin = new OutPutWindow();
        bool forceClose = false;
        public DockPanel DockPanel
        {
            get
            {
                return this.dockPanel;
            }
        }
        string fileNameOfAddin = "Addin.xml";
        string addinFolder = @"\Addins\";

        #endregion



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

        protected virtual IDockContent NewToolAccrodingPersistString(string[] parsedStrings)
        {
            return null;
        }
        protected virtual void OpenFileAccordingToFile(string fileName) { }

        #region Form事件

        protected void LoadForm()
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

            DynamicMenuGenerate();
        }

        protected void ClosingForm(object sender, FormClosingEventArgs e)
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

        #region 动态菜单

        protected AddinConfig addinConfig;

        protected void DynamicMenuGenerate()
        {
            if (File.Exists(fileNameOfAddin))
            {
                try
                {
                    addinConfig = SerializeHelper.XmlDeserializeFromFile<AddinConfig>(fileNameOfAddin);
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                }
                GenerateSubMenuForNew();
                GenerateSubMenuForOpen();
                GenerateSubMenuForTools();
            }
        }
        protected void GenerateSubMenuForNew()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.NewItems == null)
                return;
            //menuItemNew.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.NewItems)
            {
                ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                tsItem.Click += subItemOfNew_Click;
                menuItemNew.DropDownItems.Add(tsItem);
            }

        }
        protected void GenerateSubMenuForOpen()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.OpenItems == null)
                return;
            menuItemOpen.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.OpenItems)
            {
                ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                tsItem.Click += subItemOfNew_Click;
                menuItemOpen.DropDownItems.Add(tsItem);
            }

        }
        protected void GenerateSubMenuForTools()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.ToolsItems == null)
                return;
            menuItemTools.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.ToolsItems)
            {
                ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                tsItem.Click += subItemOfNew_Click;
                menuItemTools.DropDownItems.Add(tsItem);
            }

        }

        #endregion

        #region File菜单

        #region New

        private void subItemOfNew_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsItem = sender as ToolStripMenuItem;
            MenuItem data = tsItem.Tag as MenuItem;
            ActiveContent(data);

            //TestDataGenerator tdgtool = new TestDataGenerator("");
            //tdgtool.MdiParent = this;
            //tdgtool.Show(dockPanel);
        }

        #endregion

        #region Open

        private void subItemOfOpen_Click(object sender, EventArgs e)
        {
            //TestDataGenerator tdgtool = new TestDataGenerator("");
            //tdgtool.MdiParent = this;
            //tdgtool.Show(dockPanel);
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

        #region Window菜单

        private void newInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
            //this.GetType().Assembly.Location;
            //System.Windows.Forms.Application.ExecutablePath;
            //System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string fileName = this.GetType().Assembly.Location;
            pExecuteEXE.StartInfo.FileName = fileName;
            pExecuteEXE.Start();
            //pExecuteEXE.WaitForExit();//无限期等待完成
            //pExecuteEXE.WaitForExit(10000);//等待最长10秒钟完成。
        }

        #endregion

        #region Tools 菜单

        private void subItemOfTools_Click(object sender, EventArgs e)
        {
            //TestDataGenerator tdgtool = new TestDataGenerator("");
            //tdgtool.MdiParent = this;
            //tdgtool.Show(dockPanel);
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
                //menuItemOpenFile_Click(null, null);
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

        #region Notify Area

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

        #endregion

        #region 关闭子窗体
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


        #endregion

        private void ActiveContent(MenuItem data)
        {
            string classStr = data.Class;
            string[] classInfo = classStr.Trim().Split(',');
            if (classInfo.Length != 3)
            {
                this.ShowMessage("请检查Class设置");
                return;
            }
            Assembly assembly;
            if (!string.IsNullOrEmpty(classInfo[2]))
            {
                assembly = Assembly.LoadFrom(Path.Combine(Application.StartupPath, addinFolder + classInfo[2]));
            }
            else
            {
                assembly = Assembly.GetEntryAssembly();
            }
            Type type = assembly.GetType(classInfo[0]);
             
            object obj = Activator.CreateInstance(type);

            JDockForm formToShow = (JDockForm)obj;
            formToShow.MdiParent = this;
            formToShow.Show(dockPanel);
        }

        private void 事实ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abount executor = new Abount();
            executor.MdiParent = this;
            executor.Show();
        }

        private void Workbench_Load(object sender, EventArgs e)
        {
            //this.LoadForm();
        }

        private void Workbench_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.ClosingForm(sender, e);
        }


    }
}
