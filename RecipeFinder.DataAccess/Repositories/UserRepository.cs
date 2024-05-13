using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

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
                UserName = user.Email, // В Identity UserName часто используется как email
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var result = await _userManager.CreateAsync(userEntity, user.PasswordHash);

            if (result.Succeeded)
            {
                return new ApplicationUser
                {
                    Id = userEntity.Id, // IdentityUser использует строковый ID
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName
                };
            }

            throw new InvalidOperationException("User could not be created");
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var userEntity = await _userManager.FindByIdAsync(id);

            if (userEntity == null) return null;

            return new ApplicationUser
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName
            };
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => new ApplicationUser
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            var userEntity = await _userManager.FindByIdAsync(user.Id);
            if (userEntity != null)
            {
                userEntity.Email = user.Email;
                userEntity.UserName = user.Email;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;

                await _userManager.UpdateAsync(userEntity);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var userEntity = await _userManager.FindByIdAsync(id);
            if (userEntity != null)
            {
                await _userManager.DeleteAsync(userEntity);
            }
        }
    }
}
