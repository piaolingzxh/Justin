using Justin.Controls.TestDataGenerator;
namespace Justin.Controls.TestDataGenerator
{
    partial class TableConfigCtrl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableConfigCtrl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExecuteTableSQL = new System.Windows.Forms.Button();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.btnGenerateData = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvDst = new System.Windows.Forms.TreeView();
            this.tvDstMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visualeMenuItemOfDst = new System.Windows.Forms.ToolStripMenuItem();
            this.addFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabField = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxVisible = new System.Windows.Forms.ComboBox();
            this.cBoxValueType = new System.Windows.Forms.ComboBox();
            this.lbFieldName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cBoxOperator = new System.Windows.Forms.ComboBox();
            this.SaveFieldInfo = new System.Windows.Forms.Button();
            this.operandCtrl1 = new Justin.Controls.TestDataGenerator.OperandCtrl();
            this.operandCtrl2 = new Justin.Controls.TestDataGenerator.OperandCtrl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDataCount = new System.Windows.Forms.TextBox();
            this.btnSaveTable = new System.Windows.Forms.Button();
            this.lbDataCount = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBeforeSQL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAfterSQL = new ICSharpCode.TextEditor.TextEditorControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tvDstMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabField.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabTable.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 520);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnExecuteTableSQL, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveSetting, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGenerateData, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(735, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(40, 514);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // btnExecuteTableSQL
            // 
            this.btnExecuteTableSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteTableSQL.Image = global::Justin.Controls.TestDataGenerator.Properties.Resources.execute;
            this.btnExecuteTableSQL.Location = new System.Drawing.Point(3, 387);
            this.btnExecuteTableSQL.Name = "btnExecuteTableSQL";
            this.btnExecuteTableSQL.Size = new System.Drawing.Size(34, 124);
            this.btnExecuteTableSQL.TabIndex = 15;
            this.btnExecuteTableSQL.Tag = "根据表及所有字段设置生成测试数据SQL";
            this.btnExecuteTableSQL.UseVisualStyleBackColor = true;
            this.btnExecuteTableSQL.Click += new System.EventHandler(this.btnExecuteTableSQL_Click);
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveSetting.Image = global::Justin.Controls.TestDataGenerator.Properties.Resources.xml;
            this.btnSaveSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(34, 122);
            this.btnSaveSetting.TabIndex = 13;
            this.btnSaveSetting.Tag = "保存该表的所有设置";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // btnGenerateData
            // 
            this.btnGenerateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateData.Image = global::Justin.Controls.TestDataGenerator.Properties.Resources.sql2;
            this.btnGenerateData.Location = new System.Drawing.Point(3, 131);
            this.btnGenerateData.Name = "btnGenerateData";
            this.btnGenerateData.Size = new System.Drawing.Size(34, 122);
            this.btnGenerateData.TabIndex = 14;
            this.btnGenerateData.Tag = "根据表及所有字段设置生成测试数据SQL";
            this.btnGenerateData.UseVisualStyleBackColor = true;
            this.btnGenerateData.Click += new System.EventHandler(this.btnGenerateData_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvDst);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(726, 514);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 20;
            // 
            // tvDst
            // 
            this.tvDst.ContextMenuStrip = this.tvDstMenu;
            this.tvDst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDst.ImageIndex = 0;
            this.tvDst.ImageList = this.imageList1;
            this.tvDst.Location = new System.Drawing.Point(0, 0);
            this.tvDst.Name = "tvDst";
            this.tvDst.SelectedImageIndex = 0;
            this.tvDst.Size = new System.Drawing.Size(165, 514);
            this.tvDst.TabIndex = 0;
            this.tvDst.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDst_AfterSelect);
            this.tvDst.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDst_NodeMouseClick);
            // 
            // tvDstMenu
            // 
            this.tvDstMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualeMenuItemOfDst,
            this.addFieldToolStripMenuItem,
            this.deleteFieldToolStripMenuItem});
            this.tvDstMenu.Name = "tvDstMenu";
            this.tvDstMenu.Size = new System.Drawing.Size(133, 70);
            // 
            // visualeMenuItemOfDst
            // 
            this.visualeMenuItemOfDst.Name = "visualeMenuItemOfDst";
            this.visualeMenuItemOfDst.Size = new System.Drawing.Size(132, 22);
            this.visualeMenuItemOfDst.Text = "Visible";
            this.visualeMenuItemOfDst.Click += new System.EventHandler(this.visualeMenuItemOfDst_Click);
            // 
            // addFieldToolStripMenuItem
            // 
            this.addFieldToolStripMenuItem.Name = "addFieldToolStripMenuItem";
            this.addFieldToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.addFieldToolStripMenuItem.Text = "AddField";
            this.addFieldToolStripMenuItem.Click += new System.EventHandler(this.addFieldToolStripMenuItem_Click);
            // 
            // deleteFieldToolStripMenuItem
            // 
            this.deleteFieldToolStripMenuItem.Name = "deleteFieldToolStripMenuItem";
            this.deleteFieldToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.deleteFieldToolStripMenuItem.Text = "DeleteField";
            this.deleteFieldToolStripMenuItem.Click += new System.EventHandler(this.deleteFieldToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table.ico");
            this.imageList1.Images.SetKeyName(1, "Field.png");
            this.imageList1.Images.SetKeyName(2, "date.png");
            this.imageList1.Images.SetKeyName(3, "num.ico");
            this.imageList1.Images.SetKeyName(4, "GTP_date.png");
            this.imageList1.Images.SetKeyName(5, "GTP_number.png");
            this.imageList1.Images.SetKeyName(6, "GTP_wait.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabField);
            this.tabControl1.Controls.Add(this.tabTable);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(557, 514);
            this.tabControl1.TabIndex = 1;
            // 
            // tabField
            // 
            this.tabField.Controls.Add(this.panel1);
            this.tabField.ImageIndex = 1;
            this.tabField.Location = new System.Drawing.Point(4, 23);
            this.tabField.Name = "tabField";
            this.tabField.Padding = new System.Windows.Forms.Padding(3);
            this.tabField.Size = new System.Drawing.Size(549, 487);
            this.tabField.TabIndex = 0;
            this.tabField.Text = "Field";
            this.tabField.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 481);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(543, 481);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel4.SetColumnSpan(this.tableLayoutPanel6, 2);
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cBoxVisible, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.cBoxValueType, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.lbFieldName, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(537, 57);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "FieldName：";
            // 
            // cBoxVisible
            // 
            this.cBoxVisible.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxVisible.FormattingEnabled = true;
            this.cBoxVisible.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cBoxVisible.Location = new System.Drawing.Point(83, 30);
            this.cBoxVisible.Name = "cBoxVisible";
            this.cBoxVisible.Size = new System.Drawing.Size(182, 21);
            this.cBoxVisible.TabIndex = 4;
            // 
            // cBoxValueType
            // 
            this.cBoxValueType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxValueType.FormattingEnabled = true;
            this.cBoxValueType.Items.AddRange(new object[] {
            "Numeric",
            "String",
            "DateTime"});
            this.cBoxValueType.Location = new System.Drawing.Point(351, 3);
            this.cBoxValueType.Name = "cBoxValueType";
            this.cBoxValueType.Size = new System.Drawing.Size(183, 21);
            this.cBoxValueType.TabIndex = 3;
            // 
            // lbFieldName
            // 
            this.lbFieldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFieldName.Location = new System.Drawing.Point(83, 3);
            this.lbFieldName.Name = "lbFieldName";
            this.lbFieldName.ReadOnly = true;
            this.lbFieldName.Size = new System.Drawing.Size(182, 20);
            this.lbFieldName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(271, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "ValueType：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 30);
            this.label4.TabIndex = 5;
            this.label4.Text = "Visible：";
            // 
            // groupBox1
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 412);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SourceInfo";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.cBoxOperator, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.SaveFieldInfo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.operandCtrl1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.operandCtrl2, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(531, 393);
            this.tableLayoutPanel3.TabIndex = 18;
            // 
            // cBoxOperator
            // 
            this.cBoxOperator.FormattingEnabled = true;
            this.cBoxOperator.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/",
            "%"});
            this.cBoxOperator.Location = new System.Drawing.Point(243, 3);
            this.cBoxOperator.Name = "cBoxOperator";
            this.cBoxOperator.Size = new System.Drawing.Size(44, 21);
            this.cBoxOperator.TabIndex = 6;
            // 
            // SaveFieldInfo
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.SaveFieldInfo, 3);
            this.SaveFieldInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveFieldInfo.Image = global::Justin.Controls.TestDataGenerator.Properties.Resources.save;
            this.SaveFieldInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SaveFieldInfo.Location = new System.Drawing.Point(3, 361);
            this.SaveFieldInfo.Name = "SaveFieldInfo";
            this.SaveFieldInfo.Size = new System.Drawing.Size(525, 29);
            this.SaveFieldInfo.TabIndex = 8;
            this.SaveFieldInfo.Tag = "应用该字段的设置";
            this.SaveFieldInfo.Text = "Apply Field Setting";
            this.SaveFieldInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveFieldInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SaveFieldInfo.UseVisualStyleBackColor = true;
            this.SaveFieldInfo.Click += new System.EventHandler(this.btnSaveFieldInfo_Click);
            // 
            // operandCtrl1
            // 
            this.operandCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operandCtrl1.Location = new System.Drawing.Point(3, 3);
            this.operandCtrl1.Name = "operandCtrl1";
            this.operandCtrl1.Size = new System.Drawing.Size(234, 352);
            this.operandCtrl1.TabIndex = 5;
            // 
            // operandCtrl2
            // 
            this.operandCtrl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operandCtrl2.Location = new System.Drawing.Point(293, 3);
            this.operandCtrl2.Name = "operandCtrl2";
            this.operandCtrl2.Size = new System.Drawing.Size(235, 352);
            this.operandCtrl2.TabIndex = 7;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.tableLayoutPanel5);
            this.tabTable.ImageIndex = 0;
            this.tabTable.Location = new System.Drawing.Point(4, 23);
            this.tabTable.Name = "tabTable";
            this.tabTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabTable.Size = new System.Drawing.Size(549, 487);
            this.tabTable.TabIndex = 1;
            this.tabTable.Text = "Table";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.txtDataCount, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSaveTable, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lbDataCount, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(543, 481);
            this.tableLayoutPanel5.TabIndex = 17;
            // 
            // txtDataCount
            // 
            this.txtDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDataCount.Location = new System.Drawing.Point(83, 3);
            this.txtDataCount.Name = "txtDataCount";
            this.txtDataCount.Size = new System.Drawing.Size(457, 20);
            this.txtDataCount.TabIndex = 9;
            // 
            // btnSaveTable
            // 
            this.btnSaveTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveTable.Image = global::Justin.Controls.TestDataGenerator.Properties.Resources.save;
            this.btnSaveTable.Location = new System.Drawing.Point(83, 453);
            this.btnSaveTable.Name = "btnSaveTable";
            this.btnSaveTable.Size = new System.Drawing.Size(457, 25);
            this.btnSaveTable.TabIndex = 12;
            this.btnSaveTable.Tag = "应用该表信息设置（不包括字段）";
            this.btnSaveTable.Text = " Apply Table Setting";
            this.btnSaveTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveTable.UseVisualStyleBackColor = true;
            this.btnSaveTable.Click += new System.EventHandler(this.btnSaveTable_Click);
            // 
            // lbDataCount
            // 
            this.lbDataCount.AutoSize = true;
            this.lbDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDataCount.Location = new System.Drawing.Point(3, 0);
            this.lbDataCount.Name = "lbDataCount";
            this.lbDataCount.Size = new System.Drawing.Size(74, 20);
            this.lbDataCount.TabIndex = 15;
            this.lbDataCount.Text = "DataCount：";
            // 
            // splitContainer2
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.splitContainer2, 2);
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 23);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel8);
            this.tableLayoutPanel5.SetRowSpan(this.splitContainer2, 2);
            this.splitContainer2.Size = new System.Drawing.Size(537, 424);
            this.splitContainer2.SplitterDistance = 182;
            this.splitContainer2.TabIndex = 21;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtBeforeSQL, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(537, 182);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "BeforeSQL:";
            // 
            // txtBeforeSQL
            // 
            this.txtBeforeSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeforeSQL.Location = new System.Drawing.Point(83, 3);
            this.txtBeforeSQL.Name = "txtBeforeSQL";
            this.txtBeforeSQL.ShowEOLMarkers = true;
            this.txtBeforeSQL.ShowSpaces = true;
            this.txtBeforeSQL.ShowTabs = true;
            this.txtBeforeSQL.ShowVRuler = true;
            this.txtBeforeSQL.Size = new System.Drawing.Size(451, 176);
            this.txtBeforeSQL.TabIndex = 10;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtAfterSQL, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(537, 238);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "AfterSQL:";
            // 
            // txtAfterSQL
            // 
            this.txtAfterSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAfterSQL.Location = new System.Drawing.Point(83, 3);
            this.txtAfterSQL.Name = "txtAfterSQL";
            this.txtAfterSQL.ShowEOLMarkers = true;
            this.txtAfterSQL.ShowSpaces = true;
            this.txtAfterSQL.ShowTabs = true;
            this.txtAfterSQL.ShowVRuler = true;
            this.txtAfterSQL.Size = new System.Drawing.Size(451, 232);
            this.txtAfterSQL.TabIndex = 11;
            // 
            // TableConfigCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TableConfigCtrl";
            this.Size = new System.Drawing.Size(778, 520);
            this.Load += new System.EventHandler(this.TableConfigCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tvDstMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabField.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExecuteTableSQL;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Button btnGenerateData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvDst;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabField;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBoxVisible;
        private System.Windows.Forms.ComboBox cBoxValueType;
        private System.Windows.Forms.TextBox lbFieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox cBoxOperator;
        private System.Windows.Forms.Button SaveFieldInfo;
        private OperandCtrl operandCtrl1;
        private OperandCtrl operandCtrl2;
        private System.Windows.Forms.TabPage tabTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox txtDataCount;
        private System.Windows.Forms.Button btnSaveTable;
        private System.Windows.Forms.Label lbDataCount;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label5;
        private ICSharpCode.TextEditor.TextEditorControl txtBeforeSQL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label7;
        private ICSharpCode.TextEditor.TextEditorControl txtAfterSQL;
        private System.Windows.Forms.ContextMenuStrip tvDstMenu;
        private System.Windows.Forms.ToolStripMenuItem visualeMenuItemOfDst;
        private System.Windows.Forms.ToolStripMenuItem addFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFieldToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}
