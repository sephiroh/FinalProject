using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public class Mail : IMail
    {
        public Mail(string sender, IList<string> recipients, string subject, string message, EmailContentType contentType)
        {
            this.ContentType = contentType;
            this.Message = message;
            this.Recipients = recipients;
            this.Sender = sender;
            this.Subject = subject;
        }
        public EmailContentType ContentType { get; private set; }
        public string Message { get; private set; }
        public IList<string> Recipients { get; private set; }
        public string Sender { get; private set; }
        public string Subject { get; private set; }
    }
}
