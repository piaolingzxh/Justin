namespace Justin.FrameWork.WinForm.FormUI
{
    partial class NumberedTextBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberedTextBox));
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.panelLine = new System.Windows.Forms.Panel();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCloseOpen = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.AcceptsTab = true;
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Location = new System.Drawing.Point(75, 0);
            this.txtContent.Margin = new System.Windows.Forms.Padding(0);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(545, 452);
            this.txtContent.TabIndex = 1;
            this.txtContent.Text = "";
            this.txtContent.WordWrap = false;
            this.txtContent.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            this.txtContent.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panelLine
            // 
            this.panelLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLine.Location = new System.Drawing.Point(0, 0);
            this.panelLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(55, 452);
            this.panelLine.TabIndex = 2;
            this.panelLine.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // fontDialog1
            // 
            this.fontDialog1.Color = System.Drawing.SystemColors.ControlText;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtContent, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelLine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCloseOpen, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(620, 452);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnCloseOpen
            // 
            this.btnCloseOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCloseOpen.Image = global::Justin.FrameWork.WinForm.Properties.Resources.opened;
            this.btnCloseOpen.Location = new System.Drawing.Point(58, 3);
            this.btnCloseOpen.Name = "btnCloseOpen";
            this.btnCloseOpen.Size = new System.Drawing.Size(14, 446);
            this.btnCloseOpen.TabIndex = 3;
            this.btnCloseOpen.UseVisualStyleBackColor = true;
            this.btnCloseOpen.Click += new System.EventHandler(this.btnCloseOpen_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "opened.gif");
            this.imageList1.Images.SetKeyName(1, "closed.gif");
            // 
            // NumberedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NumberedTextBox";
            this.Size = new System.Drawing.Size(620, 452);
            this.Load += new System.EventHandler(this.NumberedTextBox_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCloseOpen;
        private System.Windows.Forms.ImageList imageList1;
    }
}
