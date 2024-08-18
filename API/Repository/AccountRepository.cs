using API.Data;
using API.Interfaces.Repositories;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).ToListAsync();

        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetByUserIdAsync(int userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
