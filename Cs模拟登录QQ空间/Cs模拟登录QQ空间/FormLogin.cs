//#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Cs模拟登录QQ空间
{

    public partial class FormLogin : Form
    {
        CookieContainer cookie;
        public FormLogin(string user, CookieContainer cookie)//从FormMain引用传入user和cookie
        {
            InitializeComponent();
            this.cookie = cookie;
            txtUser.LostFocus += new EventHandler(txtUser_LostFocus);//帐号框失去焦点事件,用账号取验证码图
        }
        //帐号框失去焦点事件,用账号取验证码图
        void txtUser_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Text == "") return;
                Random random = new Random();
                double r = random.NextDouble();
                string verifyImageCode;
                //判断是否需要验证码 不需要会返回 !XXX 的字符串 用来做密码加密,需要则用返回的一串码来的到验证码图片
                //http://check.ptlogin2.qq.com/check?regmaster=&uin=308182069&appid=1006102
                string tmp = HttpHelper.GetResponse("http://check.ptlogin2.qq.com/check?uin=" + txtUser.Text + "&appid=15000101&r" + r, cookie);// "*/*", "ptlogin2.qq.com", "http://qzone.qq.com/", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)");
                if (tmp.IndexOf("!") > 0) txtVerifycode.Text = tmp.Substring(tmp.IndexOf("!"), 4);
                else
                {
                    verifyImageCode = tmp.Substring(tmp.IndexOf(",") + 1, 49);
                    picVerifycodeImage.Image = getVerifycodeImage(txtUser.Text, verifyImageCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        //取得验证码图片
        Bitmap getVerifycodeImage(string user, string code)
        {
            Bitmap bmp = (Bitmap)HttpHelper.GetResponse("Bitmap", "http://captcha.qq.com/getimage?aid=15000101&r=0.6611663870094515&uin=" + user + "&vc_type=" + code, cookie);//"*/*", "ptlogin2.qq.com", "http://qzone.qq.com/", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)");
            return bmp;
        }
        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string format = "http://ptlogin2.qq.com/login?u={0}&p={1}&verifycode={2}&aid=1006102&u1=http%3A%2F%2Fid.qq.com%2Findex.html&h=1&ptredirect=1&ptlang=2052&from_ui=1&dumy=&low_login_enable=0&regmaster=&fp=loginerroralert&action=3-10-1384570213932&mibao_css=&t=1&g=1&js_ver=10052&js_type=1&login_sig=zkAXRjomzguOUWglnf4yKLCNqTRIXAQu*5aPNehk-sjMk8SbG-J15hnZUJb6PK0H&pt_rsa=0";
                string verifycode = txtVerifycode.Text;
                string password = QQPasswordExchanger.getPassword(txtPassword.Text, verifycode);

                string pwd = PasswordHelper.GetPassword(txtUser.Text, txtPassword.Text, verifycode);
                //string tmp = HttpHelper.GetResponse("http://ptlogin2.qq.com/login?u=" + txtUser.Text + "&p=" + password + "&verifycode=" + verifycode + "&aid=15000101&u1=http%3A%2F%2Fimgcache.qq.com%2Fqzone%2Fv5%2Floginsucc.html%3Fpara%3Dizone&ptredirect=1&h=1&from_ui=1&dumy=&fp=loginerroralert", cookie);// ,"*/*", "ptlogin2.qq.com", "http://qzone.qq.com/", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)");

                string tmp = HttpHelper.GetResponse(string.Format(format, txtUser.Text, pwd, verifycode), cookie);

                richTextBox1.Text = tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }


        }
    }
}
