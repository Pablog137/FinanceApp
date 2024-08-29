using Finance.API.Dtos.Users;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Finance.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserRepository _userRepo;
        private readonly ITransactionRepository _transactionRepo;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IAccountRepository accountRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepo = accountRepository;
            _userRepo = userRepository;
            _transactionRepo = transactionRepository;
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            using var transaction = await _transactionRepo.BeginTransactionAsync();

            try
            {
                var user = await _userRepo.GetUserByEmailAsync(loginDto.Email);

                if (user == null) throw new UnauthorizedAccessException("Invalid email or password");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid email or password");

                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);

                await _userRepo.UpdateAsync(user);

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
            using var transaction = await _transactionRepo.BeginTransactionAsync();

            try
            {

                var userExistsByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
                if(userExistsByEmail != null) throw new ArgumentException("Email is already taken");

                var userExistsByUsername = await _userManager.FindByNameAsync(registerDto.Username);
                if(userExistsByUsername != null) throw new ArgumentException("Username is already taken");

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

                await _accountRepo.CreateAsync(account);

                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);

                await _userRepo.UpdateAsync(user);


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
