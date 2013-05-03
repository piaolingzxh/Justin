using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Core;
using Justin.FrameWork.Settings;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;

namespace Justin.Toolbox
{
    public partial class Explorer : JForm
    {
        public Explorer()
        {
            InitializeComponent();
        }

        public Explorer(string[] args)
            : this()
        {
            if (args != null)
            {
                this.textBoxPath.Text = args[0];
            }
        }

        private void Explorer_Load(object sender, EventArgs e)
        {
            this.panel1.Paint += panel1_Paint;
        }

        void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPath.Text))
                return;
            DirectoryInfo dir = new DirectoryInfo(textBoxPath.Text);
            if (!dir.Exists)
                return;
            explorerBrowser1.Navigate(ShellFileSystemFolder.FromFolderPath(textBoxPath.Text));
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            explorerBrowser1.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            explorerBrowser1.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void buttonLocation_Click(object sender, EventArgs e)
        {
            explorerBrowser1.Navigate(ShellFileSystemFolder.FromFolderPath(textBoxPath.Text));
        }

        private void explorerBrowser1_NavigationComplete(object sender, NavigationCompleteEventArgs e)
        {
            if (textBoxPath.Text != e.NewLocation.ParsingName)
            {
                textBoxPath.Text = e.NewLocation.ParsingName;
            }
        }


        private void Explorer_Activated(object sender, EventArgs e)
        {

        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
                explorerBrowser1.Navigate(ShellFileSystemFolder.FromFolderPath(textBoxPath.Text));
            }
        }


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.textBoxPath.Text);
        }


    }
}
