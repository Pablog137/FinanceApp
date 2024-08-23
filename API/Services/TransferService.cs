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
        private readonly IAccountRepository _accountRepo;
        private readonly ITransferRepository _transferRepo;
        public TransferService(AppDbContext context, IAccountRepository accountRepo, ITransferRepository transferRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
            _transferRepo = transferRepo;
        }
        public async Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);

                if (senderAccount == null || senderAccount.Balance < transferDto.Amount) throw new Exception("Insufficient funds");

                senderAccount.Balance -= transferDto.Amount;

                var recipientAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transferDto.RecipientAccountId);

                if (recipientAccount == null) throw new Exception("Recipient account not found");

                recipientAccount.Balance += transferDto.Amount;

                var transfer = transferDto.ToEntity(senderAccount.Id);

                await _transferRepo.CreateTransferAsync(transfer);

                await transaction.CommitAsync();

                return transfer;

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Transfer> GetByIdAsync(int id, int userId)
        {
            return await _transferRepo.GetByIdAsync(id, userId);

        }
    }
}
