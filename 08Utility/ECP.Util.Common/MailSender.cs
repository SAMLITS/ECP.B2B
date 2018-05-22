using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ECP.Util.Common
{
    public class MailSender
    {
        public static void Send(string server, string sender, string recipient, string subject,
    string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            SmtpClient smtpClient = new SmtpClient(server);
            MailMessage message = new MailMessage(sender, recipient);
            message.IsBodyHtml = isBodyHtml;

            message.SubjectEncoding = encoding;
            message.BodyEncoding = encoding;
         
            message.Subject = subject;
            message.Body = body;

            message.Attachments.Clear();
            if (files != null && files.Length != 0)
            {
                for (int i = 0; i < files.Length; ++i)
                {
                    Attachment attach = new Attachment(files[i]);
                    message.Attachments.Add(attach);
                }
            }

            if (isAuthentication == true)
            {
                smtpClient.Credentials = new NetworkCredential(SmtpConfig.Create().SmtpSetting.User,
                    SmtpConfig.Create().SmtpSetting.Password);
            }
            smtpClient.Send(message);


        }

        public static void Send(string recipient, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, SmtpConfig.Create().SmtpSetting.Sender, recipient, subject, body, true, Encoding.Default, true, null);
        }

        public static void Send(string Recipient, string Sender, string Subject, string Body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, Sender, Recipient, Subject, Body, true, Encoding.UTF8, true, null);
        }

        //static readonly string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
        //static readonly string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
        //static readonly string pwd = System.Configuration.ConfigurationManager.AppSettings["Pwd"];
        //static readonly int smtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]);
        //static readonly string authorName = System.Configuration.ConfigurationManager.AppSettings["AuthorName"];
        //static readonly string to = System.Configuration.ConfigurationManager.AppSettings["To"];

        //static readonly string smtpServer = "smtp.yeah.net"; //yeah服务器地址：
        //                                                    //服务器地址如下:
        //                                                    //POP3服务器:pop.yeah.net
        //                                                    //SMTP服务器:smtp.yeah.net
        //                                                    //IMAP服务器:imap.yeah.net

        //static readonly string userName = "sz_ybs@yeah.net";
        //static readonly string pwd = "pwobckyoejajsblp";  //客户端授权密码
        //static readonly int smtpPort = 25;  //端口号，随便输入
        //static readonly string authorName = "深圳LTS技术有限公司";  //发件人
        //public static string to = "1028782451@qq.com";  //目标邮箱


        static readonly string smtpServer = "smtp.ym.163.com"; //yeah服务器地址：
        //服务器地址如下:
        //POP3服务器:pop.yeah.net
        //SMTP服务器:smtp.yeah.net
        //IMAP服务器:imap.yeah.net

        static readonly string userName = "service@ebs-in.com";
        static readonly string pwd = "13088888";  //客户端授权密码
        static readonly int smtpPort = 25;  //端口号，随便输入
        static readonly string authorName = "深圳LTS技术有限公司";  //发件人
        public string to { get; set; }  //目标邮箱


        public void Send(string subject, string body)
        {

            //List<string> toList = StringPlus.GetSubStringList(StringPlus.ToDBC(to), ',');
            //OpenSmtp.Mail.Smtp smtp = new OpenSmtp.Mail.Smtp(smtpServer, userName, pwd, smtpPort);
            //foreach (string s in toList)
            //{
            //    OpenSmtp.Mail.MailMessage msg = new OpenSmtp.Mail.MailMessage();
            //    msg.From = new OpenSmtp.Mail.EmailAddress(userName, authorName);
               
            //    msg.AddRecipient(s, OpenSmtp.Mail.AddressType.To);
               
            //    //设置邮件正文,并指定格式为 html 格式
            //    msg.HtmlBody = body;
               
            //    //设置邮件标题
            //    msg.Subject = subject;
            //    //指定邮件正文的编码
            //    msg.Charset = "gb2312";
            //    //发送邮件
            //    smtp.SendMail(msg);
            //}
        }

        public void Send_html(string subject, string body)
        {



            Send(to, userName, subject, body);
            
        }

    }

    public class SmtpSetting
    {
        private string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private bool _authentication;

        public bool Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }
        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        private string _sender;

        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }

    public class SmtpConfig
    {
        static readonly string smtpServer = "smtp.ym.163.com"; //yeah服务器地址：

        static readonly string userName = "service@ebs-in.com";
        static readonly string pwd = "13088888";  //客户端授权密码
        static readonly int smtpPort = 25;  //端口号，随便输入
        static readonly string authorName = "深圳LTS技术有限公司";  //发件人

        private static SmtpConfig _smtpConfig;
        //private string ConfigFile
        //{
        //    get
        //    {
        //        string configPath = ConfigurationManager.AppSettings["SmtpConfigPath"];
        //        if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
        //        {
        //            configPath = HttpContext.Current.Request.MapPath("/Config/SmtpSetting.config");
        //        }
        //        else
        //        {
        //            if (!Path.IsPathRooted(configPath))
        //                configPath = HttpContext.Current.Request.MapPath(Path.Combine(configPath, "SmtpSetting.config"));
        //            else
        //                configPath = Path.Combine(configPath, "SmtpSetting.config");
        //        }
        //        return configPath;
        //    }
        //}
        public SmtpSetting SmtpSetting
        {
            get
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(this.ConfigFile);
                SmtpSetting smtpSetting = new SmtpSetting();
                //smtpSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                //smtpSetting.Authentication = Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("Authentication").InnerText);
                //smtpSetting.User = doc.DocumentElement.SelectSingleNode("User").InnerText;
                //smtpSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;
                //smtpSetting.Sender = doc.DocumentElement.SelectSingleNode("Sender").InnerText;
                //修改从配制文件中拿到用户名与密码
                smtpSetting.Server = smtpServer;
                smtpSetting.Authentication = true;
                smtpSetting.User = userName;
                smtpSetting.Password = pwd;
                return smtpSetting;
            }
        }
        private SmtpConfig()
        {

        }
        public static SmtpConfig Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new SmtpConfig();
            }
            return _smtpConfig;
        }
    }
}
