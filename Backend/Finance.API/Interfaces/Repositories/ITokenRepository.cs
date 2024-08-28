using Finance.API.Models;

namespace Finance.API.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task UpdateRefreshToken(AppUser user, RefreshToken newToken, string refreshToken);
    }
}
