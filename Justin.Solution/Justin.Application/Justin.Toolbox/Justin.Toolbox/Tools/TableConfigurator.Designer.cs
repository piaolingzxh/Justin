namespace Justin.Toolbox.Tools
{
    partial class TableConfigurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableConfigurator));
            this.tableConfigCtrl1 = new Justin.Controls.TestDataGenerator.TableConfigCtrl();
            this.SuspendLayout();
            // 
            // tableConfigCtrl1
            // 
            this.tableConfigCtrl1.ConnStr = null;
            this.tableConfigCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableConfigCtrl1.Location = new System.Drawing.Point(0, 0);
            this.tableConfigCtrl1.Name = "tableConfigCtrl1";
            this.tableConfigCtrl1.Size = new System.Drawing.Size(859, 495);
            this.tableConfigCtrl1.TabIndex = 4;
            this.tableConfigCtrl1.TableSetting = null;
            // 
            // TableConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 517);
            this.Controls.Add(this.tableConfigCtrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TableConfigurator";
            this.Text = "ConfigTableForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigTableForm_FormClosing);
            this.Controls.SetChildIndex(this.tableConfigCtrl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Justin.Controls.TestDataGenerator.TableConfigCtrl tableConfigCtrl1;
    }
}