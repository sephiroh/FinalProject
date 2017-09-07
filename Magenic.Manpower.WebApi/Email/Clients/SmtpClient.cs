using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public class SmtpClient : IEmailClient
    {
        private readonly EmailSettings _settings;
        public SmtpClient(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmail(IMail email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(email.Sender));

            List<MailboxAddress> emailRecipients = new List<MailboxAddress>();
            foreach (var item in email.Recipients)
                emailRecipients.Add(new MailboxAddress(item));
            emailMessage.To.AddRange(emailRecipients);

            emailMessage.Subject = email.Subject;
            BodyBuilder builder = new BodyBuilder();
            switch (email.ContentType)
            {
                case EmailContentType.Plain:
                    builder.TextBody = email.Message;
                    break;
                case EmailContentType.Html:
                    builder.HtmlBody = email.Message;
                    break;
                default:
                    builder.TextBody = email.Message;
                    break;
            }
            emailMessage.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_settings.ServerUrl, _settings.Port, _settings.UseSSL).ConfigureAwait(false);
                if (_settings.RequiresAuthentication)
                    await client.AuthenticateAsync(_settings.Username, _settings.Password).ConfigureAwait(false);

                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

            // for troubleshooting
            //try
            //{
            //    var client = new MailKit.Net.Smtp.SmtpClient();
            //    client.Connect(_settings.ServerUrl, _settings.Port, _settings.UseSSL);
            //    if (_settings.RequiresAuthentication)
            //        client.Authenticate(_settings.Username, _settings.Password);

            //    client.Send(emailMessage);
            //    client.Disconnect(true);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }
    }
}
