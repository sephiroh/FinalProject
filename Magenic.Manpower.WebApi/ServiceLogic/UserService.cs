using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.WebApi.DTO;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.WebApi.Services.Repository;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Magenic.Manpower.EFCore.Models;
using AutoMapper;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : BaseSvc, IUserService
    {
        private readonly IUserContextRepository _userContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public UserService(IServiceProvider container, IMapper mapper) : base(container, mapper)
        {
            _userContext = container.GetService<IUserContextRepository>();
        }

        /// <summary>
        /// validates and creates a new user into the database.
        /// </summary>
        /// <param name="newUser">new user to be validated and added.</param>
        /// <returns></returns>
        public ServiceResponseDTO<UserDTO> AddUser(UserDTO newUser)
        {
            List<string> errors = ValidateUserDTO(newUser);
            
            if (errors.Count > 0)
                return new ServiceResponseDTO<UserDTO>() { Errors = errors, ResponseData = newUser, Success = false };

            var password = string.Format("{0}{1}12345"
               , newUser.Firstname.Length >= 3 ? newUser.Firstname.Substring(0, 3).ToLower() : newUser.Firstname
               , newUser.Lastname.Length >= 3 ? newUser.Lastname.Substring(0, 3).ToLower() : newUser.Lastname);
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);

            var userEntity = new User()
            {
                Firstname = newUser.Firstname,
                Lastname = newUser.Lastname,
                ContactNo = newUser.ContactNumber,
                Email = newUser.Email,
                RoleId = newUser.RoleId,
                Salt = salt,
                PasswordHash = passwordHash,
                IsActive = true
            };

            var result = _userContext.AddUser(userEntity);

            return new ServiceResponseDTO<UserDTO>() { Success = true };
        }

        /// <summary>
        /// validates and updates the existing user in the database.
        /// </summary>
        /// <param name="updatedUser"></param>
        public void UpdateUser(UserDTO updatedUser)
        {
            List<string> errors = ValidateUserDTO(updatedUser);

            if (errors.Count > 0)
                return;

            var updatedUserEntity = new User()
            {
                Id = updatedUser.Id,
                ContactNo = updatedUser.ContactNumber,
                Email = updatedUser.Email,
                Firstname = updatedUser.Firstname,
                Lastname = updatedUser.Lastname,
                RoleId = updatedUser.RoleId
            };

            _userContext.UpdateUser(updatedUserEntity);
        }

        private List<string> ValidateUserDTO(UserDTO user)
        {
            var errors = new List<string>();

            if (user == null)
                throw new ArgumentNullException();
            if (user.Firstname == string.Empty)
                errors.Add("Firstname is required");
            if (user.Lastname == string.Empty)
                errors.Add("Lastname is required");
            if (user.ContactNumber == string.Empty)
                errors.Add("Contact Number is required");
            if (user.Email == string.Empty)
                errors.Add("Email is required");

            return errors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ServiceResponseDTO<List<UserNavigationDTO>> GetUserNavigationAccess(UserDTO user)
        {
            List<UserNavigationDTO> navigations = new List<UserNavigationDTO>();
            var userToUse = _userContext.GetByEmail(user.Email);

            navigations.Add(new UserNavigationDTO("Home", "/"));

            //navigations are hardcoded for now. 
            //Feel free to update if needed.
            if (userToUse.Role.Name.ToLower() == "administrator")
            {
                navigations.Add(new UserNavigationDTO("Users", "userList"));
                navigations.Add(new UserNavigationDTO("Roles", "roleList"));
                navigations.Add(new UserNavigationDTO("Technologies", "technologyList"));
                navigations.Add(new UserNavigationDTO("Primary Skills", "primarySkillsList"));
                navigations.Add(new UserNavigationDTO("My Account", "myaccount"));
            }
            else
            {
                //consultant manager
                navigations.Add(new UserNavigationDTO("Manpower Request", "requestForm"));
            }

            return new ServiceResponseDTO<List<UserNavigationDTO>>(
                true,
                navigations,
                null);
        }

        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        public ServiceResponseDTO<IEnumerable<UserDTO>> GetUserList()
        {
            var result = new ServiceResponseDTO<IEnumerable<UserDTO>>();

            try
            {
                result.ResponseData = _userContext.GetUserList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }
            return result;
        }

        public ServiceResponseDTO<UserDTO> GetUser(string email)
        {
            var result = _userContext.GetByEmail(email);
            var currentUser = new UserDTO();

            if (result != null)
            {
                currentUser.Firstname = result.Firstname;
                currentUser.Lastname = result.Lastname;
                currentUser.Email = result.Email;
                currentUser.ContactNumber = result.ContactNo;
                currentUser.RoleId = result.RoleId.GetValueOrDefault();
                currentUser.Username = result.Email;
            }
            return new ServiceResponseDTO<UserDTO>() { ResponseData = currentUser};
        }

        public ServiceResponseDTO<UserDTO> GetUser(int userId)
        {
            var result = _userContext.GetByUserId(userId);
            var currentUser = new UserDTO();

            if (result != null)
            {
                currentUser.Firstname = result.Firstname;
                currentUser.Lastname = result.Lastname;
                currentUser.Email = result.Email;
                currentUser.ContactNumber = result.ContactNo;
                currentUser.RoleId = result.RoleId.GetValueOrDefault();
                currentUser.Username = result.Email;
            }
            return new ServiceResponseDTO<UserDTO>() { ResponseData = currentUser };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponseDTO<UserDTO> ToggleActive(int id)
        {
            var result = new ServiceResponseDTO<UserDTO>();

            try
            {
                result.ResponseData = _userContext.ToggleActive(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string>() { ex.Message };
                result.Success = false;
            }

            return result;
        }

        public void UpdateUserPassword(string username, string password)
        {
            try
            {
                 _userContext.UpdateUserPassword(username, password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
