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
    public class HrDashboardSvc : BaseSvc, IHrDashboardSvc
    {
        private readonly IHrDashboardRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public HrDashboardSvc(IServiceProvider container, IMapper mapper)
            : base(container, mapper)
        {
            _repository = container.GetService<IHrDashboardRepository>();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<HiringRequestDTO>> GetHriringRequests(int skillId = 0, int projectId = 0, int statusID = 0)
        {
            var result = new ServiceResponseDTO<IEnumerable<HiringRequestDTO>>();
            try
            {
                result.ResponseData = _repository.GetHriringRequests(skillId, projectId, statusID).Select(hr => Mapper.Map<HiringRequestDTO>(hr));
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
