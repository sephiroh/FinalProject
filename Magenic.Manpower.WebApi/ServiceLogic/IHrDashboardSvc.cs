using Magenic.Manpower.WebApi.DTO;
using System.Collections.Generic;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHrDashboardSvc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<HiringRequestDTO>> GetHriringRequests(int skillId, int projectId, int statusID);
    }
}
