using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.DataAccess.Entities;
using System;
using RecipeFinder.API.Contracts;

namespace RecipeFinder.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, UserManager<ApplicationUserEntity> userManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEntity = new ApplicationUserEntity
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(userEntity, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Optionally assign roles or perform other setup tasks
            await _userService.AssignUserRoleAsync(userEntity.Id, "User"); // Default role

            return Ok(new RegisterResponse(userEntity.Id, userEntity.Email, true, "Registration successful"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(new LoginResponse(token, user.Id, user.Email, await _userManager.GetRolesAsync(user)));
            }

            return Unauthorized("Invalid login attempt.");
        }

        private string GenerateJwtToken(ApplicationUserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
