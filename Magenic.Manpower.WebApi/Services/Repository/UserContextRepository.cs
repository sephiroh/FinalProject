using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserContextRepository : BaseRepository, IUserContextRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public UserContextRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// gets the user entity by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            using (var _newdbContext = new MagenicManpowerDBContext())
            {
                return _newdbContext.User.Include(a => a.Role).ThenInclude(ar => ar.RolePermission).ThenInclude(a => a.Permission).FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            }
        }

        /// <summary>
        /// gets the user entity by userId
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public User GetByUserId(int  userId)
        {
            return _dbContext.User.Include(a => a.Role).ThenInclude(ar => ar.RolePermission).ThenInclude(a => a.Permission).FirstOrDefault(u => u.Id == userId);
        }
        
        /// <summary>
        /// inserts the new user entity into the database.
        /// </summary>
        /// <param name="newUser">the new entity to be inserted.</param>
        /// <returns>the inserted entity along with its id.</returns>
        public User AddUser(User newUser)
        {
            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        /// <summary>
        /// updates the existing user with the supplied updated entity.
        /// </summary>
        /// <param name="updatedUser">the updated entity to be updated along with its id.</param>
        public void UpdateUser(User updatedUser)
        {
            var outdatedUser = _dbContext.User.FirstOrDefault(x => x.Id == updatedUser.Id);

            if (outdatedUser != null)
            {
                outdatedUser.Firstname = updatedUser.Firstname;
                outdatedUser.Lastname = updatedUser.Lastname;
                outdatedUser.ContactNo = updatedUser.ContactNo;
                outdatedUser.Email = updatedUser.Email;
                outdatedUser.RoleId = updatedUser.RoleId;

                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> GetUserList()
        {
            IEnumerable<UserDTO> list;
            try
            {
                list = _dbContext.User.Select(user => new UserDTO()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    ContactNumber = user.ContactNo,
                    IsActive = user.IsActive,
                    RoleId = Convert.ToInt32(user.RoleId)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO ToggleActive(int id)
        {
            var _user = _dbContext.User.Where(u => u.Id == id).Select(u => u).First();

            _user.IsActive = !_user.IsActive;

            _dbContext.User.Update(_user);

            _dbContext.SaveChanges();

            return new UserDTO() { Id = _user.Id, IsActive = _user.IsActive, Firstname = _user.Firstname, Lastname = _user.Lastname, Email = _user.Email };
        }

        /// <summary>
        /// Updates User's Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void UpdateUserPassword(string username = "", string password = "")
        {
            var salt = new byte[128 / 8];
            try
            {
                if (username == "" || password == "")
                {
                    throw new Exception("username or password can't be empty.");
                }

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                byte[] passwordHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);

                var user = _dbContext.User.FirstOrDefault(x => x.Email.ToLower() == username.ToLower());

                if (user != null)
                {
                    user.Salt = salt;
                    user.PasswordHash = passwordHash;

                    _dbContext.User.Update(user);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Password Update not Successful.");
            }
        }
    }
}
