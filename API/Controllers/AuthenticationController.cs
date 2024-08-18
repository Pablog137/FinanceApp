using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Users;
using API.Interfaces;
using API.Interfaces.Services;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase, IAuthentication
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;
        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, AppDbContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                var account = new Account
                {
                    Balance = 0,
                    Name = $"{user.UserName} account",
                    UserId = user.Id,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };
                var createdAccount = await _context.Accounts.AddAsync(account);

                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "User");
                    if (role.Succeeded)
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new UserDto
                        {
                            Username = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.GenerateToken(user)
                        });
                    }
                    else
                    {
                        return BadRequest("Failed to add user to role");
                    }
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            return Ok(new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            });

        }



    }
}