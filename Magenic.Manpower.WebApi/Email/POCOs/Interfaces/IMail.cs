using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public interface IMail
    {
        string Subject { get; }
        string Message { get; }
        string Sender { get; }
        IList<string> Recipients { get; }
        EmailContentType ContentType { get; }
    }
}
