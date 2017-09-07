using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Magenic.Manpower.WebApi.DTO;
using Magenic.Manpower.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public RoleRepository(MagenicManpowerDBContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        public RoleDTO AddRole(RoleDTO newRole)
        {
            var role = new Role()
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Name = newRole.Name
            };


            _dbContext.Role.Add(role);
            _dbContext.SaveChanges();

            newRole.Id = role.Id;

            AddRolePermissions(newRole);

            _dbContext.SaveChanges();
            return newRole;
        }

        /// <summary>
        /// Saves the role permissions.
        /// </summary>
        /// <param name="newRole">The new role.</param>
        private void AddRolePermissions(RoleDTO newRole)
        {
            var rolePermissions = newRole.Permissions.Select(t => new RolePermission()
            {
                RoleId = newRole.Id,
                PermissionId = t.Id
            }).ToList();

            _dbContext.RolePermission.AddRange(rolePermissions);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="currentPermissions"></param>
        private void UpdateRolePermissions(RoleDTO role, ICollection<RolePermission> currentPermissions)
        {
            var toAdd = role.Permissions.Where(p => !currentPermissions.Any(cp => cp.PermissionId == p.Id)).Select(p => new RolePermission() { RoleId = p.RoleId, PermissionId = p.Id });
            var toRemove = currentPermissions.Where(cp => !role.Permissions.Any(p => p.Id == cp.PermissionId));

            foreach (var p in toRemove)
            {
                currentPermissions.Remove(p);
            }

            foreach (var p in toAdd)
            {
                p.RoleId = role.Id;
                currentPermissions.Add(p);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO GetRole(int id)
        {
            RoleDTO item;
            try
            {
                item = _dbContext.Role.Where(role => role.Id == id).Select(role => new RoleDTO()
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsActive = role.IsActive,
                    Permissions = role.RolePermission.Select(b => new PermissionDTO()
                    {
                        RoleId = role.Id,
                        Id = b.PermissionId,
                        Name = b.Permission.Name,
                        Description = b.Permission.Description // No lazy loading??? <- To Edit if 
                    }).ToList()
                }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDTO> GetRoles()
        {
            IEnumerable<RoleDTO> list;
            try
            {
                list = _dbContext.Role
                    .Select(role => new RoleDTO()
                    {
                        Id = role.Id,
                        Name = role.Name,
                        IsActive = role.IsActive,
                        Permissions = role.RolePermission.Select(b => new PermissionDTO()
                        {
                            Id = b.PermissionId,
                            Name = b.Permission.Name,
                            Description = b.Permission.Description,
                            RoleId = b.RoleId
                        }).ToList()
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
        /// <param name="role"></param>
        /// <returns></returns>
        public Role UpdateRole(Role role)
        {
            Role _role;
            using (var dbCtx = new MagenicManpowerDBContext())
            {
                _role = dbCtx.Role.FirstOrDefault(r => r.Id.Equals(role.Id));
            }

            if (_role == null)
                throw new KeyNotFoundException("Role Id not found.");

            _role.DateUpdated = DateTime.Now;
            _role.Name = role.Name;
            _role.IsActive = role.IsActive;

            _dbContext.Entry(_role).State = _role.Id == 0 ? EntityState.Added : EntityState.Modified;
            _dbContext.SaveChanges();

            return _role;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO ToggleActive(int id)
        {
            var _role = _dbContext.Role.Where(r => r.Id == id).Select(r => r).First();

            _role.IsActive = !_role.IsActive;
            _role.DateUpdated = DateTime.Now;

            _dbContext.Role.Update(_role);

            _dbContext.SaveChanges();

            return new RoleDTO() { Id = _role.Id, IsActive = _role.IsActive, Name = _role.Name };
        }

        /// <summary>
        /// Updates the role permission.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="currentPermissions">The role permissions.</param>
        /// <returns></returns>
        public IQueryable<Permission> UpdateRolePermission(int roleId, ICollection<RolePermission> currentPermissions)
        {
            List<RolePermission> oldPermissions;
            using (var dbCtx = new MagenicManpowerDBContext())
            {
                oldPermissions = dbCtx.RolePermission.Where(r => r.RoleId.Equals(roleId)).ToList();
            }

            // Delete replaced
            oldPermissions.ForEach(a =>
            {
                if (!currentPermissions.Any(b => b.PermissionId.Equals(a.PermissionId)))
                {
                    _dbContext.Entry(a).State = EntityState.Deleted;
                }
            });

            // Add new and retain
            currentPermissions
                .ToList()
                .ForEach(a =>
            {
                _dbContext.Entry(a).State = !oldPermissions.Any(b => b.PermissionId.Equals(a.PermissionId)) ? EntityState.Added : EntityState.Unchanged;
            });

            _dbContext.SaveChanges();

            return _dbContext.Permission
                .Where(a => currentPermissions
                .Any(b => b.PermissionId.Equals(a.Id)));
        }

        /// <summary>
        /// Validates the specified role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public bool Validate(Role role)
        {
            if (role.Id == 0)
            {
                return !_dbContext.Role.Any(r => r.Name.Equals(role.Name));
            }

            return !_dbContext.Role
                .Where(a => !a.Id.Equals(role.Id))
                .Any(r => r.Name.Equals(role.Name));
        }
    }
}
