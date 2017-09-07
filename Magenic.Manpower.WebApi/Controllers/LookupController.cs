using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("CorsPolicy")]
    [Route("api/lookup")]
    public class LookupController : BaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly ILookupService _lookupService;
        private readonly IProjectManagementService _projectManagementService;        

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupController"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public LookupController(IServiceProvider container) : base(container)
        {
            _lookupService = container.GetService<ILookupService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("permissions")]
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> GetPermissions()
        {
            var response = _lookupService.Permissions();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("regions")]
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> GetRegions()
        {
            var response = _lookupService.Regions();
            return response;
        }

        /// <summary>
        /// Endpoint for getting of all application levels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("levels")]
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> GetApplicationLevels()
        {
            var response = _lookupService.ApplicationLevels();
            return response;
        }

        /// <summary>
        /// Endpoint for getting of all status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("status")]
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Status()
        {
            var response = _lookupService.Status();
            return response;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Returns list of projects</returns>
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<ProjectDTO>> Get()
        {
            return _projectManagementService.GetProjects();
        }

        /// <summary>
        /// Endpoint for getting of all applicant status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("applicant-status")]
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> ApplicantStatus()
        {
            var response = _lookupService.ApplicantStatus();
            return response;
        }
    }
}
