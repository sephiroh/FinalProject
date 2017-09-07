using Magenic.Manpower.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICmDashboardRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestor"></param>
        /// <param name="skillId"></param>
        /// <param name="projectId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        IList<HrDashboardView> GetProjectRequests(int requestor = 0, int skillId = 0, int projectId = 0, int statusId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refNumberId"></param>
        /// <param name="reason"></param>
        /// <param name="newStatusId"></param>
        bool UpdateReferenceNumberStatus(int refNumberId, string reason, int newStatusId);
    }
}
