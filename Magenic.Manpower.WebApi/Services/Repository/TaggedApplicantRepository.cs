using System.Collections.Generic;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System.Linq;
using System;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class TaggedApplicantRepository : BaseRepository, ITaggedApplicantRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public TaggedApplicantRepository(MagenicManpowerDBContext dbContext)
            : base(dbContext)
        {

        }

        /// <summary></summary>
        public void Add(TaggedApplicant applicant)
        {
            _dbContext.TaggedApplicant.Add(applicant);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId">based on Manpower Request, if any</param>
        /// <returns></returns>
        public IList<TaggedApplicant> GetTaggedApplicants(int? requestId)
        {
            try
            {
                var taggedApplicants = _dbContext.TaggedApplicant.AsEnumerable();
                if (requestId.HasValue)
                {
                    var refNumbersByRequestId = _dbContext.ReferenceNumber.Where(a => a.ManpowerRequestId == requestId.Value).Select(a => a.Id).ToList();
                    taggedApplicants = taggedApplicants.Where(a => refNumbersByRequestId.Contains(a.ReferenceNoId));
                }
                return taggedApplicants.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
