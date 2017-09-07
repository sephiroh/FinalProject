using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class ReferenceNumber
    {
        public int Id { get; set; }
        public string ReferenceString { get; set; }
        public int LevelId { get; set; }
        public int ManpowerRequestId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int ApplicantId { get; set; }
        public string Reason { get; set; }
        public virtual ApplicantLevel Level { get; set; }
        public virtual ManpowerRequest ManpowerRequest { get; set; }
        public virtual Status Status { get; set; }
    }
}
