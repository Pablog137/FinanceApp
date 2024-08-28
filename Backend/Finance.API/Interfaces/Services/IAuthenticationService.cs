using Finance.API.Dtos.Users;

namespace Finance.API.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task LogOutAsync();
    }
}
