using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class ManpowerRequestTechnology
    {
        public int ManpowerRequestId { get; set; }
        public int TechnologyId { get; set; }

        public virtual ManpowerRequest ManpowerRequest { get; set; }
        public virtual Technology Technology { get; set; }
    }
}
