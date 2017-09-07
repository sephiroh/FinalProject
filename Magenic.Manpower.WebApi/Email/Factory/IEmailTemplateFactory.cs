using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public interface IEmailTemplateFactory
    {
        string GetEmailTemplateMessage(EmailMessageType messageType);
    }
}
