using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace EmailHelper
{
    public static class Email
    {
        
        public static string SendEmail(string email, string code)
        {
            string rs = string.Empty;

            try
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(new MailAddress(email));
                mailMsg.From = new MailAddress("ussupport@loyola.xyz", "LOYOLA");
                // 邮件主题
                mailMsg.Subject = "Password assistance";

                //string text = "欢迎使用阿里云邮件推送";
                //mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));

                string html = @"<p>To verify your identity, please use the following code:</p><p></p><p><strong>" + code + "</strong></p><p></p><p>Loyola takes your account security very seriously. Please enter the code in official website following the instruction and reset your password.&nbsp;</p><p>If you still have questions, contact us.&nbsp;</p><p>Thank you and best wishes!&nbsp;</p><p><br/></p>";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                //邮件推送的SMTP地址和端口
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.qiye.aliyun.com", 25);
                // 使用SMTP用户名和密码进行验证
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("ussupport@loyola.xyz", "SERVICEloyola88");
                smtpClient.Credentials = credentials;
                smtpClient.Send(mailMsg);
                rs = "0";
            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }

            return rs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Send(string email, string code)
        {
            string rs = string.Empty;

            try
            {
                var emailAcount = "ussupport@loyola.xyz";
                var emailPassword = "SERVICEloyola88";
                var content = @"<p>To verify your identity, please use the following code:</p><p></p><p><strong>" + code + "</strong></p><p></p><p>Loyola takes your account security very seriously. Please enter the code in official website following the instruction and reset your password.&nbsp;</p><p>If you still have questions, contact us.&nbsp;</p><p>Thank you and best wishes!&nbsp;</p><p><br/></p>";
                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress("ussupport@loyola.xyz", "LOYOLA");
                message.From = fromAddr;
                //设置收件人,可添加多个,添加方法与下面的一样
                message.To.Add(email);
                //设置邮件标题
                message.Subject = "Password assistance";
                //设置邮件内容
                message.Body = content;
                message.IsBodyHtml = true;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.qiye.aliyun.com", 25);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                
                //发送邮件
                client.Send(message);

                rs = "0";
            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }

            return rs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Send465(string email, string code)
        {
            string rs = string.Empty;

            try
            {

                var message = new MimeKit.MimeMessage();
                message.From.Add(new MimeKit.MailboxAddress("Loyola", "ussupport@loyola.xyz"));
                message.To.Add(new MimeKit.MailboxAddress("", email));
                message.Subject = "Password assistance";

                var html = new MimeKit.TextPart("html")
                {
                    Text = @"<p>To verify your identity, please use the following code:</p><p></p><p><strong>" + code + "</strong></p><p></p><p>Loyola takes your account security very seriously. Please enter the code in official website following the instruction and reset your password.&nbsp;</p><p>If you still have questions, contact us.&nbsp;</p><p>Thank you and best wishes!&nbsp;</p><p><br/></p>"
                };
                var alternative = new MimeKit.Multipart("alternative");
                alternative.Add(html);
                // now create the multipart/mixed container to hold the message text and the
                // image attachment
                var multipart = new MimeKit.Multipart("mixed");
                multipart.Add(alternative);
                message.Body = multipart;
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.qiye.aliyun.com", 465, true);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    var mailFromAccount = "ussupport@loyola.xyz";
                    var mailPassword = "SERVICEloyola88";
                    client.Authenticate(mailFromAccount, mailPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }

                rs = "0";
            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }

            return rs;
        }


    }
}
