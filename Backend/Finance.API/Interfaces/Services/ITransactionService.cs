using Finance.API.Dtos.Transaction;
using Finance.API.Models;

namespace Finance.API.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId);
        Task<List<Transaction>> GetAllAsync(int userId);
        Task<Transaction> GetByIdAsync(int id, int userId);

    }
}
