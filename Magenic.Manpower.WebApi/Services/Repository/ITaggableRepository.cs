using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public interface ITaggableRepository
    {
        IEnumerable<Taggable> GetTaggableApplicants(int primarySkillId);
        IEnumerable<Taggable> GetTaggableRefNumbers(int primarySkillId);
        IEnumerable<TaggedApplicantView> GetTaggedApplicants(int refNoId);
        IEnumerable<TaggedApplicantView> GetTaggedRefNumbers(int app);
    }
}
