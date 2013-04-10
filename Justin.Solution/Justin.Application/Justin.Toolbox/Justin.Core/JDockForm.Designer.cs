namespace Justin.Core
{
    partial class JDockForm
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
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemCloseMe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCloseOthers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCLoseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCloseMe,
            this.menuItemCloseOthers,
            this.menuItemCLoseAll});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(153, 92);
            // 
            // menuItemCloseMe
            // 
            this.menuItemCloseMe.Name = "menuItemCloseMe";
            this.menuItemCloseMe.Size = new System.Drawing.Size(152, 22);
            this.menuItemCloseMe.Text = "Close";
            this.menuItemCloseMe.Click += new System.EventHandler(this.menuItemCloseMe_Click);
            // 
            // menuItemCloseOthers
            // 
            this.menuItemCloseOthers.Name = "menuItemCloseOthers";
            this.menuItemCloseOthers.Size = new System.Drawing.Size(152, 22);
            this.menuItemCloseOthers.Text = "Close Others";
            this.menuItemCloseOthers.Click += new System.EventHandler(this.menuItemCloseOthers_Click);
            // 
            // menuItemCLoseAll
            // 
            this.menuItemCLoseAll.Name = "menuItemCLoseAll";
            this.menuItemCLoseAll.Size = new System.Drawing.Size(152, 22);
            this.menuItemCLoseAll.Text = "Close All";
            this.menuItemCLoseAll.Click += new System.EventHandler(this.menuItemCLoseAll_Click);
            // 
            // JDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 308);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "JDockForm";
            this.TabPageContextMenuStrip = this.contextMenuTabPage;
            this.Text = "Form1";
            this.contextMenuTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseMe;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseOthers;
        private System.Windows.Forms.ToolStripMenuItem menuItemCLoseAll;
    }
}