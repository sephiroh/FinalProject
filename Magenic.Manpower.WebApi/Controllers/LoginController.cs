using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.ServiceLogic;
using Magenic.Manpower.WebApi.DTO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("CorsPolicy")]
    [Route("api/login")]
    public class LoginController : BaseController
    {
        private readonly IAuthenticationSvc _authenticationSvc;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public LoginController(IServiceProvider container) : base(container)
        {
            _authenticationSvc = container.GetService<IAuthenticationSvc>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResponseDTO<CurrentUserDTO> Post([FromBody]UserCredentialDTO data)
        {
            return _authenticationSvc.Authenticate(data.Username, data.Password);              
        }
        
    }
}
