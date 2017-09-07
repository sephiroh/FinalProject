using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestSvc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        ServiceResponseDTO<RequestDTO> SaveRequest(RequestDTO info);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<RequestDTO>> GetRequest();
    }
}
