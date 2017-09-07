using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.Services.Repository;
using AutoMapper;


namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimarySkillService : BaseSvc, IPrimarySkillService
    {
        private readonly IPrimarySkillRepository _primarySkillRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public PrimarySkillService(IServiceProvider container, IMapper mapper) : base(container, mapper)
        {
            _primarySkillRepository = container.GetService<IPrimarySkillRepository>();
        }

        /// <summary>
        /// Creates new primaryskill
        /// </summary>
        /// <param name="newPrimarySkill"></param>
        /// <returns></returns>
        public ServiceResponseDTO<PrimarySkillDTO> AddPrimarySkill(PrimarySkillDTO newPrimarySkill)
        {
            ServiceResponseDTO<PrimarySkillDTO> result = new ServiceResponseDTO<PrimarySkillDTO>();
            PrimarySkillDTO primarySkill;
            try
            {
                primarySkill = _primarySkillRepository.AddPrimarySkill(newPrimarySkill);
                result.ResponseData = primarySkill;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Get Primary Skill from Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponseDTO<PrimarySkillDTO> GetPrimarySkill(int id)
        {
            ServiceResponseDTO<PrimarySkillDTO> result = new ServiceResponseDTO<PrimarySkillDTO>(true, new PrimarySkillDTO(), new List<string>());
            if (id == 0)
            {
                result.Errors.Add("Id was not set.");
                result.Success = false;
                return result;
            }

            try
            {
                result.ResponseData = _primarySkillRepository.GetPrimarySkill(id);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// Get list of Primary Skills
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<PrimarySkillDTO>> GetPrimarySkills()
        {
            ServiceResponseDTO<IEnumerable<PrimarySkillDTO>> result = new ServiceResponseDTO<IEnumerable<PrimarySkillDTO>>();
            IEnumerable<PrimarySkillDTO> primarySkill;
            try
            {
                primarySkill = _primarySkillRepository.GetPrimarySkills();
                result.ResponseData = primarySkill;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        public ServiceResponseDTO<PrimarySkillDTO> UpdatePrimarySkill(PrimarySkillDTO primarySkill)
        {
            var result = new ServiceResponseDTO<PrimarySkillDTO>();

            try
            {
                result.ResponseData = _primarySkillRepository.UpdatePrimarySkill(primarySkill);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }
    }
}
