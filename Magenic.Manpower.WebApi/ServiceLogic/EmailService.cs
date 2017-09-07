using Magenic.Manpower.WebApi.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public class EmailService : IEmailService
    {
        private readonly IEmailComposer _composer;
        private readonly IEmailClient _client;
        public EmailService(IEmailComposer composer, IEmailClient client)
        {
            _composer = composer;
            _client = client;
        }

        /// <summary>
        /// use this method for template-driven email messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipients"></param>
        /// <param name="mailReference"></param>
        public void BuildEmailMessage(string sender, IList<string> recipients, IMailReference mailReference)
        {
            try
            {
                IMail email = _composer.Compose(sender, recipients, mailReference);
                _client.SendEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// use this method for RAW email messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="recipients"></param>
        public void BuildEmailMessage(string sender, string subject, string message, IList<string> recipients)
        {
            try
            {
                IMail email = _composer.Compose(sender, subject, recipients, message, EmailContentType.Plain);
                _client.SendEmail(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
