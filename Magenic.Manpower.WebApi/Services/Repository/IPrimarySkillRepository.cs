using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrimarySkillRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<PrimarySkillDTO> GetPrimarySkills();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PrimarySkillDTO GetPrimarySkill(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primarySkill"></param>
        /// <returns></returns>
        PrimarySkillDTO AddPrimarySkill(PrimarySkillDTO primarySkill);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primarySkill"></param>
        /// <returns></returns>
        PrimarySkillDTO UpdatePrimarySkill(PrimarySkillDTO primarySkill);
    }
}
