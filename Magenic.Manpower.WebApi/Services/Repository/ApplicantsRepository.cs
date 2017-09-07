using System;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicantsRepository : BaseRepository, IApplicantsRepository
    {
        private readonly ITaggableRepository _taggbleRefNumbersReposity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ApplicantsRepository(MagenicManpowerDBContext dbContext, ITaggableRepository taggbleRefNumbersReposity)
            : base(dbContext)
        {
            _taggbleRefNumbersReposity = taggbleRefNumbersReposity;
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicantsDTO> GetApplicants()
        {
            IEnumerable<ApplicantsDTO> list;
            try
            {
                list = _dbContext.Applicant.Select(applicant => new ApplicantsDTO()
                {
                    Id = applicant.Id,
                    FirstName = applicant.Firstname,
                    LastName = applicant.Lastname,
                    Email = applicant.Email,
                    ContactNumber = applicant.ContactNumber,
                    PrimarySkillId = applicant.PrimarySkillId,
                    LevelId = applicant.LevelId,
                    Status = applicant.Status,
                    CurrentPosition = applicant.CurrentPosition,
                    CurrentCompany = applicant.CurrentCompany,
                    NoticePeriod = applicant.NoticePeriod,
                    PendingApplication = applicant.PendingApplication,
                    YearsITExperience = applicant.YearsITExperience,
                    YearsForSpecificSkills = applicant.YearsForSpecificSkills,
                    StatusType = Enum.GetName(typeof(ApplicantStatusType), applicant.Status)
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
        /// <param name="applicant"></param>
        /// <returns></returns>
        public Applicant AddApplicant(Applicant applicant)
        {
            applicant.DateCreated = DateTime.Now;
            applicant.LevelId = 1;
            _dbContext.Applicant.Add(applicant);

            _dbContext.SaveChanges();

            var taggbleRefNos = _taggbleRefNumbersReposity.GetTaggableRefNumbers(applicant.PrimarySkillId);
         
            foreach (var taggbleRefNo in taggbleRefNos)
            {
                _dbContext.TaggedApplicant.Add(new TaggedApplicant()
                {
                    ReferenceNoId = taggbleRefNo.RefNoId.Value,
                    ApplicantId = applicant.Id,
                    TagDate = DateTime.Now
                });
            }

            _dbContext.SaveChanges();

            return applicant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        public Applicant UpdateApplicant(Applicant applicant)
        {
            Applicant _applicant;
            using (var dbCtx = new MagenicManpowerDBContext())
            {
                _applicant = dbCtx.Applicant.FirstOrDefault(a => a.Id.Equals(applicant.Id));
            }

            if (_applicant == null)
                throw new KeyNotFoundException("Applicant Id not found.");

            _applicant.Firstname = applicant.Firstname;
            _applicant.Lastname = applicant.Lastname;
            _applicant.Email = applicant.Email;
            _applicant.ContactNumber = applicant.ContactNumber;
            _applicant.CurrentPosition = applicant.CurrentPosition;
            _applicant.CurrentCompany = applicant.CurrentCompany;
            _applicant.YearsITExperience = applicant.YearsITExperience;
            _applicant.YearsForSpecificSkills = applicant.YearsForSpecificSkills;
            _applicant.Status = applicant.Status;
            _applicant.DateUpdated = DateTime.Now;
            _applicant.PrimarySkillId = applicant.PrimarySkillId;
            _applicant.LevelId = applicant.LevelId;
            _applicant.NoticePeriod = applicant.NoticePeriod;
            _applicant.PendingApplication = applicant.PendingApplication;
            
            _dbContext.Entry(_applicant).State = _applicant.Id == 0 ? EntityState.Added : EntityState.Modified;
            
            _dbContext.SaveChanges();

            return _applicant;
        }

        /// <summary>
        /// Validates the specified applicant.
        /// </summary>
        /// <param name="applicant">Applicant.</param>
        /// <returns></returns>
        public bool Validate(Applicant applicant)
        {
            //TODO:  Will change when applying validation.
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="hireDate"></param>
        /// <returns></returns>
        public bool HireApplicant(int applicantId, DateTime hireDate)
        {
            Applicant _applicant;
            using (var dbCtx = new MagenicManpowerDBContext())
            {
                _applicant = dbCtx.Applicant.FirstOrDefault(a => a.Id.Equals(applicantId));
            }

            if (_applicant == null)
                throw new Exception("Applicant Id not found.");

            _applicant.Status = (int)ApplicantStatusType.Hired;
            _applicant.HireDate = hireDate;
            
            _dbContext.Update(_applicant);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
