namespace Justin.Controls.CodeSnippet
{
    partial class CodeSnippetCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeSnippetCtrl));
            this.layoutLeft = new System.Windows.Forms.TableLayoutPanel();
            this.tvDirectory = new System.Windows.Forms.TreeView();
            this.contextMenuStripOfTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListOfDirectory = new System.Windows.Forms.ImageList(this.components);
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.layoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.editorContainer = new System.Windows.Forms.Panel();
            this.layoutTopOfRight = new System.Windows.Forms.TableLayoutPanel();
            this.txtFileName = new System.Windows.Forms.RichTextBox();
            this.comboBoxHighlightingStrategy = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutLeft.SuspendLayout();
            this.contextMenuStripOfTree.SuspendLayout();
            this.layoutRight.SuspendLayout();
            this.layoutTopOfRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutLeft
            // 
            this.layoutLeft.ColumnCount = 2;
            this.layoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutLeft.Controls.Add(this.tvDirectory, 0, 1);
            this.layoutLeft.Controls.Add(this.txtFolder, 0, 0);
            this.layoutLeft.Controls.Add(this.btnSearch, 1, 0);
            this.layoutLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutLeft.Location = new System.Drawing.Point(0, 0);
            this.layoutLeft.Name = "layoutLeft";
            this.layoutLeft.RowCount = 2;
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeft.Size = new System.Drawing.Size(169, 317);
            this.layoutLeft.TabIndex = 0;
            // 
            // tvDirectory
            // 
            this.layoutLeft.SetColumnSpan(this.tvDirectory, 2);
            this.tvDirectory.ContextMenuStrip = this.contextMenuStripOfTree;
            this.tvDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDirectory.ImageIndex = 0;
            this.tvDirectory.ImageList = this.imageListOfDirectory;
            this.tvDirectory.Location = new System.Drawing.Point(3, 28);
            this.tvDirectory.Name = "tvDirectory";
            this.tvDirectory.SelectedImageIndex = 0;
            this.tvDirectory.Size = new System.Drawing.Size(163, 286);
            this.tvDirectory.TabIndex = 2;
            this.tvDirectory.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvDirectory_AfterExpand);
            this.tvDirectory.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDirectory_NodeMouseClick);
            this.tvDirectory.DoubleClick += new System.EventHandler(this.tvDirectory_DoubleClick);
            // 
            // contextMenuStripOfTree
            // 
            this.contextMenuStripOfTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWithToolStripMenuItem,
            this.openAsTextToolStripMenuItem});
            this.contextMenuStripOfTree.Name = "contextMenuStrip1";
            this.contextMenuStripOfTree.Size = new System.Drawing.Size(159, 48);
            // 
            // openWithToolStripMenuItem
            // 
            this.openWithToolStripMenuItem.Name = "openWithToolStripMenuItem";
            this.openWithToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openWithToolStripMenuItem.Text = "打开方式";
            this.openWithToolStripMenuItem.Click += new System.EventHandler(this.openWithToolStripMenuItem_Click);
            // 
            // openAsTextToolStripMenuItem
            // 
            this.openAsTextToolStripMenuItem.Name = "openAsTextToolStripMenuItem";
            this.openAsTextToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openAsTextToolStripMenuItem.Text = "以为本方式打开";
            this.openAsTextToolStripMenuItem.Click += new System.EventHandler(this.openAsTextToolStripMenuItem_Click);
            // 
            // imageListOfDirectory
            // 
            this.imageListOfDirectory.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOfDirectory.ImageStream")));
            this.imageListOfDirectory.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOfDirectory.Images.SetKeyName(0, "ClosedFolder.ICO");
            // 
            // txtFolder
            // 
            this.txtFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFolder.Location = new System.Drawing.Point(3, 3);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(138, 20);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::Justin.Controls.CodeSnippet.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(147, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(19, 19);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // layoutRight
            // 
            this.layoutRight.ColumnCount = 1;
            this.layoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 636F));
            this.layoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutRight.Controls.Add(this.editorContainer, 0, 1);
            this.layoutRight.Controls.Add(this.layoutTopOfRight, 0, 0);
            this.layoutRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRight.Location = new System.Drawing.Point(0, 0);
            this.layoutRight.Name = "layoutRight";
            this.layoutRight.RowCount = 2;
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRight.Size = new System.Drawing.Size(636, 317);
            this.layoutRight.TabIndex = 0;
            // 
            // editorContainer
            // 
            this.editorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorContainer.Location = new System.Drawing.Point(3, 33);
            this.editorContainer.Name = "editorContainer";
            this.editorContainer.Size = new System.Drawing.Size(630, 281);
            this.editorContainer.TabIndex = 3;
            // 
            // layoutTopOfRight
            // 
            this.layoutTopOfRight.ColumnCount = 2;
            this.layoutTopOfRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTopOfRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layoutTopOfRight.Controls.Add(this.txtFileName, 0, 0);
            this.layoutTopOfRight.Controls.Add(this.comboBoxHighlightingStrategy, 1, 0);
            this.layoutTopOfRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTopOfRight.Location = new System.Drawing.Point(3, 3);
            this.layoutTopOfRight.Name = "layoutTopOfRight";
            this.layoutTopOfRight.RowCount = 1;
            this.layoutTopOfRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTopOfRight.Size = new System.Drawing.Size(630, 24);
            this.layoutTopOfRight.TabIndex = 5;
            // 
            // txtFileName
            // 
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Location = new System.Drawing.Point(3, 3);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(544, 18);
            this.txtFileName.TabIndex = 2;
            this.txtFileName.Text = "";
            // 
            // comboBoxHighlightingStrategy
            // 
            this.comboBoxHighlightingStrategy.FormattingEnabled = true;
            this.comboBoxHighlightingStrategy.Items.AddRange(new object[] {
            "XML",
            "C#",
            "Java",
            "C++.NET",
            "BAT",
            "HTML",
            "TeX"});
            this.comboBoxHighlightingStrategy.Location = new System.Drawing.Point(553, 3);
            this.comboBoxHighlightingStrategy.Name = "comboBoxHighlightingStrategy";
            this.comboBoxHighlightingStrategy.Size = new System.Drawing.Size(74, 21);
            this.comboBoxHighlightingStrategy.TabIndex = 3;
            this.comboBoxHighlightingStrategy.SelectedIndexChanged += new System.EventHandler(this.comboBoxHighlightingStrategy_SelectedIndexChanged);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.layoutLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.layoutRight);
            this.splitContainerMain.Size = new System.Drawing.Size(809, 317);
            this.splitContainerMain.SplitterDistance = 169;
            this.splitContainerMain.TabIndex = 1;
            // 
            // CodeSnippetCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "CodeSnippetCtrl";
            this.Size = new System.Drawing.Size(809, 317);
            this.Load += new System.EventHandler(this.CodeViewCtrl_Load);
            this.layoutLeft.ResumeLayout(false);
            this.layoutLeft.PerformLayout();
            this.contextMenuStripOfTree.ResumeLayout(false);
            this.layoutRight.ResumeLayout(false);
            this.layoutTopOfRight.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDirectory;
        private System.Windows.Forms.TableLayoutPanel layoutRight;
        private System.Windows.Forms.Panel editorContainer;
        private System.Windows.Forms.RichTextBox txtFileName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOfTree;
        private System.Windows.Forms.ToolStripMenuItem openWithToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel layoutLeft;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ImageList imageListOfDirectory;
        private System.Windows.Forms.TableLayoutPanel layoutTopOfRight;
        private System.Windows.Forms.ComboBox comboBoxHighlightingStrategy;
        private System.Windows.Forms.ToolStripMenuItem openAsTextToolStripMenuItem;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;
    }
}
