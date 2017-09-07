using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    interface IApplicantLevelService
    {
        ServiceResponseDTO<ApplicantLevelDTO> GetApplicantLevel(int id);
        ServiceResponseDTO<ApplicantLevelDTO> GetApplicantLevel(string name);
        ServiceResponseDTO<List<ApplicantLevelDTO>> GetApplicantLevelList();
        ServiceResponseDTO<ApplicantLevelDTO> ToggleActive(int id);
        ServiceResponseDTO<ApplicantLevelDTO> SaveApplicantLevel(ApplicantLevelDTO dto);
        
    }
}
