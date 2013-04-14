namespace Justin.Toolbox
{
    partial class TestDataGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestDataGenerator));
            this.btnDataSourceChoose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTableNameFilter = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvAllTables = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tvSource = new System.Windows.Forms.TreeView();
            this.tvSourceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TranslateMenuItemOfTVSource = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItemOfTVSource = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPrepareDBTables = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabConfigPage = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tvSourceMenu.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabConfigPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDataSourceChoose
            // 
            this.btnDataSourceChoose.Location = new System.Drawing.Point(3, 3);
            this.btnDataSourceChoose.Name = "btnDataSourceChoose";
            this.btnDataSourceChoose.Size = new System.Drawing.Size(75, 22);
            this.btnDataSourceChoose.TabIndex = 0;
            this.btnDataSourceChoose.Tag = "加载数据源，若录入表名过滤条件，将按照条件进行过滤";
            this.btnDataSourceChoose.Text = "加载数据源";
            this.btnDataSourceChoose.UseVisualStyleBackColor = true;
            this.btnDataSourceChoose.Click += new System.EventHandler(this.btnDataSourceChoose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(489, 348);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.btnDataSourceChoose, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.label18, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.txtTableNameFilter, 2, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(483, 28);
            this.tableLayoutPanel11.TabIndex = 17;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(88, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "表名";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTableNameFilter
            // 
            this.txtTableNameFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableNameFilter.Location = new System.Drawing.Point(129, 3);
            this.txtTableNameFilter.Name = "txtTableNameFilter";
            this.txtTableNameFilter.Size = new System.Drawing.Size(351, 20);
            this.txtTableNameFilter.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvAllTables);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(483, 308);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 19;
            // 
            // tvAllTables
            // 
            this.tvAllTables.CheckBoxes = true;
            this.tvAllTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAllTables.Location = new System.Drawing.Point(0, 0);
            this.tvAllTables.Name = "tvAllTables";
            this.tvAllTables.Size = new System.Drawing.Size(150, 308);
            this.tvAllTables.TabIndex = 13;
            this.tvAllTables.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAllTables_NodeMouseClick);
            this.tvAllTables.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAllTables_NodeMouseDoubleClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.tvSource, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPrepareDBTables, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(329, 308);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tvSource
            // 
            this.tvSource.ContextMenuStrip = this.tvSourceMenu;
            this.tvSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSource.Location = new System.Drawing.Point(23, 3);
            this.tvSource.Name = "tvSource";
            this.tvSource.Size = new System.Drawing.Size(818, 302);
            this.tvSource.TabIndex = 4;
            this.tvSource.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSource_NodeMouseClick);
            // 
            // tvSourceMenu
            // 
            this.tvSourceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TranslateMenuItemOfTVSource,
            this.loadMenuItemOfTVSource});
            this.tvSourceMenu.Name = "tv";
            this.tvSourceMenu.Size = new System.Drawing.Size(123, 48);
            // 
            // TranslateMenuItemOfTVSource
            // 
            this.TranslateMenuItemOfTVSource.Name = "TranslateMenuItemOfTVSource";
            this.TranslateMenuItemOfTVSource.Size = new System.Drawing.Size(122, 22);
            this.TranslateMenuItemOfTVSource.Text = "Translate";
            this.TranslateMenuItemOfTVSource.Click += new System.EventHandler(this.TranslateMenuItemOfTVSource_Click);
            // 
            // loadMenuItemOfTVSource
            // 
            this.loadMenuItemOfTVSource.Name = "loadMenuItemOfTVSource";
            this.loadMenuItemOfTVSource.Size = new System.Drawing.Size(122, 22);
            this.loadMenuItemOfTVSource.Text = "Load";
            this.loadMenuItemOfTVSource.Click += new System.EventHandler(this.loadMenuItemOfTVSource_Click);
            // 
            // btnPrepareDBTables
            // 
            this.btnPrepareDBTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrepareDBTables.Location = new System.Drawing.Point(3, 3);
            this.btnPrepareDBTables.Name = "btnPrepareDBTables";
            this.btnPrepareDBTables.Size = new System.Drawing.Size(14, 302);
            this.btnPrepareDBTables.TabIndex = 14;
            this.btnPrepareDBTables.Tag = "加载该表的字段信息";
            this.btnPrepareDBTables.Text = "→";
            this.btnPrepareDBTables.UseVisualStyleBackColor = true;
            this.btnPrepareDBTables.Click += new System.EventHandler(this.btnPrepareDBTables_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.tabControl2, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(509, 386);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabConfigPage);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(503, 380);
            this.tabControl2.TabIndex = 6;
            // 
            // tabConfigPage
            // 
            this.tabConfigPage.Controls.Add(this.tableLayoutPanel1);
            this.tabConfigPage.Location = new System.Drawing.Point(4, 22);
            this.tabConfigPage.Name = "tabConfigPage";
            this.tabConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigPage.Size = new System.Drawing.Size(495, 354);
            this.tabConfigPage.TabIndex = 0;
            this.tabConfigPage.Text = "Config";
            this.tabConfigPage.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table.ico");
            this.imageList1.Images.SetKeyName(1, "Field.png");
            this.imageList1.Images.SetKeyName(2, "pk.png");
            this.imageList1.Images.SetKeyName(3, "fk.png");
            // 
            // TestDataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 408);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestDataGenerator";
            this.Text = "SQL SERVER TEST DATA GENERATOR";
            this.Load += new System.EventHandler(this.fomr1_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel7, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tvSourceMenu.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabConfigPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDataSourceChoose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView tvSource;
        private System.Windows.Forms.TreeView tvAllTables;
        private System.Windows.Forms.Button btnPrepareDBTables;
        private System.Windows.Forms.ContextMenuStrip tvSourceMenu;
        private System.Windows.Forms.ToolStripMenuItem TranslateMenuItemOfTVSource;
        private System.Windows.Forms.ToolStripMenuItem loadMenuItemOfTVSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabConfigPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTableNameFilter;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}

