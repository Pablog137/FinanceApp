using API.Models;

namespace API.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<List<Account>> GetAllAsync();
    }
}
