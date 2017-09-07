using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <returns></returns>
        IList<PermissionDTO> GetPermissions();
    }
}
