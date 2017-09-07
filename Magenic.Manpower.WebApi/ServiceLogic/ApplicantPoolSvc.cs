using AutoMapper;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicantPoolSvc : BaseSvc, IApplicantPoolSvc
    {
        private readonly ITaggableRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public ApplicantPoolSvc(IServiceProvider container, IMapper mapper)
            : base(container, mapper)
        {
            _repository = container.GetService<ITaggableRepository>();

        }

        public ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> GetTaggedApplicantPool(int refNoId)
        {
            var result = new ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>>();
            try
            {
                result.ResponseData = _repository.GetTaggedApplicants(refNoId).Select(row => Mapper.Map<TaggedApplicantDTO>(row));
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        public ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>> GetTaggedReferencePool(int appId)
        {
            var result = new ServiceResponseDTO<IEnumerable<TaggedApplicantDTO>>();
            try
            {
                result.ResponseData = _repository.GetTaggedRefNumbers(appId).Select(row => Mapper.Map<TaggedApplicantDTO>(row));
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }
    }
}
