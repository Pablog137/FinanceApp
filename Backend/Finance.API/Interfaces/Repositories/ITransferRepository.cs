using Finance.API.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task CreateTransferAsync(Transfer transfer);
        Task<Transfer> GetByIdAsync(int id, int userId);

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
