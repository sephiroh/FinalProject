using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDTO"/> class.
        /// </summary>
        public RoleDTO()
        {
            this.Permissions = new List<PermissionDTO>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public IList<PermissionDTO> Permissions { get; set; }
    }
}
