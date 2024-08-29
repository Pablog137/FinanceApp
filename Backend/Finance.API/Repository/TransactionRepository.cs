using Finance.API.Data;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Repository
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

        public async Task<List<Transaction>> GetAllAsync(Account account)
        {
            return await _context.Transactions.Where(t => t.Account == account)
                          .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id, Account account)
        {
            return await _context.Transactions
                                            .FirstOrDefaultAsync(t => t.Id == id && t.Account == account);
        }


    }
}