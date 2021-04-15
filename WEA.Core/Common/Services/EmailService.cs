using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Interfaces;

namespace WEA.Core.Common.Services
{
    public class EmailService : IEmailService
    {

        private readonly int _port;
        private readonly SmtpDeliveryMethod _deliveryMethod;
        private readonly bool _useDefaultCredentials;
        private readonly string _host;
        private readonly bool _enableSsl;
        private readonly int _timeout;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly bool _isBodyHtml;

        //ConfigurationManager.AppSettings
        public EmailService(IConfiguration config)
        {
            _port = Convert.ToInt32(config["EmailSettings:Port"]);
            _host = config["EmailSettings:Host"];
            _smtpUsername = config["EmailSettings:User"];

            _smtpPassword = config["EmailSettings:Password"];
            _deliveryMethod =
                (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), config["EmailSettings:DeliveryMethod"]);
            //Network
            //SpecifiedPickupDirectory
            //PickupDirectoryFromIis
            _useDefaultCredentials = Convert.ToBoolean(config["EmailSettings:UseDefaultCredentials"]);
            _isBodyHtml = Convert.ToBoolean(config["EmailSettings:IsBodyHtml"]);
            _enableSsl = Convert.ToBoolean(config["EmailSettings:EnableSsl"]);
            _timeout = Convert.ToInt32(config["EmailSettings:Timeout"]);
        }
        private SmtpClient GetClient() =>
           new SmtpClient
           {
               Port = _port,
               DeliveryMethod = _deliveryMethod,
               Host = _host,
               EnableSsl = _enableSsl,
               Timeout = _timeout,
                //UseDefaultCredentials = _useDefaultCredentials,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
           };

        public async Task SendEmailAsync(string toUser, string subject, string message, object userToken = null, string[] ccList = null, params MailAttachment[] attachments)
        {
            var client = GetClient();
            var fromAddress = _smtpUsername;
            var mail = new MailMessage(fromAddress, toUser)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = _isBodyHtml,
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure
            };
            if (ccList != null)
            {
                foreach (var cc in ccList)
                {
                    mail.CC.Add(cc);
                }
            }
            foreach (var a in attachments)
            {
                var file = new MemoryStream(a.fileBytes);
                var attachment = new Attachment(file, a.filename, a.mediaType);
                mail.Attachments.Add(attachment);
            }
            try
            {
                await client.SendMailAsync(mail);
                //OnSendCompletedEvent(userToken);
            }
            catch (Exception e)
            {

            }

            mail.Dispose();
            client.Dispose();
        }
    }
}
