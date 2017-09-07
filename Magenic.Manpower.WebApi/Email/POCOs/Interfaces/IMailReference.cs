using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public interface IMailReference
    {
        string EmailSubject { get; }
        EmailContentType ContentType { get; }
        EmailMessageType MessageType { get; }
        Dictionary<string, string> MapObjectDictionary();
    }
}
