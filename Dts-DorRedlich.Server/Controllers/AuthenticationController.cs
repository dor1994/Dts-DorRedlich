using Data.DtoModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Services.Interfaces;

namespace Dts_DorRedlich.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel request)
        {
            var response = await _userService.Login(request);

            //var token = await GenerateToken(request);

            //Response.Cookies.Append("AuthToken", token, new CookieOptions
            //{
            //    HttpOnly = true,  // Prevents access to the cookie via JavaScript
            //    Secure = true,    // Ensures the cookie is sent over HTTPS
            //    SameSite = SameSiteMode.Strict
            //});

            if (response.Status == true)
            {
                return Ok(response);
            }

            return Ok(response);

        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] UserModel request)
        {
            var response = await _userService.SingUp(request);

            var token = await GenerateToken(request);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,  // Prevents access to the cookie via JavaScript
                Secure = true,    // Ensures the cookie is sent over HTTPS
                SameSite = SameSiteMode.Strict
            });

            return Ok(response);
        }

        private async Task<string> GenerateToken(UserModel user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
