namespace Justin.BI.DBLibrary.Controls
{
    partial class OperandCtrl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbSourceInfo = new System.Windows.Forms.TabControl();
            this.tbList = new System.Windows.Forms.TabPage();
            this.txtSourceList = new System.Windows.Forms.RichTextBox();
            this.tbRangeAndSequence = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMaxValue = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.tbFromTable = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRefTableName = new System.Windows.Forms.TextBox();
            this.txtRefFieldName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.tanOtherField = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.cBoxOtherFieldName = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cBoxValueCategroy = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tbSourceInfo.SuspendLayout();
            this.tbList.SuspendLayout();
            this.tbRangeAndSequence.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tbFromTable.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tanOtherField.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbSourceInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cBoxValueCategroy, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.2069F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.79311F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 230);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tbSourceInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbSourceInfo, 2);
            this.tbSourceInfo.Controls.Add(this.tbList);
            this.tbSourceInfo.Controls.Add(this.tbRangeAndSequence);
            this.tbSourceInfo.Controls.Add(this.tbFromTable);
            this.tbSourceInfo.Controls.Add(this.tanOtherField);
            this.tbSourceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSourceInfo.Location = new System.Drawing.Point(3, 28);
            this.tbSourceInfo.Name = "tbSourceInfo";
            this.tbSourceInfo.SelectedIndex = 0;
            this.tbSourceInfo.Size = new System.Drawing.Size(338, 199);
            this.tbSourceInfo.TabIndex = 18;
            // 
            // tbList
            // 
            this.tbList.Controls.Add(this.txtSourceList);
            this.tbList.Location = new System.Drawing.Point(4, 22);
            this.tbList.Name = "tbList";
            this.tbList.Padding = new System.Windows.Forms.Padding(3);
            this.tbList.Size = new System.Drawing.Size(330, 173);
            this.tbList.TabIndex = 0;
            this.tbList.Text = "List";
            this.tbList.UseVisualStyleBackColor = true;
            // 
            // txtSourceList
            // 
            this.txtSourceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceList.Location = new System.Drawing.Point(3, 3);
            this.txtSourceList.Name = "txtSourceList";
            this.txtSourceList.Size = new System.Drawing.Size(324, 167);
            this.txtSourceList.TabIndex = 11;
            this.txtSourceList.Text = "";
            // 
            // tbRangeAndSequence
            // 
            this.tbRangeAndSequence.Controls.Add(this.tableLayoutPanel9);
            this.tbRangeAndSequence.Location = new System.Drawing.Point(4, 22);
            this.tbRangeAndSequence.Name = "tbRangeAndSequence";
            this.tbRangeAndSequence.Padding = new System.Windows.Forms.Padding(3);
            this.tbRangeAndSequence.Size = new System.Drawing.Size(330, 173);
            this.tbRangeAndSequence.TabIndex = 1;
            this.tbRangeAndSequence.Text = "Range&Sequence";
            this.tbRangeAndSequence.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.txtSeed, 1, 2);
            this.tableLayoutPanel9.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.txtMaxValue, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.txtMinValue, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.txtFormat, 1, 3);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 5;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(324, 167);
            this.tableLayoutPanel9.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "MinValue：";
            // 
            // txtSeed
            // 
            this.txtSeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSeed.Location = new System.Drawing.Point(83, 43);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(238, 20);
            this.txtSeed.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 9;
            this.label12.Text = "Seed：";
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaxValue.Location = new System.Drawing.Point(83, 23);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(238, 20);
            this.txtMaxValue.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.TabIndex = 10;
            this.label13.Text = "MaxValue：";
            // 
            // txtMinValue
            // 
            this.txtMinValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMinValue.Location = new System.Drawing.Point(83, 3);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(238, 20);
            this.txtMinValue.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Format:";
            // 
            // txtFormat
            // 
            this.txtFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormat.Location = new System.Drawing.Point(83, 63);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(238, 20);
            this.txtFormat.TabIndex = 15;
            // 
            // tbFromTable
            // 
            this.tbFromTable.Controls.Add(this.tableLayoutPanel6);
            this.tbFromTable.Location = new System.Drawing.Point(4, 22);
            this.tbFromTable.Name = "tbFromTable";
            this.tbFromTable.Size = new System.Drawing.Size(330, 173);
            this.tbFromTable.TabIndex = 3;
            this.tbFromTable.Text = "FromTable";
            this.tbFromTable.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.txtRefTableName, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtRefFieldName, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.txtFilter, 1, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(330, 173);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "RefTable";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "RefField";
            // 
            // txtRefTableName
            // 
            this.txtRefTableName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRefTableName.Location = new System.Drawing.Point(91, 3);
            this.txtRefTableName.Name = "txtRefTableName";
            this.txtRefTableName.Size = new System.Drawing.Size(236, 20);
            this.txtRefTableName.TabIndex = 1;
            // 
            // txtRefFieldName
            // 
            this.txtRefFieldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRefFieldName.Location = new System.Drawing.Point(91, 23);
            this.txtRefFieldName.Name = "txtRefFieldName";
            this.txtRefFieldName.Size = new System.Drawing.Size(236, 20);
            this.txtRefFieldName.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilter.Location = new System.Drawing.Point(91, 43);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(236, 20);
            this.txtFilter.TabIndex = 3;
            // 
            // tanOtherField
            // 
            this.tanOtherField.Controls.Add(this.tableLayoutPanel10);
            this.tanOtherField.Location = new System.Drawing.Point(4, 22);
            this.tanOtherField.Name = "tanOtherField";
            this.tanOtherField.Size = new System.Drawing.Size(330, 173);
            this.tanOtherField.TabIndex = 4;
            this.tanOtherField.Text = "OtherField";
            this.tanOtherField.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.cBoxOtherFieldName, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 5;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(330, 173);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "Field：";
            // 
            // cBoxOtherFieldName
            // 
            this.cBoxOtherFieldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxOtherFieldName.FormattingEnabled = true;
            this.cBoxOtherFieldName.Location = new System.Drawing.Point(83, 3);
            this.cBoxOtherFieldName.Name = "cBoxOtherFieldName";
            this.cBoxOtherFieldName.Size = new System.Drawing.Size(244, 21);
            this.cBoxOtherFieldName.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 25);
            this.label15.TabIndex = 14;
            this.label15.Text = "取值类型：";
            // 
            // cBoxValueCategroy
            // 
            this.cBoxValueCategroy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxValueCategroy.FormattingEnabled = true;
            this.cBoxValueCategroy.Items.AddRange(new object[] {
            "Range",
            "List",
            "Sequence",
            "FromTable",
            "OtherField"});
            this.cBoxValueCategroy.Location = new System.Drawing.Point(83, 3);
            this.cBoxValueCategroy.Name = "cBoxValueCategroy";
            this.cBoxValueCategroy.Size = new System.Drawing.Size(258, 21);
            this.cBoxValueCategroy.TabIndex = 15;
            this.cBoxValueCategroy.SelectedIndexChanged += new System.EventHandler(this.cBoxValueCategroy_SelectedIndexChanged);
            // 
            // OperandCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OperandCtrl";
            this.Size = new System.Drawing.Size(344, 230);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tbSourceInfo.ResumeLayout(false);
            this.tbList.ResumeLayout(false);
            this.tbRangeAndSequence.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tbFromTable.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tanOtherField.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tbSourceInfo;
        private System.Windows.Forms.TabPage tbList;
        private System.Windows.Forms.RichTextBox txtSourceList;
        private System.Windows.Forms.TabPage tbRangeAndSequence;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMaxValue;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMinValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.TabPage tbFromTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRefTableName;
        private System.Windows.Forms.TextBox txtRefFieldName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TabPage tanOtherField;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cBoxOtherFieldName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cBoxValueCategroy;


    }
}
