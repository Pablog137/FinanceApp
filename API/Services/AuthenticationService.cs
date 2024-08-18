using API.Dtos.Users;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAccountRepository _accountRepository;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Get user by email -> Move it to a repository
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) throw new UnauthorizedAccessException("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid email or password");

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) throw new Exception("Failed to create user");

            var role = await _userManager.AddToRoleAsync(user, "User");
            if (!role.Succeeded) throw new Exception("Failed to add user to role");

            var account = new Account
            {
                Name = $"{user.UserName} account",
                UserId = user.Id
            };

            await _accountRepository.CreateAsync(account);

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            };


        }
    }
}
