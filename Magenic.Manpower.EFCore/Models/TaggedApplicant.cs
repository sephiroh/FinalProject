using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.EFCore.Models
{
    public class TaggedApplicant
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int ReferenceNoId { get; set; }
        public DateTime TagDate { get; set; }
    }
}
