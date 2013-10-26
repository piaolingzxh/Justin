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
            this.splitContainerEx1 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.splitContainerEx2 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.tvCubeInfo = new System.Windows.Forms.TreeView();
            this.splitContainerEx3 = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.dgvObjectInfo = new System.Windows.Forms.DataGridView();
            this.txtMdx = new ICSharpCode.TextEditor.TextEditorControl();
            this.serverMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).BeginInit();
            this.splitContainerEx2.Panel1.SuspendLayout();
            this.splitContainerEx2.Panel2.SuspendLayout();
            this.splitContainerEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx3)).BeginInit();
            this.splitContainerEx3.Panel1.SuspendLayout();
            this.splitContainerEx3.Panel2.SuspendLayout();
            this.splitContainerEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(335, 20);
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
            this.tvServerInfo.Size = new System.Drawing.Size(182, 411);
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
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Image = global::Justin.Controls.CubeView.Properties.Resources.conn;
            this.btnConnect.Location = new System.Drawing.Point(344, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(207, 24);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.Controls.Add(this.txtConnectionString, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainerEx1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 447);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainerEx1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainerEx1, 2);
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(3, 33);
            this.splitContainerEx1.Name = "splitContainerEx1";
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.tvServerInfo);
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.splitContainerEx2);
            this.splitContainerEx1.Size = new System.Drawing.Size(548, 411);
            this.splitContainerEx1.SplitterDistance = 182;
            this.splitContainerEx1.TabIndex = 4;
            // 
            // splitContainerEx2
            // 
            this.splitContainerEx2.CollapsePanel = Justin.FrameWork.WinForm.FormUI.SplitContainerEx.CollapsePanel.Panel2;
            this.splitContainerEx2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx2.Name = "splitContainerEx2";
            // 
            // splitContainerEx2.Panel1
            // 
            this.splitContainerEx2.Panel1.Controls.Add(this.tvCubeInfo);
            // 
            // splitContainerEx2.Panel2
            // 
            this.splitContainerEx2.Panel2.Controls.Add(this.splitContainerEx3);
            this.splitContainerEx2.Size = new System.Drawing.Size(362, 411);
            this.splitContainerEx2.SplitterDistance = 176;
            this.splitContainerEx2.TabIndex = 0;
            // 
            // tvCubeInfo
            // 
            this.tvCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCubeInfo.ImageIndex = 0;
            this.tvCubeInfo.ImageList = this.imageList1;
            this.tvCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.tvCubeInfo.Name = "tvCubeInfo";
            this.tvCubeInfo.SelectedImageIndex = 0;
            this.tvCubeInfo.Size = new System.Drawing.Size(176, 411);
            this.tvCubeInfo.TabIndex = 0;
            this.tvCubeInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCubeInfo_ItemDrag);
            this.tvCubeInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCubeInfo_AfterSelect);
            this.tvCubeInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseClick);
            this.tvCubeInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseDoubleClick);
            // 
            // splitContainerEx3
            // 
            this.splitContainerEx3.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx3.Name = "splitContainerEx3";
            this.splitContainerEx3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx3.Panel1
            // 
            this.splitContainerEx3.Panel1.Controls.Add(this.dgvObjectInfo);
            // 
            // splitContainerEx3.Panel2
            // 
            this.splitContainerEx3.Panel2.Controls.Add(this.txtMdx);
            this.splitContainerEx3.Size = new System.Drawing.Size(182, 411);
            this.splitContainerEx3.SplitterDistance = 87;
            this.splitContainerEx3.TabIndex = 0;
            // 
            // dgvObjectInfo
            // 
            this.dgvObjectInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvObjectInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvObjectInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjectInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvObjectInfo.Name = "dgvObjectInfo";
            this.dgvObjectInfo.Size = new System.Drawing.Size(182, 87);
            this.dgvObjectInfo.TabIndex = 0;
            // 
            // txtMdx
            // 
            this.txtMdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMdx.Location = new System.Drawing.Point(0, 0);
            this.txtMdx.Name = "txtMdx";
            this.txtMdx.ShowEOLMarkers = true;
            this.txtMdx.ShowSpaces = true;
            this.txtMdx.ShowTabs = true;
            this.txtMdx.ShowVRuler = true;
            this.txtMdx.Size = new System.Drawing.Size(182, 320);
            this.txtMdx.TabIndex = 3;
            this.txtMdx.Text = "\r\nSELECT \r\nNON EMPTY\r\n{\r\n    [Measures].[ZCHTAmount],[Measures].[ZCHTCount]\r\n}\r\n " +
    "ON COLUMNS,\r\nNON EMPTY\r\n{\r\n   [ProjectDim.hieInfo].[Project].Members\r\n}\r\nON ROWS" +
    "\r\nFROM ZCHT_BsJe";
            // 
            // CubeViewCtrl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CubeViewCtrl";
            this.Size = new System.Drawing.Size(554, 447);
            this.Load += new System.EventHandler(this.CubeViewCtrl_Load);
            this.serverMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.splitContainerEx2.Panel1.ResumeLayout(false);
            this.splitContainerEx2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).EndInit();
            this.splitContainerEx2.ResumeLayout(false);
            this.splitContainerEx3.Panel1.ResumeLayout(false);
            this.splitContainerEx3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx3)).EndInit();
            this.splitContainerEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectInfo)).EndInit();
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
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx1;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx2;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerEx3;
        private System.Windows.Forms.DataGridView dgvObjectInfo;
        private ICSharpCode.TextEditor.TextEditorControl txtMdx;
    }
}
