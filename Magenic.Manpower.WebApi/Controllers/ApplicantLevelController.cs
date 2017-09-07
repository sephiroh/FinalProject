using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.Services.Repository;

namespace Magenic.Manpower.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/applicantLevel")]
    public class ApplicantLevelController : BaseController
    {

        private readonly IApplicantLevelService _applicantLevelSvc;

        public ApplicantLevelController(IServiceProvider container) : base(container)
        {
            _applicantLevelSvc = container.GetService<IApplicantLevelService>();

        }
        /// <summary>
        /// Gets Applicant Level by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ServiceResponseDTO<ApplicantLevelDTO> Get(int id)
        {
            var response = new ServiceResponseDTO<ApplicantLevelDTO>(false, new ApplicantLevelDTO(), new List<string>());
            try
            {
                response = _applicantLevelSvc.GetApplicantLevel(id);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Gets all existing Applicant Levels, active/not 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getlist")]
        public ServiceResponseDTO<List<ApplicantLevelDTO>> GetList()
        {
            var response = new ServiceResponseDTO<List<ApplicantLevelDTO>>(false, new List<ApplicantLevelDTO>(), new List<string>());
            try
            {
                response = _applicantLevelSvc.GetApplicantLevelList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Create New Applicant Level
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public ServiceResponseDTO<ApplicantLevelDTO> Post([FromBody]ApplicantLevelDTO dto)
        {
            var response = new ServiceResponseDTO<ApplicantLevelDTO>(false, new ApplicantLevelDTO(), new List<string>());
            try
            {
                if (dto.Id == 0)
                    response = _applicantLevelSvc.SaveApplicantLevel(dto);
                else
                    response.Errors.Add("This ID already exists.");
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }
        
        /// <summary>
        /// Save changes made to Applicant Level
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public ServiceResponseDTO<ApplicantLevelDTO> Update([FromBody]ApplicantLevelDTO dto)
        {
            var response = new ServiceResponseDTO<ApplicantLevelDTO>(false, new ApplicantLevelDTO(), new List<string>());
            try
            {
                response = _applicantLevelSvc.SaveApplicantLevel(dto);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Perform Soft Delete / Deactivate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ServiceResponseDTO<ApplicantLevelDTO> Delete(int id)
        {
            return _applicantLevelSvc.ToggleActive(id);
        }
        /// <summary>
        /// Checks if Applicant Level already exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("verify/{name}")]
        public ServiceResponseDTO<ApplicantLevelDTO> CheckIfExists(string name)
        {
            var response = new ServiceResponseDTO<ApplicantLevelDTO>(false, new ApplicantLevelDTO(), new List<string>());
            try
            {
                response = _applicantLevelSvc.GetApplicantLevel(name);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
