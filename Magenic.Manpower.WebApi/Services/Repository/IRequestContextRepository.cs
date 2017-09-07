using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public interface IRequestContextRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int SaveRequest(ManpowerRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<RequestDTO> GetRequest();
    }
}
