using AutoMapper;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<RequestDTO, ManpowerRequest>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<PermissionDTO, Permission>().ReverseMap();
            CreateMap<User, CurrentUserDTO>().ForMember(dest => dest.Role, opt => opt.MapFrom(a => a.Role.Name));
            CreateMap<TechnologyDetailDTO, Technology>().ReverseMap();
            CreateMap<ApplicantLevelDTO, ApplicantLevel>().ReverseMap();
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<IEnumerable<ProjectDTO>, IEnumerable<Project>>().ReverseMap();
            CreateMap<HiringRequestDTO, HrDashboardView>().ReverseMap();
            CreateMap<ApplicantsDTO, Applicant>().ReverseMap();
            CreateMap<TaggableDTO, Taggable>().ReverseMap();
            CreateMap<TaggedApplicantDTO, TaggedApplicantView>().ReverseMap();
        }
    }
}
