using System.Collections.Generic;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System.Linq;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaggedApplicantRepository
    {
        /// <summary></summary>
        void Add(TaggedApplicant applicant);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId">based on Manpower Request, if any</param>
        /// <returns></returns>
        IList<TaggedApplicant> GetTaggedApplicants(int? requestId);
    }
}
