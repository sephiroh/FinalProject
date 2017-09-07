using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/dashboard")]
    public class DashboardController : BaseController
    {
        private readonly IHrDashboardSvc _service;
        private readonly IApplicantPoolSvc _applicantPoolService;
        private readonly ICmDashboardService _cmDashboardService;
        private readonly IApplicantsService _applicantService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public DashboardController(IServiceProvider container) : base(container)
        {
            _service = _container.GetService<IHrDashboardSvc>();
            _applicantPoolService = container.GetService<IApplicantPoolSvc>();
            _cmDashboardService = container.GetService<ICmDashboardService>();
            _applicantService = container.GetService<IApplicantsService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hiringrequests")]
        public ServiceResponseDTO<IEnumerable<HiringRequestDTO>> Get([FromQuery] int skillId, [FromQuery] int projectId, [FromQuery] int statusID, [FromQuery] string field, [FromQuery] string sort)
        {
            var response = _service.GetHriringRequests(skillId, projectId, statusID);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("hiringrequests/{id}/applicants")]
        public ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> Applicants(int id)
        {
            try
            {
                return _applicantPoolService.GetTaggedApplicantPool(id);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>>(false, null, errors);
            }
        }
        
        [HttpGet]
        [Route("projectrequests")]
        public ServiceResponseDTO<List<HiringRequestDTO>> CMProjects([FromQuery] int requestor, [FromQuery] int skillId, [FromQuery] int projectId, [FromQuery] int statusID, [FromQuery] string field, [FromQuery] string sort)
        {
            try
            {
                return _cmDashboardService.GetCmProjects(requestor, skillId, projectId, statusID);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<List<HiringRequestDTO>>(false, null, errors);
            }
        }

        [HttpPost]
        [Route("hireApplicant")]
        public ServiceResponseDTO<bool> HireApplicant([FromBody] HiredApplicantDTO hiredApplicant)
        {
           
            try
            {
                return _applicantService.HireApplicant(hiredApplicant);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<bool>(false, false, errors);
            }
        }

        [HttpPost]
        [Route("togglerefnumberstatus")]
        public ServiceResponseDTO<ReferenceNumberReasonDTO> ToggleRefNumberStatus([FromBody]ReferenceNumberReasonDTO dto)
        {
            try
            {
                return _cmDashboardService.UpdateReferenceNumberStatus(dto);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<ReferenceNumberReasonDTO>(false, null, errors);
            }
            
        }
    }
}
