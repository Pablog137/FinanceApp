using API.Data;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly AppDbContext _context;

        public TransferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransferAsync(Transfer transfer)
        {
            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task<Transfer> GetByIdAsync(int id, int userId)
        {
            return await _context.Transfers.Where(t => t.Id == id && t.RecipientAccountId == userId || t.SenderAccountId == userId).FirstOrDefaultAsync();

        }
    }
}
