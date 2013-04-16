namespace Justin.Toolbox
{
    partial class JCodeCompiler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JCodeCompiler));
            this.codeComplierCtrl1 = new Justin.Controls.CodeCompiler.CodeComplierCtrl();
            this.SuspendLayout();
            // 
            // codeComplierCtrl1
            // 
            this.codeComplierCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeComplierCtrl1.FileName = null;
            this.codeComplierCtrl1.Location = new System.Drawing.Point(0, 0);
            this.codeComplierCtrl1.Name = "codeComplierCtrl1";
            this.codeComplierCtrl1.Size = new System.Drawing.Size(744, 383);
            this.codeComplierCtrl1.TabIndex = 3;
            // 
            // JCodeCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 405);
            this.Controls.Add(this.codeComplierCtrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JCodeCompiler";
            this.ShowStatus = true;
            this.Text = "JCodeCompiler";
            this.Load += new System.EventHandler(this.JCodeCompiler_Load);
            this.Controls.SetChildIndex(this.codeComplierCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.CodeCompiler.CodeComplierCtrl codeComplierCtrl1;



    }
}