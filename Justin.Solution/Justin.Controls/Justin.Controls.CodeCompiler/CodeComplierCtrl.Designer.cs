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
            this.btnDisassembly = new System.Windows.Forms.Button();
            this.layoutOfButtons = new System.Windows.Forms.TableLayoutPanel();
            this.txtCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStripOfEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertCSharpTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertVBTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertJavaTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMSILCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainerEx1 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerEx2 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutOfButtons.SuspendLayout();
            this.contextMenuStripOfEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).BeginInit();
            this.splitContainerEx2.Panel1.SuspendLayout();
            this.splitContainerEx2.Panel2.SuspendLayout();
            this.splitContainerEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompiler
            // 
            this.btnCompiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCompiler.Location = new System.Drawing.Point(3, 38);
            this.btnCompiler.Name = "btnCompiler";
            this.btnCompiler.Size = new System.Drawing.Size(79, 29);
            this.btnCompiler.TabIndex = 2;
            this.btnCompiler.Text = "Compiler";
            this.btnCompiler.UseVisualStyleBackColor = true;
            this.btnCompiler.Click += new System.EventHandler(this.btnCompiler_Click);
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRun.Location = new System.Drawing.Point(3, 73);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(79, 29);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnDisassembly
            // 
            this.btnDisassembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDisassembly.Location = new System.Drawing.Point(3, 3);
            this.btnDisassembly.Name = "btnDisassembly";
            this.btnDisassembly.Size = new System.Drawing.Size(79, 29);
            this.btnDisassembly.TabIndex = 1;
            this.btnDisassembly.Text = "Disassembly";
            this.btnDisassembly.UseVisualStyleBackColor = true;
            this.btnDisassembly.Click += new System.EventHandler(this.btnShowILCode_Click);
            // 
            // layoutOfButtons
            // 
            this.layoutOfButtons.ColumnCount = 1;
            this.layoutOfButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.Controls.Add(this.btnDisassembly, 0, 0);
            this.layoutOfButtons.Controls.Add(this.btnCompiler, 0, 1);
            this.layoutOfButtons.Controls.Add(this.btnRun, 0, 2);
            this.layoutOfButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutOfButtons.Location = new System.Drawing.Point(0, 0);
            this.layoutOfButtons.Name = "layoutOfButtons";
            this.layoutOfButtons.RowCount = 7;
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutOfButtons.Size = new System.Drawing.Size(85, 390);
            this.layoutOfButtons.TabIndex = 22;
            // 
            // txtCode
            // 
            this.txtCode.ContextMenuStrip = this.contextMenuStripOfEditor;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.ShowEOLMarkers = true;
            this.txtCode.ShowSpaces = true;
            this.txtCode.ShowTabs = true;
            this.txtCode.ShowVRuler = true;
            this.txtCode.Size = new System.Drawing.Size(320, 390);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = resources.GetString("txtCode.Text");
            // 
            // contextMenuStripOfEditor
            // 
            this.contextMenuStripOfEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCSharpTemplateToolStripMenuItem,
            this.insertVBTemplateToolStripMenuItem,
            this.insertJavaTemplateToolStripMenuItem});
            this.contextMenuStripOfEditor.Name = "contextMenuStrip1";
            this.contextMenuStripOfEditor.Size = new System.Drawing.Size(198, 92);
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
            // txtMSILCode
            // 
            this.txtMSILCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMSILCode.Location = new System.Drawing.Point(0, 0);
            this.txtMSILCode.Name = "txtMSILCode";
            this.txtMSILCode.ShowEOLMarkers = true;
            this.txtMSILCode.ShowSpaces = true;
            this.txtMSILCode.ShowTabs = true;
            this.txtMSILCode.ShowVRuler = true;
            this.txtMSILCode.Size = new System.Drawing.Size(335, 390);
            this.txtMSILCode.TabIndex = 4;
            this.txtMSILCode.Text = resources.GetString("txtMSILCode.Text");
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.Name = "splitContainerEx1";
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.txtCode);
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.splitContainerEx2);
            this.splitContainerEx1.Size = new System.Drawing.Size(748, 390);
            this.splitContainerEx1.SplitterDistance = 320;
            this.splitContainerEx1.TabIndex = 4;
            // 
            // splitContainerEx2
            // 
            this.splitContainerEx2.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.None;
            this.splitContainerEx2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerEx2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx2.Name = "splitContainerEx2";
            // 
            // splitContainerEx2.Panel1
            // 
            this.splitContainerEx2.Panel1.Controls.Add(this.txtMSILCode);
            // 
            // splitContainerEx2.Panel2
            // 
            this.splitContainerEx2.Panel2.Controls.Add(this.layoutOfButtons);
            this.splitContainerEx2.Size = new System.Drawing.Size(424, 390);
            this.splitContainerEx2.SplitterDistance = 335;
            this.splitContainerEx2.TabIndex = 0;
            // 
            // CodeComplierCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "CodeComplierCtrl";
            this.Size = new System.Drawing.Size(748, 390);
            this.Load += new System.EventHandler(this.CodeComplierCtrl_Load);
            this.layoutOfButtons.ResumeLayout(false);
            this.contextMenuStripOfEditor.ResumeLayout(false);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.splitContainerEx2.Panel1.ResumeLayout(false);
            this.splitContainerEx2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).EndInit();
            this.splitContainerEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCompiler;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnDisassembly;
        private System.Windows.Forms.TableLayoutPanel layoutOfButtons;
        private ICSharpCode.TextEditor.TextEditorControl txtCode;
        private ICSharpCode.TextEditor.TextEditorControl txtMSILCode;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOfEditor;
        private System.Windows.Forms.ToolStripMenuItem insertCSharpTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertVBTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertJavaTemplateToolStripMenuItem;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx1;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx2;



    }
}
