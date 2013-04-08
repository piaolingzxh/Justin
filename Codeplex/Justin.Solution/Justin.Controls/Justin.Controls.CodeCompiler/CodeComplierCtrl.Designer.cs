namespace Justin.Controls.CodeCompiler
{
    partial class CodeComplierCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeComplierCtrl));
            this.btnCompiler = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnMSIL = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.txtMSILCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSaveCodeToFile = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertCSharpTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertVBTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertJavaTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompiler
            // 
            this.btnCompiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCompiler.Location = new System.Drawing.Point(3, 38);
            this.btnCompiler.Name = "btnCompiler";
            this.btnCompiler.Size = new System.Drawing.Size(68, 29);
            this.btnCompiler.TabIndex = 0;
            this.btnCompiler.Text = "Compiler";
            this.btnCompiler.UseVisualStyleBackColor = true;
            this.btnCompiler.Click += new System.EventHandler(this.btnCompiler_Click);
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRun.Location = new System.Drawing.Point(3, 73);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(68, 29);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnMSIL
            // 
            this.btnMSIL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMSIL.Location = new System.Drawing.Point(3, 3);
            this.btnMSIL.Name = "btnMSIL";
            this.btnMSIL.Size = new System.Drawing.Size(68, 29);
            this.btnMSIL.TabIndex = 2;
            this.btnMSIL.Text = "MSIL";
            this.btnMSIL.UseVisualStyleBackColor = true;
            this.btnMSIL.Click += new System.EventHandler(this.btnShowILCode_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnMSIL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCompiler, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRun, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveCodeToFile, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(609, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(74, 288);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // txtCode
            // 
            this.txtCode.ContextMenuStrip = this.contextMenuStrip1;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.IsReadOnly = false;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.ShowEOLMarkers = true;
            this.txtCode.ShowSpaces = true;
            this.txtCode.ShowTabs = true;
            this.txtCode.Size = new System.Drawing.Size(316, 335);
            this.txtCode.TabIndex = 21;
            this.txtCode.Text = resources.GetString("txtCode.Text");
            // 
            // txtMSILCode
            // 
            this.txtMSILCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMSILCode.IsReadOnly = false;
            this.txtMSILCode.Location = new System.Drawing.Point(0, 0);
            this.txtMSILCode.Name = "txtMSILCode";
            this.txtMSILCode.ShowEOLMarkers = true;
            this.txtMSILCode.ShowSpaces = true;
            this.txtMSILCode.ShowTabs = true;
            this.txtMSILCode.Size = new System.Drawing.Size(280, 335);
            this.txtMSILCode.TabIndex = 23;
            this.txtMSILCode.Text = resources.GetString("txtMSILCode.Text");
            // 
            // splitContainer1
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtCode);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtMSILCode);
            this.splitContainer1.Size = new System.Drawing.Size(600, 335);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 23;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(687, 341);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(701, 373);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanelMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(693, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(693, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSaveCodeToFile
            // 
            this.btnSaveCodeToFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveCodeToFile.Location = new System.Drawing.Point(3, 256);
            this.btnSaveCodeToFile.Name = "btnSaveCodeToFile";
            this.btnSaveCodeToFile.Size = new System.Drawing.Size(68, 29);
            this.btnSaveCodeToFile.TabIndex = 3;
            this.btnSaveCodeToFile.Text = "SaveCode";
            this.btnSaveCodeToFile.UseVisualStyleBackColor = true;
            this.btnSaveCodeToFile.Click += new System.EventHandler(this.btnSaveCodeToFile_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCSharpTemplateToolStripMenuItem,
            this.insertVBTemplateToolStripMenuItem,
            this.insertJavaTemplateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(198, 92);
            // 
            // insertCSharpTemplateToolStripMenuItem
            // 
            this.insertCSharpTemplateToolStripMenuItem.Name = "insertCSharpTemplateToolStripMenuItem";
            this.insertCSharpTemplateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.insertCSharpTemplateToolStripMenuItem.Text = "Insert CSharp Template";
            this.insertCSharpTemplateToolStripMenuItem.Click += new System.EventHandler(this.insertCSharpTemplateToolStripMenuItem_Click);
            // 
            // insertVBTemplateToolStripMenuItem
            // 
            this.insertVBTemplateToolStripMenuItem.Name = "insertVBTemplateToolStripMenuItem";
            this.insertVBTemplateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.insertVBTemplateToolStripMenuItem.Text = "Insert VB Template";
            this.insertVBTemplateToolStripMenuItem.Click += new System.EventHandler(this.insertVBTemplateToolStripMenuItem_Click);
            // 
            // insertJavaTemplateToolStripMenuItem
            // 
            this.insertJavaTemplateToolStripMenuItem.Name = "insertJavaTemplateToolStripMenuItem";
            this.insertJavaTemplateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.insertJavaTemplateToolStripMenuItem.Text = "Insert Java Template";
            this.insertJavaTemplateToolStripMenuItem.Click += new System.EventHandler(this.insertJavaTemplateToolStripMenuItem_Click);
            // 
            // CodeComplierCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "CodeComplierCtrl";
            this.Size = new System.Drawing.Size(701, 373);
            this.Load += new System.EventHandler(this.CodeComplierCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCompiler;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnMSIL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ICSharpCode.TextEditor.TextEditorControl txtCode;
        private ICSharpCode.TextEditor.TextEditorControl txtMSILCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnSaveCodeToFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem insertCSharpTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertVBTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertJavaTemplateToolStripMenuItem;



    }
}
