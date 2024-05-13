using RecipeFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Core.Abstractions
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
    }
}
