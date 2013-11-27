using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace Justin.FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebRequest request;
        private void button1_Click(object sender, EventArgs e)
        {
            request = WebRequest.Create(txtRequest.Text);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string htmlString = reader.ReadToEnd();
            txtResponse.Text = htmlString;
            Console.WriteLine(htmlString);
        }
        public static class CreateConnectString
        {
            public static string Create(string sConn)
            {
                string targetDBConnectionString = string.Empty;
                DataConnectionDialog dataConnectionDialog = new DataConnectionDialog();
                DataSource.AddStandardDataSources(dataConnectionDialog);
                #region 配置是否选择的源，否则会弹出配置源界面
                dataConnectionDialog.DataSources.Add(DataSource.SqlDataSource);
                dataConnectionDialog.SelectedDataSource = DataSource.SqlDataSource;
                dataConnectionDialog.SelectedDataProvider = DataProvider.SqlDataProvider;
                //dataConnectionDialog.ConnectionString = "Data Source=.;Initial Catalog=MSK;Integrated Security=True";
                if (!string.IsNullOrEmpty(sConn))
                {
                    dataConnectionDialog.ConnectionString = sConn;
                }
                #endregion
                if (DataConnectionDialog.Show(dataConnectionDialog) == DialogResult.OK)
                {
                    targetDBConnectionString = dataConnectionDialog.ConnectionString;
                }
                return targetDBConnectionString;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = "1740365002";
            string password = "jst@163.com";
            string code = txtverifycode.Text;
            string wnPwd2 = PasswordHelper.GetPassword(user, password, code);
            string url = string.Format("http://ptlogin2.qq.com/login?u={0}&p={1}&verifycode={2}&aid=1006102&u1=http%3A%2F%2Fid.qq.com%2Findex.html&h=1&ptredirect=1&ptlang=2052&from_ui=1&dumy=&low_login_enable=0&regmaster=&fp=loginerroralert&action=1-6-1384495990479&mibao_css=&t=1&g=1&js_ver=10052&js_type=1&login_sig=RRZ8sLQBLQa0K1vbYCwuYXR7JWA1HDBIkUNjavOwUMQtk8Z9hbLx0ZJcpbE9dlOm&pt_rsa=0"
                , user
                , wnPwd2
                , code);

            txtRequest.Text = url;


        }
    }

    public class PasswordHelper
    {
        /// <summary>  
        /// 根据QQ号码和验证码加密密码  
        /// </summary>  
        /// <param name="qqNum">QQ号码</param>  
        /// <param name="password">QQ密码</param>  
        /// <param name="verifycode">验证码</param>  
        /// <returns>密码密文</returns>  
        public static string GetPassword(string qqNum, string password, string verifycode)
        {
            //uin为QQ号码转换为16位的16进制  
            int qq;
            int.TryParse(qqNum, out qq);

            qqNum = qq.ToString("x");
            qqNum = qqNum.PadLeft(16, '0');

            String P = hexchar2bin(md5(password));
            String U = md5(P + hexchar2bin(qqNum)).ToUpper();
            String V = md5(U + verifycode.ToUpper()).ToUpper();
            return V;
        }

        public static string md5(string input)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.GetEncoding("ISO-8859-1").GetBytes(input));
            return binl2hex(buffer);
        }

        public static string binl2hex(byte[] buffer)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string hexchar2bin(string passWord)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < passWord.Length; i = i + 2)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(passWord.Substring(i, 2), 16)));
            }
            return builder.ToString();
        }
    }

}
