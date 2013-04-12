namespace Justin.Toolbox.Tools
{
    partial class JEditor
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
            this.jEditorCtrl1 = new Justin.Controls.TextEditor.JEditorCtrl();
            this.SuspendLayout();
            // 
            // jEditorCtrl1
            // 
            this.jEditorCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jEditorCtrl1.FileName = "";
            this.jEditorCtrl1.Location = new System.Drawing.Point(0, 0);
            this.jEditorCtrl1.Name = "jEditorCtrl1";
            this.jEditorCtrl1.Size = new System.Drawing.Size(545, 308);
            this.jEditorCtrl1.TabIndex = 0;
            // 
            // JEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 308);
            this.Controls.Add(this.jEditorCtrl1);
            this.Name = "JEditor";
            this.Text = "JEditor";
            this.Load += new System.EventHandler(this.JEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TextEditor.JEditorCtrl jEditorCtrl1;
    }
}