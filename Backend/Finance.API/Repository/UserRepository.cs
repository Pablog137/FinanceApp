using Finance.API.Data;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Repository
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
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens) // Include the RefreshTokens collection in the query
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken)); // Retrieve the user or null if not found

            if (user == null || !user.RefreshTokens.Any(r => r.Token == refreshToken && r.IsActive))
            {
                return null;
            }

            return user;
        }

        public async Task UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to update user");

        }
    }
}
