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
        public async Task<AppUser> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _userManager.Users
                .Include(u => u.RefreshTokens) // Include the RefreshTokens collection in the query
                .Where(u => u.RefreshTokens.Any(r => r.Token == refreshToken && r.IsActive))
                .SingleOrDefaultAsync(); // Retrieve the user or null if not found
        }

        public async Task UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to update user");
           
        }
    }
}
