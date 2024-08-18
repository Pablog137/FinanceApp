using API.Data;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(int userId)
        {
            return await _context.Transactions
                      .Where(t => t.Account.UserId == userId)
                      .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id, int userId)
        {
            return await _context.Transactions
                                            .FirstOrDefaultAsync(t => t.Id == id && t.Account.UserId == userId);
        }


    }
}