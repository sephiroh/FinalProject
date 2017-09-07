using Magenic.Manpower.WebApi.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public interface IEmailService
    {
        void BuildEmailMessage(string sender, IList<string> recipients, IMailReference mailReference);
        void BuildEmailMessage(string sender, string subject, string message, IList<string> recipients);
    }
}
