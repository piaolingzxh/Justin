using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Justin.FrameWork.Utility
{
    public class JMailConfig//配置结构
    {
        public string Addressor { get; set; }
        public string AddressorName { get; set; }
        public string AddressorPassword { get; set; }
        public string SmtpServer { get; set; }
        public JMailConfig(string addressor, string addressorName, string addressorPassword, string smtpServer)
        {
            Addressor = addressor;
            AddressorName = addressorName;
            AddressorPassword = addressorPassword;
            SmtpServer = smtpServer;
        }
    }
    public class JMailBody
    {
        StringBuilder builder = new StringBuilder();

        public JMailBody Append(DataTable data)
        {
            builder.Append("<div align=\"center\">");
            builder.Append("<table cellspacing=\"1\" cellpadding=\"3\" border=\"0\" bgcolor=\"000000\" style=\"font-size: 10pt;line-height: 15px;\">");
            builder.Append("<tr>");

            for (int hcol = 0; hcol < data.Columns.Count; hcol++)
            {
                builder.Append("<td bgcolor=\"999999\">&nbsp;&nbsp;&nbsp;");
                builder.Append(data.Columns[hcol].ColumnName.ToString());
                builder.Append("&nbsp;&nbsp;&nbsp;</td>");
            }
            builder.Append("</tr>");

            for (int row = 0; row < data.Rows.Count; row++)
            {
                builder.Append("<tr>");
                for (int col = 0; col < data.Columns.Count; col++)
                {
                    builder.Append("<td bgcolor=\"dddddd\">&nbsp;&nbsp;&nbsp;");
                    builder.Append(data.Rows[row][col].ToString());
                    builder.Append("&nbsp;&nbsp;&nbsp;</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table><br>");
            builder.Append("</div>");
            return this;
        }
        public JMailBody Append(string htmlBody)
        {
            builder.Append(htmlBody);
            return this;
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }

    public class JMail
    {
        public static JMailConfig Config { get; set; }

        public void Send(string addressees, string subject, string body)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            if (addressees.IndexOf(',') > -1)
            {
                string[] mails = addressees.Split(',');
                for (int counti = 0; counti < mails.Length; counti++)
                {
                    if (mails[counti].Trim() != "")
                    {
                        msg.To.Add(mails[counti]);
                    }
                }
            }
            else
            {
                msg.To.Add(addressees);
            }

            msg.From = new System.Net.Mail.MailAddress(Config.Addressor, Config.AddressorName, System.Text.Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(Config.Addressor, Config.AddressorPassword);
            client.Host = Config.SmtpServer;

            object userState = msg;

            client.SendAsync(msg, userState);


        }

        public void Send(string addressees, string subject, JMailBody body)
        {
            Send(addressees, subject, body.ToString());
        }

        public void SendDemo()
        {
            string smtpHost = "smtp.163.com";
            string from = "piaolingzxh@163.com";
            string pwd = "26082408163";
            string to = "piaolingzxh@126.com";
            DataTable t1 = new DataTable();
            t1.Columns.Add("Id");
            t1.Columns.Add("Name");
            t1.Columns.Add("Age");
            for (int i = 0; i < 50; i++)
            {
                t1.Rows.Add(new object[] { 1000 + i, "Name" + i, 25 + i });
            }

            JMailConfig config = new JMailConfig(from, "张旭辉", pwd, smtpHost);
            JMail email = new JMail();
            JMail.Config = config;
            JMailBody body = new JMailBody().Append(t1).Append("一大段Html");
            email.Send(to, "邮件测试", body);

        }

    }

}
