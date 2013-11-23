using System.Windows.Forms;
namespace Justin.Message
{
    partial class QQStyleMessage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QQStyleMessage));
            this.btnHelper = new System.Windows.Forms.Button();
            this.timShow = new System.Windows.Forms.Timer(this.components);
            this.labelMessage = new System.Windows.Forms.Label();
            this.pnlImgTx = new System.Windows.Forms.PictureBox();
            this.pnlTx = new System.Windows.Forms.Panel();
            this.label1BottomLeft = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlImgTx)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHelper
            // 
            this.btnHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelper.BackColor = System.Drawing.Color.Transparent;
            this.btnHelper.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelper.Image = ((System.Drawing.Image)(resources.GetObject("btnHelper.Image")));
            this.btnHelper.Location = new System.Drawing.Point(202, 119);
            this.btnHelper.Margin = new System.Windows.Forms.Padding(0);
            this.btnHelper.Name = "btnHelper";
            this.btnHelper.Size = new System.Drawing.Size(16, 16);
            this.btnHelper.TabIndex = 131;
            this.btnHelper.UseVisualStyleBackColor = false;
            this.btnHelper.Visible = false;
            this.btnHelper.Click += new System.EventHandler(this.btnHelper_Click);
            // 
            // timShow
            // 
            this.timShow.Enabled = true;
            this.timShow.Interval = 3000;
            this.timShow.Tick += new System.EventHandler(this.timShow_Tick);
            // 
            // labelMessage
            // 
            this.labelMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.Location = new System.Drawing.Point(82, 22);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(173, 74);
            this.labelMessage.TabIndex = 130;
            this.labelMessage.Text = "您昨日没有累计活跃天。\r\n参与CC2013新春版嘉年华，\r\n赢QQ蛇年公仔，抢新年好彩\r\n头！";
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlImgTx.BackgroundImage")));
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.Location = new System.Drawing.Point(17, 24);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.Size = new System.Drawing.Size(53, 53);
            this.pnlImgTx.TabIndex = 6;
            this.pnlImgTx.TabStop = false;
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Location = new System.Drawing.Point(15, 22);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.Size = new System.Drawing.Size(57, 57);
            this.pnlTx.TabIndex = 129;
            // 
            // label1BottomLeft
            // 
            this.label1BottomLeft.AutoSize = true;
            this.label1BottomLeft.BackColor = System.Drawing.Color.Transparent;
            this.label1BottomLeft.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1BottomLeft.Location = new System.Drawing.Point(8, 119);
            this.label1BottomLeft.Name = "label1BottomLeft";
            this.label1BottomLeft.Size = new System.Drawing.Size(80, 17);
            this.label1BottomLeft.TabIndex = 128;
            this.label1BottomLeft.Text = "查看更多新闻";
            this.label1BottomLeft.Visible = false;
            this.label1BottomLeft.Click += new System.EventHandler(this.label1BottomLeft_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.labelMessage);
            this.panel1.Controls.Add(this.pnlImgTx);
            this.panel1.Controls.Add(this.label1BottomLeft);
            this.panel1.Controls.Add(this.pnlTx);
            this.panel1.Controls.Add(this.btnHelper);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 144);
            this.panel1.TabIndex = 132;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "友情提示：";
            // 
            // QQStyleMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
            this.ClientSize = new System.Drawing.Size(266, 171);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QQStyleMessage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "腾讯网新闻";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.QQForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlImgTx)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnHelper;
        private System.Windows.Forms.Timer timShow;
        private Label labelMessage;
        private PictureBox pnlImgTx;
        private Panel pnlTx;
        private Label label1BottomLeft;
        private Panel panel1;
        private ToolTip toolTip1;
    }
}