using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Magenic.Manpower.WebApi.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IRequestSvc _requestSvc;
        public RequestController(IServiceProvider container) : base(container)
        {
            _requestSvc = container.GetService<IRequestSvc>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("api/request/submit")]
        [HttpPost]
        public ServiceResponseDTO<RequestDTO> Submit([FromBody]RequestDTO info)
        {
            var result = _requestSvc.SaveRequest(info);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/request/get")]
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<RequestDTO>> Get()
        {
            try
            {
                return _requestSvc.GetRequest();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<IEnumerable<RequestDTO>>(false, null, errors);
            }
        }

    }
}
