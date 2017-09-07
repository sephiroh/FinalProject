using Magenic.Manpower.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public interface ILookupRepository
    {
        List<Permission> Permissions { get; }
        List<MagenicRegion> Regions { get; }
        List<ApplicantLevel> ApplicantLevels { get; }
        List<Status> Status { get; }
        List<ApplicantStatus> ApplicantStatus { get; }
    }
}
