using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public interface ITechnologyDetailService
    {
        ServiceResponseDTO<TechnologyDetailDTO> GetTechnologyDetail(int id);
        ServiceResponseDTO<TechnologyDetailDTO> GetTechnologyDetail(string name);
        ServiceResponseDTO<TechnologyDetailDTO> SaveTechnologyDetail(TechnologyDetailDTO dto);

        ServiceResponseDTO<List<TechnologyDetailDTO>> GetTechnologyDetailList();

        ServiceResponseDTO<TechnologyDetailDTO> ToggleActive(int id);
    }
}
