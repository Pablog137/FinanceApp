using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Dtos.Token;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserRepository _userRepo;
        public TokenService(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userRepo = userRepo;

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
                Expires = DateTime.UtcNow.AddMinutes(5),
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
            // Transaction
            var user = await _userRepo.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null) throw new Exception("Invalid refresh token");

            var newToken = GenerateRefreshToken();

            user.RefreshTokens.Add(newToken);

            // Revoke old token
            var oldToken = user.RefreshTokens.Single(r => r.Token == refreshToken);
            oldToken.Revoked = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);

            return new TokenDto
            {
                RefreshToken = newToken.Token,
                Token = GenerateToken(user)
            };

        }
    }
}