namespace Justin.Toolbox
{
    partial class JsonViewer
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
            this.jsonViewCtrl1 = new Justin.Controls.JsonView.JsonViewCtrl();
            this.SuspendLayout();
            // 
            // jsonViewCtrl1
            // 
            this.jsonViewCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonViewCtrl1.FileName = null;
            this.jsonViewCtrl1.Location = new System.Drawing.Point(0, 0);
            this.jsonViewCtrl1.Name = "jsonViewCtrl1";
            this.jsonViewCtrl1.Size = new System.Drawing.Size(813, 353);
            this.jsonViewCtrl1.TabIndex = 0;
            // 
            // JsonViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 375);
            this.Controls.Add(this.jsonViewCtrl1);
            this.Name = "JsonViewer";
            this.Text = "JsonViewer";
            this.Load += new System.EventHandler(this.JsonViewer_Load);
            this.Controls.SetChildIndex(this.jsonViewCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.JsonView.JsonViewCtrl jsonViewCtrl1;
    }
}