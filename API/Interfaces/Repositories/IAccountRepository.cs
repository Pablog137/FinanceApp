using API.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByUserIdAsync(int userId);
        Task UpdateAsync(Account account);
        Task CreateAsync(Account account);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
