using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public interface IUserService
    {
        /// <summary>
        /// validates and creates a new user into the database.
        /// </summary>
        /// <param name="newUser">new user to be validated and added.</param>
        /// <returns>the added user entity with its id.</returns>
        ServiceResponseDTO<UserDTO> AddUser(UserDTO newUser);
        /// <summary>
        /// validates and updates the existing user in the database.
        /// </summary>
        /// <param name="updatedUser"></param>
        void UpdateUser(UserDTO updatedUser);
        ServiceResponseDTO<List<UserNavigationDTO>> GetUserNavigationAccess(UserDTO user);

        /// <summary>
        /// Get User list
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<IEnumerable<UserDTO>> GetUserList();

        /// <summary>
        /// Get User by email
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<UserDTO> GetUser(string email);

        /// <summary>
        /// Get User by userId
        /// </summary>
        /// <returns></returns>
        ServiceResponseDTO<UserDTO> GetUser(int userId);

        /// <summary>
        /// Activate/deactivate user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponseDTO<UserDTO> ToggleActive(int id);

        /// <summary>
        /// updates user password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void UpdateUserPassword(string username, string password);
    }
}
