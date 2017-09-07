using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public interface IAuthenticationSvc
    {
        ServiceResponseDTO<CurrentUserDTO> Authenticate(string username, string password);
    }
}
