using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using Justin.Stock.Service.Models;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace Justin.Justin_Stock_VsAddin
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    // This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(JStockWindow))]
    [Guid(GuidList.guidJustin_Stock_VsAddinPkgString)]
    public sealed class Justin_Stock_VsAddinPackage : Package
    {
        DTE2 dte;
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public Justin_Stock_VsAddinPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            JStockWindow.Display += Display;
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.FindToolWindow(typeof(JStockWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidJustin_Stock_VsAddinCmdSet, (int)PkgCmdIDList.cmdidShowHideWarn);
                MenuCommand menuItem = new MenuCommand(slnExplUIHierarchyExample, menuCommandID);
                mcs.AddCommand(menuItem);
                // Create the command for the tool window
                CommandID toolwndCommandID = new CommandID(GuidList.guidJustin_Stock_VsAddinCmdSet, (int)PkgCmdIDList.cmdidShowStockWindow);
                MenuCommand menuToolWin = new MenuCommand(ShowHideWindowMenuItemCallback, toolwndCommandID);
                mcs.AddCommand(menuToolWin);

                CommandID showHideWindowCmdId = new CommandID(GuidList.guidJustin_Stock_VsAddinCmdSet, (int)PkgCmdIDList.cmdidShowHideStockWindow);
                MenuCommand showHideWindowMenuItem = new MenuCommand(ShowHideWindowMenuItemCallback, showHideWindowCmdId);
                mcs.AddCommand(showHideWindowMenuItem);
            }

            dte = GetService(typeof(DTE)) as DTE2;
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            Justin.Stock.Controls.Entities.Constants.Setting.ShowWarn = !Justin.Stock.Controls.Entities.Constants.Setting.ShowWarn;
        }
        private static bool isFirst = true;
        private void ShowHideWindowMenuItemCallback(object sender, EventArgs e)
        {
            ToolWindowPane window = this.FindToolWindow(typeof(JStockWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            if (windowFrame.IsVisible() == VSConstants.S_OK)
            {
                if (isFirst)
                {
                    StockService.Start();
                    isFirst = false;
                }
                else
                {
                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Hide());
                    StockService.Stop();
                }
            }
            else
            {
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
                StockService.ReStart();
            }
        }

        void Display(bool show, bool force)
        {

            ToolWindowPane window = this.FindToolWindow(typeof(JStockWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;

            if (force)
            {
                if (show)
                {
                    windowFrame.Show();
                }
                else
                {
                    windowFrame.Hide();
                }
            }
            else
            {
                if (windowFrame.IsVisible() == VSConstants.S_OK)
                {
                    windowFrame.Hide();
                }
                else
                {
                    windowFrame.Show();
                }

            }
        }

        public void slnExplUIHierarchyExample(object sender, EventArgs e)
        {
            //UIHierarchyItem UIH = dte.ToolWindows.SolutionExplorer.UIHierarchyItems.Item(1);
            //string fileName = dte.ActiveDocument.FullName;
            //bool found = false;
            //EnumUIHierarchyItems(UIH.UIHierarchyItems
            //    , (item) =>
            //    {
            //        if (item.Object is ProjectItem)
            //        {
            //            ProjectItem pItem = item.Object as ProjectItem;
            //            if (pItem.Properties != null)
            //            {
            //                object FullPath = pItem.Properties.Item("FullPath").Value;
            //                if (FullPath != null && FullPath.ToString().Equals(fileName))
            //                {

            //                    WriteOutput(string.Format("FullName:{0},Kind:{1},{2}", pItem.Document.FullName, pItem.Kind, Environment.NewLine));
            //                    return true;
            //                }
            //            }
            //        }
            //        return false;
            //    }
            //, ref found);
            WriteOutput("Location Ready");
            dte.ExecuteCommand("View.TrackActivityinSolutionExplorer", "true");
            dte.ExecuteCommand("View.TrackActivityinSolutionExplorer", "false");

            WriteOutput("Location Success");

        }


        private OutputWindowPane _outputWindowPane;
        internal void WriteOutput(string msg, bool clear = false, bool hide = false, string paneName = "Studio")
        {
            if (_outputWindowPane == null)
            {
                _outputWindowPane = GetOutputWindow(hide, paneName);
            }
            if (clear)
                _outputWindowPane.Clear();

            _outputWindowPane.OutputString(msg);
        }

        internal OutputWindowPane GetOutputWindow(bool hide = true, string paneName = "Studio")
        {
            EnvDTE.Window win = dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            //if (autoHide)
            //{
            //    //设置窗体为停靠状态
            //    //try
            //    //{
            //    //    win.LinkedWindowFrame.IsFloating = false;
            //    //}
            //    //catch { }
            //    win.IsFloating = false;
            //    //设置窗体为自动隐藏,只有窗体为停靠状态，才可以设置自动隐藏属性
            //    win.AutoHides = true;
            //}
            win.Visible = !hide;

            OutputWindow outWin = (OutputWindow)win.Object;
            OutputWindowPane pane;
            try
            {
                if (ContainsPane(outWin.OutputWindowPanes, paneName))
                    pane = outWin.OutputWindowPanes.Item(paneName);
                else
                    pane = outWin.OutputWindowPanes.Add(paneName);
            }
            catch (Exception)
            {
                pane = outWin.OutputWindowPanes.Add(paneName);
            }
            if (!hide)
                pane.Activate();
            return pane;
        }
        private bool ContainsPane(OutputWindowPanes outputWindowPanes, string paneName)
        {
            IEnumerator enumerator = outputWindowPanes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (string.Compare(((OutputWindowPane)enumerator.Current).Name, paneName, true) == 0)
                    return true;
            }

            return false;
        }


        private void EnumUIHierarchyItems(UIHierarchyItems items, Func<UIHierarchyItem, bool> func, ref bool skip)
        {
            if (skip) return;

            if (items != null && items.Count > 0)
            {

                foreach (UIHierarchyItem item in items)
                {
                    bool result = func(item);
                    if (result)
                    {
                        skip = true;
                        break;
                    }
                    else
                    {
                        EnumUIHierarchyItems(item.UIHierarchyItems, func, ref skip);
                    }

                }
            }
            return;
        }
    }
}
