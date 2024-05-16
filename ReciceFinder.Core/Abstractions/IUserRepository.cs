using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
        Task<bool> SetUserRoleAsync(string userId, string role);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

    }
}
