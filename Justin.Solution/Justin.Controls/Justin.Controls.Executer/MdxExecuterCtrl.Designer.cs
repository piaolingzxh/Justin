namespace Justin.Controls.Executer
{
    partial class MdxExecuterCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanelMdx = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExecuteWithFormatted = new System.Windows.Forms.Button();
            this.btnExecuteDataSet = new System.Windows.Forms.Button();
            this.btnExecuteWithUnFormatted = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtMdx = new ICSharpCode.TextEditor.TextEditorControl();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cboxConnStrings = new System.Windows.Forms.ComboBox();
            this.gvMdxresult = new System.Windows.Forms.DataGridView();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerQueryResult = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutResult = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanelMdx.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMdxresult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerQueryResult)).BeginInit();
            this.splitContainerQueryResult.Panel1.SuspendLayout();
            this.splitContainerQueryResult.Panel2.SuspendLayout();
            this.splitContainerQueryResult.SuspendLayout();
            this.tableLayoutResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMdx
            // 
            this.tableLayoutPanelMdx.AutoSize = true;
            this.tableLayoutPanelMdx.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMdx.ColumnCount = 2;
            this.tableLayoutPanelMdx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMdx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMdx.Controls.Add(this.tableLayoutPanelButtons, 1, 1);
            this.tableLayoutPanelMdx.Controls.Add(this.txtMdx, 0, 1);
            this.tableLayoutPanelMdx.Controls.Add(this.btnConnect, 1, 0);
            this.tableLayoutPanelMdx.Controls.Add(this.cboxConnStrings, 0, 0);
            this.tableLayoutPanelMdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMdx.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMdx.Name = "tableLayoutPanelMdx";
            this.tableLayoutPanelMdx.RowCount = 2;
            this.tableLayoutPanelMdx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMdx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMdx.Size = new System.Drawing.Size(882, 280);
            this.tableLayoutPanelMdx.TabIndex = 0;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.ColumnCount = 1;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnClear, 0, 3);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecuteWithFormatted, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecuteDataSet, 0, 2);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecuteWithUnFormatted, 0, 1);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExportExcel, 0, 4);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(835, 33);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 8;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(44, 244);
            this.tableLayoutPanelButtons.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Image = global::Justin.Controls.Executer.Properties.Resources.clear;
            this.btnClear.Location = new System.Drawing.Point(3, 93);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(38, 24);
            this.btnClear.TabIndex = 5;
            this.btnClear.Tag = "Clear Query Result";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExecuteWithFormatted
            // 
            this.btnExecuteWithFormatted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteWithFormatted.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.btnExecuteWithFormatted.Location = new System.Drawing.Point(3, 3);
            this.btnExecuteWithFormatted.Name = "btnExecuteWithFormatted";
            this.btnExecuteWithFormatted.Size = new System.Drawing.Size(38, 24);
            this.btnExecuteWithFormatted.TabIndex = 3;
            this.btnExecuteWithFormatted.Tag = "Execute and ReturnCellSet,Display With FormattedValue";
            this.btnExecuteWithFormatted.UseVisualStyleBackColor = true;
            this.btnExecuteWithFormatted.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExecuteDataSet
            // 
            this.btnExecuteDataSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteDataSet.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.btnExecuteDataSet.Location = new System.Drawing.Point(3, 63);
            this.btnExecuteDataSet.Name = "btnExecuteDataSet";
            this.btnExecuteDataSet.Size = new System.Drawing.Size(38, 24);
            this.btnExecuteDataSet.TabIndex = 4;
            this.btnExecuteDataSet.Tag = "Execute and Return DataSet";
            this.btnExecuteDataSet.UseVisualStyleBackColor = true;
            this.btnExecuteDataSet.Click += new System.EventHandler(this.btnExecuteDataSet_Click);
            // 
            // btnExecuteWithUnFormatted
            // 
            this.btnExecuteWithUnFormatted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteWithUnFormatted.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.btnExecuteWithUnFormatted.Location = new System.Drawing.Point(3, 33);
            this.btnExecuteWithUnFormatted.Name = "btnExecuteWithUnFormatted";
            this.btnExecuteWithUnFormatted.Size = new System.Drawing.Size(38, 24);
            this.btnExecuteWithUnFormatted.TabIndex = 3;
            this.btnExecuteWithUnFormatted.Tag = "Execute and ReturnCellSet,Display With UnFormattedValue";
            this.btnExecuteWithUnFormatted.UseVisualStyleBackColor = true;
            this.btnExecuteWithUnFormatted.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Image = global::Justin.Controls.Executer.Properties.Resources.excel;
            this.btnExportExcel.Location = new System.Drawing.Point(3, 123);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(38, 24);
            this.btnExportExcel.TabIndex = 5;
            this.btnExportExcel.Tag = "Export Query Result";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtMdx
            // 
            this.txtMdx.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtMdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMdx.Location = new System.Drawing.Point(3, 33);
            this.txtMdx.Name = "txtMdx";
            this.txtMdx.ShowEOLMarkers = true;
            this.txtMdx.ShowSpaces = true;
            this.txtMdx.ShowTabs = true;
            this.txtMdx.ShowVRuler = true;
            this.txtMdx.Size = new System.Drawing.Size(826, 244);
            this.txtMdx.TabIndex = 2;
            this.txtMdx.Text = "\r\nSELECT \r\nNON EMPTY\r\n{\r\n    [Measures].[ZCHTAmount],[Measures].[ZCHTCount]\r\n}\r\n " +
    "ON COLUMNS,\r\nNON EMPTY\r\n{\r\n   [ProjectDim.hieInfo].[Project].Members\r\n}\r\nON ROWS" +
    "\r\nFROM ZCHT_BsJe";
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Image = global::Justin.Controls.Executer.Properties.Resources.conn;
            this.btnConnect.Location = new System.Drawing.Point(835, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(44, 24);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Tag = "Connection To OLAP  Server";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnectOLAP_Click);
            // 
            // cboxConnStrings
            // 
            this.cboxConnStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxConnStrings.FormattingEnabled = true;
            this.cboxConnStrings.Location = new System.Drawing.Point(3, 3);
            this.cboxConnStrings.Name = "cboxConnStrings";
            this.cboxConnStrings.Size = new System.Drawing.Size(826, 21);
            this.cboxConnStrings.TabIndex = 7;
            // 
            // gvMdxresult
            // 
            this.gvMdxresult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvMdxresult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvMdxresult.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMdxresult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMdxresult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMdxresult.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvMdxresult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvMdxresult.Location = new System.Drawing.Point(3, 28);
            this.gvMdxresult.Name = "gvMdxresult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMdxresult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMdxresult.Size = new System.Drawing.Size(529, 245);
            this.gvMdxresult.TabIndex = 4;
            this.gvMdxresult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMdxresult_CellClick);
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(3, 3);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(529, 20);
            this.txtResult.TabIndex = 5;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tableLayoutPanelMdx);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerQueryResult);
            this.splitContainerMain.Size = new System.Drawing.Size(882, 560);
            this.splitContainerMain.SplitterDistance = 280;
            this.splitContainerMain.TabIndex = 6;
            // 
            // splitContainerQueryResult
            // 
            this.splitContainerQueryResult.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerQueryResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerQueryResult.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerQueryResult.Location = new System.Drawing.Point(0, 0);
            this.splitContainerQueryResult.Name = "splitContainerQueryResult";
            // 
            // splitContainerQueryResult.Panel1
            // 
            this.splitContainerQueryResult.Panel1.Controls.Add(this.tableLayoutResult);
            // 
            // splitContainerQueryResult.Panel2
            // 
            this.splitContainerQueryResult.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainerQueryResult.Size = new System.Drawing.Size(882, 276);
            this.splitContainerQueryResult.SplitterDistance = 535;
            this.splitContainerQueryResult.TabIndex = 1;
            // 
            // tableLayoutResult
            // 
            this.tableLayoutResult.ColumnCount = 1;
            this.tableLayoutResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutResult.Controls.Add(this.gvMdxresult, 0, 1);
            this.tableLayoutResult.Controls.Add(this.txtResult, 0, 0);
            this.tableLayoutResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutResult.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutResult.Name = "tableLayoutResult";
            this.tableLayoutResult.RowCount = 2;
            this.tableLayoutResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutResult.Size = new System.Drawing.Size(535, 276);
            this.tableLayoutResult.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(343, 276);
            this.propertyGrid.TabIndex = 6;
            // 
            // MdxExecuterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "MdxExecuterCtrl";
            this.Size = new System.Drawing.Size(882, 560);
            this.Load += new System.EventHandler(this.MdxExecuterCtrl_Load);
            this.tableLayoutPanelMdx.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMdxresult)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerQueryResult.Panel1.ResumeLayout(false);
            this.splitContainerQueryResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerQueryResult)).EndInit();
            this.splitContainerQueryResult.ResumeLayout(false);
            this.tableLayoutResult.ResumeLayout(false);
            this.tableLayoutResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl txtMdx;
        private System.Windows.Forms.DataGridView gvMdxresult;
        private System.Windows.Forms.Button btnExecuteWithFormatted;
        private System.Windows.Forms.Button btnExecuteDataSet;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExecuteWithUnFormatted;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMdx;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnExportExcel;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutResult;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cboxConnStrings;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerQueryResult;
        private System.Windows.Forms.PropertyGrid propertyGrid;

    }
}
