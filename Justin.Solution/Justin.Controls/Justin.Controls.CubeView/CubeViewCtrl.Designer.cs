namespace Justin.Controls.CubeView
{
    partial class CubeViewCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CubeViewCtrl));
            this.tvServerInfo = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.serverMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browerCubeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerServerAndCubeInfo = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerCubeInfo = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboxFilterType = new System.Windows.Forms.ComboBox();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilterUniqueName = new System.Windows.Forms.TextBox();
            this.btnCancelFilter = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tvCubeInfo = new System.Windows.Forms.TreeView();
            this.splitContainerDataAndMdx = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.dgvObjectInfo = new System.Windows.Forms.DataGridView();
            this.tabControlMdxEditorCollection = new System.Windows.Forms.TabControl();
            this.tabControlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveMdxInCurrentTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMdxInAllTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMdxFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageDefault = new System.Windows.Forms.TabPage();
            this.mdxExecuterCtrl1 = new Justin.Controls.Executer.MdxExecuterCtrl();
            this.cboxConnStrings = new System.Windows.Forms.ComboBox();
            this.cubeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateSampleMdxTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentCubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllCubesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllCubesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerAndCubeInfo)).BeginInit();
            this.splitContainerServerAndCubeInfo.Panel1.SuspendLayout();
            this.splitContainerServerAndCubeInfo.Panel2.SuspendLayout();
            this.splitContainerServerAndCubeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCubeInfo)).BeginInit();
            this.splitContainerCubeInfo.Panel1.SuspendLayout();
            this.splitContainerCubeInfo.Panel2.SuspendLayout();
            this.splitContainerCubeInfo.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataAndMdx)).BeginInit();
            this.splitContainerDataAndMdx.Panel1.SuspendLayout();
            this.splitContainerDataAndMdx.Panel2.SuspendLayout();
            this.splitContainerDataAndMdx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).BeginInit();
            this.tabControlMdxEditorCollection.SuspendLayout();
            this.tabControlMenu.SuspendLayout();
            this.tabPageDefault.SuspendLayout();
            this.cubeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvServerInfo
            // 
            this.tvServerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvServerInfo.ImageIndex = 0;
            this.tvServerInfo.ImageList = this.imageList1;
            this.tvServerInfo.Location = new System.Drawing.Point(0, 0);
            this.tvServerInfo.Name = "tvServerInfo";
            this.tvServerInfo.SelectedImageIndex = 0;
            this.tvServerInfo.ShowNodeToolTips = true;
            this.tvServerInfo.Size = new System.Drawing.Size(173, 395);
            this.tvServerInfo.TabIndex = 2;
            this.tvServerInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServerInfo_AfterSelect);
            this.tvServerInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvServerInfo_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CalMeasure");
            this.imageList1.Images.SetKeyName(1, "conn");
            this.imageList1.Images.SetKeyName(2, "Dim");
            this.imageList1.Images.SetKeyName(3, "Dims");
            this.imageList1.Images.SetKeyName(4, "Hie");
            this.imageList1.Images.SetKeyName(5, "Level");
            this.imageList1.Images.SetKeyName(6, "Measure");
            this.imageList1.Images.SetKeyName(7, "Self_Hie");
            this.imageList1.Images.SetKeyName(8, "Member");
            this.imageList1.Images.SetKeyName(9, "Catalog");
            this.imageList1.Images.SetKeyName(10, "Group");
            this.imageList1.Images.SetKeyName(11, "Cubes");
            this.imageList1.Images.SetKeyName(12, "Cube");
            this.imageList1.Images.SetKeyName(13, "SingleHie");
            // 
            // serverMenu
            // 
            this.serverMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browerCubeInfoToolStripMenuItem});
            this.serverMenu.Name = "serverMenu";
            this.serverMenu.Size = new System.Drawing.Size(112, 26);
            // 
            // browerCubeInfoToolStripMenuItem
            // 
            this.browerCubeInfoToolStripMenuItem.Name = "browerCubeInfoToolStripMenuItem";
            this.browerCubeInfoToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.browerCubeInfoToolStripMenuItem.Text = "Brower";
            this.browerCubeInfoToolStripMenuItem.Click += new System.EventHandler(this.browerCubeInfoToolStripMenuItem_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Image = global::Justin.Controls.CubeView.Properties.Resources.conn;
            this.btnConnect.Location = new System.Drawing.Point(849, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(44, 24);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainerMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboxConnStrings, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(896, 431);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainerMain, 2);
            this.splitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 33);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerServerAndCubeInfo);
            this.splitContainerMain.Panel1MinSize = 20;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerDataAndMdx);
            this.splitContainerMain.Size = new System.Drawing.Size(890, 395);
            this.splitContainerMain.SplitterDistance = 376;
            this.splitContainerMain.TabIndex = 4;
            // 
            // splitContainerServerAndCubeInfo
            // 
            this.splitContainerServerAndCubeInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerServerAndCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerServerAndCubeInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerServerAndCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.splitContainerServerAndCubeInfo.Name = "splitContainerServerAndCubeInfo";
            // 
            // splitContainerServerAndCubeInfo.Panel1
            // 
            this.splitContainerServerAndCubeInfo.Panel1.Controls.Add(this.tvServerInfo);
            this.splitContainerServerAndCubeInfo.Panel1MinSize = 10;
            // 
            // splitContainerServerAndCubeInfo.Panel2
            // 
            this.splitContainerServerAndCubeInfo.Panel2.Controls.Add(this.splitContainerCubeInfo);
            this.splitContainerServerAndCubeInfo.Size = new System.Drawing.Size(376, 395);
            this.splitContainerServerAndCubeInfo.SplitterDistance = 173;
            this.splitContainerServerAndCubeInfo.TabIndex = 3;
            // 
            // splitContainerCubeInfo
            // 
            this.splitContainerCubeInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCubeInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCubeInfo.Name = "splitContainerCubeInfo";
            this.splitContainerCubeInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerCubeInfo.Panel1
            // 
            this.splitContainerCubeInfo.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.splitContainerCubeInfo.Panel1MinSize = 130;
            // 
            // splitContainerCubeInfo.Panel2
            // 
            this.splitContainerCubeInfo.Panel2.Controls.Add(this.tvCubeInfo);
            this.splitContainerCubeInfo.Size = new System.Drawing.Size(199, 395);
            this.splitContainerCubeInfo.SplitterDistance = 130;
            this.splitContainerCubeInfo.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboxFilterType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtFilterName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtFilterText, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtFilterUniqueName, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnCancelFilter, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(199, 130);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "过滤类型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboxFilterType
            // 
            this.cboxFilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxFilterType.FormattingEnabled = true;
            this.cboxFilterType.Location = new System.Drawing.Point(83, 3);
            this.cboxFilterType.Name = "cboxFilterType";
            this.cboxFilterType.Size = new System.Drawing.Size(113, 21);
            this.cboxFilterType.TabIndex = 1;
            // 
            // txtFilterName
            // 
            this.txtFilterName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilterName.Location = new System.Drawing.Point(83, 28);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(113, 20);
            this.txtFilterName.TabIndex = 3;
            // 
            // txtFilterText
            // 
            this.txtFilterText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilterText.Location = new System.Drawing.Point(83, 53);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(113, 20);
            this.txtFilterText.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Text";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "UniqueName";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFilterUniqueName
            // 
            this.txtFilterUniqueName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilterUniqueName.Location = new System.Drawing.Point(83, 78);
            this.txtFilterUniqueName.Name = "txtFilterUniqueName";
            this.txtFilterUniqueName.Size = new System.Drawing.Size(113, 20);
            this.txtFilterUniqueName.TabIndex = 7;
            // 
            // btnCancelFilter
            // 
            this.btnCancelFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancelFilter.Location = new System.Drawing.Point(83, 103);
            this.btnCancelFilter.Name = "btnCancelFilter";
            this.btnCancelFilter.Size = new System.Drawing.Size(113, 24);
            this.btnCancelFilter.TabIndex = 8;
            this.btnCancelFilter.Text = "取消过滤";
            this.btnCancelFilter.UseVisualStyleBackColor = true;
            this.btnCancelFilter.Click += new System.EventHandler(this.btnCancelFilter_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilter.Location = new System.Drawing.Point(3, 103);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(74, 24);
            this.btnFilter.TabIndex = 8;
            this.btnFilter.Text = "过滤";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tvCubeInfo
            // 
            this.tvCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCubeInfo.ImageIndex = 0;
            this.tvCubeInfo.ImageList = this.imageList1;
            this.tvCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.tvCubeInfo.Name = "tvCubeInfo";
            this.tvCubeInfo.SelectedImageIndex = 0;
            this.tvCubeInfo.Size = new System.Drawing.Size(199, 261);
            this.tvCubeInfo.TabIndex = 0;
            this.tvCubeInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCubeInfo_ItemDrag);
            this.tvCubeInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCubeInfo_AfterSelect);
            this.tvCubeInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseClick);
            this.tvCubeInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseDoubleClick);
            // 
            // splitContainerDataAndMdx
            // 
            this.splitContainerDataAndMdx.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerDataAndMdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDataAndMdx.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerDataAndMdx.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDataAndMdx.Name = "splitContainerDataAndMdx";
            this.splitContainerDataAndMdx.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDataAndMdx.Panel1
            // 
            this.splitContainerDataAndMdx.Panel1.Controls.Add(this.dgvObjectInfo);
            // 
            // splitContainerDataAndMdx.Panel2
            // 
            this.splitContainerDataAndMdx.Panel2.Controls.Add(this.tabControlMdxEditorCollection);
            this.splitContainerDataAndMdx.Size = new System.Drawing.Size(510, 395);
            this.splitContainerDataAndMdx.SplitterDistance = 65;
            this.splitContainerDataAndMdx.TabIndex = 0;
            // 
            // dgvObjectInfo
            // 
            this.dgvObjectInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvObjectInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvObjectInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvObjectInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjectInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvObjectInfo.Name = "dgvObjectInfo";
            this.dgvObjectInfo.Size = new System.Drawing.Size(510, 65);
            this.dgvObjectInfo.TabIndex = 0;
            // 
            // tabControlMdxEditorCollection
            // 
            this.tabControlMdxEditorCollection.ContextMenuStrip = this.tabControlMenu;
            this.tabControlMdxEditorCollection.Controls.Add(this.tabPageDefault);
            this.tabControlMdxEditorCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMdxEditorCollection.HotTrack = true;
            this.tabControlMdxEditorCollection.Location = new System.Drawing.Point(0, 0);
            this.tabControlMdxEditorCollection.Name = "tabControlMdxEditorCollection";
            this.tabControlMdxEditorCollection.SelectedIndex = 0;
            this.tabControlMdxEditorCollection.Size = new System.Drawing.Size(510, 326);
            this.tabControlMdxEditorCollection.TabIndex = 1;
            this.tabControlMdxEditorCollection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControlMdxEditorCollection_MouseDown);
            // 
            // tabControlMenu
            // 
            this.tabControlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMdxInCurrentTabPageToolStripMenuItem,
            this.saveMdxInAllTabPageToolStripMenuItem,
            this.loadMdxFileToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.closeCurrentTabToolStripMenuItem,
            this.closeAllTabsButThisToolStripMenuItem});
            this.tabControlMenu.Name = "tabControlmenu";
            this.tabControlMenu.Size = new System.Drawing.Size(230, 136);
            // 
            // saveMdxInCurrentTabPageToolStripMenuItem
            // 
            this.saveMdxInCurrentTabPageToolStripMenuItem.Name = "saveMdxInCurrentTabPageToolStripMenuItem";
            this.saveMdxInCurrentTabPageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.saveMdxInCurrentTabPageToolStripMenuItem.Text = "Save Mdx In Current TabPage";
            this.saveMdxInCurrentTabPageToolStripMenuItem.Click += new System.EventHandler(this.saveMdxInCurrentTabPageToolStripMenuItem_Click);
            // 
            // saveMdxInAllTabPageToolStripMenuItem
            // 
            this.saveMdxInAllTabPageToolStripMenuItem.Name = "saveMdxInAllTabPageToolStripMenuItem";
            this.saveMdxInAllTabPageToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.saveMdxInAllTabPageToolStripMenuItem.Text = "Save Mdx In All TabPage";
            this.saveMdxInAllTabPageToolStripMenuItem.Click += new System.EventHandler(this.saveMdxInAllTabPageToolStripMenuItem_Click);
            // 
            // loadMdxFileToolStripMenuItem
            // 
            this.loadMdxFileToolStripMenuItem.Name = "loadMdxFileToolStripMenuItem";
            this.loadMdxFileToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.loadMdxFileToolStripMenuItem.Text = "Load Saved Mdx File";
            this.loadMdxFileToolStripMenuItem.Click += new System.EventHandler(this.loadMdxFileToolStripMenuItem_Click);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.closeAllTabsToolStripMenuItem.Text = "Close All Tabs";
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.closeAllTabsToolStripMenuItem_Click);
            // 
            // closeCurrentTabToolStripMenuItem
            // 
            this.closeCurrentTabToolStripMenuItem.Name = "closeCurrentTabToolStripMenuItem";
            this.closeCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.closeCurrentTabToolStripMenuItem.Text = "Close Current Tab";
            this.closeCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentTabToolStripMenuItem_Click);
            // 
            // closeAllTabsButThisToolStripMenuItem
            // 
            this.closeAllTabsButThisToolStripMenuItem.Name = "closeAllTabsButThisToolStripMenuItem";
            this.closeAllTabsButThisToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.closeAllTabsButThisToolStripMenuItem.Text = "Close All Tabs But This";
            this.closeAllTabsButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllTabsButThisToolStripMenuItem_Click);
            // 
            // tabPageDefault
            // 
            this.tabPageDefault.Controls.Add(this.mdxExecuterCtrl1);
            this.tabPageDefault.Location = new System.Drawing.Point(4, 22);
            this.tabPageDefault.Name = "tabPageDefault";
            this.tabPageDefault.Size = new System.Drawing.Size(502, 300);
            this.tabPageDefault.TabIndex = 0;
            this.tabPageDefault.Text = "Default";
            this.tabPageDefault.UseVisualStyleBackColor = true;
            // 
            // mdxExecuterCtrl1
            // 
            this.mdxExecuterCtrl1.ConnStr = "";
            this.mdxExecuterCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdxExecuterCtrl1.FileName = null;
            this.mdxExecuterCtrl1.Location = new System.Drawing.Point(0, 0);
            this.mdxExecuterCtrl1.Mdx = "\r\nSELECT \r\nNON EMPTY\r\n{\r\n    [Measures].[ZCHTAmount],[Measures].[ZCHTCount]\r\n}\r\n " +
    "ON COLUMNS,\r\nNON EMPTY\r\n{\r\n   [ProjectDim.hieInfo].[Project].Members\r\n}\r\nON ROWS" +
    "\r\nFROM ZCHT_BsJe";
            this.mdxExecuterCtrl1.Name = "mdxExecuterCtrl1";
            this.mdxExecuterCtrl1.Size = new System.Drawing.Size(502, 300);
            this.mdxExecuterCtrl1.TabIndex = 0;
            // 
            // cboxConnStrings
            // 
            this.cboxConnStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxConnStrings.FormattingEnabled = true;
            this.cboxConnStrings.Location = new System.Drawing.Point(3, 3);
            this.cboxConnStrings.Name = "cboxConnStrings";
            this.cboxConnStrings.Size = new System.Drawing.Size(840, 21);
            this.cboxConnStrings.TabIndex = 5;
            // 
            // cubeMenu
            // 
            this.cubeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateSampleMdxTabPageToolStripMenuItem,
            this.closeCurrentCubeToolStripMenuItem,
            this.closeAllCubesToolStripMenuItem,
            this.collapseAllCubesToolStripMenuItem});
            this.cubeMenu.Name = "serverMenu";
            this.cubeMenu.Size = new System.Drawing.Size(190, 92);
            // 
            // generateSampleMdxTabPageToolStripMenuItem
            // 
            this.generateSampleMdxTabPageToolStripMenuItem.Name = "generateSampleMdxTabPageToolStripMenuItem";
            this.generateSampleMdxTabPageToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.generateSampleMdxTabPageToolStripMenuItem.Text = "Generate Sample Mdx";
            this.generateSampleMdxTabPageToolStripMenuItem.Click += new System.EventHandler(this.generateSampleMdxTabPageToolStripMenuItem_Click);
            // 
            // closeCurrentCubeToolStripMenuItem
            // 
            this.closeCurrentCubeToolStripMenuItem.Name = "closeCurrentCubeToolStripMenuItem";
            this.closeCurrentCubeToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.closeCurrentCubeToolStripMenuItem.Text = "Close Current Cube";
            this.closeCurrentCubeToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentCubeToolStripMenuItem_Click);
            // 
            // closeAllCubesToolStripMenuItem
            // 
            this.closeAllCubesToolStripMenuItem.Name = "closeAllCubesToolStripMenuItem";
            this.closeAllCubesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.closeAllCubesToolStripMenuItem.Text = "Close All Cubes";
            this.closeAllCubesToolStripMenuItem.Click += new System.EventHandler(this.closeAllCubesToolStripMenuItem_Click);
            // 
            // collapseAllCubesToolStripMenuItem
            // 
            this.collapseAllCubesToolStripMenuItem.Name = "collapseAllCubesToolStripMenuItem";
            this.collapseAllCubesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.collapseAllCubesToolStripMenuItem.Text = "Collapse All Cubes";
            this.collapseAllCubesToolStripMenuItem.Click += new System.EventHandler(this.collapseAllCubesToolStripMenuItem_Click);
            // 
            // CubeViewCtrl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CubeViewCtrl";
            this.Size = new System.Drawing.Size(896, 431);
            this.Load += new System.EventHandler(this.CubeViewCtrl_Load);
            this.serverMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerServerAndCubeInfo.Panel1.ResumeLayout(false);
            this.splitContainerServerAndCubeInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerAndCubeInfo)).EndInit();
            this.splitContainerServerAndCubeInfo.ResumeLayout(false);
            this.splitContainerCubeInfo.Panel1.ResumeLayout(false);
            this.splitContainerCubeInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCubeInfo)).EndInit();
            this.splitContainerCubeInfo.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainerDataAndMdx.Panel1.ResumeLayout(false);
            this.splitContainerDataAndMdx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataAndMdx)).EndInit();
            this.splitContainerDataAndMdx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).EndInit();
            this.tabControlMdxEditorCollection.ResumeLayout(false);
            this.tabControlMenu.ResumeLayout(false);
            this.tabPageDefault.ResumeLayout(false);
            this.cubeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvServerInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TreeView tvCubeInfo;
        private System.Windows.Forms.ContextMenuStrip serverMenu;
        private System.Windows.Forms.ToolStripMenuItem browerCubeInfoToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerDataAndMdx;
        private System.Windows.Forms.DataGridView dgvObjectInfo;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerServerAndCubeInfo;
        private System.Windows.Forms.ContextMenuStrip cubeMenu;
        private System.Windows.Forms.TabControl tabControlMdxEditorCollection;
        private System.Windows.Forms.ToolStripMenuItem generateSampleMdxTabPageToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip tabControlMenu;
        private System.Windows.Forms.ToolStripMenuItem saveMdxInCurrentTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMdxInAllTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMdxFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDefault;
        private Executer.MdxExecuterCtrl mdxExecuterCtrl1;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentCubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllCubesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllCubesToolStripMenuItem;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerCubeInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxFilterType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilterUniqueName;
        private System.Windows.Forms.Button btnCancelFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cboxConnStrings;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsButThisToolStripMenuItem;
    }
}
