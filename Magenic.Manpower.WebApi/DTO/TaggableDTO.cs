using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public partial class TaggableDTO
    {
        public int Id { get; set; }
        public int PrimarySkillid { get; set; }
        public int? RefNoId { get; set; }
    }
}
