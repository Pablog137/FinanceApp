using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Finance.API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public Account Account { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();



        public AppUser Clone()
        {
            return new AppUser
            {
                Id = this.Id,
                UserName = this.UserName,
                Email = this.Email
            };
        }
    }
}