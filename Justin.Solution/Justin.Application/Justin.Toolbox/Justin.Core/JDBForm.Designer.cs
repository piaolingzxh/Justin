namespace Justin.Core
{
    partial class JDBForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDataSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusTitle,
            this.toolStripStatusMessage,
            this.toolStripStatusDataSource});
            this.statusStrip1.Location = new System.Drawing.Point(0, 264);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(545, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip2";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusTitle
            // 
            this.toolStripStatusTitle.Name = "toolStripStatusTitle";
            this.toolStripStatusTitle.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusMessage
            // 
            this.toolStripStatusMessage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusMessage.Name = "toolStripStatusMessage";
            this.toolStripStatusMessage.Size = new System.Drawing.Size(530, 17);
            this.toolStripStatusMessage.Spring = true;
            // 
            // toolStripStatusDataSource
            // 
            this.toolStripStatusDataSource.Name = "toolStripStatusDataSource";
            this.toolStripStatusDataSource.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusDataSource.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusDataSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // JDBDcokForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 308);
            this.Controls.Add(this.statusStrip1);
            this.Name = "JDBDcokForm";
            this.Text = "JDBDcokForm";
            this.Load += new System.EventHandler(this.JDBDcokForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTitle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDataSource;
    }
}