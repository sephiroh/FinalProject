using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public interface ICmDashboardService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestor"></param>
        /// <param name="skillId"></param>
        /// <param name="projectId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        ServiceResponseDTO<List<HiringRequestDTO>> GetCmProjects(int requestor = 0, int skillId = 0, int projectId = 0, int statusId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        ServiceResponseDTO<ReferenceNumberReasonDTO> UpdateReferenceNumberStatus(ReferenceNumberReasonDTO dto);
    }
}
