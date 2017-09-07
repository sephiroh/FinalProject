using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public class ReferenceNumberReasonDTO
    {
        public int RefNumberId { get; set; }
        public string Reason { get; set; }
        public int NewStatus { get; set; }
    }
}
