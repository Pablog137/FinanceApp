using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Finance.API.Data;
using Finance.API.Dtos.Token;
using Finance.API.Dtos.Users;
using Finance.API.Interfaces;
using Finance.API.Interfaces.Services;
using Finance.API.Models;
using Finance.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
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
            catch (ArgumentException a)
            {
                Log.Error(a, a.Message);
                return Conflict(a.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error registering user");
                return StatusCode(500, e.Message);
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
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error logging user");
                return StatusCode(500, e.Message);
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error logging out user");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("refresh-token")]

        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var refreshToken = await _tokenService.HandleRefreshTokenAsync(request.RefreshToken);
                return Ok(refreshToken);

            }
            catch (Exception e)
            {
                Log.Error(e, "Error with refresh token");
                return StatusCode(500, e.Message);
            }

        }
    }
}