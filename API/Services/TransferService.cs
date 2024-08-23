using API.Data;
using API.Dtos.Transfer;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TransferService : ITransferService
    {
        private readonly AppDbContext _context;
        private readonly IAccountRepository _accountRepository;
        public TransferService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);

                if(senderAccount == null || senderAccount.Balance < transferDto.Amount)
                {
                    throw new Exception("Insufficient funds");
                }

                senderAccount.Balance -= transferDto.Amount;

                var recipientAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transferDto.RecipientAccountId);

                if(recipientAccount == null)
                {
                    throw new Exception("Recipient account not found");
                }

                recipientAccount.Balance += transferDto.Amount;

                var transfer = transferDto.ToEntity(senderAccount.Id);

                _context.Transfers.Add(transfer);
              
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return transfer;

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Transfer> GetTransferByIdAsync(int id, int userId)
        {
            return await _context.Transfers.Where(t => t.Id == id && t.RecipientAccountId == userId || t.SenderAccountId == userId).FirstOrDefaultAsync();
        }
    }
}
