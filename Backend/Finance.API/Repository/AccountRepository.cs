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

        /// <summary>
        /// Retrieves an account by user ID or throws an exception if not found.
        /// </summary>
        /// <param name="userId">The ID of the user whose account is to be retrieved.</param>
        /// <returns>The account associated with the given user ID.</returns>
        /// <exception cref="AccountNotFoundException">Thrown when no account is found for the given user ID.</exception>
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

   
    }
}
