using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dtos.Token;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Interfaces.Services
{
    public interface ITokenService
    {

        string GenerateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
        Task<TokenDto> HandleRefreshTokenAsync(string refreshToken);

        




    }
}