using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Justin.FrameWork.WinForm.Extensions
{
    public class AutoAnchorForm : Form
    {
        public AutoAnchorForm()
            : base()
        {
            this.Load += new EventHandler(AutoHideForm_Load);
            StopRectTimer.Tick += new EventHandler(stopRectTimer_Tick);
            StopRectTimer.Interval = 50;
        }

        int displayWidth = 1;
        AnchorStyles StopAanhor = AnchorStyles.None;
        Timer StopRectTimer = new Timer();
        public bool EnableAutoAnchor
        {
            get
            {
                return StopRectTimer.Enabled;
            }
            set
            {
                this.StopRectTimer.Enabled = value;

                if (value)
                {
                    this.LocationChanged -= new EventHandler(hide_LocationChanged);
                    this.LocationChanged += new EventHandler(hide_LocationChanged);
                    StopRectTimer.Start();
                }
                else
                {
                    this.LocationChanged -= new EventHandler(hide_LocationChanged);
                    StopRectTimer.Stop();
                    StopAnchor();

                }
            }
        }

        void AutoHideForm_Load(object sender, EventArgs e)
        {
            StopRectTimer.Enabled = false;
        }

        private void stopRectTimer_Tick(object sender, EventArgs e)
        {
            if (this.StopAanhor.Equals(AnchorStyles.None)) return;
            if (this.Bounds.Contains(Cursor.Position))
            {
                StopAnchor();
            }
            else
            {
                StartAnchor();
            }
        }
        private void hide_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top <= 0 && this.Left <= 0)
            {
                StopAanhor = AnchorStyles.None;
            }
            else if (this.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (this.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else if (this.Top >= Screen.PrimaryScreen.Bounds.Height - this.Height)
            {
                StopAanhor = AnchorStyles.Bottom;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }

        private void StartAnchor()
        {
            switch (this.StopAanhor)
            {
                case AnchorStyles.Top:
                    this.Location = new Point(this.Location.X, (this.Height - displayWidth) * (-1));
                    break;
                case AnchorStyles.Left:
                    this.Location = new Point((-1) * (this.Width - displayWidth), this.Location.Y);
                    break;
                case AnchorStyles.Right:
                    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - displayWidth, this.Location.Y);
                    break;
                case AnchorStyles.Bottom:
                    this.Location = new Point(this.Location.X, (Screen.PrimaryScreen.Bounds.Height - displayWidth));
                    break;
            }
        }
        private void StopAnchor()
        {
            switch (this.StopAanhor)
            {
                case AnchorStyles.Top:
                    this.Location = new Point(this.Location.X, 0);
                    break;
                case AnchorStyles.Left:
                    this.Location = new Point(0, this.Location.Y);
                    break;
                case AnchorStyles.Right:
                    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                    break;
                case AnchorStyles.Bottom:
                    this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - this.Height);
                    break;
            }
        }


    }
}
