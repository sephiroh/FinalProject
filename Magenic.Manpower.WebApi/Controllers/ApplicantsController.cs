using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Magenic.Manpower.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/applicants")]
    public class ApplicantsController : BaseController
    {
        private readonly IApplicantsService _applicantsService;
        private readonly IApplicantPoolSvc _applicantPoolService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ApplicantsController(IServiceProvider container) : base(container)
        {
            _applicantsService = container.GetService<IApplicantsService>();
            _applicantPoolService = container.GetService<IApplicantPoolSvc>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<ApplicantsDTO>> Get()
        {
            try
            {
                return _applicantsService.GetApplicants();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<IEnumerable<ApplicantsDTO>>(false, null, errors);
            }
        }

        /// <summary></summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/refNos")]
        public ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> GetRefNos(int id = 0)
        {
            try
            {
                return _applicantPoolService.GetTaggedReferencePool(id);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return new ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>>(false, null, errors);
            }
        }

        /// <summary>
        /// Posts the specified dto.
        /// </summary>
        /// <param name="value">The dto.</param>
        /// <returns>Returns the project added</returns>
        [HttpPost]
        public ServiceResponseDTO<ApplicantsDTO> Post([FromBody]ApplicantsDTO value)
        {
            return _applicantsService.AddApplicant(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public ServiceResponseDTO<ApplicantsDTO> Put(int id, [FromBody]ApplicantsDTO value)
        {
            return _applicantsService.UpdateApplicant(value);
        }

    }
}
