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
using Justin.Core;
using Justin.Core.Customization;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using WeifenLuo.WinFormsUI.Docking;

namespace Justin.Workspace
{
    public partial class Workbench : WorkbenchBase
    {
        public override DockPanel DockPanel
        {
            get
            {
                return this.dockPanel;
            }
        }
        bool forceClose = false;


        #region 加载上次打开的文件

        //加载上次打开的窗口
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

        public Workbench()
        {
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
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

        #region 右键直接打开文件

        bool specialFile = false;
        //右键菜单打开文件
        string rightContextMenuFileName = "";

        public Workbench(string[] args)
        {
            InitializeComponent();
            this.specialFile = true;
            this.rightContextMenuFileName = args[0];
            this.m_bSaveLayout = false;
        }

        #endregion

        protected virtual IDockContent NewToolAccrodingPersistString(string[] parsedStrings)
        {
            if (parsedStrings.Length < 1 || parsedStrings[0] == typeof(OutPutWindow).ToString())
                return null;
            string[] args = null;
            if (parsedStrings.Length > 1)
            {
                args = new string[parsedStrings.Length - 1];

                for (int i = 1; i <= parsedStrings.Length - 1; i++)
                {
                    args[i - 1] = parsedStrings[i];
                }
            }
            string className = parsedStrings[0];
            var query = addinConfig.Tools.First(row => row.ClassName == className);
            string dllName = query != null ? query.DllFileName : "";

            JForm form = CreateJDockForm(className, dllName, args == null ? null : new object[] { args });
            return form;
        }
        protected virtual void OpenFileAccordingToFile(string fileName) { }

        #region 关闭子窗体

        //关闭窗体 (不关闭OutPutWindow)   
        public void CloseAllDocumentBut(JForm exceptForm)
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

        #region File 菜单

        //New  
        private void subItemOfNew_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsItem = sender as ToolStripMenuItem;
            Justin.Core.MenuItem data = tsItem.Tag as Justin.Core.MenuItem;
            ActiveContent(data);

            //TestDataGenerator tdgtool = new TestDataGenerator("");
            //tdgtool.MdiParent = this;
            //tdgtool.Show(dockPanel);
        }

        //Open
        private void subItemOfOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsItem = sender as ToolStripMenuItem;
            Justin.Core.MenuItem data = tsItem.Tag as Justin.Core.MenuItem;
            openFileDialog1.InitialDirectory = Constants.ConfigFileFolder;//注意这里写路径时要用c:\\而不是c:\
            openFileDialog1.Filter = GetOpenFileDialogFilter(data.Extensions);
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                ActiveContent(data, fileName);
            }
        }

        //关闭当前、关闭所有、关闭所有（当前除外）
        private void menuItemClose_Click(object sender, EventArgs e)
        {
            if (sender == closeCurrentToolStripMenuItem)
            {
                this.CloseCurrent();
            }
            else if (sender == closeAllToolStripMenuItem)
            {
                this.CloseAllDocuments();
            }
            else if (sender == closeAllButCurrentToolStripMenuItem)
            {
                this.CloseAllDocumentButCurrent();
            }
        }

        //退出、退出前保存
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            forceClose = true;
            if (sender == exitToolStripMenuItem || sender == exitToolStripMenuItem1)
            {
                this.Close();
            }
            else if (sender == exitWithoutSavingLayoutToolStripMenuItem)
            {
                m_bSaveLayout = false;
                this.Close();
                m_bSaveLayout = true;
            }

        }

        #endregion

        #region View 菜单

        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            outputWindowToolStripMenuItem.Checked = this.OutPutWin.Visible;
        }

        private void outputWindowToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolBar.Visible = menuItemToolBar.Checked = !toolBar.Visible;
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip1.Visible = menuItemStatusBar.Checked = !statusStrip1.Visible;
        }


        #endregion

        #region Style

        private void styleToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            lockLayoutToolStripMenuItem.Checked = !this.dockPanel.AllowEndUserDocking;
            showDocumentIconToolStripMenuItem.Checked = this.dockPanel.ShowDocumentIcon;

        }

        private void lockLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }

        private void showDocumentIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dockPanel.ShowDocumentIcon = showDocumentIconToolStripMenuItem.Checked = !showDocumentIconToolStripMenuItem.Checked;
        }

        private void SetSchema(object sender, System.EventArgs e)
        {
            CloseAllContents();

            if (sender == schemaVS2005ToolStripMenuItem)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2005);
            else if (sender == schemaVS2003ToolStripMenuItem)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2003);

            schemaVS2005ToolStripMenuItem.Checked = (sender == schemaVS2005ToolStripMenuItem);
            schemaVS2003ToolStripMenuItem.Checked = (sender == schemaVS2003ToolStripMenuItem);
        }

        private void SetDocumentStyle(object sender, System.EventArgs e)
        {
            DocumentStyle oldStyle = dockPanel.DocumentStyle;
            DocumentStyle newStyle;

            if (sender == documentStyleDockingMDIToolStripMenuItem)
                newStyle = DocumentStyle.DockingMdi;
            else if (sender == documentStyleDockingWindowToolStripMenuItem)
                newStyle = DocumentStyle.DockingWindow;
            else if (sender == documentStyleDockingSDIToolStripMenuItem)
                newStyle = DocumentStyle.DockingSdi;
            else
                newStyle = DocumentStyle.SystemMdi;

            if (oldStyle == newStyle)
                return;

            if (oldStyle == DocumentStyle.SystemMdi || newStyle == DocumentStyle.SystemMdi)
                CloseAllDocuments();

            dockPanel.DocumentStyle = newStyle;
            documentStyleDockingMDIToolStripMenuItem.Checked = (newStyle == DocumentStyle.DockingMdi);
            documentStyleDockingWindowToolStripMenuItem.Checked = (newStyle == DocumentStyle.DockingWindow);
            documentStyleDockingSDIToolStripMenuItem.Checked = (newStyle == DocumentStyle.DockingSdi);
            documentStyleSystemMDIToolStripMenuItem.Checked = (newStyle == DocumentStyle.SystemMdi);

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

        #region Window 菜单

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

        #region Help 菜单

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JForm a = new Abount();
            a.MdiParent = this;
            a.Show(this.dockPanel);
        }

        #endregion

        #region 工具栏

        private void toolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (e.ClickedItem == toolBarButtonNew)
            //{
            //    //menuItemNewTestDataConfigFile_Click(null, null);
            //}
            //else if (e.ClickedItem == toolBarButtonOpen)
            //{
            //    //menuItemOpenFile_Click(null, null);
            //}
            //else if (e.ClickedItem == toolBarButtonOutputWindow)
            //{
            //    menuItemOutputWindow_Click(null, null);
            //}
            //else if (e.ClickedItem == toolBarButtonLayoutByCode)
            //{ }
            //else if (e.ClickedItem == toolBarButtonLayoutByXml)
            //{ }
        }

        #endregion

        #region 动态菜单

        string fileNameOfAddin = "Justin.Toolbox.addin";
        string addinFolder = @"";
        protected AddinConfig addinConfig;

        protected void DynamicMenuGenerate()
        {
            GenerateSubMenuForNew();
            GenerateSubMenuForOpen();
            GenerateSubMenuForTools();
        }
        protected void GenerateSubMenuForNew()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.NewItems == null)
                return;
            newToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.NewItems)
            {
                if (item.Type == MenuType.Menu)
                {
                    ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                    tsItem.Click += subItemOfNew_Click;
                    newToolStripMenuItem.DropDownItems.Add(tsItem);
                }
                else
                {
                    ToolStripSeparator splitor = new ToolStripSeparator();
                    newToolStripMenuItem.DropDownItems.Add(splitor);
                }
            }

        }
        protected void GenerateSubMenuForOpen()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.OpenItems == null)
                return;
            openToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.OpenItems)
            {
                if (item.Type == MenuType.Menu)
                {
                    ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                    tsItem.Click += subItemOfOpen_Click;
                    openToolStripMenuItem.DropDownItems.Add(tsItem);
                }
                else
                {
                    ToolStripSeparator splitor = new ToolStripSeparator();
                    openToolStripMenuItem.DropDownItems.Add(splitor);
                }
            }

        }
        protected void GenerateSubMenuForTools()
        {
            if (addinConfig == null && addinConfig.Menu == null && addinConfig.Menu.ToolsItems == null)
                return;
            toolsToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in addinConfig.Menu.ToolsItems)
            {
                //ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                //tsItem.Click += subItemOfNew_Click;
                //toolsToolStripMenuItem.DropDownItems.Add(tsItem);

                if (item.Type == MenuType.Menu)
                {
                    ToolStripMenuItem tsItem = new ToolStripMenuItem(item.Text) { Name = item.Name, Tag = item };
                    tsItem.Click += subItemOfNew_Click;
                    toolsToolStripMenuItem.DropDownItems.Add(tsItem);
                }
                else
                {
                    ToolStripSeparator splitor = new ToolStripSeparator();
                    toolsToolStripMenuItem.DropDownItems.Add(splitor);
                }
            }

        }

        private void ActiveContent(Justin.Core.MenuItem data, string fileName = "")
        {
            string classStr = data.Class;
            string[] classInfo = classStr.Trim().Split(',');
            if (classInfo.Length != 3)
            {
                this.ShowMessage("请检查Class设置");
                return;
            }
            JForm form = CreateJDockForm(classInfo[0], classInfo[2], string.IsNullOrEmpty(fileName) ? null : new object[] { new string[] { fileName } });
            form.Show(dockPanel);
        }

        protected JForm CreateJDockForm(string typeStr, string dllName, params object[] constructArgs)
        {
            Type type;
            if (!string.IsNullOrEmpty(dllName))//单独dll存放在插件文件夹
            {
                Assembly assembly = Assemblies.ContainsKey(dllName) ? Assemblies[dllName] : null; ;
                type = assembly.GetType(typeStr);
            }
            else
            {
                type = Assembly.GetEntryAssembly().GetType(typeStr);       //dll为Exe所在程序集
                if (type == null)
                {
                    type = Assembly.GetExecutingAssembly().GetType(typeStr);      //dll为当前程序集
                }
            }
            if (type == null)
            {
                this.ShowMessage(string.Format("工具{0}找不到入口函数", typeStr));
                return null;
            }
            object obj = null;
            try
            {
                if (constructArgs == null || constructArgs.Length == 0)
                {
                    obj = Activator.CreateInstance(type);
                }
                else
                {
                    obj = Activator.CreateInstance(type, constructArgs);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
            JForm formToShow = (JForm)obj;
            formToShow.MdiParent = this;
            return formToShow;
        }
        #endregion

        #region 通知区域

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
                this.Show();
            else
                this.Hide();
        }


        #endregion

        private void WorkspaceBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                //e.Cancel = true;
                //this.Hide();
                if (m_bSaveLayout && !specialFile)
                {
                    string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
                    dockPanel.SaveAsXml(configFile);
                }
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

        private string GetOpenFileDialogFilter(string extensionsString)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(extensionsString))
            {
                string[] extensions = extensionsString.Split(',');
                if (extensions != null)
                {
                    foreach (var item in extensions)
                    {
                        sb.AppendFormat(Constants.OpenFileDialogFilterFormart, item.TrimStart('.'), item.TrimStart('.'));
                    }
                }
            }
            sb.AppendFormat(Constants.OpenFileDialogFilterFormart, "*", "所有");
            return sb.ToString().TrimEnd('|');
        }

        public static Dictionary<string, Assembly> Assemblies = new Dictionary<string, Assembly>();

        private void Workbench_Load(object sender, EventArgs e)
        {
            if (File.Exists(fileNameOfAddin))
            {
                try
                {
                    addinConfig = SerializeHelper.XmlDeserializeFromFile<AddinConfig>(fileNameOfAddin);

                    if (addinConfig != null && addinConfig.Tools != null)
                    {
                        foreach (var item in addinConfig.Tools)
                        {
                            if (!Assemblies.ContainsKey(item.DllFileName))
                            {
                                Assembly assembly = Assembly.LoadFrom(Path.Combine(Application.StartupPath, addinFolder + item.DllFileName+".dll"));
                                Assemblies.Add(item.DllFileName, assembly);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ex);
                }
            }
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


    }
}
