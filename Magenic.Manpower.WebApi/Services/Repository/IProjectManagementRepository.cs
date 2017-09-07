using System.Collections.Generic;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// </summary>
    public interface IProjectManagementRepository
    {
        /// <summary>
        ///     Gets the projects.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Project> GetProjects();

        /// <summary>
        ///     Gets the project.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Project GetProject(int id);

        /// <summary>
        ///     Adds the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        Project AddProject(Project project);

        /// <summary>
        ///     Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        Project UpdateProject(Project project);

        /// <summary>
        ///     Toggles the active.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Project ToggleActive(int id);

        /// <summary>
        ///     Validates the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        bool Validate(Project project);
    }
}