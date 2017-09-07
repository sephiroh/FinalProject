using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class PrimarySkill
    {
        public PrimarySkill()
        {
            ManpowerRequest = new HashSet<ManpowerRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ManpowerRequest> ManpowerRequest { get; set; }
    }
}
