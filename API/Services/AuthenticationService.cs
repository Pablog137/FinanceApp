using API.Dtos.Users;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            using var transaction = await _accountRepository.BeginTransactionAsync();

            try
            {
                var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

                if (user == null) throw new UnauthorizedAccessException("Invalid email or password");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid email or password");

                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);

                await _userRepository.UpdateAsync(user);

                await transaction.CommitAsync();

                return user.ToDto(_tokenService.GenerateToken(user), refreshToken.Token);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }


        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            using var transaction = await _accountRepository.BeginTransactionAsync();

            try
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

                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);

                await _userRepository.UpdateAsync(user);


                await transaction.CommitAsync();

                return user.ToDto(_tokenService.GenerateToken(user), refreshToken.Token);

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }



        }


        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();

        }
    }
}
