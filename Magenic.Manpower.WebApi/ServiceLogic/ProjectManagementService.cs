using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Magenic.Manpower.WebApi.ServiceLogic.BaseSvc" />
    /// <seealso cref="Magenic.Manpower.WebApi.ServiceLogic.IProjectManagementService" />
    public class ProjectManagementService : BaseSvc, IProjectManagementService
    {
        /// <summary>
        ///     The Project Management repository
        /// </summary>
        private readonly IProjectManagementRepository _pmRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectManagementService" /> class.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public ProjectManagementService(IServiceProvider container, IMapper mapper)
            : base(container, mapper)
        {
            _pmRepository = container.GetService<IProjectManagementRepository>();
        }

        /// <summary>
        ///     Adds the project.
        /// </summary>
        /// <param name="projectDto">The project dto.</param>
        /// <returns></returns>
        public ServiceResponseDTO<ProjectDTO> AddProject(ProjectDTO projectDto)
        {
            var result = new ServiceResponseDTO<ProjectDTO>();

            try
            {
                var projectEntity = Mapper.Map<Project>(projectDto);
                var valid = _pmRepository.Validate(projectEntity);

                projectEntity.DateCreated = DateTime.Now;
                projectEntity.DateUpdated = DateTime.Now;

                result.ResponseData = valid ? Mapper.Map<ProjectDTO>(_pmRepository.AddProject(projectEntity)) : null;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.Message};
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        ///     Gets the projects.
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<ProjectDTO>> GetProjects()
        {
            var result = new ServiceResponseDTO<IEnumerable<ProjectDTO>>();

            try
            {
                
                var projects = _pmRepository.GetProjects();



                result.ResponseData = projects.Select(a => Mapper.Map<ProjectDTO>(a)).ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.Message};
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        ///     Updates the project.
        /// </summary>
        /// <param name="projectDto">The project dto.</param>
        /// <returns></returns>
        public ServiceResponseDTO<ProjectDTO> UpdateProject(ProjectDTO projectDto)
        {
            var result = new ServiceResponseDTO<ProjectDTO>();

            try
            {
                var project = Mapper.Map<Project>(projectDto);
                var valid = _pmRepository.Validate(project);

                result.ResponseData = valid ? Mapper.Map<ProjectDTO>(_pmRepository.UpdateProject(project)) : null;

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.Message};
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        ///     Toggles the active.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ServiceResponseDTO<ProjectDTO> ToggleActive(int id)
        {
            var result = new ServiceResponseDTO<ProjectDTO>();

            try
            {
                result.ResponseData = Mapper.Map<ProjectDTO>(_pmRepository.ToggleActive(id));
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.Message};
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<ProjectDTO> GetProject(int id)
        {
            var result = new ServiceResponseDTO<ProjectDTO>();
            try
            {
                var project = _pmRepository.GetProject(id);
                result.ResponseData = _mapper.Map<ProjectDTO>(project);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }
    }
}