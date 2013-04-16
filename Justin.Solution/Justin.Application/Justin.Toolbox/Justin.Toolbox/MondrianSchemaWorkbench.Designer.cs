namespace Justin.Toolbox
{
    partial class MondrianSchemaWorkbench
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MondrianSchemaWorkbench));
            this.schemaViewerCtrl1 = new Justin.Controls.Mondrian.SchemaViewerCtrl();
            this.SuspendLayout();
            // 
            // schemaViewerCtrl1
            // 
            this.schemaViewerCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaViewerCtrl1.FileName = "";
            this.schemaViewerCtrl1.Location = new System.Drawing.Point(0, 0);
            this.schemaViewerCtrl1.Name = "schemaViewerCtrl1";
            this.schemaViewerCtrl1.SaveSchemaFileName = "";
            this.schemaViewerCtrl1.SchemaFileName = "";
            this.schemaViewerCtrl1.Size = new System.Drawing.Size(768, 438);
            this.schemaViewerCtrl1.TabIndex = 6;
            // 
            // MondrianSchemaWorkbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 460);
            this.Controls.Add(this.schemaViewerCtrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MondrianSchemaWorkbench";
            this.ShowStatus = true;
            this.Text = "MondrianSchemaWorkbench";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MondrianSchemaWorkbench_Load);
            this.Controls.SetChildIndex(this.schemaViewerCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.Mondrian.SchemaViewerCtrl schemaViewerCtrl1;

    }
}