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
    public interface IApplicantsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApplicantsDTO> GetApplicants();

        /// <summary>
        /// Add Applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        Applicant AddApplicant(Applicant applicant);

        /// <summary>
        /// Update applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        Applicant UpdateApplicant(Applicant applicant);

        /// <summary>
        /// Validates the specified applicant.
        /// </summary>
        /// <param name="applicant">Applicant.</param>
        /// <returns></returns>
        /// 
        bool Validate(Applicant applicant);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        bool HireApplicant(int applicantId, DateTime hireDate);
    }
}
