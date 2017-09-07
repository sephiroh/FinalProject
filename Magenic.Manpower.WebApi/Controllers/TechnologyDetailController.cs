using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class TechnologyDetailController : BaseController
    {
        private readonly ITechnologyDetailService _service;
        public TechnologyDetailController(IServiceProvider container) : base(container)
        {
            _service = container.GetService<ITechnologyDetailService>();
        }

        /// <summary>
        /// gets the data from db by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ServiceResponseDTO<TechnologyDetailDTO> Get(int id)
        {
            try
            {
                return _service.GetTechnologyDetail(id);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<TechnologyDetailDTO>(false, null, errors);
            }
        }

        /// <summary>
        /// used to check if a particular 
        /// technology name already exists in the db
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("verify/{name}")]
        public ServiceResponseDTO<TechnologyDetailDTO> CheckIfExists(string name)
        {
            try
            {
                return _service.GetTechnologyDetail(name);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<TechnologyDetailDTO>(false, null, errors);
            }
        }

        [HttpPost("create")]
        public ServiceResponseDTO<TechnologyDetailDTO> Create([FromBody]TechnologyDetailDTO dto)
        {
            var response = new ServiceResponseDTO<TechnologyDetailDTO>(false, new TechnologyDetailDTO(), new List<string>());
            try
            {
                if (dto.Id == 0)
                    response = _service.SaveTechnologyDetail(dto);
                else
                    response.Errors.Add("Id is already present.");
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        [HttpPost("update")]
        public ServiceResponseDTO<TechnologyDetailDTO> Update([FromBody]TechnologyDetailDTO dto)
        {
            var response = new ServiceResponseDTO<TechnologyDetailDTO>(false, new TechnologyDetailDTO(), new List<string>());
            try
            {
                if (dto.Id != 0)
                    response = _service.SaveTechnologyDetail(dto);
                else
                    response.Errors.Add("Id not present.");
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }


        [HttpGet("getlist")]
        public ServiceResponseDTO<List<TechnologyDetailDTO>> GetTechnologyList()
        {
            try
            {
                return _service.GetTechnologyDetailList();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<List<TechnologyDetailDTO>>(false, null, errors);
            }
        }


        [HttpDelete("{id}")]
        public ServiceResponseDTO<TechnologyDetailDTO> Delete(int id)
        {
            return _service.ToggleActive(id);
        }
    }
}
