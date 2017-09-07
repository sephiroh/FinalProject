using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;
using System.Text;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class CmDashboardRepository : BaseRepository, ICmDashboardRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CmDashboardRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<HrDashboardView> GetProjectRequests(int requestor = 0, int skillId = 0, int projectId = 0, int statusId = 0)
        {
            IEnumerable<HrDashboardView> list = new List<HrDashboardView>();

            try
            {
                list = _dbContext.HrDashboardView.Where(hrv => (skillId == 0 || hrv.PrimarySkillId == skillId) && (projectId == 0 || hrv.ProjectId == projectId) && (statusId == 0 || hrv.Status == statusId) && (requestor == 0 || hrv.ManagerId == requestor)).ToList();
                return list.ToList();
            }
            catch (Exception)
            {
                // logging
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refNumberId"></param>
        /// <param name="reason"></param>
        /// <param name="newStatusId"></param>
        public bool UpdateReferenceNumberStatus(int refNumberId, string reason, int newStatusId)
        {
            try
            {
                var refNumber = _dbContext.ReferenceNumber.FirstOrDefault(a => a.Id == refNumberId);
                if (refNumber == null)
                    throw new ArgumentNullException("Reference Number not found!");

                var status = _dbContext.Status.FirstOrDefault(a => a.Id == newStatusId);
                if (status == null)
                    throw new ArgumentNullException("Status not found!");

                refNumber.StatusId = newStatusId;
                refNumber.Reason = reason;
                refNumber.DateUpdated = DateTime.Now;

                _dbContext.Update(refNumber);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
