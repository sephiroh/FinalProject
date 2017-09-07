using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        ServiceResponseDTO<RoleDTO> AddRole(RoleDTO newRole);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<RoleDTO>> GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        ServiceResponseDTO<RoleDTO> UpdateRole(RoleDTO newRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponseDTO<RoleDTO> ToggleActive(int id);
    }
}
