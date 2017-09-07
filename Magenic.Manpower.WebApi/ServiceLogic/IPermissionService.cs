using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Gets the permissions list of values.
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IList<PermissionDTO>> GetPermissionsLov();
    }
}
