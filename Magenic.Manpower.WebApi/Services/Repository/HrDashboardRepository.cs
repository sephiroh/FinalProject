using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HrDashboardRepository : BaseRepository, IHrDashboardRepository
    {
        public HrDashboardRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDashboardView> GetHriringRequests(int skillId = 0, int projectId = 0, int statusId = 0)
        {
            IEnumerable<HrDashboardView> list = new List<HrDashboardView>();
            try
            {
                list = _dbContext.HrDashboardView.Where(hrv => (skillId == 0 || hrv.PrimarySkillId == skillId) && (projectId == 0 || hrv.ProjectId == projectId) && (statusId == 0 || hrv.Status == statusId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        } 
    }
}
