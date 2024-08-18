using API.Dtos.Transaction;
using API.Models;

namespace API.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId);
        Task<List<Transaction>> GetAllTransactionsAsync(int userId);
        Task<Transaction> GetTransactionByIdAsync(int id, int userId);

    }
}
