using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    interface IApplicantLevelRepository
    {
        ApplicantLevel GetApplicantLevelById(int id);
        ApplicantLevel GetApplicantLevelByName(string name);
        ApplicantLevel CreateApplicantLevel(ApplicantLevel level);
        ApplicantLevel UpdateApplicantLevel(ApplicantLevel level);

        IList<ApplicantLevel> GetApplicantLevelList();
        ApplicantLevelDTO ToggleActive(int id);
    }
}
