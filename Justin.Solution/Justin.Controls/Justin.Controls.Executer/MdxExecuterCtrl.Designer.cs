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
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnExecuteDataSet = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDefaultConnStr = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.txtMdx = new ICSharpCode.TextEditor.TextEditorControl();
            this.button1 = new System.Windows.Forms.Button();
            this.gvMdxresult = new System.Windows.Forms.DataGridView();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.splitContainerEx1 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutresult = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMdx.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMdxresult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.tableLayoutresult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMdx
            // 
            this.tableLayoutPanelMdx.ColumnCount = 2;
            this.tableLayoutPanelMdx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMdx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMdx.Controls.Add(this.tableLayoutPanelButtons, 1, 1);
            this.tableLayoutPanelMdx.Controls.Add(this.txtConnectionString, 0, 0);
            this.tableLayoutPanelMdx.Controls.Add(this.txtMdx, 0, 1);
            this.tableLayoutPanelMdx.Controls.Add(this.button1, 1, 0);
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
            this.tableLayoutPanelButtons.Controls.Add(this.btnClear, 0, 4);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecute, 0, 1);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecuteDataSet, 0, 3);
            this.tableLayoutPanelButtons.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanelButtons.Controls.Add(this.btnDefaultConnStr, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExportExcel, 0, 5);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(835, 33);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 9;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(44, 244);
            this.tableLayoutPanelButtons.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Image = global::Justin.Controls.Executer.Properties.Resources.clear;
            this.btnClear.Location = new System.Drawing.Point(3, 123);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(38, 24);
            this.btnClear.TabIndex = 5;
            this.btnClear.Tag = "Clear Query Result";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecute.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.btnExecute.Location = new System.Drawing.Point(3, 33);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(38, 24);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Tag = "Execute and ReturnCellSet,Display With FormattedValue";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExecuteDataSet
            // 
            this.btnExecuteDataSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteDataSet.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.btnExecuteDataSet.Location = new System.Drawing.Point(3, 93);
            this.btnExecuteDataSet.Name = "btnExecuteDataSet";
            this.btnExecuteDataSet.Size = new System.Drawing.Size(38, 24);
            this.btnExecuteDataSet.TabIndex = 4;
            this.btnExecuteDataSet.Tag = "Execute and Return DataSet";
            this.btnExecuteDataSet.UseVisualStyleBackColor = true;
            this.btnExecuteDataSet.Click += new System.EventHandler(this.btnExecuteDataSet_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Image = global::Justin.Controls.Executer.Properties.Resources.execute;
            this.button2.Location = new System.Drawing.Point(3, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 24);
            this.button2.TabIndex = 3;
            this.button2.Tag = "Execute and ReturnCellSet,Display With UnFormattedValue";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnDefaultConnStr
            // 
            this.btnDefaultConnStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDefaultConnStr.Image = global::Justin.Controls.Executer.Properties.Resources.conn;
            this.btnDefaultConnStr.Location = new System.Drawing.Point(3, 3);
            this.btnDefaultConnStr.Name = "btnDefaultConnStr";
            this.btnDefaultConnStr.Size = new System.Drawing.Size(38, 24);
            this.btnDefaultConnStr.TabIndex = 6;
            this.btnDefaultConnStr.Tag = "Set Default OLAP Connection String";
            this.btnDefaultConnStr.UseVisualStyleBackColor = true;
            this.btnDefaultConnStr.Click += new System.EventHandler(this.btnDefaultConnStr_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Image = global::Justin.Controls.Executer.Properties.Resources.excel;
            this.btnExportExcel.Location = new System.Drawing.Point(3, 153);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(38, 24);
            this.btnExportExcel.TabIndex = 5;
            this.btnExportExcel.Tag = "Clear Query Result";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(826, 20);
            this.txtConnectionString.TabIndex = 5;
            // 
            // txtMdx
            // 
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
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Image = global::Justin.Controls.Executer.Properties.Resources.conn;
            this.button1.Location = new System.Drawing.Point(835, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 24);
            this.button1.TabIndex = 6;
            this.button1.Tag = "Set Default OLAP Connection String";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnDefaultConnStr_Click);
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
            this.gvMdxresult.Size = new System.Drawing.Size(876, 245);
            this.gvMdxresult.TabIndex = 4;
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(3, 3);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(876, 20);
            this.txtResult.TabIndex = 5;
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.tableLayoutPanelMdx);
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.tableLayoutresult);
            this.splitContainerEx1.Size = new System.Drawing.Size(882, 560);
            this.splitContainerEx1.SplitterDistance = 280;
            this.splitContainerEx1.TabIndex = 6;
            // 
            // tableLayoutresult
            // 
            this.tableLayoutresult.ColumnCount = 1;
            this.tableLayoutresult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutresult.Controls.Add(this.gvMdxresult, 0, 1);
            this.tableLayoutresult.Controls.Add(this.txtResult, 0, 0);
            this.tableLayoutresult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutresult.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutresult.Name = "tableLayoutresult";
            this.tableLayoutresult.RowCount = 2;
            this.tableLayoutresult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutresult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutresult.Size = new System.Drawing.Size(882, 276);
            this.tableLayoutresult.TabIndex = 0;
            // 
            // MdxExecuterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "MdxExecuterCtrl";
            this.Size = new System.Drawing.Size(882, 560);
            this.Load += new System.EventHandler(this.MdxExecuterCtrl_Load);
            this.tableLayoutPanelMdx.ResumeLayout(false);
            this.tableLayoutPanelMdx.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMdxresult)).EndInit();
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.tableLayoutresult.ResumeLayout(false);
            this.tableLayoutresult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl txtMdx;
        private System.Windows.Forms.DataGridView gvMdxresult;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnExecuteDataSet;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMdx;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnDefaultConnStr;
        private System.Windows.Forms.Button btnExportExcel;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutresult;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button button1;

    }
}
