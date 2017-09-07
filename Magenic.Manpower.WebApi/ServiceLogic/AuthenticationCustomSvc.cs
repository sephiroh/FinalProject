using System;
using System.Linq;
using Magenic.Manpower.WebApi.DTO;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.Services.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationCustomSvc : BaseSvc, IAuthenticationSvc
    {
        private readonly IUserContextRepository _userContext;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public AuthenticationCustomSvc(IServiceProvider container, IMapper mapper) : base(container, mapper)
        {
            _userContext = container.GetService<IUserContextRepository>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ServiceResponseDTO<CurrentUserDTO> Authenticate(string username, string password)
        {
            var response = new ServiceResponseDTO<CurrentUserDTO>();
            response.Success = false;
            response.ResponseData = new DTO.CurrentUserDTO();

            try
            {
                var user = _userContext.GetByEmail(username);

                if (user == null)
                    throw new Exception("User not found.");

                //bypass admin password hashing for now
                if (username == "admin@magenic.com")
                {
                    response.Success = password == "admadm12345";
                    if (!response.Success)
                        throw new Exception("Invalid password.");
                }
                else
                {
                    if(!user.IsActive)
                        throw new Exception("User account is deactivated.");

                    byte[] passwordHash = KeyDerivation.Pbkdf2(password, user.Salt, KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);
                    if (!user.PasswordHash.SequenceEqual(passwordHash))
                        throw new Exception("Invalid password.");
                }

                var model = _mapper.Map<CurrentUserDTO>(user);
                model.Permissions = new List<string>();
                
                foreach(var permission in user.Role.RolePermission)
                {
                    model.Permissions.Add(permission.Permission.Name);
                }
                response.ResponseData = model;
                response.Success = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }

        }
    }

}
