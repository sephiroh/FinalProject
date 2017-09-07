using System;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Magenic.Manpower.EFCore.Models;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.WebApi.Email;
using Microsoft.Extensions.Options;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestSvc : BaseSvc, IRequestSvc
    {
        private readonly IRequestContextRepository _requestCtxRepository;
        private readonly IReferenceNumberRepository _referenceNumberRepository;
        private readonly IRequestTechnologyRepository _requestTechnologyRepository;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IProjectManagementService _projectService;
        private readonly IRoleService _roleService;
        private readonly IStatusRepository _statusRepository;
        private readonly IApplicantLevelRepository _applicantLevelRepository;
        private readonly EmailSettings _emailSettings;
        private readonly ITaggableRepository _taggableApplicantsReposity;
        private readonly ITaggedApplicantRepository _taggedApplicantRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        /// <param name="emailSettings"></param>
        /// <param name="taggbleApplicantsReposity"></param>
        public RequestSvc(IServiceProvider container, IMapper mapper, IOptions<EmailSettings> emailSettings) : base(container, mapper)
        {
            _requestCtxRepository = container.GetService<IRequestContextRepository>();
            _referenceNumberRepository = container.GetService<IReferenceNumberRepository>();
            _requestTechnologyRepository = container.GetService<IRequestTechnologyRepository>();
            _emailService = container.GetService<IEmailService>();
            _userService = container.GetService<IUserService>();
            _projectService = container.GetService<IProjectManagementService>();
            _roleService = container.GetService<IRoleService>();
            _statusRepository = container.GetService<IStatusRepository>();
            _applicantLevelRepository = container.GetService<IApplicantLevelRepository>();
            _emailSettings = emailSettings.Value;
            _taggableApplicantsReposity = container.GetService<ITaggableRepository>(); ;
            _taggedApplicantRepository = container.GetService<ITaggedApplicantRepository>(); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ServiceResponseDTO<RequestDTO> SaveRequest(RequestDTO dto)
        {
            try
            {
                var request = _mapper.Map<ManpowerRequest>(dto);
                var requestId = _requestCtxRepository.SaveRequest(request);
                dto.Id = requestId;

                var taggbleApplicants = _taggableApplicantsReposity.GetTaggableApplicants(request.PrimarySkillId);
                var applicantLevel = _applicantLevelRepository.GetApplicantLevelList().ToArray();
                for (var i = 0; i < dto.NumberOfHires.Count; i++)
                {
                    for (int j = 0; j < dto.NumberOfHires[i]; j++)
                    {
                        //TODO: Create enum of statuses
                        var refNoId = _referenceNumberRepository.CreateReferenceNumber(requestId, 1, applicantLevel[i].Id);

                        foreach (var taggbleApplicant in taggbleApplicants)
                        {
                            var taggedApplicant = new TaggedApplicant()
                            {
                                ReferenceNoId = refNoId,
                                ApplicantId = taggbleApplicant.Id,
                                TagDate = DateTime.Now
                            };
                            _taggedApplicantRepository.Add(taggedApplicant);
                        }
                    }
                }

                for (var i = 0; i < dto.Technologies.Count; i++)
                {
                    var model = new ManpowerRequestTechnology()
                    {
                        ManpowerRequestId = requestId,
                        TechnologyId = dto.Technologies[i]
                    };
                    _requestTechnologyRepository.AddRequestTechnology(model);
                }

                #region Send Email

                UserDTO requestor = new UserDTO();
                List<string> recipientEmails = new List<string>();
                // get user list
                var usersDto = _userService.GetUserList();
                if (usersDto.Success)
                {
                    requestor = usersDto.ResponseData.FirstOrDefault(a => a.Id == dto.RequestedBy);
                    // get roles with 'Tag Applicant' permission
                    var roles = _roleService.GetRoles();
                    if (roles.Success)
                    {
                        var rolesWithTagApplicant = roles.ResponseData.SelectMany(a => a.Permissions).Where(b => b.Name == "Tag Applicants").Select(c => c.RoleId).Distinct();
                        recipientEmails = usersDto.ResponseData.Where(a => rolesWithTagApplicant.Contains(a.RoleId)).Select(a => a.Email).ToList();
                    }
                }
                // get project details
                var projectDto = _projectService.GetProject(dto.ProjectId);
                // build email reference
                TagApplicant emailReference = new TagApplicant();
                emailReference.ProjectName = projectDto.Success ? projectDto.ResponseData.Name : string.Join(String.Empty, projectDto.Errors);
                emailReference.EstimateHires = dto.NumberOfHires.Sum();
                emailReference.Requestor = requestor != null ? requestor.Firstname + " " + requestor.Lastname : "";
                emailReference.ManpowerAppUrl = _emailSettings.ManpowerAppUrl;
                _emailService.BuildEmailMessage(requestor != null ? requestor.Email : "", recipientEmails, emailReference);
                #endregion

                return new ServiceResponseDTO<RequestDTO>() { ResponseData = dto, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponseDTO<RequestDTO>() { Errors = new List<string>() { ex.Message }, Success = false };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<RequestDTO>> GetRequest()
        {
            ServiceResponseDTO<IEnumerable<RequestDTO>> result = new ServiceResponseDTO<IEnumerable<RequestDTO>>();
            IEnumerable<RequestDTO> request;
            try
            {
                request = _requestCtxRepository.GetRequest();
                result.ResponseData = request;
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
