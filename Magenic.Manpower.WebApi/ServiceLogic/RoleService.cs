using System;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.Services.Repository;
using Magenic.Manpower.EFCore.Models;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Magenic.Manpower.WebApi.ServiceLogic.BaseSvc" />
    /// <seealso cref="Magenic.Manpower.WebApi.ServiceLogic.IRoleService" />
    public class RoleService : BaseSvc, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public RoleService(IServiceProvider container, IMapper mapper)
            : base(container, mapper)
        {
            _roleRepository = container.GetService<IRoleRepository>();
        }

        /// <summary>
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        public ServiceResponseDTO<RoleDTO> AddRole(RoleDTO newRole)
        {
            var result = new ServiceResponseDTO<RoleDTO>();

            try
            {
                var valid = _roleRepository.Validate(Mapper.Map<Role>(newRole));
                
                if (valid)
                {
                    newRole.IsActive = true;
                    result.ResponseData = _roleRepository.AddRole(newRole);
                }
                else
                {
                    result.ResponseData = null;
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<RoleDTO>> GetRoles()
        {
            var result = new ServiceResponseDTO<IEnumerable<RoleDTO>>();

            try
            {
                result.ResponseData = _roleRepository.GetRoles();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Updates the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public ServiceResponseDTO<RoleDTO> UpdateRole(RoleDTO role)
        {
            var result = new ServiceResponseDTO<RoleDTO>();

            try
            {
                var _role = Mapper.Map<Role>(role);
                var valid = _roleRepository.Validate(_role);

                if (valid)
                {
                    _role.RolePermission = role.Permissions.Select(a => new RolePermission()
                    {
                        RoleId = role.Id,
                        PermissionId = a.Id
                    }).ToList();

                    role = Mapper.Map<RoleDTO>(_roleRepository.UpdateRole(_role));

                    _roleRepository.UpdateRolePermission(role.Id, _role.RolePermission)
                        .ToList()
                        .ForEach(a =>
                        {
                            role.Permissions.Add(Mapper.Map<PermissionDTO>(a));
                        });

                    result.ResponseData = role;
                }
                else
                {
                    result.ResponseData = null;
                }
                 
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponseDTO<RoleDTO> ToggleActive(int id)
        {
            var result = new ServiceResponseDTO<RoleDTO>();

            try
            {
                result.ResponseData = _roleRepository.ToggleActive(id);
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
