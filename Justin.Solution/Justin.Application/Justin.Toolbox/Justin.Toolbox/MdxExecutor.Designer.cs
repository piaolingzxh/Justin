namespace Justin.Toolbox
{
    partial class MdxExecutor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdxExecutor));
            this.mdxExecuterCtrl1 = new Justin.Controls.Executer.MdxExecuterCtrl();
            this.SuspendLayout();
            // 
            // mdxExecuterCtrl1
            // 
            this.mdxExecuterCtrl1.ConnStr = "Provider=mondrian;Data Source=http://localhost:8080/mondrian350_oracle/xmla";
            this.mdxExecuterCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdxExecuterCtrl1.FileName = null;
            this.mdxExecuterCtrl1.Location = new System.Drawing.Point(0, 0);
            this.mdxExecuterCtrl1.Name = "mdxExecuterCtrl1";
            this.mdxExecuterCtrl1.Size = new System.Drawing.Size(676, 398);
            this.mdxExecuterCtrl1.TabIndex = 3;
            // 
            // MdxExecutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 420);
            this.Controls.Add(this.mdxExecuterCtrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MdxExecutor";
            this.Text = "MdxExecutor";
            this.Load += new System.EventHandler(this.MdxExecutor_Load);
            this.Controls.SetChildIndex(this.mdxExecuterCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.Executer.MdxExecuterCtrl mdxExecuterCtrl1;

    }
}