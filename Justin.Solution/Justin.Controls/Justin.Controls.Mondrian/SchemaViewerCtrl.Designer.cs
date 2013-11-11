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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnBrowerFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.RichTextBox();
            this.treeViewSchema = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.priviewSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListOfTree = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lbPropertyGridSource = new System.Windows.Forms.TextBox();
            this.btnSerch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtLookFor = new System.Windows.Forms.RichTextBox();
            this.txtDstFileName = new System.Windows.Forms.RichTextBox();
            this.txtSchemaXML = new ICSharpCode.TextEditor.TextEditorControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.splitContainerEx1 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerEx2 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).BeginInit();
            this.splitContainerEx2.Panel1.SuspendLayout();
            this.splitContainerEx2.Panel2.SuspendLayout();
            this.splitContainerEx2.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.Controls.Add(this.btnConnect, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnBrowerFile, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtFileName, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(326, 29);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(249, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(74, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnBrowerFile
            // 
            this.btnBrowerFile.Location = new System.Drawing.Point(169, 3);
            this.btnBrowerFile.Name = "btnBrowerFile";
            this.btnBrowerFile.Size = new System.Drawing.Size(74, 23);
            this.btnBrowerFile.TabIndex = 2;
            this.btnBrowerFile.Text = "Brower";
            this.btnBrowerFile.UseVisualStyleBackColor = true;
            this.btnBrowerFile.Click += new System.EventHandler(this.btnBrowerFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Location = new System.Drawing.Point(3, 3);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(160, 23);
            this.txtFileName.TabIndex = 1;
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
            this.treeViewSchema.Size = new System.Drawing.Size(163, 516);
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
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.propertyGrid, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbPropertyGridSource, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(159, 516);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(3, 27);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(153, 486);
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
            // btnSerch
            // 
            this.btnSerch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSerch.Location = new System.Drawing.Point(142, 3);
            this.btnSerch.Name = "btnSerch";
            this.btnSerch.Size = new System.Drawing.Size(74, 23);
            this.btnSerch.TabIndex = 7;
            this.btnSerch.Text = "Search";
            this.btnSerch.UseVisualStyleBackColor = true;
            this.btnSerch.Click += new System.EventHandler(this.btnSerch_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(361, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtLookFor
            // 
            this.txtLookFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLookFor.Location = new System.Drawing.Point(3, 3);
            this.txtLookFor.Name = "txtLookFor";
            this.txtLookFor.Size = new System.Drawing.Size(133, 23);
            this.txtLookFor.TabIndex = 6;
            this.txtLookFor.Text = "";
            // 
            // txtDstFileName
            // 
            this.txtDstFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDstFileName.Location = new System.Drawing.Point(222, 3);
            this.txtDstFileName.Name = "txtDstFileName";
            this.txtDstFileName.Size = new System.Drawing.Size(133, 23);
            this.txtDstFileName.TabIndex = 8;
            this.txtDstFileName.Text = "";
            // 
            // txtSchemaXML
            // 
            this.txtSchemaXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchemaXML.Location = new System.Drawing.Point(3, 38);
            this.txtSchemaXML.Name = "txtSchemaXML";
            this.txtSchemaXML.ShowEOLMarkers = true;
            this.txtSchemaXML.ShowSpaces = true;
            this.txtSchemaXML.ShowTabs = true;
            this.txtSchemaXML.ShowVRuler = true;
            this.txtSchemaXML.Size = new System.Drawing.Size(439, 516);
            this.txtSchemaXML.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(233, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 20);
            this.textBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(153, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSerch_Click);
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(103, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(44, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(283, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 29);
            this.button2.TabIndex = 0;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 364);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 29);
            this.button3.TabIndex = 1;
            this.button3.Text = "Priview Schema";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnPriview_Click);
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.Name = "splitContainerEx1";
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.tableLayoutPanel7);
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.tableLayoutPanel8);
            this.splitContainerEx1.Size = new System.Drawing.Size(781, 557);
            this.splitContainerEx1.SplitterDistance = 332;
            this.splitContainerEx1.TabIndex = 7;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.splitContainerEx2, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(332, 557);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // splitContainerEx2
            // 
            this.splitContainerEx2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx2.Location = new System.Drawing.Point(3, 38);
            this.splitContainerEx2.Name = "splitContainerEx2";
            // 
            // splitContainerEx2.Panel1
            // 
            this.splitContainerEx2.Panel1.Controls.Add(this.treeViewSchema);
            // 
            // splitContainerEx2.Panel2
            // 
            this.splitContainerEx2.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainerEx2.Size = new System.Drawing.Size(326, 516);
            this.splitContainerEx2.SplitterDistance = 163;
            this.splitContainerEx2.TabIndex = 5;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtSchemaXML, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(445, 557);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSerch, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDstFileName, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtLookFor, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 29);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // SchemaViewerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "SchemaViewerCtrl";
            this.Size = new System.Drawing.Size(781, 557);
            this.Load += new System.EventHandler(this.SchemaViewerCtrl_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.splitContainerEx2.Panel1.ResumeLayout(false);
            this.splitContainerEx2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).EndInit();
            this.splitContainerEx2.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnBrowerFile;
        private System.Windows.Forms.TreeView treeViewSchema;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.TextBox lbPropertyGridSource;
        private System.Windows.Forms.Button btnSerch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ImageList imageListOfTree;
        private System.Windows.Forms.RichTextBox txtLookFor;
        private System.Windows.Forms.RichTextBox txtDstFileName;
        private System.Windows.Forms.RichTextBox txtFileName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem priviewSchemaToolStripMenuItem;
        private ICSharpCode.TextEditor.TextEditorControl txtSchemaXML;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

    }
}
