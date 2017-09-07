using System;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Magenic.Manpower.WebApi.Services.Repository.BaseRepository" />
    /// <seealso cref="Magenic.Manpower.WebApi.Services.Repository.IProjectManagementRepository" />
    public class ProjectManagementRepository : BaseRepository, IProjectManagementRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectManagementRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ProjectManagementRepository(MagenicManpowerDBContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        ///     Adds the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public Project AddProject(Project project)
        {
            _dbContext.Project.Add(project);
            //TODO: get applicant pool filtered by project's primary skill, then tag all applicants available
            _dbContext.SaveChanges();

            return project;
        }

        /// <summary>
        ///     Gets the project.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Project GetProject(int id)
        {
            try
            {
                return _dbContext.Project.FirstOrDefault(a => a.Id.Equals(id));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///     Gets the projects.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetProjects()
        {
            // TODO: Pagination
            try
            {
                return _dbContext.Project.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///     Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Project not found.</exception>
        public Project UpdateProject(Project project)
        {
            Project _project;
            using (var dbCtx = new MagenicManpowerDBContext())
            {
                _project = dbCtx.Project.FirstOrDefault(r => r.Id.Equals(project.Id));
            }

            if (_project == null)
                throw new KeyNotFoundException("Project not found.");

            _project.DateUpdated = DateTime.Now;
            _project.Name = project.Name;
            _project.Description = project.Description;
            _project.IsActive = project.IsActive;

            _dbContext.Entry(_project).State = _project.Id == 0 ? EntityState.Added : EntityState.Modified;
            _dbContext.SaveChanges();

            return _project;
        }

        /// <summary>
        ///     Toggles the active.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Project ToggleActive(int id)
        {
            var _project = _dbContext.Project.Where(r => r.Id == id).Select(r => r).First();

            _project.IsActive = !_project.IsActive;
            _project.DateUpdated = DateTime.Now;

            _dbContext.Project.Update(_project);

            _dbContext.SaveChanges();

            return _project;
        }

        /// <summary>
        ///     Validates the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public bool Validate(Project project)
        {
            if (project.Id == 0)
                return !_dbContext.Project.Any(r => r.Name.Equals(project.Name));

            return !_dbContext.Project
                .Where(a => !a.Id.Equals(project.Id))
                .Any(r => r.Name.Equals(project.Name));
        }
    }
}