using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.API.Dtos.Token;
using Finance.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Finance.API.Interfaces.Services
{
    public interface ITokenService
    {

        string GenerateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
        Task<TokenDto> HandleRefreshTokenAsync(string refreshToken);

        




    }
}