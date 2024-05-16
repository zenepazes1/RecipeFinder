using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
        Task<bool> AssignUserRoleAsync(string userId, string role);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
