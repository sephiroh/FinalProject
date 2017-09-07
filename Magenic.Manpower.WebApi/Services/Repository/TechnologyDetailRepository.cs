using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class TechnologyDetailRepository : BaseRepository, ITechnologyDetailRepository
    {
        public TechnologyDetailRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        { }

        public Technology CreateTechnologyDetail(Technology tech)
        {
            tech.DateCreated = DateTime.Now;
            tech.DateUpdated = DateTime.Now;
            tech.IsActive = true;
            try
            {
                _dbContext.Technology.Add(tech);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                // logging
                throw;
            }
            return tech;
        }

        public Technology GetTechnologyDetailById(int id)
        {
            try
            {
                var tech = _dbContext.Technology.FirstOrDefault(a => a.Id == id);
                if (tech != null)
                    return tech;
            }
            catch (Exception)
            {
                // logging
                throw;
            }

            return null;
        }

        public IList<Technology> GetTechnologyDetailList()
        {
            try
            {
                var tech = _dbContext.Technology.ToList();
                if (tech != null)
                    return tech;
            }
            catch (Exception)
            {
                // logging
                throw;
            }

            return null;
        }

        public Technology GetTechnologyDetailByName(string name)
        {
            try
            {
                var tech = _dbContext.Technology.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
                if (tech != null)
                    return tech;
            }
            catch (Exception)
            {
                // logging
                throw;
            }

            return null;
        }

        public Technology UpdateTechnologyDetail(Technology tech)
        {
            tech.DateUpdated = DateTime.Now;

            try
            {
                _dbContext.Technology.Update(tech);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                // logging
                throw;
            }
            return tech;
        }

        public TechnologyDetailDTO ToggleActive(int id)
        {
            var _tech = _dbContext.Technology.Where(r => r.Id == id).Select(r => r).First();

            _tech.IsActive = !_tech.IsActive;
            _tech.DateUpdated = DateTime.Now;

            _dbContext.Technology.Update(_tech);

            _dbContext.SaveChanges();

            return new TechnologyDetailDTO() { Id = _tech.Id, IsActive = _tech.IsActive, Name = _tech.Name };
        }
    }

}
