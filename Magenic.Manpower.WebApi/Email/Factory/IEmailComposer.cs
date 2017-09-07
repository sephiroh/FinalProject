using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public interface IEmailComposer
    {
        IMail Compose(string sender, IList<string> recipients, IMailReference mailReference);
        IMail Compose(string sender, string subject, IList<string> recipients, string message, EmailContentType contentType);
    }
}
