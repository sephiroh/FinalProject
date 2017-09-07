using System;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimarySkillRepository : BaseRepository, IPrimarySkillRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public PrimarySkillRepository(MagenicManpowerDBContext dbContext)
            : base(dbContext)
        {
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PrimarySkillDTO GetPrimarySkill(int id)
        {
            PrimarySkillDTO dto = new PrimarySkillDTO();
            try
            {
                var primarySkill = _dbContext.PrimarySkill.FirstOrDefault(a => a.Id == id);
                if (primarySkill != null)
                {
                    dto = ConvertToDTO(primarySkill);
                }
            }
            catch (Exception)
            {
                // logging
                throw;
            }

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PrimarySkillDTO> GetPrimarySkills()
        {
            IEnumerable<PrimarySkillDTO> list;
            try
            {
                list = _dbContext.PrimarySkill.Select(primarySkill => new PrimarySkillDTO()
                {
                    Id = primarySkill.Id,
                    Name = primarySkill.Name,
                    Description = primarySkill.Description,
                    IsActive = primarySkill.IsActive,
                    DateCreated = primarySkill.DateCreated,
                    DateUpdated = primarySkill.DateUpdated ?? DateTime.Now
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPrimarySkill"></param>
        /// <returns></returns>
        public PrimarySkillDTO AddPrimarySkill(PrimarySkillDTO newPrimarySkill)
        {
            var primarySkill = new PrimarySkill()
            {
                Name = newPrimarySkill.Name,
                Description = newPrimarySkill.Description,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            try
            {
                _dbContext.PrimarySkill.Add(primarySkill);
                _dbContext.SaveChanges();
                newPrimarySkill.Id = primarySkill.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newPrimarySkill;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="primarySkill"></param>
        /// <returns></returns>
        public PrimarySkillDTO UpdatePrimarySkill(PrimarySkillDTO primarySkill)
        {
            var _primarySkill = _dbContext.PrimarySkill.Where(r => r.Id == primarySkill.Id).First();

            _primarySkill.Name = primarySkill.Name;
            _primarySkill.Description = primarySkill.Description;
            _primarySkill.IsActive = primarySkill.IsActive;
            _primarySkill.DateUpdated = DateTime.Now;
            _dbContext.PrimarySkill.Update(_primarySkill);
            _dbContext.SaveChanges();

            return primarySkill;
        }


        public PrimarySkill ConvertToModel(PrimarySkillDTO dto)
        {
            PrimarySkill model = new PrimarySkill()
            {
                Name = dto.Name,
                Description = dto.Description,
                Id = dto.Id,
                IsActive = dto.IsActive,
                DateCreated = dto.DateCreated,
                DateUpdated = dto.DateUpdated
            };

            return model;
        }

        public PrimarySkillDTO ConvertToDTO(PrimarySkill model)
        {
            PrimarySkillDTO dto = new PrimarySkillDTO()
            {
                IsActive = model.IsActive,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated ?? DateTime.Now
            };

            return dto;
        }
    }
}
