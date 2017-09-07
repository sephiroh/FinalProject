using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProjectManagementService
    {
        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="projectDto">The project dto.</param>
        /// <returns></returns>
        ServiceResponseDTO<ProjectDTO> AddProject(ProjectDTO projectDto);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<ProjectDTO>> GetProjects();

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        ServiceResponseDTO<ProjectDTO> UpdateProject(ProjectDTO project);

        /// <summary>
        /// Toggles the active.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ServiceResponseDTO<ProjectDTO> ToggleActive(int id);

        /// <summary>
        /// Get project details based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponseDTO<ProjectDTO> GetProject(int id);
    }
}
