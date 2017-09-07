
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicantsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<ApplicantsDTO>> GetApplicants();

        /// <summary>
        /// Adds the applicant.
        /// </summary>
        /// <param name="value">The applicant dto.</param>
        /// <returns></returns>
        ServiceResponseDTO<ApplicantsDTO> AddApplicant(ApplicantsDTO value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedApplicant"></param>
        /// <returns></returns>
        ServiceResponseDTO<ApplicantsDTO> UpdateApplicant(ApplicantsDTO updatedApplicant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        ServiceResponseDTO<bool> HireApplicant(HiredApplicantDTO applicant);
    }
}
