using System;
using System.Collections.Generic;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System.Linq;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestContextRepository : BaseRepository, IRequestContextRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public RequestContextRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RequestDTO> GetRequest()
        {
            IEnumerable<RequestDTO> list;
            try
            {
                list = (from mpr in _dbContext.ManpowerRequest
                              join prj in _dbContext.Project on mpr.ProjectId equals prj.Id
                              join rgn in _dbContext.MagenicRegion on mpr.RegionId equals rgn.Id
                              join skl in _dbContext.PrimarySkill on mpr.PrimarySkillId equals skl.Id
                              select new RequestDTO
                              {
                                  Id = mpr.Id,
                                  ProjectId = mpr.ProjectId,
                                  ProjectName = prj.Name,
                                  PrimarySkillId = mpr.PrimarySkillId,
                                  SkillName = skl.Name,
                                  RegionId = mpr.RegionId,
                                  RegionName = rgn.Name,
                                  ProjectedStartDate = mpr.ProjectedStartDate,
                                  JobDescription = mpr.JobDescription,
                                  RequestedBy = mpr.RequestedBy,
                                  IsForReplacement = mpr.IsForReplacement,
                                  IsForAdditionalResource = mpr.IsForAdditionalResource,
                                  IsChangeRequest = mpr.IsChangeRequest
                              }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int SaveRequest(ManpowerRequest request)
        {
            request.DateCreated = DateTime.Now;
            _dbContext.ManpowerRequest.Add(request);
            _dbContext.SaveChanges();
            return request.Id;
        }
    }
}
