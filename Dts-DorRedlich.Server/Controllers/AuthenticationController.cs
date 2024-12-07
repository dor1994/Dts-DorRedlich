using Data.DtoModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(response);
        }
    }
}
