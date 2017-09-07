using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class Status
    {
        public Status()
        {
            ReferenceNumber = new HashSet<ReferenceNumber>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ReferenceNumber> ReferenceNumber { get; set; }
    }

    public enum ReferenceNumberStatus
    {
        Open = 1,
        Filled = 2,
        Closed = 3
    }
}
