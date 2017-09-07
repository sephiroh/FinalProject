using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public class SendGridClient : IEmailClient
    {
        private readonly EmailSettings _settings;
        public SendGridClient(IOptions<EmailSettings> settings)
        {
            // get apikey from config
            _settings = settings.Value;
        }
        public async Task SendEmail(IMail email)
        {
            // build message
            var emailMessage = new SendGridMessage();
            emailMessage.From = new EmailAddress(email.Sender);

            List<EmailAddress> emailRecipients = new List<EmailAddress>();
            foreach (var item in email.Recipients)
                emailRecipients.Add(new EmailAddress(item));

            emailMessage.AddTos(emailRecipients);
            emailMessage.Subject = email.Subject;
            switch (email.ContentType)
            {
                case EmailContentType.Plain:
                    emailMessage.PlainTextContent = email.Message;
                    break;
                case EmailContentType.Html:
                    emailMessage.HtmlContent = email.Message;
                    break;
                default:
                    emailMessage.PlainTextContent = email.Message;
                    break;
            }

            // startup email client
            var client = new SendGrid.SendGridClient(_settings.ApiKey);
            // send the email
            await client.SendEmailAsync(emailMessage);
        }
    }
}
