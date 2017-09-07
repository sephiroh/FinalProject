using Magenic.Manpower.WebApi.DTO;
using System.Collections.Generic;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicantPoolSvc
    {
        /// <summary>
        /// Gets the tagged applicants for a reference number.
        /// </summary>
        /// <param name="primarySkillId"></param>
        /// <param name="levelId"></param>
        /// <param name="refNoId"></param>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> GetTaggedApplicantPool(int refNoId);

        /// <summary>
        /// Gets the Reference Numbers tagged to an applicant.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> GetTaggedReferencePool(int appId);

    }
}
