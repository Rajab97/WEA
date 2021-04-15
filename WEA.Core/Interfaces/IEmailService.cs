using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WEA.Core.Interfaces
{
    public class MailAttachment
    {
        public byte[] fileBytes;
        public string filename;
        public string mediaType;
    }
    public interface IEmailService
    {
        Task SendEmailAsync(string toUser, string subject, string message,
            object userToken = null, string[] ccList = null, params MailAttachment[] attachments);
    }
}
