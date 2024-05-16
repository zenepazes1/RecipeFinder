using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Application.Services;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = users.Select(u => new UserResponse(u.Id, u.Email, u.FirstName, u.LastName));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            var response = new UserResponse(user.Id, user.Email, user.FirstName, user.LastName);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserRequest request)
        {
            var userToUpdate = await _userService.GetUserByIdAsync(id);
            if (userToUpdate == null)
                return NotFound($"User with ID {id} not found.");

            userToUpdate.FirstName = request.FirstName;
            userToUpdate.LastName = request.LastName;

            await _userService.UpdateUserAsync(userToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            var result = await _userService.ChangePasswordAsync(id, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            if (!result)
                return BadRequest("Password could not be changed.");
            return NoContent();
        }
    }
}
