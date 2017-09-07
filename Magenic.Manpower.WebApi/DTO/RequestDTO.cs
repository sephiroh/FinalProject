using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PrimarySkillId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SkillName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> NumberOfHires { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ProjectedStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JobDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<int> Technologies { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsForReplacement { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsForAdditionalResource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsChangeRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<string> ReferenceNumbers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RequestedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestedByName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<TaggedApplicantDTO> TaggedApplicants { get; set; }
    }
}
