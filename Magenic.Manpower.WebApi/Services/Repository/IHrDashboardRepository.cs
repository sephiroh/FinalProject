using Magenic.Manpower.EFCore.Models;
using System.Collections.Generic;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHrDashboardRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<HrDashboardView> GetHriringRequests(int skillId, int projectId, int statusId);       
    }
}
