using API.Models;

namespace API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByEmailAsync(string email);
    }
}
