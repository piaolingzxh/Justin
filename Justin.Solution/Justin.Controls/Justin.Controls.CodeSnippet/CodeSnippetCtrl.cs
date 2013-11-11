using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AxDSOFramer;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.WinForm.Helper;
using Microsoft.Win32;
namespace Justin.Controls.CodeSnippet
{
    public partial class CodeSnippetCtrl : UserControl
    {
        public CodeSnippetCtrl()
        {
            InitializeComponent();
        }
        private TextEditorControl txtCode = new TextEditorControl();
        private AxFramerControl axFramerControl1 = new AxFramerControl();
        private WebBrowser webBrower = new WebBrowser();

        ComponentResourceManager resources = new ComponentResourceManager(typeof(CodeSnippetCtrl));

        public static string CodeSnippetFileDirectory = @"d:\";

        private string folderKeyOfImageList = "folder";

        private void CodeViewCtrl_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodeSnippetFileDirectory))
                return;
            txtFolder.Text = CodeSnippetFileDirectory;
            InitTree();
        }
        private void InitTree()
        {
            DirectoryInfo dir = new DirectoryInfo(CodeSnippetFileDirectory);
            if (!dir.Exists) return;

            if (!imageListOfDirectory.Images.ContainsKey(folderKeyOfImageList))
            {
                imageListOfDirectory.Images.Add(folderKeyOfImageList, FileHelper.GetDirectoryIcon());
            }

            TreeNode rootNode = new TreeNode("我的代码段") { Name = CodeSnippetFileDirectory, Tag = CodeSnippetFileDirectory, ImageKey = folderKeyOfImageList, SelectedImageKey = folderKeyOfImageList };
            tvDirectory.Nodes.Clear();
            tvDirectory.Nodes.Add(rootNode);
            BindTreeNode(rootNode, dir, false);
            rootNode.Expand();
            CreateSharpEditor();
            CreateWebBrower();
            CreateDSOFramer();
        }
        private void BindTree(TreeNode node, DirectoryInfo dir, int depth)
        {
            if (depth > 0)
            {
                foreach (var subdir in dir.GetDirectories().Where(row => row.Name != "System Volume Information").OrderBy(row => row.FullName))
                {
                    TreeNode childNode = new TreeNode(subdir.Name) { Name = subdir.FullName, Tag = subdir.FullName, ImageIndex = 0, SelectedImageIndex = 0 };
                    node.Nodes.Add(childNode);
                    BindTree(childNode, subdir, depth - 1);
                }
                foreach (FileInfo file in dir.GetFiles().OrderBy(row => row.Extension).ThenBy(row => row.FullName))
                {
                    string strExt = Path.GetExtension(file.FullName);
                    if (!imageListOfDirectory.Images.ContainsKey(strExt))
                    {
                        imageListOfDirectory.Images.Add(strExt, FileHelper.GetFileIcon(file.FullName));
                    }
                    node.Nodes.Add(new TreeNode(file.Name) { Name = file.FullName, Tag = file.FullName, ImageKey = strExt, SelectedImageKey = strExt });
                }
            }
        }

        private void tvDirectory_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectNode = tvDirectory.SelectedNode;
            if (selectNode != null && selectNode.Tag != null)
            {
                if (selectNode.ImageKey != folderKeyOfImageList)
                {
                    #region 打开文件

                    string dsoFramerExtensionString = ".doc,.docx,.rtf,.ppt,.odt,.docm,.dotx,.dotm,.dot,.xls,";
                    string generalExtensionString = ".txt,.cs,.vb,.java,.cpp,.il,.xml,.config,";
                    string webBrowerExtensionString = ".htm,.html,.mht,.pdf,";

                    string[] generalExtensions = generalExtensionString.Split(',');
                    string[] dsoFramerExtensions = dsoFramerExtensionString.Split(',');
                    string[] webBrowerExtensions = webBrowerExtensionString.Split(',');
                    string[] extensions = (dsoFramerExtensionString + generalExtensionString + webBrowerExtensionString).TrimEnd(',').Split(',');
                    string path = selectNode.Tag.ToString();
                    try
                    {

                        if (extensions.Contains(Path.GetExtension(path)))
                        {
                            if (dsoFramerExtensions.Contains(Path.GetExtension(path)))
                            {
                                this.editorContainer.Controls.Clear();
                                //dsoFramer必须先把控件Add上，再打开文件
                                this.editorContainer.Controls.Add(this.axFramerControl1);
                                this.axFramerControl1.Open(path);
                                //this.axFramerControl1.Focus();
                            }
                            else if (webBrowerExtensions.Contains(Path.GetExtension(path)))
                            {
                                this.editorContainer.Controls.Clear();
                                this.webBrower.Navigate(path);
                                this.editorContainer.Controls.Add(this.webBrower);
                                this.webBrower.Focus();
                            }
                            else
                            {
                                string content = File.ReadAllText(path);
                                this.editorContainer.Controls.Clear();
                                txtCode.LoadFile(path);
                                this.editorContainer.Controls.Add(this.txtCode);
                                txtCode.Focus();
                            }
                            txtFileName.Text = path.ToString();
                        }
                        else
                        {
                            OpenFileBySystem(path);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessage(ex);
                        OpenFileBySystem(path);
                    }
                    #endregion
                }
            }
        }

        private void OpenFileBySystem(string path)
        {
            try
            {
                Process pc = Process.Start(path);
                if (pc != null)
                {
                    pc.CloseMainWindow();
                }
            }
            catch
            {
                ShowOpenFileWithDialog(path);
            }
        }

        private void CreateSharpEditor()
        {
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.IsReadOnly = false;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.ShowEOLMarkers = true;
            this.txtCode.ShowSpaces = true;
            this.txtCode.ShowTabs = true;
            this.txtCode.Size = new System.Drawing.Size(621, 431);
            this.txtCode.TabIndex = 22;
            this.txtCode.Text = resources.GetString("txtCode.Text");

            txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtCode.Encoding = Encoding.Default;
        }
        private void CreateDSOFramer()
        {
            ((System.ComponentModel.ISupportInitialize)(this.axFramerControl1)).BeginInit();
            this.axFramerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axFramerControl1.Enabled = true;
            this.axFramerControl1.Location = new System.Drawing.Point(0, 0);
            this.axFramerControl1.Name = "axFramerControl1";
            this.axFramerControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axFramerControl1.OcxState")));
            this.axFramerControl1.Size = new System.Drawing.Size(75, 23);
            this.axFramerControl1.TabIndex = 0;
            ((System.ComponentModel.ISupportInitialize)(this.axFramerControl1)).EndInit();

        }
        private void CreateWebBrower()
        {
            this.webBrower.Dock = DockStyle.Fill;
            this.webBrower.ScriptErrorsSuppressed = true;
        }

        private void openWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvDirectory.SelectedNode.Tag == null) return;
            ShowOpenFileWithDialog(tvDirectory.SelectedNode.Tag.ToString());
        }

        private void tvDirectory_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvDirectory.SelectedNode = e.Node;
        }
        private void ShowOpenFileWithDialog(string filename)
        {
            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "rundll32.exe";
            proc.StartInfo.Arguments = string.Format("shell32,OpenAs_RunDLL {0}", filename);
            proc.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                txtFolder.Text = CodeSnippetFileDirectory = folderBrowserDialog1.SelectedPath;
                InitTree();
            }
        }

        private void tvDirectory_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode childNode in e.Node.Nodes)
            {
                if (childNode.ImageKey == folderKeyOfImageList)
                {
                    string childFolder = childNode.Tag.ToString();
                    if (childNode.Nodes.Count > 0)
                    {
                        continue;
                    }

                    BindTreeNode(childNode, new DirectoryInfo(childFolder), false);
                }
            }
        }

        private void BindTreeNode(TreeNode node, DirectoryInfo crtDir, bool recursion)
        {
            //加载该目录下的所有文件夹
            foreach (DirectoryInfo childDir in crtDir.GetDirectories().Where(row => row.Name != "System Volume Information").OrderBy(row => row.FullName))
            {

                if (childDir.Attributes == FileAttributes.Hidden)
                {
                    continue;
                }
                TreeNode childNode = new TreeNode(childDir.Name);
                childNode.Tag = childDir.FullName;
                childNode.Name = childDir.FullName;

                if (!imageListOfDirectory.Images.ContainsKey(folderKeyOfImageList))
                {
                    imageListOfDirectory.Images.Add(folderKeyOfImageList, FileHelper.GetDirectoryIcon());
                }
                childNode.ImageKey = childNode.SelectedImageKey = folderKeyOfImageList;

                if (recursion)
                {
                    BindTreeNode(childNode, childDir, recursion);
                }
                node.Nodes.Add(childNode);
            }
            //加载该目录下的所有文件
            foreach (FileInfo file in crtDir.GetFiles().OrderBy(row => row.Extension).ThenBy(row => row.FullName))
            {
                string strExt = Path.GetExtension(file.FullName);
                if (!imageListOfDirectory.Images.ContainsKey(strExt))
                {
                    imageListOfDirectory.Images.Add(strExt, FileHelper.GetFileIcon(file.FullName));
                }
                node.Nodes.Add(new TreeNode(file.Name) { Name = file.FullName, Tag = file.FullName, ImageKey = strExt, SelectedImageKey = strExt });
            }
        }

        private void comboBoxHighlightingStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(comboBoxHighlightingStrategy.Text);
        }

        private void openAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectNode = tvDirectory.SelectedNode;
                if (selectNode != null && selectNode.Tag != null)
                {
                    if (selectNode.ImageKey != folderKeyOfImageList)
                    {
                        string path = selectNode.Tag.ToString();
                        this.editorContainer.Controls.Clear();
                        txtCode.LoadFile(path);
                        this.editorContainer.Controls.Add(this.txtCode);
                        txtCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
