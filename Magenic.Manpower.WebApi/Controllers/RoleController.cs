using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("CorsPolicy")]
    [Route("api/roles")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public RoleController(IServiceProvider container) : base(container)
        {
            _roleService = _container.GetService<IRoleService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<RoleDTO>> Get()
        {
            var response = _roleService.GetRoles();
            return response;
        }
        
        /// <summary>
        /// Posts the specified parameter.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public ServiceResponseDTO<RoleDTO> Post([FromBody]RoleDTO value)
        {
            return _roleService.AddRole(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public ServiceResponseDTO<RoleDTO> Put(int id, [FromBody]RoleDTO value)
        {
            return _roleService.UpdateRole(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ServiceResponseDTO<RoleDTO> Delete(int id)
        {
            return _roleService.ToggleActive(id);
        }
    }
}
