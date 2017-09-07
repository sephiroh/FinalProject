
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrimarySkillService
    {
        ServiceResponseDTO<IEnumerable<PrimarySkillDTO>> GetPrimarySkills();
        ServiceResponseDTO<PrimarySkillDTO> GetPrimarySkill(int id);
        ServiceResponseDTO<PrimarySkillDTO> AddPrimarySkill(PrimarySkillDTO primarySkill);        
        ServiceResponseDTO<PrimarySkillDTO> UpdatePrimarySkill(PrimarySkillDTO primarySkill);
    }
}
