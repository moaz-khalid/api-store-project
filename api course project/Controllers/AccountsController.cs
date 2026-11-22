using api_course_project.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Auth;
using Store.Core.Services.Contract;

namespace api_course_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(new Store.Core.Dtos.Auth.LoginDto());

            if (user == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(new Store.Core.Dtos.Auth.RegisterDto());

            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid registration."));

            return Ok(user);
        }

    }
}
