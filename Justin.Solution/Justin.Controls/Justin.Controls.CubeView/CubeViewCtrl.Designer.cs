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
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.tvServerInfo = new System.Windows.Forms.TreeView();
            this.serverMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browerCubeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerServerAndCubeInfo = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tabControlmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveMdxInCurrentTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMdxInAllTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMdxFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvCubeInfo = new System.Windows.Forms.TreeView();
            this.cubeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateSampleMdxTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerDataAndMdx = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.dgvObjectInfo = new System.Windows.Forms.DataGridView();
            this.tabControlMdxEditorCollection = new System.Windows.Forms.TabControl();
            this.tabPageDefault = new System.Windows.Forms.TabPage();
            this.mdxExecuterCtrl1 = new Justin.Controls.Executer.MdxExecuterCtrl();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabControlmenu.SuspendLayout();
            this.cubeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataAndMdx)).BeginInit();
            this.splitContainerDataAndMdx.Panel1.SuspendLayout();
            this.splitContainerDataAndMdx.Panel2.SuspendLayout();
            this.splitContainerDataAndMdx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).BeginInit();
            this.tabControlMdxEditorCollection.SuspendLayout();
            this.tabPageDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(790, 20);
            this.txtConnectionString.TabIndex = 0;
            // 
            // tvServerInfo
            // 
            this.tvServerInfo.ContextMenuStrip = this.serverMenu;
            this.tvServerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvServerInfo.ImageIndex = 0;
            this.tvServerInfo.ImageList = this.imageList1;
            this.tvServerInfo.Location = new System.Drawing.Point(0, 0);
            this.tvServerInfo.Name = "tvServerInfo";
            this.tvServerInfo.SelectedImageIndex = 0;
            this.tvServerInfo.ShowNodeToolTips = true;
            this.tvServerInfo.Size = new System.Drawing.Size(173, 395);
            this.tvServerInfo.TabIndex = 2;
            this.tvServerInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvServerInfo_ItemDrag);
            this.tvServerInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServerInfo_AfterSelect);
            this.tvServerInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvServerInfo_NodeMouseClick);
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
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Image = global::Justin.Controls.CubeView.Properties.Resources.conn;
            this.btnConnect.Location = new System.Drawing.Point(799, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(94, 24);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.txtConnectionString, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainerMain, 0, 1);
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
            this.splitContainerMain.Location = new System.Drawing.Point(3, 33);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerServerAndCubeInfo);
            this.splitContainerMain.Panel1MinSize = 200;
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
            this.splitContainerServerAndCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.splitContainerServerAndCubeInfo.Name = "splitContainerServerAndCubeInfo";
            // 
            // splitContainerServerAndCubeInfo.Panel1
            // 
            this.splitContainerServerAndCubeInfo.Panel1.Controls.Add(this.tvServerInfo);
            // 
            // splitContainerServerAndCubeInfo.Panel2
            // 
            this.splitContainerServerAndCubeInfo.Panel2.ContextMenuStrip = this.tabControlmenu;
            this.splitContainerServerAndCubeInfo.Panel2.Controls.Add(this.tvCubeInfo);
            this.splitContainerServerAndCubeInfo.Size = new System.Drawing.Size(376, 395);
            this.splitContainerServerAndCubeInfo.SplitterDistance = 173;
            this.splitContainerServerAndCubeInfo.TabIndex = 3;
            // 
            // tabControlmenu
            // 
            this.tabControlmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMdxInCurrentTabPageToolStripMenuItem,
            this.saveMdxInAllTabPageToolStripMenuItem,
            this.loadMdxFileToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.closeCurrentTabToolStripMenuItem});
            this.tabControlmenu.Name = "tabControlmenu";
            this.tabControlmenu.Size = new System.Drawing.Size(230, 136);
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
            // tvCubeInfo
            // 
            this.tvCubeInfo.ContextMenuStrip = this.cubeMenu;
            this.tvCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCubeInfo.ImageIndex = 0;
            this.tvCubeInfo.ImageList = this.imageList1;
            this.tvCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.tvCubeInfo.Name = "tvCubeInfo";
            this.tvCubeInfo.SelectedImageIndex = 0;
            this.tvCubeInfo.Size = new System.Drawing.Size(199, 395);
            this.tvCubeInfo.TabIndex = 0;
            this.tvCubeInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCubeInfo_ItemDrag);
            this.tvCubeInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCubeInfo_AfterSelect);
            this.tvCubeInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseClick);
            this.tvCubeInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseDoubleClick);
            // 
            // cubeMenu
            // 
            this.cubeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateSampleMdxTabPageToolStripMenuItem});
            this.cubeMenu.Name = "serverMenu";
            this.cubeMenu.Size = new System.Drawing.Size(190, 26);
            // 
            // generateSampleMdxTabPageToolStripMenuItem
            // 
            this.generateSampleMdxTabPageToolStripMenuItem.Name = "generateSampleMdxTabPageToolStripMenuItem";
            this.generateSampleMdxTabPageToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.generateSampleMdxTabPageToolStripMenuItem.Text = "Generate Sample Mdx";
            this.generateSampleMdxTabPageToolStripMenuItem.Click += new System.EventHandler(this.generateSampleMdxTabPageToolStripMenuItem_Click);
            // 
            // splitContainerDataAndMdx
            // 
            this.splitContainerDataAndMdx.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerDataAndMdx.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainerDataAndMdx.SplitterDistance = 82;
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
            this.dgvObjectInfo.Size = new System.Drawing.Size(510, 82);
            this.dgvObjectInfo.TabIndex = 0;
            // 
            // tabControlMdxEditorCollection
            // 
            this.tabControlMdxEditorCollection.ContextMenuStrip = this.tabControlmenu;
            this.tabControlMdxEditorCollection.Controls.Add(this.tabPageDefault);
            this.tabControlMdxEditorCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMdxEditorCollection.HotTrack = true;
            this.tabControlMdxEditorCollection.Location = new System.Drawing.Point(0, 0);
            this.tabControlMdxEditorCollection.Name = "tabControlMdxEditorCollection";
            this.tabControlMdxEditorCollection.SelectedIndex = 0;
            this.tabControlMdxEditorCollection.Size = new System.Drawing.Size(510, 309);
            this.tabControlMdxEditorCollection.TabIndex = 1;
            this.tabControlMdxEditorCollection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControlMdxEditorCollection_MouseDown);
            // 
            // tabPageDefault
            // 
            this.tabPageDefault.Controls.Add(this.mdxExecuterCtrl1);
            this.tabPageDefault.Location = new System.Drawing.Point(4, 22);
            this.tabPageDefault.Name = "tabPageDefault";
            this.tabPageDefault.Size = new System.Drawing.Size(502, 283);
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
            this.mdxExecuterCtrl1.Size = new System.Drawing.Size(502, 283);
            this.mdxExecuterCtrl1.TabIndex = 0;
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
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerServerAndCubeInfo.Panel1.ResumeLayout(false);
            this.splitContainerServerAndCubeInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerAndCubeInfo)).EndInit();
            this.splitContainerServerAndCubeInfo.ResumeLayout(false);
            this.tabControlmenu.ResumeLayout(false);
            this.cubeMenu.ResumeLayout(false);
            this.splitContainerDataAndMdx.Panel1.ResumeLayout(false);
            this.splitContainerDataAndMdx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataAndMdx)).EndInit();
            this.splitContainerDataAndMdx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).EndInit();
            this.tabControlMdxEditorCollection.ResumeLayout(false);
            this.tabPageDefault.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
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
        private System.Windows.Forms.ContextMenuStrip tabControlmenu;
        private System.Windows.Forms.ToolStripMenuItem saveMdxInCurrentTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMdxInAllTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMdxFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDefault;
        private Executer.MdxExecuterCtrl mdxExecuterCtrl1;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentTabToolStripMenuItem;
    }
}
