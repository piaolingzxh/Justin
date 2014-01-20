namespace Justin.Controls.Mondrian
{
    partial class SchemaViewerCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaViewerCtrl));
            this.layoutLeftTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnBrowerFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.RichTextBox();
            this.treeViewSchema = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.priviewSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListOfTree = new System.Windows.Forms.ImageList(this.components);
            this.layoutProperty = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lbPropertyGridSource = new System.Windows.Forms.TextBox();
            this.txtLookFor = new System.Windows.Forms.RichTextBox();
            this.txtDstFileName = new System.Windows.Forms.RichTextBox();
            this.txtSchemaXML = new ICSharpCode.TextEditor.TextEditorControl();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutLeft = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerTreeAndProperty = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutXML = new System.Windows.Forms.TableLayoutPanel();
            this.layoutTopOfXML = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSerch = new System.Windows.Forms.Button();
            this.layoutLeftTop.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.layoutProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.layoutLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndProperty)).BeginInit();
            this.splitContainerTreeAndProperty.Panel1.SuspendLayout();
            this.splitContainerTreeAndProperty.Panel2.SuspendLayout();
            this.splitContainerTreeAndProperty.SuspendLayout();
            this.layoutXML.SuspendLayout();
            this.layoutTopOfXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutLeftTop
            // 
            this.layoutLeftTop.ColumnCount = 3;
            this.layoutLeftTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeftTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutLeftTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutLeftTop.Controls.Add(this.btnConnect, 2, 0);
            this.layoutLeftTop.Controls.Add(this.btnBrowerFile, 1, 0);
            this.layoutLeftTop.Controls.Add(this.txtFileName, 0, 0);
            this.layoutLeftTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutLeftTop.Location = new System.Drawing.Point(3, 3);
            this.layoutLeftTop.Name = "layoutLeftTop";
            this.layoutLeftTop.RowCount = 1;
            this.layoutLeftTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeftTop.Size = new System.Drawing.Size(325, 29);
            this.layoutLeftTop.TabIndex = 4;
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::Justin.Controls.Mondrian.Properties.Resources.conn;
            this.btnConnect.Location = new System.Drawing.Point(278, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(44, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Tag = "重新加载文件内容";
            this.btnConnect.Text = " ";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnBrowerFile
            // 
            this.btnBrowerFile.Image = global::Justin.Controls.Mondrian.Properties.Resources.Group;
            this.btnBrowerFile.Location = new System.Drawing.Point(228, 3);
            this.btnBrowerFile.Name = "btnBrowerFile";
            this.btnBrowerFile.Size = new System.Drawing.Size(44, 23);
            this.btnBrowerFile.TabIndex = 2;
            this.btnBrowerFile.Tag = "打开文件，预览内容";
            this.btnBrowerFile.Text = " ";
            this.btnBrowerFile.UseVisualStyleBackColor = true;
            this.btnBrowerFile.Click += new System.EventHandler(this.btnBrowerFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Location = new System.Drawing.Point(3, 3);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(219, 23);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Tag = "Schema 路径";
            this.txtFileName.Text = "";
            // 
            // treeViewSchema
            // 
            this.treeViewSchema.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSchema.ImageIndex = 0;
            this.treeViewSchema.ImageList = this.imageListOfTree;
            this.treeViewSchema.Location = new System.Drawing.Point(0, 0);
            this.treeViewSchema.Name = "treeViewSchema";
            this.treeViewSchema.SelectedImageIndex = 0;
            this.treeViewSchema.Size = new System.Drawing.Size(162, 252);
            this.treeViewSchema.TabIndex = 4;
            this.treeViewSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSchema_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priviewSchemaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 26);
            // 
            // priviewSchemaToolStripMenuItem
            // 
            this.priviewSchemaToolStripMenuItem.Name = "priviewSchemaToolStripMenuItem";
            this.priviewSchemaToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.priviewSchemaToolStripMenuItem.Text = "Priview Schema";
            this.priviewSchemaToolStripMenuItem.Click += new System.EventHandler(this.priviewSchemaToolStripMenuItem_Click);
            // 
            // imageListOfTree
            // 
            this.imageListOfTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOfTree.ImageStream")));
            this.imageListOfTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOfTree.Images.SetKeyName(0, "Cube.png");
            this.imageListOfTree.Images.SetKeyName(1, "Dim.png");
            this.imageListOfTree.Images.SetKeyName(2, "Measure.png");
            this.imageListOfTree.Images.SetKeyName(3, "CalMeasure.png");
            this.imageListOfTree.Images.SetKeyName(4, "hie.png");
            this.imageListOfTree.Images.SetKeyName(5, "level.png");
            this.imageListOfTree.Images.SetKeyName(6, "property.png");
            this.imageListOfTree.Images.SetKeyName(7, "dimUse.png");
            this.imageListOfTree.Images.SetKeyName(8, "namedset.png");
            this.imageListOfTree.Images.SetKeyName(9, "script.png");
            this.imageListOfTree.Images.SetKeyName(10, "role.png");
            // 
            // layoutProperty
            // 
            this.layoutProperty.ColumnCount = 1;
            this.layoutProperty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutProperty.Controls.Add(this.propertyGrid, 0, 1);
            this.layoutProperty.Controls.Add(this.lbPropertyGridSource, 0, 0);
            this.layoutProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutProperty.Location = new System.Drawing.Point(0, 0);
            this.layoutProperty.Name = "layoutProperty";
            this.layoutProperty.RowCount = 2;
            this.layoutProperty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutProperty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutProperty.Size = new System.Drawing.Size(159, 252);
            this.layoutProperty.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(3, 27);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(153, 222);
            this.propertyGrid.TabIndex = 5;
            // 
            // lbPropertyGridSource
            // 
            this.lbPropertyGridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPropertyGridSource.Location = new System.Drawing.Point(3, 3);
            this.lbPropertyGridSource.Name = "lbPropertyGridSource";
            this.lbPropertyGridSource.ReadOnly = true;
            this.lbPropertyGridSource.Size = new System.Drawing.Size(153, 20);
            this.lbPropertyGridSource.TabIndex = 3;
            // 
            // txtLookFor
            // 
            this.txtLookFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLookFor.Location = new System.Drawing.Point(3, 3);
            this.txtLookFor.Name = "txtLookFor";
            this.txtLookFor.Size = new System.Drawing.Size(164, 23);
            this.txtLookFor.TabIndex = 6;
            this.txtLookFor.Tag = "查询的字符串";
            this.txtLookFor.Text = "";
            // 
            // txtDstFileName
            // 
            this.txtDstFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDstFileName.Location = new System.Drawing.Point(223, 3);
            this.txtDstFileName.Name = "txtDstFileName";
            this.txtDstFileName.Size = new System.Drawing.Size(164, 23);
            this.txtDstFileName.TabIndex = 8;
            this.txtDstFileName.Tag = "将要保存的文件名";
            this.txtDstFileName.Text = "";
            // 
            // txtSchemaXML
            // 
            this.txtSchemaXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchemaXML.IsReadOnly = false;
            this.txtSchemaXML.Location = new System.Drawing.Point(3, 38);
            this.txtSchemaXML.Name = "txtSchemaXML";
            this.txtSchemaXML.ShowEOLMarkers = true;
            this.txtSchemaXML.ShowSpaces = true;
            this.txtSchemaXML.ShowTabs = true;
            this.txtSchemaXML.Size = new System.Drawing.Size(440, 252);
            this.txtSchemaXML.TabIndex = 0;
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
            this.splitContainerMain.Panel2.Controls.Add(this.layoutXML);
            this.splitContainerMain.Size = new System.Drawing.Size(781, 293);
            this.splitContainerMain.SplitterDistance = 331;
            this.splitContainerMain.TabIndex = 7;
            // 
            // layoutLeft
            // 
            this.layoutLeft.ColumnCount = 1;
            this.layoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeft.Controls.Add(this.layoutLeftTop, 0, 0);
            this.layoutLeft.Controls.Add(this.splitContainerTreeAndProperty, 0, 1);
            this.layoutLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutLeft.Location = new System.Drawing.Point(0, 0);
            this.layoutLeft.Name = "layoutLeft";
            this.layoutLeft.RowCount = 2;
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutLeft.Size = new System.Drawing.Size(331, 293);
            this.layoutLeft.TabIndex = 0;
            // 
            // splitContainerTreeAndProperty
            // 
            this.splitContainerTreeAndProperty.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerTreeAndProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTreeAndProperty.Location = new System.Drawing.Point(3, 38);
            this.splitContainerTreeAndProperty.Name = "splitContainerTreeAndProperty";
            // 
            // splitContainerTreeAndProperty.Panel1
            // 
            this.splitContainerTreeAndProperty.Panel1.Controls.Add(this.treeViewSchema);
            // 
            // splitContainerTreeAndProperty.Panel2
            // 
            this.splitContainerTreeAndProperty.Panel2.Controls.Add(this.layoutProperty);
            this.splitContainerTreeAndProperty.Size = new System.Drawing.Size(325, 252);
            this.splitContainerTreeAndProperty.SplitterDistance = 162;
            this.splitContainerTreeAndProperty.TabIndex = 5;
            // 
            // layoutXML
            // 
            this.layoutXML.ColumnCount = 1;
            this.layoutXML.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutXML.Controls.Add(this.layoutTopOfXML, 0, 0);
            this.layoutXML.Controls.Add(this.txtSchemaXML, 0, 1);
            this.layoutXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutXML.Location = new System.Drawing.Point(0, 0);
            this.layoutXML.Name = "layoutXML";
            this.layoutXML.RowCount = 2;
            this.layoutXML.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutXML.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutXML.Size = new System.Drawing.Size(446, 293);
            this.layoutXML.TabIndex = 0;
            // 
            // layoutTopOfXML
            // 
            this.layoutTopOfXML.ColumnCount = 4;
            this.layoutTopOfXML.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTopOfXML.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutTopOfXML.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTopOfXML.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutTopOfXML.Controls.Add(this.btnSave, 3, 0);
            this.layoutTopOfXML.Controls.Add(this.btnSerch, 1, 0);
            this.layoutTopOfXML.Controls.Add(this.txtDstFileName, 2, 0);
            this.layoutTopOfXML.Controls.Add(this.txtLookFor, 0, 0);
            this.layoutTopOfXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTopOfXML.Location = new System.Drawing.Point(3, 3);
            this.layoutTopOfXML.Name = "layoutTopOfXML";
            this.layoutTopOfXML.RowCount = 1;
            this.layoutTopOfXML.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTopOfXML.Size = new System.Drawing.Size(440, 29);
            this.layoutTopOfXML.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Image = global::Justin.Controls.Mondrian.Properties.Resources.save;
            this.btnSave.Location = new System.Drawing.Point(393, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Tag = "保存XML";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSerch
            // 
            this.btnSerch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSerch.Image = global::Justin.Controls.Mondrian.Properties.Resources.search;
            this.btnSerch.Location = new System.Drawing.Point(173, 3);
            this.btnSerch.Name = "btnSerch";
            this.btnSerch.Size = new System.Drawing.Size(44, 23);
            this.btnSerch.TabIndex = 7;
            this.btnSerch.Tag = "从xml查找";
            this.btnSerch.UseVisualStyleBackColor = true;
            this.btnSerch.Click += new System.EventHandler(this.btnSerch_Click);
            // 
            // SchemaViewerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "SchemaViewerCtrl";
            this.Size = new System.Drawing.Size(781, 293);
            this.Load += new System.EventHandler(this.SchemaViewerCtrl_Load);
            this.layoutLeftTop.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.layoutProperty.ResumeLayout(false);
            this.layoutProperty.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.layoutLeft.ResumeLayout(false);
            this.splitContainerTreeAndProperty.Panel1.ResumeLayout(false);
            this.splitContainerTreeAndProperty.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndProperty)).EndInit();
            this.splitContainerTreeAndProperty.ResumeLayout(false);
            this.layoutXML.ResumeLayout(false);
            this.layoutTopOfXML.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutLeftTop;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnBrowerFile;
        private System.Windows.Forms.TreeView treeViewSchema;
        private System.Windows.Forms.TableLayoutPanel layoutProperty;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.TextBox lbPropertyGridSource;
        private System.Windows.Forms.Button btnSerch;
        private System.Windows.Forms.Button btnSave;
        //private System.Windows.Forms.TextBox textBox1;
        //private System.Windows.Forms.Button button1;
        //private System.Windows.Forms.TextBox textBox2;
        //private System.Windows.Forms.Button button2;
        //private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        //private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ImageList imageListOfTree;
        private System.Windows.Forms.RichTextBox txtLookFor;
        private System.Windows.Forms.RichTextBox txtDstFileName;
        private System.Windows.Forms.RichTextBox txtFileName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem priviewSchemaToolStripMenuItem;
        private ICSharpCode.TextEditor.TextEditorControl txtSchemaXML;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel layoutLeft;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerTreeAndProperty;
        private System.Windows.Forms.TableLayoutPanel layoutXML;
        private System.Windows.Forms.TableLayoutPanel layoutTopOfXML;

    }
}
