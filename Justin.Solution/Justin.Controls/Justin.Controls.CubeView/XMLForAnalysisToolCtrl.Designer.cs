namespace Justin.Controls.CubeView
{
    partial class XMLForAnalysisToolCtrl
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
            this.splitContainerEx1 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridRestrictions = new System.Windows.Forms.DataGridView();
            this.cboxRequestType = new System.Windows.Forms.ComboBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.gridResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRestrictions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.gridResults);
            this.splitContainerEx1.Size = new System.Drawing.Size(734, 525);
            this.splitContainerEx1.SplitterDistance = 262;
            this.splitContainerEx1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridRestrictions, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboxRequestType, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnRequest, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtConnStr, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 262);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // gridRestrictions
            // 
            this.gridRestrictions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridRestrictions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.gridRestrictions, 2);
            this.gridRestrictions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRestrictions.Location = new System.Drawing.Point(3, 33);
            this.gridRestrictions.Name = "gridRestrictions";
            this.gridRestrictions.Size = new System.Drawing.Size(728, 196);
            this.gridRestrictions.TabIndex = 1;
            // 
            // cboxRequestType
            // 
            this.cboxRequestType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxRequestType.FormattingEnabled = true;
            this.cboxRequestType.Items.AddRange(new object[] {
            "DBSCHEMA_CATALOGS",
            "DBSCHEMA_COLUMNS",
            "DBSCHEMA_PROVIDER_TYPES",
            "DBSCHEMA_TABLES",
            "DBSCHEMA_TABLES_INFO",
            "MDSCHEMA_ACTIONS",
            "MDSCHEMA_CUBES",
            "MDSCHEMA_DIMENSIONS",
            "MDSCHEMA_FUNCTIONS",
            "MDSCHEMA_HIERARCHIES",
            "MDSCHEMA_MEASURES",
            "MDSCHEMA_MEMBERS",
            "MDSCHEMA_PROPERTIES",
            "MDSCHEMA_SETS"});
            this.cboxRequestType.Location = new System.Drawing.Point(3, 235);
            this.cboxRequestType.Name = "cboxRequestType";
            this.cboxRequestType.Size = new System.Drawing.Size(628, 21);
            this.cboxRequestType.TabIndex = 3;
            // 
            // btnRequest
            // 
            this.btnRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRequest.Location = new System.Drawing.Point(637, 235);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(94, 24);
            this.btnRequest.TabIndex = 4;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // txtConnStr
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtConnStr, 2);
            this.txtConnStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnStr.Location = new System.Drawing.Point(3, 3);
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(728, 20);
            this.txtConnStr.TabIndex = 5;
            this.txtConnStr.Text = "Provider=mondrian;Data Source=http://localhost:8894/mondrian/xmla;Initial Catalog" +
    "=Derby;\r\n";
            // 
            // gridResults
            // 
            this.gridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResults.Location = new System.Drawing.Point(0, 0);
            this.gridResults.Name = "gridResults";
            this.gridResults.Size = new System.Drawing.Size(734, 259);
            this.gridResults.TabIndex = 1;
            // 
            // XMLForAnalysisToolCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "XMLForAnalysisToolCtrl";
            this.Size = new System.Drawing.Size(734, 525);
            this.Load += new System.EventHandler(this.XMLForAnalysisTool_Load);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRestrictions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridRestrictions;
        private System.Windows.Forms.ComboBox cboxRequestType;
        private System.Windows.Forms.DataGridView gridResults;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.TextBox txtConnStr;

    }
}
