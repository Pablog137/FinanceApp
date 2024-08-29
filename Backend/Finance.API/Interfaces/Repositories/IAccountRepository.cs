using Finance.API.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByUserIdAsyncOrThrowException(int userId);
        Task UpdateAsync(Account account);
        Task CreateAsync(Account account);
    }
}
