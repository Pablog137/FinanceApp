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
        private readonly IAccountRepository _accountRepo;
        private readonly ITransferRepository _transferRepo;
        private readonly ITransactionRepository _transactionRepo;
        public TransferService(AppDbContext context, IAccountRepository accountRepo, ITransferRepository transferRepo,
            ITransactionRepository transactionRepo

            )
        {
            _accountRepo = accountRepo;
            _transferRepo = transferRepo;
            _transactionRepo = transactionRepo;
        }
        public async Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto)
        {
            using var transaction = await _transferRepo.BeginTransactionAsync();

            try
            {

                var senderAccount = await _accountRepo.GetByUserIdAsync(userId);

                if (senderAccount == null || senderAccount.Balance < transferDto.Amount) throw new Exception("Insufficient funds");

                senderAccount.Balance -= transferDto.Amount;

                await _accountRepo.UpdateAsync(senderAccount);

                var recipientAccount = await _accountRepo.GetByIdAsync(transferDto.RecipientAccountId);
                if (recipientAccount == null) throw new Exception("Recipient account not found");

                recipientAccount.Balance += transferDto.Amount;

                await _accountRepo.UpdateAsync(recipientAccount);

                var transfer = transferDto.ToEntity(senderAccount.Id);

                await _transferRepo.CreateTransferAsync(transfer);

                var senderTransaction = new Transaction
                {
                    AccountId = senderAccount.Id,
                    Amount = -transferDto.Amount,
                    Description = $"Transfer to the account with id : {recipientAccount.Id}",
                    Type = Enums.TransactionType.Expense
                };

                await _transactionRepo.AddAsync(senderTransaction);

                var recipientTransaction = new Transaction
                {
                    AccountId = recipientAccount.Id,
                    Amount = transferDto.Amount,
                    Description = $"Transfer from the account with id : {senderAccount.Id}",
                    Type = Enums.TransactionType.Income
                };

                await _transactionRepo.AddAsync(recipientTransaction);

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
