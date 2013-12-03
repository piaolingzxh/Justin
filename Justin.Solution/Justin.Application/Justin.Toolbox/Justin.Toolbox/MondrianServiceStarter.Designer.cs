namespace Justin.Toolbox
{
    partial class MondrianServiceStarter
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
            this.mondrianServiceCtrl1 = new Justin.Controls.Mondrian.MondrianServiceCtrl();
            this.SuspendLayout();
            // 
            // mondrianServiceCtrl1
            // 
            this.mondrianServiceCtrl1.ConnStr = null;
            this.mondrianServiceCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mondrianServiceCtrl1.FileName = null;
            this.mondrianServiceCtrl1.Location = new System.Drawing.Point(0, 0);
            this.mondrianServiceCtrl1.Name = "mondrianServiceCtrl1";
            this.mondrianServiceCtrl1.Size = new System.Drawing.Size(575, 233);
            this.mondrianServiceCtrl1.TabIndex = 0;
            // 
            // MondrianServiceStarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 233);
            this.Controls.Add(this.mondrianServiceCtrl1);
            this.Name = "MondrianServiceStarter";
            this.ShowStatus = true;
            this.Text = "MondrianServiceStarter";
            this.Load += new System.EventHandler(this.MondrianServiceStarter_Load);
            this.Controls.SetChildIndex(this.mondrianServiceCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Mondrian.MondrianServiceCtrl mondrianServiceCtrl1;
    }
}