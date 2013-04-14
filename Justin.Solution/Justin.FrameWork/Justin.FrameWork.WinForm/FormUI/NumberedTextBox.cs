using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.Properties;

namespace Justin.FrameWork.WinForm.FormUI
{
    public partial class NumberedTextBox : UserControl
    {
        public NumberedTextBox()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.panelLine.Invalidate();
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            this.panelLine.Invalidate();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ReDrawLinePart();
        }
        private void ShowLineNo()
        {
            //获得当前坐标信息
            Point p = this.txtContent.Location;
            int crntFirstIndex = this.txtContent.GetCharIndexFromPosition(p);
            int crntFirstLine = this.txtContent.GetLineFromCharIndex(crntFirstIndex);
            Point crntFirstPos = this.txtContent.GetPositionFromCharIndex(crntFirstIndex);
            //
            p.Y += this.txtContent.Height;
            //
            int crntLastIndex = this.txtContent.GetCharIndexFromPosition(p);
            int crntLastLine = this.txtContent.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.txtContent.GetPositionFromCharIndex(crntLastIndex);
            //
            //
            //准备画图
            Graphics g = this.panelLine.CreateGraphics();
            Font font = new Font(this.txtContent.Font, this.txtContent.Font.Style);
            SolidBrush brush = new SolidBrush(Color.Green);
            //
            //
            //画图开始
            //刷新画布
            Rectangle rect = this.panelLine.ClientRectangle;
            brush.Color = this.panelLine.BackColor;
            g.FillRectangle(brush, 0, 0, this.panelLine.ClientRectangle.Width, this.panelLine.ClientRectangle.Height);
            brush.Color = Color.Green;//重置画笔颜色
            //
            //绘制行号
            int lineSpace = 0;
            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);
            }
            else
            {
                lineSpace = Convert.ToInt32(this.txtContent.Font.Size);
            }
            int brushX = this.panelLine.ClientRectangle.Width - Convert.ToInt32(font.Size * 3) - 30;
            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);//惊人的算法啊！！
            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {
                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);
                brushY -= lineSpace;
            }
            g.Dispose();
            font.Dispose();
            brush.Dispose();
        }
        private void ReDrawLinePart()
        {
            this.btnCloseOpen.Image = this.panelLine.Visible ? Resources.opened : Resources.closed;
            if (this.panelLine.Visible)
            {
                ShowLineNo();
                this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                this.tableLayoutPanel1.ColumnStyles[0].Width = 55;
            }
            else
            {
                this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                this.tableLayoutPanel1.ColumnStyles[0].Width = 0;
            }

        }


        public override string Text
        {
            get
            {
                return this.txtContent.Text;
            }
            set
            {
                this.txtContent.Text = value;
            }
        }
        public RichTextBox BoxPart
        {
            get
            {
                return txtContent;
            }
        }
        public Panel LinePart
        {
            get
            {
                return this.panelLine;
            }
        }
        public bool ShowLineNumber
        {
            get
            {
                return this.panelLine.Visible;
            }
            set
            {
                this.panelLine.Visible = value;
                ReDrawLinePart();
            }
        }

        private void btnCloseOpen_Click(object sender, EventArgs e)
        {
            this.ShowLineNumber = !this.ShowLineNumber;
        }

        private void NumberedTextBox_Load(object sender, EventArgs e)
        {
            this.btnCloseOpen.Image = this.panelLine.Visible ? Resources.opened : Resources.closed;
        }

    }
}
