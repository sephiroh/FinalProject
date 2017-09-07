using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class ApplicantLevel
    {
        public ApplicantLevel()
        {
            ReferenceNumber = new HashSet<ReferenceNumber>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ReferenceNumber> ReferenceNumber { get; set; }
    }
}
