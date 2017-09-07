using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.DTO;
using AutoMapper;
using Magenic.Manpower.WebApi.Services.Repository;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public class CMDashboardService : BaseSvc, ICmDashboardService
    {
        private readonly ICmDashboardRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="mapper"></param>
        public CMDashboardService(IServiceProvider provider, IMapper mapper) : base(provider, mapper)
        {
            _repository = provider.GetService<ICmDashboardRepository>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestor"></param>
        /// <returns></returns>
        public ServiceResponseDTO<List<HiringRequestDTO>> GetCmProjects(int requestor = 0, int skillId = 0, int projectId = 0, int statusId = 0)
        {
            ServiceResponseDTO<List<HiringRequestDTO>> result = new ServiceResponseDTO<List<HiringRequestDTO>>(true, new List<HiringRequestDTO>(), new List<string>());
            try
            {
                List<HiringRequestDTO> requests = new List<HiringRequestDTO>();
                var projRequests = _repository.GetProjectRequests(requestor, skillId, projectId, statusId);
                foreach (var item in projRequests)
                    requests.Add(_mapper.Map<HiringRequestDTO>(item));

                result.ResponseData = requests.OrderBy(a => a.ProjectName).ToList();
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }

            return result;
        }

        public ServiceResponseDTO<ReferenceNumberReasonDTO> UpdateReferenceNumberStatus(ReferenceNumberReasonDTO dto)
        {
            ServiceResponseDTO<ReferenceNumberReasonDTO> result = new ServiceResponseDTO<ReferenceNumberReasonDTO>() { Success = true, ResponseData = new ReferenceNumberReasonDTO(), Errors = new List<string>() };

            try
            {
                result.Success = _repository.UpdateReferenceNumberStatus(dto.RefNumberId, dto.Reason, dto.NewStatus);
                result.ResponseData = dto;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
