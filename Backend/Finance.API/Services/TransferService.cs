using Finance.API.Dtos.Transfer;
using Finance.API.Exceptions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;

namespace Finance.API.Services
{
    public class TransferService : ITransferService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransferRepository _transferRepo;
        private readonly ITransactionRepository _transactionRepo;
        public TransferService(IAccountRepository accountRepo, ITransferRepository transferRepo,
            ITransactionRepository transactionRepo

            )
        {
            _accountRepo = accountRepo;
            _transferRepo = transferRepo;
            _transactionRepo = transactionRepo;
        }
        public async Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto)
        {
            using var transaction = await _transactionRepo.BeginTransactionAsync();

            try
            {

                var senderAccount = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);
                var recipientAccount = await _accountRepo.GetByIdAsync(transferDto.RecipientAccountId);

                if (senderAccount == recipientAccount) throw new Exception("You can't transfer to the same account");

                if (recipientAccount == null) throw new AccountNotFoundException("Recipient account not found");

                if (senderAccount == null || senderAccount.Balance < transferDto.Amount) throw new Exception("Insufficient funds");

                senderAccount.Balance -= transferDto.Amount;

                await _accountRepo.UpdateAsync(senderAccount);

                recipientAccount.Balance += transferDto.Amount;

                await _accountRepo.UpdateAsync(recipientAccount);

                var transfer = transferDto.ToEntity(senderAccount.Id);

                var result = await _transferRepo.CreateTransferAsync(transfer);

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

                return result;

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Transfer> GetByIdAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _transferRepo.GetByIdAsync(id, account);

        }
    }
}
