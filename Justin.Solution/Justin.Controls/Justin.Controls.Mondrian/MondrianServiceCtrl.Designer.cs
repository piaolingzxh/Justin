namespace Justin.Controls.Mondrian
{
    partial class MondrianServiceCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabelDeafultLocation = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMondrianRootPath = new System.Windows.Forms.TextBox();
            this.txtJREExecuteFileName = new System.Windows.Forms.TextBox();
            this.txtTomcatRootPath = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.checkGroupBoxTomcat = new Justin.FrameWork.WinForm.FormUI.CheckGroupBox();
            this.checkGroupBoxMondrian = new Justin.FrameWork.WinForm.FormUI.CheckGroupBox();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxShowCmd = new System.Windows.Forms.CheckBox();
            this.btnStopService = new System.Windows.Forms.Button();
            this.checkBoxStopServiceWhenExit = new System.Windows.Forms.CheckBox();
            this.checkBoxRenameJREFile = new System.Windows.Forms.CheckBox();
            this.txtJREReName = new System.Windows.Forms.TextBox();
            this.checkGroupBoxTomcat.SuspendLayout();
            this.checkGroupBoxMondrian.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabelDeafultLocation
            // 
            this.linkLabelDeafultLocation.AutoSize = true;
            this.linkLabelDeafultLocation.Location = new System.Drawing.Point(74, 1);
            this.linkLabelDeafultLocation.Name = "linkLabelDeafultLocation";
            this.linkLabelDeafultLocation.Size = new System.Drawing.Size(82, 13);
            this.linkLabelDeafultLocation.TabIndex = 15;
            this.linkLabelDeafultLocation.TabStop = true;
            this.linkLabelDeafultLocation.Text = "DefaultLocation";
            this.linkLabelDeafultLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeafultLocation_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Mondrian Root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "JRE Execute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Tomcat Root";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Port";
            // 
            // txtMondrianRootPath
            // 
            this.txtMondrianRootPath.Location = new System.Drawing.Point(88, 19);
            this.txtMondrianRootPath.Name = "txtMondrianRootPath";
            this.txtMondrianRootPath.Size = new System.Drawing.Size(614, 20);
            this.txtMondrianRootPath.TabIndex = 8;
            // 
            // txtJREExecuteFileName
            // 
            this.txtJREExecuteFileName.Location = new System.Drawing.Point(88, 73);
            this.txtJREExecuteFileName.Name = "txtJREExecuteFileName";
            this.txtJREExecuteFileName.Size = new System.Drawing.Size(520, 20);
            this.txtJREExecuteFileName.TabIndex = 9;
            // 
            // txtTomcatRootPath
            // 
            this.txtTomcatRootPath.Location = new System.Drawing.Point(88, 45);
            this.txtTomcatRootPath.Name = "txtTomcatRootPath";
            this.txtTomcatRootPath.Size = new System.Drawing.Size(520, 20);
            this.txtTomcatRootPath.TabIndex = 10;
            this.txtTomcatRootPath.TextChanged += new System.EventHandler(this.txtTomcatRootPath_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(88, 17);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(520, 20);
            this.txtPort.TabIndex = 7;
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::Justin.Controls.Mondrian.Properties.Resources.conn;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStart.Location = new System.Drawing.Point(637, 49);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 26);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // checkGroupBoxTomcat
            // 
            this.checkGroupBoxTomcat.Controls.Add(this.txtPort);
            this.checkGroupBoxTomcat.Controls.Add(this.label1);
            this.checkGroupBoxTomcat.Controls.Add(this.linkLabelDeafultLocation);
            this.checkGroupBoxTomcat.Controls.Add(this.txtTomcatRootPath);
            this.checkGroupBoxTomcat.Controls.Add(this.label2);
            this.checkGroupBoxTomcat.Controls.Add(this.txtJREExecuteFileName);
            this.checkGroupBoxTomcat.Controls.Add(this.label3);
            this.checkGroupBoxTomcat.Location = new System.Drawing.Point(17, 10);
            this.checkGroupBoxTomcat.Name = "checkGroupBoxTomcat";
            this.checkGroupBoxTomcat.Size = new System.Drawing.Size(614, 100);
            this.checkGroupBoxTomcat.TabIndex = 18;
            this.checkGroupBoxTomcat.TabStop = false;
            this.checkGroupBoxTomcat.Text = "Tomcat";
            // 
            // checkGroupBoxMondrian
            // 
            this.checkGroupBoxMondrian.Checked = false;
            this.checkGroupBoxMondrian.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkGroupBoxMondrian.Controls.Add(this.txtMondrianRootPath);
            this.checkGroupBoxMondrian.Controls.Add(this.txtConnStr);
            this.checkGroupBoxMondrian.Controls.Add(this.button1);
            this.checkGroupBoxMondrian.Controls.Add(this.label4);
            this.checkGroupBoxMondrian.Controls.Add(this.label5);
            this.checkGroupBoxMondrian.Location = new System.Drawing.Point(17, 134);
            this.checkGroupBoxMondrian.Name = "checkGroupBoxMondrian";
            this.checkGroupBoxMondrian.Size = new System.Drawing.Size(714, 78);
            this.checkGroupBoxMondrian.TabIndex = 18;
            this.checkGroupBoxMondrian.TabStop = false;
            this.checkGroupBoxMondrian.Text = "Mondrian";
            // 
            // txtConnStr
            // 
            this.txtConnStr.Location = new System.Drawing.Point(88, 45);
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.ReadOnly = true;
            this.txtConnStr.Size = new System.Drawing.Size(520, 20);
            this.txtConnStr.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Justin.Controls.Mondrian.Properties.Resources.sync;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Location = new System.Drawing.Point(620, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Sync";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "ConnStr";
            // 
            // checkBoxShowCmd
            // 
            this.checkBoxShowCmd.AutoSize = true;
            this.checkBoxShowCmd.Location = new System.Drawing.Point(21, 241);
            this.checkBoxShowCmd.Name = "checkBoxShowCmd";
            this.checkBoxShowCmd.Size = new System.Drawing.Size(145, 17);
            this.checkBoxShowCmd.TabIndex = 19;
            this.checkBoxShowCmd.Text = "Show Command Window";
            this.checkBoxShowCmd.UseVisualStyleBackColor = true;
            // 
            // btnStopService
            // 
            this.btnStopService.BackgroundImage = global::Justin.Controls.Mondrian.Properties.Resources.stop;
            this.btnStopService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStopService.Location = new System.Drawing.Point(637, 83);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(82, 26);
            this.btnStopService.TabIndex = 6;
            this.btnStopService.Text = "Stop";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // checkBoxStopServiceWhenExit
            // 
            this.checkBoxStopServiceWhenExit.AutoSize = true;
            this.checkBoxStopServiceWhenExit.Checked = true;
            this.checkBoxStopServiceWhenExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStopServiceWhenExit.Location = new System.Drawing.Point(21, 264);
            this.checkBoxStopServiceWhenExit.Name = "checkBoxStopServiceWhenExit";
            this.checkBoxStopServiceWhenExit.Size = new System.Drawing.Size(235, 17);
            this.checkBoxStopServiceWhenExit.TabIndex = 20;
            this.checkBoxStopServiceWhenExit.Text = "Stop Mondrian Service When Exit This From";
            this.checkBoxStopServiceWhenExit.UseVisualStyleBackColor = true;
            // 
            // checkBoxRenameJREFile
            // 
            this.checkBoxRenameJREFile.AutoSize = true;
            this.checkBoxRenameJREFile.Checked = true;
            this.checkBoxRenameJREFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRenameJREFile.Location = new System.Drawing.Point(21, 218);
            this.checkBoxRenameJREFile.Name = "checkBoxRenameJREFile";
            this.checkBoxRenameJREFile.Size = new System.Drawing.Size(150, 17);
            this.checkBoxRenameJREFile.TabIndex = 20;
            this.checkBoxRenameJREFile.Text = "Rename JRE Execute File";
            this.checkBoxRenameJREFile.UseVisualStyleBackColor = true;
            // 
            // txtJREReName
            // 
            this.txtJREReName.Location = new System.Drawing.Point(172, 215);
            this.txtJREReName.Name = "txtJREReName";
            this.txtJREReName.Size = new System.Drawing.Size(132, 20);
            this.txtJREReName.TabIndex = 21;
            // 
            // MondrianServiceCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtJREReName);
            this.Controls.Add(this.checkBoxRenameJREFile);
            this.Controls.Add(this.checkBoxStopServiceWhenExit);
            this.Controls.Add(this.checkBoxShowCmd);
            this.Controls.Add(this.checkGroupBoxMondrian);
            this.Controls.Add(this.checkGroupBoxTomcat);
            this.Controls.Add(this.btnStopService);
            this.Controls.Add(this.btnStart);
            this.Name = "MondrianServiceCtrl";
            this.Size = new System.Drawing.Size(738, 294);
            this.Load += new System.EventHandler(this.MondrianServiceCtrl_Load);
            this.checkGroupBoxTomcat.ResumeLayout(false);
            this.checkGroupBoxTomcat.PerformLayout();
            this.checkGroupBoxMondrian.ResumeLayout(false);
            this.checkGroupBoxMondrian.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelDeafultLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMondrianRootPath;
        private System.Windows.Forms.TextBox txtJREExecuteFileName;
        private System.Windows.Forms.TextBox txtTomcatRootPath;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStart;
        private FrameWork.WinForm.FormUI.CheckGroupBox checkGroupBoxTomcat;
        private FrameWork.WinForm.FormUI.CheckGroupBox checkGroupBoxMondrian;
        private System.Windows.Forms.TextBox txtConnStr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxShowCmd;
        private System.Windows.Forms.Button btnStopService;
        private System.Windows.Forms.CheckBox checkBoxStopServiceWhenExit;
        private System.Windows.Forms.CheckBox checkBoxRenameJREFile;
        private System.Windows.Forms.TextBox txtJREReName;
    }
}
