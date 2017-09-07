using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public class EmailMessageComposer : IEmailComposer
    {
        private readonly IEmailTemplateFactory _templateFactory;
        public EmailMessageComposer(IEmailTemplateFactory templateFactory)
        {
            _templateFactory = templateFactory;
        }

        public IMail Compose(string sender, IList<string> recipients, IMailReference mailReference)
        {
            // build email message
            // get email template
            string emailTemplate = _templateFactory.GetEmailTemplateMessage(mailReference.MessageType);
            // replace markers from email template
            Dictionary<string, string> dictMarkers = mailReference.MapObjectDictionary();
            foreach (var item in dictMarkers)
                emailTemplate = emailTemplate.Replace("##" + item.Key + "##", item.Value);

            return new Mail(sender, recipients, mailReference.EmailSubject, emailTemplate, mailReference.ContentType);
        }

        public IMail Compose(string sender, string subject, IList<string> recipients, string message, EmailContentType contentType)
        {
            return new Mail(sender, recipients, subject, message, contentType);
        }
    }
}
