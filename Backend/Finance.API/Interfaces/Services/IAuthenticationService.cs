using API.Dtos.Users;

namespace API.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task LogOutAsync();
    }
}
