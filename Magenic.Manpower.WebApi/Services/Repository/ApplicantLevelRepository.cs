using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class ApplicantLevelRepository : BaseRepository, IApplicantLevelRepository
    {

        public ApplicantLevelRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        { }


        public ApplicantLevel CreateApplicantLevel(ApplicantLevel level)
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                level.DateCreated = DateTime.Now;
                level.DateUpdated = DateTime.Now;
                level.IsActive = true;
                try
                {
                    _mydbcontext.ApplicantLevel.Add(level);
                    _mydbcontext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return level;
            }
        }

        public ApplicantLevel GetApplicantLevelById(int id)
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                try
                {
                    return _mydbcontext.ApplicantLevel.FirstOrDefault(a => a.Id == id);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public ApplicantLevel GetApplicantLevelByName(string name)
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                try
                {
                    return _mydbcontext.ApplicantLevel.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<ApplicantLevel> GetApplicantLevelList()
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                try
                {
                    return _mydbcontext.ApplicantLevel.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public ApplicantLevelDTO ToggleActive(int id)
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                try
                {
                    var level = _mydbcontext.ApplicantLevel.FirstOrDefault(a => a.Id == id);
                    level.IsActive = !level.IsActive;
                    level.DateUpdated = DateTime.Now;

                    _mydbcontext.Update(level);
                    _mydbcontext.SaveChanges();

                    return new ApplicantLevelDTO() {
                        Id = level.Id,
                        Name = level.Name,
                        DateUpdated = level.DateUpdated,
                        IsActive = level.IsActive
                    };
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public ApplicantLevel UpdateApplicantLevel(ApplicantLevel level)
        {
            using (var _mydbcontext = new MagenicManpowerDBContext())
            {
                level.DateUpdated = DateTime.Now;
                try
                {
                    _mydbcontext.ApplicantLevel.Update(level);
                    _mydbcontext.SaveChanges();
                    return level;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
