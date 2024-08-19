using API.Models;

namespace API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByEmailAsync(string email);

        Task UpdateAsync(AppUser user);

        Task<AppUser?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
