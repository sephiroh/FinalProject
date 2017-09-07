using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public interface ITechnologyDetailRepository
    {
        Technology GetTechnologyDetailById(int id);
        Technology GetTechnologyDetailByName(string name);
        Technology CreateTechnologyDetail(Technology tech);
        Technology UpdateTechnologyDetail(Technology tech);

        IList<Technology> GetTechnologyDetailList();
        TechnologyDetailDTO ToggleActive(int id);
    }
}
