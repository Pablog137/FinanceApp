using Finance.API.Data;
using Finance.API.Exceptions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Mappers;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task CreateAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).ToListAsync();

        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetByUserIdAsyncOrThrowException(int userId)
        {
            var account = await _context.Accounts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            if(account == null) throw new AccountNotFoundException("Account not found");
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
