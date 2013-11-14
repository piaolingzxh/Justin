namespace Justin.Controls.Executer
{
    partial class SqlExecuterCtrl
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtSQLFileName = new System.Windows.Forms.TextBox();
            this.txtSQLPreview = new Justin.FrameWork.WinForm.FormUI.NumberedTextBox();
            this.layoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnEnableEditSQL = new System.Windows.Forms.Button();
            this.btnModifySQLFileContent = new System.Windows.Forms.Button();
            this.btnShowLineNum = new System.Windows.Forms.Button();
            this.btnExecuteSQLByLine = new System.Windows.Forms.Button();
            this.btnExecuteAllSQL = new System.Windows.Forms.Button();
            this.btnIntelligentExecuteSQL = new System.Windows.Forms.Button();
            this.btnPreviewSQLFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnBrowerSQLFile = new System.Windows.Forms.Button();
            this.layOutButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnUseSqlCmd = new System.Windows.Forms.Button();
            this.splitContainerSQL = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.layoutRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.layoutTop.SuspendLayout();
            this.layOutButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQL)).BeginInit();
            this.splitContainerSQL.Panel1.SuspendLayout();
            this.splitContainerSQL.Panel2.SuspendLayout();
            this.splitContainerSQL.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "SQl File：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSQLFileName
            // 
            this.txtSQLFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLFileName.Location = new System.Drawing.Point(68, 3);
            this.txtSQLFileName.Name = "txtSQLFileName";
            this.txtSQLFileName.ReadOnly = true;
            this.txtSQLFileName.Size = new System.Drawing.Size(626, 20);
            this.txtSQLFileName.TabIndex = 15;
            // 
            // txtSQLPreview
            // 
            this.txtSQLPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLPreview.Location = new System.Drawing.Point(0, 0);
            this.txtSQLPreview.Name = "txtSQLPreview";
            this.txtSQLPreview.ShowLineNumber = true;
            this.txtSQLPreview.Size = new System.Drawing.Size(693, 366);
            this.txtSQLPreview.TabIndex = 1;
            this.txtSQLPreview.Load += new System.EventHandler(this.txtSQLPreview_Load);
            // 
            // layoutRight
            // 
            this.layoutRight.ColumnCount = 1;
            this.layoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRight.Controls.Add(this.btnEnableEditSQL, 0, 1);
            this.layoutRight.Controls.Add(this.btnModifySQLFileContent, 0, 2);
            this.layoutRight.Controls.Add(this.btnShowLineNum, 0, 0);
            this.layoutRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRight.Location = new System.Drawing.Point(0, 0);
            this.layoutRight.Name = "layoutRight";
            this.layoutRight.RowCount = 5;
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutRight.Size = new System.Drawing.Size(100, 366);
            this.layoutRight.TabIndex = 7;
            // 
            // btnEnableEditSQL
            // 
            this.btnEnableEditSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEnableEditSQL.Image = global::Justin.Controls.Executer.Properties.Resources.edit1;
            this.btnEnableEditSQL.Location = new System.Drawing.Point(3, 43);
            this.btnEnableEditSQL.Name = "btnEnableEditSQL";
            this.btnEnableEditSQL.Size = new System.Drawing.Size(94, 34);
            this.btnEnableEditSQL.TabIndex = 3;
            this.btnEnableEditSQL.Text = "启用编辑";
            this.btnEnableEditSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnableEditSQL.UseVisualStyleBackColor = true;
            this.btnEnableEditSQL.Click += new System.EventHandler(this.btnEnableEditSQL_Click);
            // 
            // btnModifySQLFileContent
            // 
            this.btnModifySQLFileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifySQLFileContent.Image = global::Justin.Controls.Executer.Properties.Resources.save;
            this.btnModifySQLFileContent.Location = new System.Drawing.Point(3, 83);
            this.btnModifySQLFileContent.Name = "btnModifySQLFileContent";
            this.btnModifySQLFileContent.Size = new System.Drawing.Size(94, 34);
            this.btnModifySQLFileContent.TabIndex = 4;
            this.btnModifySQLFileContent.Text = "保存";
            this.btnModifySQLFileContent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModifySQLFileContent.UseVisualStyleBackColor = true;
            this.btnModifySQLFileContent.Click += new System.EventHandler(this.btnModifySQLFileContent_Click);
            // 
            // btnShowLineNum
            // 
            this.btnShowLineNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShowLineNum.Image = global::Justin.Controls.Executer.Properties.Resources.LineNum;
            this.btnShowLineNum.Location = new System.Drawing.Point(3, 3);
            this.btnShowLineNum.Name = "btnShowLineNum";
            this.btnShowLineNum.Size = new System.Drawing.Size(94, 34);
            this.btnShowLineNum.TabIndex = 2;
            this.btnShowLineNum.Text = "显示行号";
            this.btnShowLineNum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShowLineNum.UseVisualStyleBackColor = true;
            this.btnShowLineNum.Click += new System.EventHandler(this.btnShowLineNum_Click);
            // 
            // btnExecuteSQLByLine
            // 
            this.btnExecuteSQLByLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteSQLByLine.Image = global::Justin.Controls.Executer.Properties.Resources.ExecuteByLine;
            this.btnExecuteSQLByLine.Location = new System.Drawing.Point(429, 3);
            this.btnExecuteSQLByLine.Name = "btnExecuteSQLByLine";
            this.btnExecuteSQLByLine.Size = new System.Drawing.Size(94, 28);
            this.btnExecuteSQLByLine.TabIndex = 5;
            this.btnExecuteSQLByLine.Text = "逐行执行";
            this.btnExecuteSQLByLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExecuteSQLByLine.UseVisualStyleBackColor = true;
            this.btnExecuteSQLByLine.Click += new System.EventHandler(this.btnExecuteSQLByLine_Click);
            // 
            // btnExecuteAllSQL
            // 
            this.btnExecuteAllSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteAllSQL.Image = global::Justin.Controls.Executer.Properties.Resources.ExecuteAll;
            this.btnExecuteAllSQL.Location = new System.Drawing.Point(529, 3);
            this.btnExecuteAllSQL.Name = "btnExecuteAllSQL";
            this.btnExecuteAllSQL.Size = new System.Drawing.Size(94, 28);
            this.btnExecuteAllSQL.TabIndex = 6;
            this.btnExecuteAllSQL.Text = "全部执行";
            this.btnExecuteAllSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExecuteAllSQL.UseVisualStyleBackColor = true;
            this.btnExecuteAllSQL.Click += new System.EventHandler(this.btnExecuteAllSQL_Click);
            // 
            // btnIntelligentExecuteSQL
            // 
            this.btnIntelligentExecuteSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIntelligentExecuteSQL.Image = global::Justin.Controls.Executer.Properties.Resources.zhineng;
            this.btnIntelligentExecuteSQL.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnIntelligentExecuteSQL.Location = new System.Drawing.Point(629, 3);
            this.btnIntelligentExecuteSQL.Name = "btnIntelligentExecuteSQL";
            this.btnIntelligentExecuteSQL.Size = new System.Drawing.Size(94, 28);
            this.btnIntelligentExecuteSQL.TabIndex = 7;
            this.btnIntelligentExecuteSQL.Tag = "//->";
            this.btnIntelligentExecuteSQL.Text = "智能执行";
            this.btnIntelligentExecuteSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIntelligentExecuteSQL.UseVisualStyleBackColor = true;
            this.btnIntelligentExecuteSQL.Click += new System.EventHandler(this.btnIntelligentExecuteSQL_Click);
            // 
            // btnPreviewSQLFile
            // 
            this.btnPreviewSQLFile.Image = global::Justin.Controls.Executer.Properties.Resources.open;
            this.btnPreviewSQLFile.Location = new System.Drawing.Point(3, 3);
            this.btnPreviewSQLFile.Name = "btnPreviewSQLFile";
            this.btnPreviewSQLFile.Size = new System.Drawing.Size(94, 28);
            this.btnPreviewSQLFile.TabIndex = 0;
            this.btnPreviewSQLFile.Text = "预览脚本";
            this.btnPreviewSQLFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPreviewSQLFile.UseVisualStyleBackColor = true;
            this.btnPreviewSQLFile.Click += new System.EventHandler(this.btnPreviewSQLFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.layoutTop);
            this.splitContainerMain.Panel1MinSize = 80;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerSQL);
            this.splitContainerMain.Size = new System.Drawing.Size(797, 450);
            this.splitContainerMain.SplitterDistance = 80;
            this.splitContainerMain.TabIndex = 6;
            // 
            // layoutTop
            // 
            this.layoutTop.ColumnCount = 3;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutTop.Controls.Add(this.label8, 0, 0);
            this.layoutTop.Controls.Add(this.txtSQLFileName, 1, 0);
            this.layoutTop.Controls.Add(this.btnBrowerSQLFile, 2, 0);
            this.layoutTop.Controls.Add(this.layOutButton, 1, 1);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.RowCount = 3;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.Size = new System.Drawing.Size(797, 80);
            this.layoutTop.TabIndex = 1;
            // 
            // btnBrowerSQLFile
            // 
            this.btnBrowerSQLFile.Image = global::Justin.Controls.Executer.Properties.Resources.open;
            this.btnBrowerSQLFile.Location = new System.Drawing.Point(700, 3);
            this.btnBrowerSQLFile.Name = "btnBrowerSQLFile";
            this.btnBrowerSQLFile.Size = new System.Drawing.Size(94, 34);
            this.btnBrowerSQLFile.TabIndex = 0;
            this.btnBrowerSQLFile.Text = "浏览文件";
            this.btnBrowerSQLFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowerSQLFile.UseVisualStyleBackColor = true;
            this.btnBrowerSQLFile.Click += new System.EventHandler(this.btnBrowerSQLFile_Click);
            // 
            // layOutButton
            // 
            this.layOutButton.ColumnCount = 6;
            this.layoutTop.SetColumnSpan(this.layOutButton, 2);
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layOutButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layOutButton.Controls.Add(this.btnIntelligentExecuteSQL, 5, 0);
            this.layOutButton.Controls.Add(this.btnExecuteAllSQL, 4, 0);
            this.layOutButton.Controls.Add(this.btnExecuteSQLByLine, 3, 0);
            this.layOutButton.Controls.Add(this.btnPreviewSQLFile, 0, 0);
            this.layOutButton.Controls.Add(this.btnUseSqlCmd, 2, 0);
            this.layOutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layOutButton.Location = new System.Drawing.Point(68, 43);
            this.layOutButton.Name = "layOutButton";
            this.layOutButton.RowCount = 1;
            this.layOutButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layOutButton.Size = new System.Drawing.Size(726, 34);
            this.layOutButton.TabIndex = 16;
            // 
            // btnUseSqlCmd
            // 
            this.btnUseSqlCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUseSqlCmd.Image = global::Justin.Controls.Executer.Properties.Resources.ExecuteByLine;
            this.btnUseSqlCmd.Location = new System.Drawing.Point(329, 3);
            this.btnUseSqlCmd.Name = "btnUseSqlCmd";
            this.btnUseSqlCmd.Size = new System.Drawing.Size(94, 28);
            this.btnUseSqlCmd.TabIndex = 5;
            this.btnUseSqlCmd.Text = "SqlCmd";
            this.btnUseSqlCmd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUseSqlCmd.UseVisualStyleBackColor = true;
            this.btnUseSqlCmd.Click += new System.EventHandler(this.btnUseSqlCmd_Click);
            // 
            // splitContainerSQL
            // 
            this.splitContainerSQL.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerSQL.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSQL.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerSQL.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSQL.Name = "splitContainerSQL";
            // 
            // splitContainerSQL.Panel1
            // 
            this.splitContainerSQL.Panel1.Controls.Add(this.txtSQLPreview);
            // 
            // splitContainerSQL.Panel2
            // 
            this.splitContainerSQL.Panel2.Controls.Add(this.layoutRight);
            this.splitContainerSQL.Panel2MinSize = 100;
            this.splitContainerSQL.Size = new System.Drawing.Size(797, 366);
            this.splitContainerSQL.SplitterDistance = 693;
            this.splitContainerSQL.TabIndex = 1;
            // 
            // SqlExecuterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "SqlExecuterCtrl";
            this.Size = new System.Drawing.Size(797, 450);
            this.layoutRight.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            this.layOutButton.ResumeLayout(false);
            this.splitContainerSQL.Panel1.ResumeLayout(false);
            this.splitContainerSQL.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQL)).EndInit();
            this.splitContainerSQL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSQLFileName;
        private System.Windows.Forms.TableLayoutPanel layoutRight;
        private System.Windows.Forms.Button btnExecuteSQLByLine;
        private System.Windows.Forms.Button btnExecuteAllSQL;
        private System.Windows.Forms.Button btnIntelligentExecuteSQL;
        private System.Windows.Forms.Button btnEnableEditSQL;
        private System.Windows.Forms.Button btnModifySQLFileContent;
        private System.Windows.Forms.Button btnShowLineNum;
        private FrameWork.WinForm.FormUI.NumberedTextBox txtSQLPreview;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnPreviewSQLFile;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel layOutButton;
        private System.Windows.Forms.Button btnBrowerSQLFile;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerSQL;
        private System.Windows.Forms.Button btnUseSqlCmd;
    }
}
