namespace Justin.Controls.JsonView
{
    partial class JsonViewCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonViewCtrl));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerMain = new Justin.FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx();
            this.txtJson = new ICSharpCode.TextEditor.TextEditorControl();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.tvJson = new System.Windows.Forms.TreeView();
            this.menuOfTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSplitor = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnShow = new System.Windows.Forms.Button();
            this.layTopRight = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripInTop = new System.Windows.Forms.ToolStrip();
            this.btnFormat = new System.Windows.Forms.ToolStripButton();
            this.btnStrip = new System.Windows.Forms.ToolStripSplitButton();
            this.btnStripToCurly = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStripToSqr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButtonRemoveCharacters = new System.Windows.Forms.ToolStripSplitButton();
            this.removenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSpecialCharsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            this.menuOfTree.SuspendLayout();
            this.layTopRight.SuspendLayout();
            this.toolStripInTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerMain, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.layTopRight, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(726, 452);
            this.tableLayoutPanelMain.TabIndex = 3;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 38);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.txtJson);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(720, 411);
            this.splitContainerMain.SplitterDistance = 240;
            this.splitContainerMain.TabIndex = 4;
            // 
            // txtJson
            // 
            this.txtJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJson.IsReadOnly = false;
            this.txtJson.Location = new System.Drawing.Point(0, 0);
            this.txtJson.Name = "txtJson";
            this.txtJson.ShowEOLMarkers = true;
            this.txtJson.ShowSpaces = true;
            this.txtJson.ShowTabs = true;
            this.txtJson.Size = new System.Drawing.Size(240, 411);
            this.txtJson.TabIndex = 1;
            this.txtJson.Text = resources.GetString("txtJson.Text");
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 2;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.tvJson, 1, 0);
            this.tableLayoutPanelRight.Controls.Add(this.btnShow, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 1;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(476, 411);
            this.tableLayoutPanelRight.TabIndex = 1;
            // 
            // tvJson
            // 
            this.tvJson.ContextMenuStrip = this.menuOfTree;
            this.tvJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvJson.ImageIndex = 0;
            this.tvJson.ImageList = this.imgList;
            this.tvJson.Location = new System.Drawing.Point(23, 3);
            this.tvJson.Name = "tvJson";
            this.tvJson.SelectedImageIndex = 0;
            this.tvJson.Size = new System.Drawing.Size(450, 405);
            this.tvJson.TabIndex = 0;
            this.tvJson.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvJson_NodeMouseClick);
            // 
            // menuOfTree
            // 
            this.menuOfTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExpandAll,
            this.toolStripMenuItemSplitor,
            this.menuCopy,
            this.menuCopyValue});
            this.menuOfTree.Name = "mnuTree";
            this.menuOfTree.Size = new System.Drawing.Size(135, 76);
            // 
            // menuExpandAll
            // 
            this.menuExpandAll.Name = "menuExpandAll";
            this.menuExpandAll.Size = new System.Drawing.Size(134, 22);
            this.menuExpandAll.Text = "Expand &All";
            this.menuExpandAll.Click += new System.EventHandler(this.mnuExpandAll_Click);
            // 
            // toolStripMenuItemSplitor
            // 
            this.toolStripMenuItemSplitor.Name = "toolStripMenuItemSplitor";
            this.toolStripMenuItemSplitor.Size = new System.Drawing.Size(131, 6);
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(134, 22);
            this.menuCopy.Text = "&Copy";
            this.menuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // menuCopyValue
            // 
            this.menuCopyValue.Name = "menuCopyValue";
            this.menuCopyValue.Size = new System.Drawing.Size(134, 22);
            this.menuCopyValue.Text = "Copy &Value";
            this.menuCopyValue.Click += new System.EventHandler(this.mnuCopyValue_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.White;
            this.imgList.Images.SetKeyName(0, "obj.bmp");
            this.imgList.Images.SetKeyName(1, "array.bmp");
            this.imgList.Images.SetKeyName(2, "prop.bmp");
            // 
            // btnShow
            // 
            this.btnShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShow.Location = new System.Drawing.Point(3, 3);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(14, 405);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "→";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // layTopRight
            // 
            this.layTopRight.ColumnCount = 3;
            this.layTopRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTopRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTopRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layTopRight.Controls.Add(this.toolStripInTop, 0, 0);
            this.layTopRight.Controls.Add(this.txtSearch, 1, 0);
            this.layTopRight.Controls.Add(this.btnSearch, 2, 0);
            this.layTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layTopRight.Location = new System.Drawing.Point(3, 3);
            this.layTopRight.Name = "layTopRight";
            this.layTopRight.RowCount = 1;
            this.layTopRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layTopRight.Size = new System.Drawing.Size(720, 29);
            this.layTopRight.TabIndex = 2;
            // 
            // toolStripInTop
            // 
            this.toolStripInTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripInTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFormat,
            this.btnStrip,
            this.toolStripSplitButtonRemoveCharacters});
            this.toolStripInTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripInTop.Name = "toolStripInTop";
            this.toolStripInTop.Size = new System.Drawing.Size(339, 29);
            this.toolStripInTop.TabIndex = 7;
            this.toolStripInTop.Text = "toolStrip1";
            // 
            // btnFormat
            // 
            this.btnFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFormat.Image = ((System.Drawing.Image)(resources.GetObject("btnFormat.Image")));
            this.btnFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(49, 26);
            this.btnFormat.Text = "&Format";
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // btnStrip
            // 
            this.btnStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStripToCurly,
            this.btnStripToSqr});
            this.btnStrip.Image = ((System.Drawing.Image)(resources.GetObject("btnStrip.Image")));
            this.btnStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStrip.Name = "btnStrip";
            this.btnStrip.Size = new System.Drawing.Size(72, 26);
            this.btnStrip.Text = "Strip to {}";
            this.btnStrip.Click += new System.EventHandler(this.btnStripToCurly_Click);
            // 
            // btnStripToCurly
            // 
            this.btnStripToCurly.Name = "btnStripToCurly";
            this.btnStripToCurly.Size = new System.Drawing.Size(123, 22);
            this.btnStripToCurly.Text = "Strip to {}";
            this.btnStripToCurly.Click += new System.EventHandler(this.btnStripToCurly_Click);
            // 
            // btnStripToSqr
            // 
            this.btnStripToSqr.Name = "btnStripToSqr";
            this.btnStripToSqr.Size = new System.Drawing.Size(123, 22);
            this.btnStripToSqr.Text = "Strip to []";
            this.btnStripToSqr.Click += new System.EventHandler(this.btnStripToSqr_Click);
            // 
            // toolStripSplitButtonRemoveCharacters
            // 
            this.toolStripSplitButtonRemoveCharacters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButtonRemoveCharacters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removenToolStripMenuItem,
            this.removeSpecialCharsToolStripMenuItem});
            this.toolStripSplitButtonRemoveCharacters.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonRemoveCharacters.Image")));
            this.toolStripSplitButtonRemoveCharacters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonRemoveCharacters.Name = "toolStripSplitButtonRemoveCharacters";
            this.toolStripSplitButtonRemoveCharacters.Size = new System.Drawing.Size(141, 26);
            this.toolStripSplitButtonRemoveCharacters.Text = "Remove new lines (\\n)";
            this.toolStripSplitButtonRemoveCharacters.Click += new System.EventHandler(this.removeNewLineMenuItem_Click);
            // 
            // removenToolStripMenuItem
            // 
            this.removenToolStripMenuItem.Name = "removenToolStripMenuItem";
            this.removenToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.removenToolStripMenuItem.Text = "Remove new lines (\\n)";
            this.removenToolStripMenuItem.Click += new System.EventHandler(this.removeNewLineMenuItem_Click);
            // 
            // removeSpecialCharsToolStripMenuItem
            // 
            this.removeSpecialCharsToolStripMenuItem.Name = "removeSpecialCharsToolStripMenuItem";
            this.removeSpecialCharsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.removeSpecialCharsToolStripMenuItem.Text = "Remove special chars (\\)";
            this.removeSpecialCharsToolStripMenuItem.Click += new System.EventHandler(this.removeSpecialCharsToolStripMenuItem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(342, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(333, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(681, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // JsonViewCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "JsonViewCtrl";
            this.Size = new System.Drawing.Size(726, 452);
            this.Load += new System.EventHandler(this.JsonViewCtrl_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.menuOfTree.ResumeLayout(false);
            this.layTopRight.ResumeLayout(false);
            this.layTopRight.PerformLayout();
            this.toolStripInTop.ResumeLayout(false);
            this.toolStripInTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private ICSharpCode.TextEditor.TextEditorControl txtJson;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.TreeView tvJson;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TableLayoutPanel layTopRight;
        private System.Windows.Forms.ToolStrip toolStripInTop;
        private System.Windows.Forms.ToolStripButton btnFormat;
        private System.Windows.Forms.ToolStripSplitButton btnStrip;
        private System.Windows.Forms.ToolStripMenuItem btnStripToCurly;
        private System.Windows.Forms.ToolStripMenuItem btnStripToSqr;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonRemoveCharacters;
        private System.Windows.Forms.ToolStripMenuItem removenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSpecialCharsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ContextMenuStrip menuOfTree;
        private System.Windows.Forms.ToolStripMenuItem menuExpandAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSplitor;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuCopyValue;
        private System.Windows.Forms.ImageList imgList;
        private FrameWork.WinForm.FormUI.SplitContainerEx.SplitContainerEx splitContainerMain;

    }
}
