namespace Justin.Toolbox
{
    partial class FileAssociation
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
            this.cListBoxFileExtension = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDoFileAssociate = new System.Windows.Forms.Button();
            this.btnUoDoFileAssociate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cListBoxFileExtension
            // 
            this.cListBoxFileExtension.CheckOnClick = true;
            this.cListBoxFileExtension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cListBoxFileExtension.FormattingEnabled = true;
            this.cListBoxFileExtension.Location = new System.Drawing.Point(3, 3);
            this.cListBoxFileExtension.Name = "cListBoxFileExtension";
            this.cListBoxFileExtension.Size = new System.Drawing.Size(455, 280);
            this.cListBoxFileExtension.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.Controls.Add(this.cListBoxFileExtension, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(545, 286);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnDoFileAssociate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUoDoFileAssociate, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(464, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(78, 280);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btnDoFileAssociate
            // 
            this.btnDoFileAssociate.Location = new System.Drawing.Point(3, 3);
            this.btnDoFileAssociate.Name = "btnDoFileAssociate";
            this.btnDoFileAssociate.Size = new System.Drawing.Size(72, 23);
            this.btnDoFileAssociate.TabIndex = 0;
            this.btnDoFileAssociate.Text = "关联";
            this.btnDoFileAssociate.UseVisualStyleBackColor = true;
            this.btnDoFileAssociate.Click += new System.EventHandler(this.btnDoFileAssociate_Click);
            // 
            // btnUoDoFileAssociate
            // 
            this.btnUoDoFileAssociate.Location = new System.Drawing.Point(3, 33);
            this.btnUoDoFileAssociate.Name = "btnUoDoFileAssociate";
            this.btnUoDoFileAssociate.Size = new System.Drawing.Size(72, 23);
            this.btnUoDoFileAssociate.TabIndex = 1;
            this.btnUoDoFileAssociate.Text = "取消关联";
            this.btnUoDoFileAssociate.UseVisualStyleBackColor = true;
            this.btnUoDoFileAssociate.Click += new System.EventHandler(this.btnUoDoFileAssociate_Click);
            // 
            // FileAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 308);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileAssociation";
            this.Text = "FileAssociation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cListBoxFileExtension;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDoFileAssociate;
        private System.Windows.Forms.Button btnUoDoFileAssociate;
    }
}