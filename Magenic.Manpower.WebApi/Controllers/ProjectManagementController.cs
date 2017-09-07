using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("CorsPolicy")]
    [Route("api/projectManagement")]
    public class ProjectManagementController : BaseController
    {
        /// <summary>
        /// The project management service
        /// </summary>
        private readonly IProjectManagementService _projectManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManagementController"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ProjectManagementController(IServiceProvider container) : base(container)
        {
            _projectManagementService = _container.GetService<IProjectManagementService>();
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
        /// Posts the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>Returns the project added</returns>
        [HttpPost]
        public ServiceResponseDTO<ProjectDTO> Post([FromBody]ProjectDTO dto)
        {
            return _projectManagementService.AddProject(dto);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>Returns the project updated</returns>
        [HttpPut("{id}")]
        public ServiceResponseDTO<ProjectDTO> Put(int id, [FromBody]ProjectDTO dto)
        {
            return _projectManagementService.UpdateProject(dto);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns the project updated</returns>
        [HttpPut("toggle/{id}")]
        public ServiceResponseDTO<ProjectDTO> Put(int id)
        {
            return _projectManagementService.ToggleActive(id);
        }
    }
}
