using RecipeFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}
