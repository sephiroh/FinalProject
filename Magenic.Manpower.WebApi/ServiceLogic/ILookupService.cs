using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Get all regions
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Regions();

        /// <summary>
        /// Get all application levels
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<SelectionItemDTO>> ApplicationLevels();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Permissions();

        /// <summary>
        /// Get all Status
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Status();

        /// <summary>
        /// Get all Applicant Status
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<SelectionItemDTO>> ApplicantStatus();

    }
}
