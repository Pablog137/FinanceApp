using Finance.API.Dtos.Token;
using Finance.API.Dtos.Users;
using Finance.API.Exceptions;
using Finance.API.Interfaces;
using Finance.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase, IAuthentication
    {


        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IAuthenticationService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var userDto = await _authService.RegisterAsync(registerDto);
                return Ok(userDto);

            }
            catch (ArgumentException e)
            {
                Log.Error(e, e.Message);
                return Conflict(new { message = e.Message });
            }
            catch (Exception e)
            {
                Log.Error(e, "Error registering user");
                return StatusCode(500, new { message = "Error registering user" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var userDto = await _authService.LoginAsync(loginDto);
                return Ok(userDto);

            }
            catch (UnauthorizedAccessException e)
            {
                Log.Error(e, "Invalid email or password");
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception e)
            {
                Log.Error(e, "Error logging user");
                return StatusCode(500, new { message = e.Message });
            }

        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _authService.LogOutAsync();
                return Ok("Logged out successfully");
            }
            catch (Exception e)
            {
                Log.Error(e, "Error logging out user");
                return StatusCode(500, new { message = "Error logging out user" });
            }

        }

        [HttpPost("refresh-token")]
        //[Authorize]

        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var refreshToken = await _tokenService.HandleRefreshTokenAsync(request.RefreshToken);
                return Ok(refreshToken);

            }
            catch (InvalidTokenException e)
            {
                Log.Error(e, "Token does not exist");
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception e)
            {
                Log.Error(e, "Error with refresh token");
                return StatusCode(500, new { message = "Error with refresh token" });
            }

        }
    }
}