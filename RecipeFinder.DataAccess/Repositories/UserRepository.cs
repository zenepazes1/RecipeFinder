using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;


namespace RecipeFinder.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RecipeFinderDbContext _context;

        public UserRepository(RecipeFinderDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            var userEntity = new UserEntity
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                IsAdmin = user.IsAdmin
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            user.UserId = userEntity.UserId;
            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (userEntity == null) return null;

            return new User
            {
                UserId = userEntity.UserId,
                Username = userEntity.Username,
                Email = userEntity.Email,
                PasswordHash = userEntity.PasswordHash,
                IsAdmin = userEntity.IsAdmin
            };
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    IsAdmin = u.IsAdmin
                }).ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var userEntity = await _context.Users.FindAsync(user.UserId);

            if (userEntity != null)
            {
                userEntity.Username = user.Username;
                userEntity.Email = user.Email;
                userEntity.PasswordHash = user.PasswordHash;
                userEntity.IsAdmin = user.IsAdmin;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
