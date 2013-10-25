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
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.tvServerinfo = new System.Windows.Forms.TreeView();
            this.serverMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browerCubeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCubeInfo = new System.Windows.Forms.TreeView();
            this.serverMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(498, 20);
            this.txtConnectionString.TabIndex = 0;
            // 
            // tvServerinfo
            // 
            this.tvServerinfo.ContextMenuStrip = this.serverMenu;
            this.tvServerinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvServerinfo.Location = new System.Drawing.Point(0, 0);
            this.tvServerinfo.Name = "tvServerinfo";
            this.tvServerinfo.Size = new System.Drawing.Size(181, 411);
            this.tvServerinfo.TabIndex = 2;
            this.tvServerinfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvServerinfo_ItemDrag);
            this.tvServerinfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServerinfo_AfterSelect);
            this.tvServerinfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvServerinfo_NodeMouseClick);
            this.tvServerinfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvServerinfo_DragEnter);
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
            this.btnConnect.Location = new System.Drawing.Point(507, 3);
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
            this.tableLayoutPanel1.Controls.Add(this.txtConnectionString, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 447);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvServerinfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tvCubeInfo);
            this.splitContainer1.Size = new System.Drawing.Size(548, 411);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvCubeInfo
            // 
            this.tvCubeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCubeInfo.Location = new System.Drawing.Point(0, 0);
            this.tvCubeInfo.Name = "tvCubeInfo";
            this.tvCubeInfo.Size = new System.Drawing.Size(363, 411);
            this.tvCubeInfo.TabIndex = 0;
            this.tvCubeInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCubeInfo_ItemDrag);
            this.tvCubeInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCubeInfo_AfterSelect);
            this.tvCubeInfo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCubeInfo_NodeMouseClick);
            this.tvCubeInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvCubeInfo_DragEnter);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.TreeView tvServerinfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvCubeInfo;
        private System.Windows.Forms.ContextMenuStrip serverMenu;
        private System.Windows.Forms.ToolStripMenuItem browerCubeInfoToolStripMenuItem;
    }
}
