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
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBrowerSQLFile = new System.Windows.Forms.Button();
            this.txtSQLFileName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExecuteSQLByLine = new System.Windows.Forms.Button();
            this.btnExecuteAllSQL = new System.Windows.Forms.Button();
            this.btnIntelligentExecuteSQL = new System.Windows.Forms.Button();
            this.btnEnableEditSQL = new System.Windows.Forms.Button();
            this.btnModifySQLFileContent = new System.Windows.Forms.Button();
            this.btnShowLineNum = new System.Windows.Forms.Button();
            this.txtSQLPreview = new Justin.FrameWork.WinForm.FormUI.NumberedTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnBrowerSQLFile, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtSQLFileName, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.txtSQLPreview, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(732, 368);
            this.tableLayoutPanel8.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "SQl File：";
            // 
            // btnBrowerSQLFile
            // 
            this.btnBrowerSQLFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowerSQLFile.Image = global::Justin.Controls.Executer.Properties.Resources.open;
            this.btnBrowerSQLFile.Location = new System.Drawing.Point(630, 3);
            this.btnBrowerSQLFile.Name = "btnBrowerSQLFile";
            this.btnBrowerSQLFile.Size = new System.Drawing.Size(99, 24);
            this.btnBrowerSQLFile.TabIndex = 2;
            this.btnBrowerSQLFile.Text = "浏览文件";
            this.btnBrowerSQLFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowerSQLFile.UseVisualStyleBackColor = true;
            this.btnBrowerSQLFile.Click += new System.EventHandler(this.btnBrowerSQLFile_Click);
            // 
            // txtSQLFileName
            // 
            this.txtSQLFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLFileName.Location = new System.Drawing.Point(84, 3);
            this.txtSQLFileName.Name = "txtSQLFileName";
            this.txtSQLFileName.ReadOnly = true;
            this.txtSQLFileName.Size = new System.Drawing.Size(540, 20);
            this.txtSQLFileName.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnExecuteSQLByLine, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnExecuteAllSQL, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnIntelligentExecuteSQL, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnEnableEditSQL, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnModifySQLFileContent, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnShowLineNum, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(630, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(99, 332);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btnExecuteSQLByLine
            // 
            this.btnExecuteSQLByLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteSQLByLine.Image = global::Justin.Controls.Executer.Properties.Resources.ExecuteByLine;
            this.btnExecuteSQLByLine.Location = new System.Drawing.Point(3, 215);
            this.btnExecuteSQLByLine.Name = "btnExecuteSQLByLine";
            this.btnExecuteSQLByLine.Size = new System.Drawing.Size(93, 34);
            this.btnExecuteSQLByLine.TabIndex = 3;
            this.btnExecuteSQLByLine.Text = "逐行执行";
            this.btnExecuteSQLByLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExecuteSQLByLine.UseVisualStyleBackColor = true;
            this.btnExecuteSQLByLine.Click += new System.EventHandler(this.btnExecuteSQLByLine_Click);
            // 
            // btnExecuteAllSQL
            // 
            this.btnExecuteAllSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteAllSQL.Image = global::Justin.Controls.Executer.Properties.Resources.ExecuteAll;
            this.btnExecuteAllSQL.Location = new System.Drawing.Point(3, 255);
            this.btnExecuteAllSQL.Name = "btnExecuteAllSQL";
            this.btnExecuteAllSQL.Size = new System.Drawing.Size(93, 34);
            this.btnExecuteAllSQL.TabIndex = 4;
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
            this.btnIntelligentExecuteSQL.Location = new System.Drawing.Point(3, 295);
            this.btnIntelligentExecuteSQL.Name = "btnIntelligentExecuteSQL";
            this.btnIntelligentExecuteSQL.Size = new System.Drawing.Size(93, 34);
            this.btnIntelligentExecuteSQL.TabIndex = 5;
            this.btnIntelligentExecuteSQL.Tag = "//->";
            this.btnIntelligentExecuteSQL.Text = "智能执行";
            this.btnIntelligentExecuteSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIntelligentExecuteSQL.UseVisualStyleBackColor = true;
            this.btnIntelligentExecuteSQL.Click += new System.EventHandler(this.btnIntelligentExecuteSQL_Click);
            // 
            // btnEnableEditSQL
            // 
            this.btnEnableEditSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEnableEditSQL.Image = global::Justin.Controls.Executer.Properties.Resources.edit1;
            this.btnEnableEditSQL.Location = new System.Drawing.Point(3, 43);
            this.btnEnableEditSQL.Name = "btnEnableEditSQL";
            this.btnEnableEditSQL.Size = new System.Drawing.Size(93, 34);
            this.btnEnableEditSQL.TabIndex = 6;
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
            this.btnModifySQLFileContent.Size = new System.Drawing.Size(93, 34);
            this.btnModifySQLFileContent.TabIndex = 7;
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
            this.btnShowLineNum.Size = new System.Drawing.Size(93, 34);
            this.btnShowLineNum.TabIndex = 8;
            this.btnShowLineNum.Text = "显示行号";
            this.btnShowLineNum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShowLineNum.UseVisualStyleBackColor = true;
            this.btnShowLineNum.Click += new System.EventHandler(this.btnShowLineNum_Click);
            // 
            // txtSQLPreview
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.txtSQLPreview, 2);
            this.txtSQLPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLPreview.Location = new System.Drawing.Point(3, 33);
            this.txtSQLPreview.Name = "txtSQLPreview";
            this.txtSQLPreview.ShowLineNumber = true;
            this.txtSQLPreview.Size = new System.Drawing.Size(621, 332);
            this.txtSQLPreview.TabIndex = 8;
            this.txtSQLPreview.Load += new System.EventHandler(this.txtSQLPreview_Load);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // SqlExecuterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel8);
            this.Name = "SqlExecuterCtrl";
            this.Size = new System.Drawing.Size(732, 368);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBrowerSQLFile;
        private System.Windows.Forms.TextBox txtSQLFileName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExecuteSQLByLine;
        private System.Windows.Forms.Button btnExecuteAllSQL;
        private System.Windows.Forms.Button btnIntelligentExecuteSQL;
        private System.Windows.Forms.Button btnEnableEditSQL;
        private System.Windows.Forms.Button btnModifySQLFileContent;
        private System.Windows.Forms.Button btnShowLineNum;
        private FrameWork.WinForm.FormUI.NumberedTextBox txtSQLPreview;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
