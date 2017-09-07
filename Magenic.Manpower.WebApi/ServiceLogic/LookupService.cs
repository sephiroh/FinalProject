using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.Services.Repository;
using Magenic.Manpower.WebApi.DTO;
using AutoMapper;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class LookupService :BaseSvc, ILookupService
    {
        private readonly ILookupRepository _lookupRepository;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public LookupService(IServiceProvider container, IMapper mapper)
            :base(container, mapper)
        {
            _lookupRepository = container.GetService<ILookupRepository>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> ApplicationLevels()
        {
            var dto = new List<DTO.SelectionItemDTO>();
            var response = new ServiceResponseDTO<IEnumerable<SelectionItemDTO>>();
            try
            {
                var result = _lookupRepository.ApplicantLevels;
                if (response != null && result.Count > 0)
                {
                    result.ForEach(a => dto.Add(new DTO.SelectionItemDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }));

                    response.Success = true;
                    response.ResponseData = dto;
                }
                else
                    throw new Exception("No regions found.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Regions()
        {
            var dto = new List<DTO.SelectionItemDTO>();
            var response = new ServiceResponseDTO<IEnumerable<SelectionItemDTO>>();
            try
            {
                var result = _lookupRepository.Regions;
                if (response != null && result.Count > 0)
                {
                    result.ForEach(a => dto.Add(new DTO.SelectionItemDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }));

                    response.Success = true;
                    response.ResponseData = dto;
                }
                else
                    throw new Exception("No application levels found.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Permissions()
        {
            var dto = new List<DTO.SelectionItemDTO>();
            var response = new ServiceResponseDTO<IEnumerable<SelectionItemDTO>>();
            try
            {
                var result = _lookupRepository.Permissions;
                if (response != null && result.Count > 0)
                {
                    result.ForEach(a => dto.Add(new DTO.SelectionItemDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }));

                    response.Success = true;
                    response.ResponseData = dto;
                }
                else
                    throw new Exception("No permissons found.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> Status()
        {
            var dto = new List<DTO.SelectionItemDTO>();
            var response = new ServiceResponseDTO<IEnumerable<SelectionItemDTO>>();
            try
            {
                var result = _lookupRepository.Status;
                if (response != null && result.Count > 0)
                {
                    result.ForEach(a => dto.Add(new DTO.SelectionItemDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }));

                    response.Success = true;
                    response.ResponseData = dto;
                }
                else
                    throw new Exception("No status found.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<SelectionItemDTO>> ApplicantStatus()
        {
            var dto = new List<DTO.SelectionItemDTO>();
            var response = new ServiceResponseDTO<IEnumerable<SelectionItemDTO>>();
            try
            {
                var result = _lookupRepository.ApplicantStatus;
                if (response != null && result.Count > 0)
                {
                    result.ForEach(a => dto.Add(new DTO.SelectionItemDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }));

                    response.Success = true;
                    response.ResponseData = dto;
                }
                else
                    throw new Exception("No status found.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
