using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Application.Service;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password // тут потом поправим
            };

            var createdUser = await _userService.CreateUserAsync(user);
            var response = new UserResponse(
                createdUser.UserId,
                createdUser.Username,
                createdUser.Email);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, response);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var response = new UserResponse(
                user.UserId,
                user.Username,
                user.Email);

            return Ok(response);
        }

        // 
    }
}
