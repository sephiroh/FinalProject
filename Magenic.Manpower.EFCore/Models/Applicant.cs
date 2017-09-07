using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class Applicant
    {
        public string ContactNumber { get; set; }
        public string CurrentCompany { get; set; }
        public string CurrentPosition { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public int LevelId { get; set; }
        public string NoticePeriod { get; set; }
        public string PendingApplication { get; set; }
        public int PrimarySkillId { get; set; }
        public byte[] ResumeFile { get; set; }
        public int Status { get; set; }
        public string YearsForSpecificSkills { get; set; }
        public string YearsITExperience { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
