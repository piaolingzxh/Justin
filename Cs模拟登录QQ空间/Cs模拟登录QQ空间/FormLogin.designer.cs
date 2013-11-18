namespace Cs模拟登录QQ空间
{
    partial class FormLogin
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
            this.labUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtVerifycode = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.picVerifycodeImage = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picVerifycodeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // labUser
            // 
            this.labUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labUser.Location = new System.Drawing.Point(21, 10);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(57, 18);
            this.labUser.TabIndex = 0;
            this.labUser.Text = "QQ:";
            this.labUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(21, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "密码:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(21, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "验证码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(84, 9);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(105, 20);
            this.txtUser.TabIndex = 3;
            this.txtUser.Text = "308182069";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(84, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(105, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtVerifycode
            // 
            this.txtVerifycode.Location = new System.Drawing.Point(84, 66);
            this.txtVerifycode.Name = "txtVerifycode";
            this.txtVerifycode.Size = new System.Drawing.Size(105, 20);
            this.txtVerifycode.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(195, 96);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(73, 25);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // picVerifycodeImage
            // 
            this.picVerifycodeImage.Location = new System.Drawing.Point(193, 37);
            this.picVerifycodeImage.Name = "picVerifycodeImage";
            this.picVerifycodeImage.Size = new System.Drawing.Size(85, 49);
            this.picVerifycodeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picVerifycodeImage.TabIndex = 8;
            this.picVerifycodeImage.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 126);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(412, 125);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 251);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.picVerifycodeImage);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtVerifycode);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labUser);
            this.Name = "FormLogin";
            this.Text = "登录";
            ((System.ComponentModel.ISupportInitialize)(this.picVerifycodeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtVerifycode;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox picVerifycodeImage;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}