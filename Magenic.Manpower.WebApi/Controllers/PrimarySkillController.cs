
using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[EnableCors("CorsPolicy")]
    [Route("api/primaryskill")]
    public class PrimarySkillController : BaseController
    {
        private readonly IPrimarySkillService _primarySkillService;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public PrimarySkillController(IServiceProvider container) : base(container)
        {
            _primarySkillService = container.GetService<IPrimarySkillService>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<PrimarySkillDTO>> Get()
        {
            try
            {
                return _primarySkillService.GetPrimarySkills();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<IEnumerable<PrimarySkillDTO>>(false, null, errors);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ServiceResponseDTO<PrimarySkillDTO> Get(int id)
        {
            try
            {
                return _primarySkillService.GetPrimarySkill(id);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<PrimarySkillDTO>(false, null, errors);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public ObjectResult Post([FromBody]PrimarySkillDTO value)
        {
            try
            {
                var primarySkill = _primarySkillService.AddPrimarySkill(value);
                return new OkObjectResult(primarySkill.ResponseData);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>        
        /// <param name="value"></param>
        [HttpPut]
        public ServiceResponseDTO<PrimarySkillDTO> Put([FromBody]PrimarySkillDTO value)
        {
            var response = new ServiceResponseDTO<PrimarySkillDTO>();

            try
            {
                if (value.Id != 0)
                    response = _primarySkillService.UpdatePrimarySkill(value);
                else
                    response.Errors.Add("Id not present.");
            }
            catch (Exception ex)
            {
                response.Errors = new List<string>() { ex.Message };
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
