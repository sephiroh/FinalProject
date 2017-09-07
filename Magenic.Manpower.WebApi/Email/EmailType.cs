using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public enum EmailMessageType
    {
        TagApplicantEmail,
        FutureExpandEmail
    }

    public enum EmailContentType
    {
        Plain,
        Html
    }
}
