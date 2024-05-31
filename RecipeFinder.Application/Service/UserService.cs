using Microsoft.AspNetCore.Identity;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUserEntity> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUserEntity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Password is required.");

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
                throw new InvalidOperationException("A user with the given email already exists.");

            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;
            return user;
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return await _userRepository.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public async Task<bool> AssignUserRoleAsync(string userId, string role)
        {
            return await _userRepository.SetUserRoleAsync(userId, role);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
    }
}
