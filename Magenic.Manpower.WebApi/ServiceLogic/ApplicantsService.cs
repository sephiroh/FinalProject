using System;
using System.Collections.Generic;
using AutoMapper;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicantsService : BaseSvc, IApplicantsService
    {
        private readonly IApplicantsRepository _applicantsRepository;
        private readonly IReferenceNumberRepository _refNumberRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public ApplicantsService(IServiceProvider container, IMapper mapper) : base(container, mapper)
        {
            _applicantsRepository = container.GetService<IApplicantsRepository>();
            _refNumberRepository = container.GetService<IReferenceNumberRepository>();
        }

        
        /// <summary>
        /// Get list of Applicants
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<ApplicantsDTO>> GetApplicants()
        {
            ServiceResponseDTO<IEnumerable<ApplicantsDTO>> result = new ServiceResponseDTO<IEnumerable<ApplicantsDTO>>();
            IEnumerable<ApplicantsDTO> applicants;
            try
            {
                applicants = _applicantsRepository.GetApplicants();
                result.ResponseData = applicants;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Add Applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        public ServiceResponseDTO<ApplicantsDTO> AddApplicant(ApplicantsDTO applicant)
        {
            var result = new ServiceResponseDTO<ApplicantsDTO>();

            try
            {
                var _applicant = Mapper.Map<Applicant>(applicant);
                var valid = _applicantsRepository.Validate(_applicant);

                if (valid)
                {
                    applicant = Mapper.Map<ApplicantsDTO>(_applicantsRepository.AddApplicant(_applicant));
                    applicant.Id = _applicant.Id;
                    applicant.StatusType = Enum.GetName(typeof(ApplicantStatusType), applicant.Status);
                    result.ResponseData = applicant;
                }
                else
                {
                    result.ResponseData = null;
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// Updates the applicant.
        /// </summary>
        /// <param name="applicant">Applicant.</param>
        /// <returns></returns>
        public ServiceResponseDTO<ApplicantsDTO> UpdateApplicant(ApplicantsDTO applicant)
        {
            var result = new ServiceResponseDTO<ApplicantsDTO>();

            try
            {
                var _applicant = Mapper.Map<Applicant>(applicant);
                var valid = _applicantsRepository.Validate(_applicant);

                if (valid)
                {
                    applicant = Mapper.Map<ApplicantsDTO>(_applicantsRepository.UpdateApplicant(_applicant));
                    applicant.StatusType = Enum.GetName(typeof(ApplicantStatusType), applicant.Status);

                    result.ResponseData = applicant;
                }
                else
                {
                    result.ResponseData = null;
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<bool> HireApplicant(HiredApplicantDTO applicant)
        {
            var result = new ServiceResponseDTO<bool>();
            
            try
            {
                bool hasApplicantUpdated = _applicantsRepository.HireApplicant(applicant.ApplicantId, applicant.HiredDate);

                if(hasApplicantUpdated)
                {
                    result.Success = _refNumberRepository.FillRequest(applicant.ReferenceNoId, applicant.ApplicantId);
                }

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
