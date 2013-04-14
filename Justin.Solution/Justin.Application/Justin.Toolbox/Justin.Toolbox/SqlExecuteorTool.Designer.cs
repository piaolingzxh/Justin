namespace Justin.Toolbox.Controls
{
    partial class SqlExecuteorTool
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlSqlResultContainer = new System.Windows.Forms.TabControl();
            this.txtSqlText = new System.Windows.Forms.RichTextBox();
            this.btnExecuteSQLFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cBoxTxtSqlSyntax = new System.Windows.Forms.ComboBox();
            this.btnExecuteSQLText = new System.Windows.Forms.Button();
            this.cBoxFileSqlSyntax = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlSqlResultContainer, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtSqlText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnExecuteSQLFile, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cBoxFileSqlSyntax, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 394);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControlSqlResultContainer
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControlSqlResultContainer, 4);
            this.tabControlSqlResultContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSqlResultContainer.Location = new System.Drawing.Point(3, 163);
            this.tabControlSqlResultContainer.Name = "tabControlSqlResultContainer";
            this.tabControlSqlResultContainer.SelectedIndex = 0;
            this.tabControlSqlResultContainer.Size = new System.Drawing.Size(594, 228);
            this.tabControlSqlResultContainer.TabIndex = 6;
            // 
            // txtSqlText
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtSqlText, 3);
            this.txtSqlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSqlText.Location = new System.Drawing.Point(3, 63);
            this.txtSqlText.Name = "txtSqlText";
            this.txtSqlText.Size = new System.Drawing.Size(494, 94);
            this.txtSqlText.TabIndex = 0;
            this.txtSqlText.Text = "";
            // 
            // btnExecuteSQLFile
            // 
            this.btnExecuteSQLFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteSQLFile.Location = new System.Drawing.Point(503, 33);
            this.btnExecuteSQLFile.Name = "btnExecuteSQLFile";
            this.btnExecuteSQLFile.Size = new System.Drawing.Size(94, 24);
            this.btnExecuteSQLFile.TabIndex = 4;
            this.btnExecuteSQLFile.Text = "→";
            this.btnExecuteSQLFile.UseVisualStyleBackColor = true;
            this.btnExecuteSQLFile.Click += new System.EventHandler(this.btnExecuteSQLFile_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cBoxTxtSqlSyntax, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExecuteSQLText, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(503, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 94);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // cBoxTxtSqlSyntax
            // 
            this.cBoxTxtSqlSyntax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxTxtSqlSyntax.FormattingEnabled = true;
            this.cBoxTxtSqlSyntax.Items.AddRange(new object[] {
            "MSSQL",
            "Oracle",
            "GSQL",
            "Mdx"});
            this.cBoxTxtSqlSyntax.Location = new System.Drawing.Point(3, 3);
            this.cBoxTxtSqlSyntax.Name = "cBoxTxtSqlSyntax";
            this.cBoxTxtSqlSyntax.Size = new System.Drawing.Size(88, 21);
            this.cBoxTxtSqlSyntax.TabIndex = 3;
            // 
            // btnExecuteSQLText
            // 
            this.btnExecuteSQLText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteSQLText.Location = new System.Drawing.Point(3, 23);
            this.btnExecuteSQLText.Name = "btnExecuteSQLText";
            this.btnExecuteSQLText.Size = new System.Drawing.Size(88, 68);
            this.btnExecuteSQLText.TabIndex = 2;
            this.btnExecuteSQLText.Text = "→";
            this.btnExecuteSQLText.UseVisualStyleBackColor = true;
            this.btnExecuteSQLText.Click += new System.EventHandler(this.btnExecuteSQLText_Click);
            // 
            // cBoxFileSqlSyntax
            // 
            this.cBoxFileSqlSyntax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxFileSqlSyntax.FormattingEnabled = true;
            this.cBoxFileSqlSyntax.Items.AddRange(new object[] {
            "MSSQL",
            "Oracle",
            "GSQL",
            "Mdx"});
            this.cBoxFileSqlSyntax.Location = new System.Drawing.Point(403, 33);
            this.cBoxFileSqlSyntax.Name = "cBoxFileSqlSyntax";
            this.cBoxFileSqlSyntax.Size = new System.Drawing.Size(94, 21);
            this.cBoxFileSqlSyntax.TabIndex = 4;
            // 
            // SqlExecuteorTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SqlExecuteorTool";
            this.Text = "SqlExecuteorTool";
            this.Load += new System.EventHandler(this.SqlExecuteorTool_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox txtSqlText;
        private System.Windows.Forms.Button btnExecuteSQLText;
        private System.Windows.Forms.ComboBox cBoxTxtSqlSyntax;
        private System.Windows.Forms.Button btnExecuteSQLFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cBoxFileSqlSyntax;
        private System.Windows.Forms.TabControl tabControlSqlResultContainer;
    }
}