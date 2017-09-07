using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public class HiredApplicantDTO
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int ReferenceNoId { get; set; }
        public DateTime HiredDate { get; set; }
        public int LevelId { get; set; }
        public string ReferenceString { get; set; }
        public string ProjectName { get; set; }
    }
}
