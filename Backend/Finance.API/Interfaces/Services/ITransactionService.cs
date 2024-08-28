using Finance.API.Dtos.Transaction;
using Finance.API.Models;

namespace Finance.API.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId);
        Task<List<Transaction>> GetAllTransactionsAsync(int userId);
        Task<Transaction> GetTransactionByIdAsync(int id, int userId);

    }
}
