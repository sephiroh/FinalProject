using System;
using System.Collections.Generic;
using AutoMapper;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public class ApplicantLevelService : BaseSvc, IApplicantLevelService
    {
        private readonly IApplicantLevelRepository _applicantLevelRepo;

        public ApplicantLevelService(IServiceProvider provider, IMapper mapper) : base(provider, mapper)
        {
            _applicantLevelRepo = provider.GetService<IApplicantLevelRepository>();
        }

        public ServiceResponseDTO<ApplicantLevelDTO> GetApplicantLevel(string name)
        {
            var result = new ServiceResponseDTO<ApplicantLevelDTO>(true, new ApplicantLevelDTO(), new List<string>());
            if (name.Trim().Length == 0)
            {
                result.Errors.Add("Name is empty.");
                result.Success = false;
                return result;
            }

            try
            {
                var request = _applicantLevelRepo.GetApplicantLevelByName(name);
                if (request != null)
                    result.ResponseData = _mapper.Map<ApplicantLevelDTO>(request);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<ApplicantLevelDTO> GetApplicantLevel(int id)
        {
            var result = new ServiceResponseDTO<ApplicantLevelDTO>(true, new ApplicantLevelDTO(), new List<string>());
            if (id == 0)
            {
                result.Errors.Add("Id was not set.");
                result.Success = false;
                return result;
            }

            try
            {
                var request = _applicantLevelRepo.GetApplicantLevelById(id);
                if (request != null)
                    result.ResponseData = _mapper.Map<ApplicantLevelDTO>(request);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<List<ApplicantLevelDTO>> GetApplicantLevelList()
        {
            var result =
                new ServiceResponseDTO<List<ApplicantLevelDTO>>(true, new List<ApplicantLevelDTO>(),
                    new List<string>());

            try
            {
                var reply = _applicantLevelRepo.GetApplicantLevelList();
                if (reply != null)
                {
                    var list = new List<ApplicantLevelDTO>();
                    foreach (var item in reply)
                        list.Add(_mapper.Map<ApplicantLevelDTO>(item));

                    result.ResponseData = list;
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<ApplicantLevelDTO> SaveApplicantLevel(ApplicantLevelDTO dto)
        {
            var result = new ServiceResponseDTO<ApplicantLevelDTO>(true, new ApplicantLevelDTO(), new List<string>());

            try
            {
                var request = _mapper.Map<ApplicantLevel>(dto);
                ApplicantLevel model;

                if (dto.Id == 0)
                    model = _applicantLevelRepo.CreateApplicantLevel(request);
                else
                    model = _applicantLevelRepo.UpdateApplicantLevel(request);

                result.ResponseData = _mapper.Map<ApplicantLevelDTO>(model);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        public ServiceResponseDTO<ApplicantLevelDTO> ToggleActive(int id)
        {
            var result = new ServiceResponseDTO<ApplicantLevelDTO>();

            try
            {
                result.ResponseData = _applicantLevelRepo.ToggleActive(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.Message};
                result.Success = false;
            }

            return result;
        }
    }
}
