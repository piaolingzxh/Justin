namespace Justin.Toolbox.Tools
{
    partial class CodeSnippetMgr
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
            this.codeSnippetCtrl1 = new Justin.Controls.CodeSnippet.CodeSnippetCtrl();
            this.SuspendLayout();
            // 
            // codeSnippetCtrl1
            // 
            this.codeSnippetCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeSnippetCtrl1.Location = new System.Drawing.Point(0, 0);
            this.codeSnippetCtrl1.Name = "codeSnippetCtrl1";
            this.codeSnippetCtrl1.Size = new System.Drawing.Size(767, 365);
            this.codeSnippetCtrl1.TabIndex = 0;
            // 
            // CodeSnippetMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 365);
            this.Controls.Add(this.codeSnippetCtrl1);
            this.Name = "CodeSnippetMgr";
            this.Text = "CodeSnippetMgr";
            this.Load += new System.EventHandler(this.CodeSnippetMgr_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Justin.Controls.CodeSnippet.CodeSnippetCtrl codeSnippetCtrl1;
    }
}