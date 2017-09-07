using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicantsDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentCompany { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? LevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NoticePeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PendingApplication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PrimarySkillId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YearsForSpecificSkills { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YearsITExperience { get; set; }
        public string StatusType { get; set; }
    }
}
