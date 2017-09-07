using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    /// <summary>
    /// factory that gets the email template from physical drive
    /// </summary>
    public class EmailTemplateFactory : IEmailTemplateFactory
    {
        private readonly IHostingEnvironment _env;
        public EmailTemplateFactory(IHostingEnvironment env)
        {
            _env = env;
        }

        public string GetEmailTemplateMessage(EmailMessageType messageType)
        {
            string templateFilename = string.Empty;

            switch (messageType)
            {
                case EmailMessageType.TagApplicantEmail:
                    templateFilename = "TagApplicantEmail.txt";
                    break;
                case EmailMessageType.FutureExpandEmail:
                    break;
                default:
                    break;
            } 
            // http://stackoverflow.com/questions/35842547/read-solution-data-files-asp-net-core
            string templateMessage = File.ReadAllText(Path.Combine(_env.ContentRootPath, "Email", "Templates", templateFilename));

            return templateMessage;
        }
    }
}
