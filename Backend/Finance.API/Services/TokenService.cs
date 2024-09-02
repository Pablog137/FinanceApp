using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Finance.API.Dtos.Token;
using Finance.API.Exceptions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Finance.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserRepository _userRepo;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITransactionRepository _transactionRepository;
        public TokenService(IConfiguration config, IUserRepository userRepo, IAccountRepository accountRepository, ITokenRepository tokenRepository, ITransactionRepository transactionRepository)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userRepo = userRepo;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _transactionRepository = transactionRepository;
        }


        public string GenerateToken(AppUser user)
        {
            // Creates claims. They are pieces of information that are being included in the token.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Creates credentials. It is used to sign the token.
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            // Creates a token descriptor. It contains the claims, the expiration date, the signing credentials, and the issuer and audience.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(50),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            // Creates a token handler. It is used to create the token.
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Converts the token to a string and returns it.
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {

            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

        }

        public async Task<TokenDto> HandleRefreshTokenAsync(string refreshToken)
        {
            using var transaction = await _transactionRepository.BeginTransactionAsync();

            try
            {
                var user = await _userRepo.GetUserByRefreshTokenAsync(refreshToken);

                if (user == null) throw new InvalidTokenException("Invalid refresh token");

                var newToken = GenerateRefreshToken();

                await _tokenRepository.UpdateRefreshToken(user, newToken, refreshToken);

                await transaction.CommitAsync();

                return new TokenDto
                {
                    RefreshToken = newToken.Token,
                    Token = GenerateToken(user)
                };

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }


        }
    }
}