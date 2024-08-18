using API.Data;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
       private UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
       
        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
          return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
