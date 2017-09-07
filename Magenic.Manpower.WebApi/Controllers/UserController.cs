using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Magenic.Manpower.WebApi.Services.Repository;

namespace Magenic.Manpower.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationSvc _authenticationSvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="container"></param>
        public UserController(IServiceProvider container) : base(container)
        {
            _userService = _container.GetService<IUserService>();
            _authenticationSvc = container.GetService<IAuthenticationSvc>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponseDTO<IEnumerable<UserDTO>> Get()
        {
            var response = _userService.GetUserList();
            return response;
        }

        [HttpGet]
        [Route("navigations")]
        public ServiceResponseDTO<List<UserNavigationDTO>> GetUserNavigations([FromBody]UserDTO user)
        {
            //call this api via url api/user/navigations            
            var serviceResponse = new ServiceResponseDTO<List<UserNavigationDTO>>();

            try
            {
                serviceResponse = _userService.GetUserNavigationAccess(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(ex.Message);
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        /// <summary>
        /// get the current user details by email
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>    
        [HttpGet("{email}")]
        public ServiceResponseDTO<UserDTO> Get(string email)
        {

            var serviceResponse = new ServiceResponseDTO<UserDTO>();

            try
            {
                serviceResponse = _userService.GetUser(email);
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(ex.Message);
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }



        /// <summary>
        /// get the current user details by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>    
        [HttpGet("getByUserId/{userid}")]
        public ServiceResponseDTO<UserDTO> GetByUserId(int userId)
        {

            var serviceResponse = new ServiceResponseDTO<UserDTO>();

            try
            {
                serviceResponse = _userService.GetUser(userId);
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(ex.Message);
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        /// <summary>
        /// add new user to the database.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResponseDTO<UserDTO> Post([FromBody]UserDTO newUser)
        {
            var serviceResponse = new ServiceResponseDTO<UserDTO>();

            try
            {
                serviceResponse = _userService.AddUser(newUser);
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(ex.Message);
                serviceResponse.Success = false;                                
            }

            return serviceResponse;
        }

        /// <summary>
        /// update the existing user from the database.
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        [HttpPut]
        public ServiceResponseDTO<UserDTO> Put([FromBody]UserDTO updatedUser)
        {
            var response = new ServiceResponseDTO<UserDTO>();

            try
            {
                _userService.UpdateUser(updatedUser);
            }
            catch (Exception ex)
            {
                response.Success = false;
            }

            response.Success = true;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ServiceResponseDTO<UserDTO> Delete(int id)
        {
            return _userService.ToggleActive(id);
        }

        /// <summary>
        /// Updates  User's Password
        /// </summary>
        /// <param name="updatePwdDto"></param>
        /// <returns></returns>
        [HttpPut("updatePassword")]
        public ServiceResponseDTO<CurrentUserDTO> Put([FromBody]UpdatePasswordDTO updatePwdDto)
        {
            var response = new ServiceResponseDTO<CurrentUserDTO>();
            try
            {
                response = _authenticationSvc.Authenticate(updatePwdDto.UserName, updatePwdDto.CurrentPassword);
                if (response.Success)
                {
                    _userService.UpdateUserPassword(updatePwdDto.UserName, updatePwdDto.NewPassword);
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Errors.Add(e.Message);
            }
            return response;
        }
    }
}
