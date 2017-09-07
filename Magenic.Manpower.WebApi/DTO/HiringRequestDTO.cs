using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class HiringRequestDTO
    {
        public int Id { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string RequestNumber { get; set; }
        public int PrimarySkillId { get; set; }
        public string PrimarySkillName { get; set; }
        public int ApplicantLevelId { get; set; }
        public string ApplicantLevelName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ManagerName { get; set; }
        public int ManagerId { get; set; }
        public string ApplicantName { get; set; }
        public System.DateTime ApplicantStartDate { get; set; }
        public int Status { get; set; }
        public DateTime? DateFilled { get; set; }
        public string Reason { get; set; }

    }
}
