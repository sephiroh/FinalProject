using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public int? RoleId { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; }
    }
}
