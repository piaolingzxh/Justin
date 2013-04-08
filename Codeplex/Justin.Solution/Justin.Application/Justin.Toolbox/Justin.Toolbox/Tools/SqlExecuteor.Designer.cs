namespace Justin.Toolbox.Tools
{
    partial class SqlExecuteor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlExecuteor));
            this.sqlExecuterCtrl1 = new Justin.Controls.Executer.SqlExecuterCtrl();
            this.SuspendLayout();
            // 
            // sqlExecuterCtrl1
            // 
            this.sqlExecuterCtrl1.ConnStr = null;
            this.sqlExecuterCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlExecuterCtrl1.FileName = "";
            this.sqlExecuterCtrl1.Location = new System.Drawing.Point(0, 0);
            this.sqlExecuterCtrl1.Name = "sqlExecuterCtrl1";
            this.sqlExecuterCtrl1.Size = new System.Drawing.Size(677, 354);
            this.sqlExecuterCtrl1.TabIndex = 3;
            // 
            // SqlExecuteor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 376);
            this.Controls.Add(this.sqlExecuterCtrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlExecuteor";
            this.ShowInTaskbar = false;
            this.Text = "SqlExecuteor";
            this.Controls.SetChildIndex(this.sqlExecuterCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.Executer.SqlExecuterCtrl sqlExecuterCtrl1;

    }
}