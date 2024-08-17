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
            var accounts = await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).ToListAsync();

            return accounts;

        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            var account = await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == id);
            return account;
        }
    }
}
