using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Interfaces
{
    public interface ITokenService
    {

        string GenerateToken(User user);




    }
}