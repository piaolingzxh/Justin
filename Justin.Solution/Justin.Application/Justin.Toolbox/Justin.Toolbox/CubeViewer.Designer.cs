namespace Justin.Toolbox
{
    partial class CubeViewer
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
            this.cubeViewCtrl1 = new Justin.Controls.CubeView.CubeViewCtrl();
            this.SuspendLayout();
            // 
            // cubeViewCtrl1
            // 
            this.cubeViewCtrl1.AllowDrop = true;
            this.cubeViewCtrl1.ConnStr = "";
            this.cubeViewCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cubeViewCtrl1.FileName = null;
            this.cubeViewCtrl1.Location = new System.Drawing.Point(0, 0);
            this.cubeViewCtrl1.Name = "cubeViewCtrl1";
            this.cubeViewCtrl1.Size = new System.Drawing.Size(545, 286);
            this.cubeViewCtrl1.TabIndex = 4;
            // 
            // CubeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 308);
            this.Controls.Add(this.cubeViewCtrl1);
            this.Name = "CubeViewer";
            this.ShowStatus = true;
            this.Text = "CubeViewer";
            this.Load += new System.EventHandler(this.CubeViewer_Load);
            this.Controls.SetChildIndex(this.cubeViewCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CubeView.CubeViewCtrl cubeViewCtrl1;
    }
}