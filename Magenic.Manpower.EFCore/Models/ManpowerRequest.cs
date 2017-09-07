using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class ManpowerRequest
    {
        public ManpowerRequest()
        {
            ManpowerRequestTechnology = new HashSet<ManpowerRequestTechnology>();
            ReferenceNumber = new HashSet<ReferenceNumber>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PrimarySkillId { get; set; }
        public int RegionId { get; set; }
        public DateTime ProjectedStartDate { get; set; }
        public string JobDescription { get; set; }
        public int RequestedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsForReplacement { get; set; }
        public bool IsForAdditionalResource { get; set; }
        public bool IsChangeRequest { get; set; }

        public virtual ICollection<ManpowerRequestTechnology> ManpowerRequestTechnology { get; set; }
        public virtual ICollection<ReferenceNumber> ReferenceNumber { get; set; }
        public virtual PrimarySkill PrimarySkill { get; set; }
        public virtual Project Project { get; set; }
        public virtual MagenicRegion Region { get; set; }
    }
}
