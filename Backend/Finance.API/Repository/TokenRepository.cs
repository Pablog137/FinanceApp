using Finance.API.Data;
using Finance.API.Interfaces.Repositories;
using Finance.API.Migrations;
using Finance.API.Models;

namespace Finance.API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IUserRepository _userRepo;

        public TokenRepository(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task UpdateRefreshToken(AppUser user, RefreshToken newToken, string refreshToken)
        {

            user.RefreshTokens.Add(newToken);

            // Revoke old token
            var oldToken = user.RefreshTokens.Single(r => r.Token == refreshToken);
            oldToken.Revoked = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);
        }
    }
}
