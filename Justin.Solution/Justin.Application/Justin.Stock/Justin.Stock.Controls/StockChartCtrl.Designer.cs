namespace Justin.Stock
{
    partial class StockChartCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbTransChart = new System.Windows.Forms.TabControl();
            this.tbTimeSheet0 = new System.Windows.Forms.TabPage();
            this.picTimeSheet0Trans = new System.Windows.Forms.PictureBox();
            this.tbTimeSheet = new System.Windows.Forms.TabPage();
            this.picTimeSheetTrans = new System.Windows.Forms.PictureBox();
            this.tbDayTrans = new System.Windows.Forms.TabPage();
            this.picDayTrans = new System.Windows.Forms.PictureBox();
            this.tbWeekTrans = new System.Windows.Forms.TabPage();
            this.picWeekTrans = new System.Windows.Forms.PictureBox();
            this.tbMonthTrans = new System.Windows.Forms.TabPage();
            this.picMonthTrans = new System.Windows.Forms.PictureBox();
            this.tbKLine = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnRefreshChart = new System.Windows.Forms.Button();
            this.tbTransChart.SuspendLayout();
            this.tbTimeSheet0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTimeSheet0Trans)).BeginInit();
            this.tbTimeSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTimeSheetTrans)).BeginInit();
            this.tbDayTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDayTrans)).BeginInit();
            this.tbWeekTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWeekTrans)).BeginInit();
            this.tbMonthTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMonthTrans)).BeginInit();
            this.tbKLine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTransChart
            // 
            this.tbTransChart.Controls.Add(this.tbTimeSheet0);
            this.tbTransChart.Controls.Add(this.tbTimeSheet);
            this.tbTransChart.Controls.Add(this.tbDayTrans);
            this.tbTransChart.Controls.Add(this.tbWeekTrans);
            this.tbTransChart.Controls.Add(this.tbMonthTrans);
            this.tbTransChart.Controls.Add(this.tbKLine);
            this.tbTransChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTransChart.Location = new System.Drawing.Point(0, 0);
            this.tbTransChart.Name = "tbTransChart";
            this.tbTransChart.SelectedIndex = 0;
            this.tbTransChart.Size = new System.Drawing.Size(542, 400);
            this.tbTransChart.TabIndex = 2;
            this.tbTransChart.SelectedIndexChanged += new System.EventHandler(this.tbTransChart_SelectedIndexChanged);
            // 
            // tbTimeSheet0
            // 
            this.tbTimeSheet0.Controls.Add(this.picTimeSheet0Trans);
            this.tbTimeSheet0.Location = new System.Drawing.Point(4, 22);
            this.tbTimeSheet0.Name = "tbTimeSheet0";
            this.tbTimeSheet0.Size = new System.Drawing.Size(534, 374);
            this.tbTimeSheet0.TabIndex = 5;
            this.tbTimeSheet0.Text = "盘前";
            this.tbTimeSheet0.UseVisualStyleBackColor = true;
            // 
            // picTimeSheet0Trans
            // 
            this.picTimeSheet0Trans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTimeSheet0Trans.Location = new System.Drawing.Point(0, 0);
            this.picTimeSheet0Trans.Name = "picTimeSheet0Trans";
            this.picTimeSheet0Trans.Size = new System.Drawing.Size(534, 374);
            this.picTimeSheet0Trans.TabIndex = 2;
            this.picTimeSheet0Trans.TabStop = false;
            // 
            // tbTimeSheet
            // 
            this.tbTimeSheet.Controls.Add(this.picTimeSheetTrans);
            this.tbTimeSheet.Location = new System.Drawing.Point(4, 22);
            this.tbTimeSheet.Name = "tbTimeSheet";
            this.tbTimeSheet.Padding = new System.Windows.Forms.Padding(3);
            this.tbTimeSheet.Size = new System.Drawing.Size(534, 374);
            this.tbTimeSheet.TabIndex = 0;
            this.tbTimeSheet.Text = "分时";
            this.tbTimeSheet.UseVisualStyleBackColor = true;
            // 
            // picTimeSheetTrans
            // 
            this.picTimeSheetTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTimeSheetTrans.Location = new System.Drawing.Point(3, 3);
            this.picTimeSheetTrans.Name = "picTimeSheetTrans";
            this.picTimeSheetTrans.Size = new System.Drawing.Size(528, 368);
            this.picTimeSheetTrans.TabIndex = 1;
            this.picTimeSheetTrans.TabStop = false;
            // 
            // tbDayTrans
            // 
            this.tbDayTrans.Controls.Add(this.picDayTrans);
            this.tbDayTrans.Location = new System.Drawing.Point(4, 22);
            this.tbDayTrans.Name = "tbDayTrans";
            this.tbDayTrans.Padding = new System.Windows.Forms.Padding(3);
            this.tbDayTrans.Size = new System.Drawing.Size(534, 374);
            this.tbDayTrans.TabIndex = 1;
            this.tbDayTrans.Text = "日线";
            this.tbDayTrans.UseVisualStyleBackColor = true;
            // 
            // picDayTrans
            // 
            this.picDayTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDayTrans.Location = new System.Drawing.Point(3, 3);
            this.picDayTrans.Name = "picDayTrans";
            this.picDayTrans.Size = new System.Drawing.Size(528, 368);
            this.picDayTrans.TabIndex = 0;
            this.picDayTrans.TabStop = false;
            // 
            // tbWeekTrans
            // 
            this.tbWeekTrans.Controls.Add(this.picWeekTrans);
            this.tbWeekTrans.Location = new System.Drawing.Point(4, 22);
            this.tbWeekTrans.Name = "tbWeekTrans";
            this.tbWeekTrans.Size = new System.Drawing.Size(534, 374);
            this.tbWeekTrans.TabIndex = 2;
            this.tbWeekTrans.Text = "周线";
            this.tbWeekTrans.UseVisualStyleBackColor = true;
            // 
            // picWeekTrans
            // 
            this.picWeekTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWeekTrans.Location = new System.Drawing.Point(0, 0);
            this.picWeekTrans.Name = "picWeekTrans";
            this.picWeekTrans.Size = new System.Drawing.Size(534, 374);
            this.picWeekTrans.TabIndex = 0;
            this.picWeekTrans.TabStop = false;
            // 
            // tbMonthTrans
            // 
            this.tbMonthTrans.Controls.Add(this.picMonthTrans);
            this.tbMonthTrans.Location = new System.Drawing.Point(4, 22);
            this.tbMonthTrans.Name = "tbMonthTrans";
            this.tbMonthTrans.Size = new System.Drawing.Size(534, 374);
            this.tbMonthTrans.TabIndex = 3;
            this.tbMonthTrans.Text = "月线";
            this.tbMonthTrans.UseVisualStyleBackColor = true;
            // 
            // picMonthTrans
            // 
            this.picMonthTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMonthTrans.Location = new System.Drawing.Point(0, 0);
            this.picMonthTrans.Name = "picMonthTrans";
            this.picMonthTrans.Size = new System.Drawing.Size(534, 374);
            this.picMonthTrans.TabIndex = 0;
            this.picMonthTrans.TabStop = false;
            // 
            // tbKLine
            // 
            this.tbKLine.Controls.Add(this.tableLayoutPanel1);
            this.tbKLine.Location = new System.Drawing.Point(4, 22);
            this.tbKLine.Name = "tbKLine";
            this.tbKLine.Size = new System.Drawing.Size(534, 374);
            this.tbKLine.TabIndex = 4;
            this.tbKLine.Text = "图表";
            this.tbKLine.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRefreshChart, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 374);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 28);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(528, 343);
            this.webBrowser1.TabIndex = 0;
            // 
            // btnRefreshChart
            // 
            this.btnRefreshChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefreshChart.Location = new System.Drawing.Point(3, 3);
            this.btnRefreshChart.Name = "btnRefreshChart";
            this.btnRefreshChart.Size = new System.Drawing.Size(528, 19);
            this.btnRefreshChart.TabIndex = 1;
            this.btnRefreshChart.Text = "→";
            this.btnRefreshChart.UseVisualStyleBackColor = true;
            this.btnRefreshChart.Click += new System.EventHandler(this.btnRefreshChart_Click);
            // 
            // StockChartCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTransChart);
            this.Name = "StockChartCtrl";
            this.Size = new System.Drawing.Size(542, 400);
            this.tbTransChart.ResumeLayout(false);
            this.tbTimeSheet0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTimeSheet0Trans)).EndInit();
            this.tbTimeSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTimeSheetTrans)).EndInit();
            this.tbDayTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDayTrans)).EndInit();
            this.tbWeekTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWeekTrans)).EndInit();
            this.tbMonthTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMonthTrans)).EndInit();
            this.tbKLine.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbTransChart;
        private System.Windows.Forms.TabPage tbTimeSheet0;
        private System.Windows.Forms.PictureBox picTimeSheet0Trans;
        private System.Windows.Forms.TabPage tbTimeSheet;
        private System.Windows.Forms.PictureBox picTimeSheetTrans;
        private System.Windows.Forms.TabPage tbDayTrans;
        private System.Windows.Forms.PictureBox picDayTrans;
        private System.Windows.Forms.TabPage tbWeekTrans;
        private System.Windows.Forms.PictureBox picWeekTrans;
        private System.Windows.Forms.TabPage tbMonthTrans;
        private System.Windows.Forms.PictureBox picMonthTrans;
        private System.Windows.Forms.TabPage tbKLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnRefreshChart;
    }
}
