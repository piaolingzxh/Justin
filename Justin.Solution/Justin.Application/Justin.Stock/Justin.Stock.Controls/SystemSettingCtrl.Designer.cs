namespace Justin.Stock.Controls
{
    partial class SystemSettingCtrl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemSettingCtrl));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBalance = new System.Windows.Forms.Button();
            this.btnDeskDisplayFormat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesktopDisplayFormat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnShowWarn = new System.Windows.Forms.Button();
            this.checkBoxShowWarn = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.checkBoxCheckTime = new System.Windows.Forms.CheckBox();
            this.btnCheckTime = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.btnDBPath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.btnAutoStart = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "可用余额：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBalance
            // 
            this.txtBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBalance.Location = new System.Drawing.Point(103, 3);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(208, 20);
            this.txtBalance.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.txtBalance, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBalance, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeskDisplayFormat, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDesktopDisplayFormat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnShowWarn, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxShowWarn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxCheckTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCheckTime, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtDBPath, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDBPath, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxAutoStart, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnAutoStart, 2, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(354, 301);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnBalance
            // 
            this.btnBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBalance.Location = new System.Drawing.Point(317, 3);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(34, 24);
            this.btnBalance.TabIndex = 2;
            this.btnBalance.Text = "→";
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // btnDeskDisplayFormat
            // 
            this.btnDeskDisplayFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeskDisplayFormat.Location = new System.Drawing.Point(317, 33);
            this.btnDeskDisplayFormat.Name = "btnDeskDisplayFormat";
            this.btnDeskDisplayFormat.Size = new System.Drawing.Size(34, 24);
            this.btnDeskDisplayFormat.TabIndex = 3;
            this.btnDeskDisplayFormat.Text = "→";
            this.btnDeskDisplayFormat.UseVisualStyleBackColor = true;
            this.btnDeskDisplayFormat.Click += new System.EventHandler(this.btnDeskDisplayFormat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "桌面显示格式：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDesktopDisplayFormat
            // 
            this.txtDesktopDisplayFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesktopDisplayFormat.Location = new System.Drawing.Point(103, 33);
            this.txtDesktopDisplayFormat.Name = "txtDesktopDisplayFormat";
            this.txtDesktopDisplayFormat.Size = new System.Drawing.Size(208, 20);
            this.txtDesktopDisplayFormat.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "报警：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnShowWarn
            // 
            this.btnShowWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShowWarn.Location = new System.Drawing.Point(317, 63);
            this.btnShowWarn.Name = "btnShowWarn";
            this.btnShowWarn.Size = new System.Drawing.Size(34, 24);
            this.btnShowWarn.TabIndex = 8;
            this.btnShowWarn.Text = "→";
            this.btnShowWarn.UseVisualStyleBackColor = true;
            this.btnShowWarn.Click += new System.EventHandler(this.btnShowWarn_Click);
            // 
            // checkBoxShowWarn
            // 
            this.checkBoxShowWarn.AutoSize = true;
            this.checkBoxShowWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowWarn.Location = new System.Drawing.Point(103, 63);
            this.checkBoxShowWarn.Name = "checkBoxShowWarn";
            this.checkBoxShowWarn.Size = new System.Drawing.Size(208, 24);
            this.checkBoxShowWarn.TabIndex = 9;
            this.checkBoxShowWarn.Text = "报警";
            this.checkBoxShowWarn.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(317, 273);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(34, 24);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "→";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // checkBoxCheckTime
            // 
            this.checkBoxCheckTime.AutoSize = true;
            this.checkBoxCheckTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxCheckTime.Location = new System.Drawing.Point(103, 93);
            this.checkBoxCheckTime.Name = "checkBoxCheckTime";
            this.checkBoxCheckTime.Size = new System.Drawing.Size(208, 24);
            this.checkBoxCheckTime.TabIndex = 11;
            this.checkBoxCheckTime.Text = "时间检查";
            this.checkBoxCheckTime.UseVisualStyleBackColor = true;
            // 
            // btnCheckTime
            // 
            this.btnCheckTime.Location = new System.Drawing.Point(317, 93);
            this.btnCheckTime.Name = "btnCheckTime";
            this.btnCheckTime.Size = new System.Drawing.Size(34, 24);
            this.btnCheckTime.TabIndex = 12;
            this.btnCheckTime.Text = "→";
            this.btnCheckTime.UseVisualStyleBackColor = true;
            this.btnCheckTime.Click += new System.EventHandler(this.btnCheckTime_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 30);
            this.label4.TabIndex = 13;
            this.label4.Text = "DB：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDBPath
            // 
            this.txtDBPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDBPath.Location = new System.Drawing.Point(103, 123);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(208, 20);
            this.txtDBPath.TabIndex = 14;
            // 
            // btnDBPath
            // 
            this.btnDBPath.Location = new System.Drawing.Point(317, 123);
            this.btnDBPath.Name = "btnDBPath";
            this.btnDBPath.Size = new System.Drawing.Size(34, 24);
            this.btnDBPath.TabIndex = 10;
            this.btnDBPath.Text = "→";
            this.btnDBPath.UseVisualStyleBackColor = true;
            this.btnDBPath.Click += new System.EventHandler(this.btnDBPath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 30);
            this.label5.TabIndex = 15;
            this.label5.Text = "开机启动：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(103, 153);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(208, 24);
            this.checkBoxAutoStart.TabIndex = 16;
            this.checkBoxAutoStart.Text = "AutoStart";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            // 
            // btnAutoStart
            // 
            this.btnAutoStart.Location = new System.Drawing.Point(317, 153);
            this.btnAutoStart.Name = "btnAutoStart";
            this.btnAutoStart.Size = new System.Drawing.Size(34, 23);
            this.btnAutoStart.TabIndex = 17;
            this.btnAutoStart.Text = "→";
            this.btnAutoStart.UseVisualStyleBackColor = true;
            this.btnAutoStart.Click += new System.EventHandler(this.btnAutoStart_Click);
            // 
            // SystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 301);
            this.Controls.Add(this.tableLayoutPanel1);

            this.Name = "SystemSetting";
            this.Text = "SystemSetting";
            this.Load += new System.EventHandler(this.SystemSetting_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDesktopDisplayFormat;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.Button btnDeskDisplayFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShowWarn;
        private System.Windows.Forms.CheckBox checkBoxShowWarn;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox checkBoxCheckTime;
        private System.Windows.Forms.Button btnCheckTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Button btnDBPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
        private System.Windows.Forms.Button btnAutoStart;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}