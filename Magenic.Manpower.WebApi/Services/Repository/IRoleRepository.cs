using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleDTO> GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDTO GetRole(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        RoleDTO AddRole(RoleDTO role);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Role UpdateRole(Role role);

        /// <summary>
        /// Updates the role permission.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="currentPermissions">The role permissions.</param>
        /// <returns></returns>
        IQueryable<Permission> UpdateRolePermission(int roleId, ICollection<RolePermission> currentPermissions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        RoleDTO ToggleActive(int id);

        /// <summary>
        /// Validates the specified role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        bool Validate(Role role);
    }
}
