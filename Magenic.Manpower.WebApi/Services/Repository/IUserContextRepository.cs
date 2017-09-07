using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserContextRepository
    {
        /// <summary>
        /// gets the user entity by email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetByEmail(string email);
        /// <summary>
        /// gets the user entity by userId
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetByUserId(int userId);
        /// <summary>
        /// inserts the new user entity into the database.
        /// </summary>
        /// <param name="user">the new entity to be inserted.</param>
        /// <returns>the inserted entity along with its id.</returns>
        User AddUser(User user);
        /// <summary>
        /// updates the existing user with the supplied updated entity.
        /// </summary>
        /// <param name="updatedUser">the updated entity to be updated along with its id.</param>
        void UpdateUser(User updatedUser);

        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDTO> GetUserList();

        /// <summary>
        /// Activate/Deactivate user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDTO ToggleActive(int id);

        /// <summary>
        /// Updates User's password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        void UpdateUserPassword(string email, string password);
    }
}
