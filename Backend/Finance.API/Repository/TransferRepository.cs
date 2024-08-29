using Finance.API.Data;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly AppDbContext _context;

        public TransferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transfer?> CreateTransferAsync(Transfer transfer)
        {
            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        }

        public async Task<Transfer> GetByIdAsync(int id, Account account)
        {
            return await _context.Transfers.Where(t => t.Id == id && t.RecipientAccountId == account.Id || t.SenderAccountId == account.Id).FirstOrDefaultAsync();

        }
      
    }
}
