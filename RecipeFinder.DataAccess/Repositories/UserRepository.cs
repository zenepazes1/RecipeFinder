using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinder.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly RecipeFinderDbContext _context;

        public UserRepository(UserManager<ApplicationUserEntity> userManager, RecipeFinderDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            var userEntity = new ApplicationUserEntity
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var result = await _userManager.CreateAsync(userEntity, user.PasswordHash);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return MapToApplicationUser(userEntity);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var userEntity = await _userManager.FindByIdAsync(id);
            return userEntity != null ? MapToApplicationUser(userEntity) : null;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = await _userManager.Users
                .AsNoTracking()
                .ToListAsync();
            return users.Select(MapToApplicationUser);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            return userEntity != null ? MapToApplicationUser(userEntity) : null;
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            var userEntity = await _userManager.FindByIdAsync(user.Id);
            if (userEntity != null)
            {
                userEntity.Email = user.Email;
                userEntity.UserName = user.Email; // This assumes email is the username
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;

                var result = await _userManager.UpdateAsync(userEntity);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"User update failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            var userEntity = await _userManager.FindByIdAsync(id);
            if (userEntity != null)
            {
                var result = await _userManager.DeleteAsync(userEntity);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"User deletion failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public ApplicationUser MapToApplicationUser(ApplicationUserEntity userEntity)
        {
            return new ApplicationUser
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName
            };
        }

        public async Task<bool> SetUserRoleAsync(string userId, string role)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var currentRoles = await _userManager.GetRolesAsync(userEntity);
            if (currentRoles.Contains(role))
            {
                return false; // The user already has this role, no changes made
            }

            var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(userEntity, currentRoles);
            if (!removeFromRolesResult.Succeeded)
            {
                throw new InvalidOperationException($"Removing old roles failed: {string.Join(", ", removeFromRolesResult.Errors.Select(e => e.Description))}");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(userEntity, role);
            if (!addToRoleResult.Succeeded)
            {
                throw new InvalidOperationException($"Adding role failed: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
            }

            return true; // Role assignment successful
        }
        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(userEntity, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Password change failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return true;
        }

    }
}
