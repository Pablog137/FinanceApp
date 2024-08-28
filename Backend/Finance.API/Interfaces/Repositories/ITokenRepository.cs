using API.Models;

namespace API.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task UpdateRefreshToken(AppUser user, RefreshToken newToken, string refreshToken);
    }
}
