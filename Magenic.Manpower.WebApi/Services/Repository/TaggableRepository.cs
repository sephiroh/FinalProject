using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.ServiceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class TaggableRepository : BaseRepository, ITaggableRepository
    {
        public TaggableRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Taggable> GetTaggableApplicants(int primarySkillId)
        {
            IEnumerable<Taggable> list = new List<Taggable>();
            try
            {
                var data = _dbContext.Applicant.Where(r => r.PrimarySkillId == primarySkillId && (ApplicantStatusType)r.Status != ApplicantStatusType.Hired).ToList();
                list = data.Select(r => new Taggable() { Id = r.Id }).Distinct(new TaggableUniqueApplicantComparer());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public IEnumerable<Taggable> GetTaggableRefNumbers(int primarySkillId)
        {
            IEnumerable<Taggable> list = new List<Taggable>();
            try
            {
                var data = _dbContext.HrDashboardView.Where(hrv => hrv.PrimarySkillId == primarySkillId && (ReferenceNumberStatus)hrv.Status == ReferenceNumberStatus.Open).ToList();
                list = data.Select(r => new Taggable() { RefNoId = r.Id }).Distinct(new TaggableUniqueRefNoComparer());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public IEnumerable<TaggedApplicantView> GetTaggedApplicants(int refNoId)
        {
            IEnumerable<TaggedApplicantView> list = new List<TaggedApplicantView>();
            try
            {
                list = _dbContext.TaggedApplicantView.Where(r => r.ReferenceNoId == refNoId && r.Status != (int)ApplicantStatusType.Hired).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public IEnumerable<TaggedApplicantView> GetTaggedRefNumbers(int appId)
        {
            IEnumerable<TaggedApplicantView> list = new List<TaggedApplicantView>();
            try
            {
                list = _dbContext.TaggedApplicantView.Where(r => r.ApplicantId == appId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }

}
