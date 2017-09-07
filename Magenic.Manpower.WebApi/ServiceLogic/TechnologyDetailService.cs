using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.Services.Repository;
using AutoMapper;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public class TechnologyDetailService : BaseSvc, ITechnologyDetailService
    {
        private readonly ITechnologyDetailRepository _techDetailContext;
        public TechnologyDetailService(IServiceProvider provider, IMapper mapper) : base(provider, mapper)
        {
            _techDetailContext = provider.GetService<ITechnologyDetailRepository>();
        }
        public ServiceResponseDTO<TechnologyDetailDTO> GetTechnologyDetail(string name)
        {
            ServiceResponseDTO<TechnologyDetailDTO> result = new ServiceResponseDTO<TechnologyDetailDTO>(true, new TechnologyDetailDTO(), new List<string>());
            if (name.Trim().Length == 0)
            {
                result.Errors.Add("Name is empty.");
                result.Success = false;
                return result;
            }

            try
            {
                var request = _techDetailContext.GetTechnologyDetailByName(name);
                if (request != null)
                    result.ResponseData = _mapper.Map<TechnologyDetailDTO>(request);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<TechnologyDetailDTO> GetTechnologyDetail(int id)
        {
            ServiceResponseDTO<TechnologyDetailDTO> result = new ServiceResponseDTO<TechnologyDetailDTO>(true, new TechnologyDetailDTO(), new List<string>());
            if (id == 0)
            {
                result.Errors.Add("Id was not set.");
                result.Success = false;
                return result;
            }

            try
            {
                var request = _techDetailContext.GetTechnologyDetailById(id);
                if (request != null)
                    result.ResponseData = _mapper.Map<TechnologyDetailDTO>(request);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<TechnologyDetailDTO> SaveTechnologyDetail(TechnologyDetailDTO dto)
        {
            ServiceResponseDTO<TechnologyDetailDTO> result = new ServiceResponseDTO<TechnologyDetailDTO>(true, new TechnologyDetailDTO(), new List<string>());

            try
            {
                var request = _mapper.Map<Technology>(dto);
                Technology model;

                if (dto.Id == 0)
                    model = _techDetailContext.CreateTechnologyDetail(request);
                else
                    model = _techDetailContext.UpdateTechnologyDetail(request);

                result.ResponseData = _mapper.Map<TechnologyDetailDTO>(model);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        public ServiceResponseDTO<List<TechnologyDetailDTO>> GetTechnologyDetailList()
        {
            ServiceResponseDTO<List<TechnologyDetailDTO>> result = new ServiceResponseDTO<List<TechnologyDetailDTO>>(true, new List<TechnologyDetailDTO>(), new List<string>());

            try
            {
                var reply = _techDetailContext.GetTechnologyDetailList();
                if (reply != null)
                {
                    var list = new List<TechnologyDetailDTO>();
                    foreach (var item in reply)
                        list.Add(_mapper.Map<TechnologyDetailDTO>(item));

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

        public ServiceResponseDTO<TechnologyDetailDTO> ToggleActive(int id)
        {
            var result = new ServiceResponseDTO<TechnologyDetailDTO>();

            try
            {
                result.ResponseData = _techDetailContext.ToggleActive(id);
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
