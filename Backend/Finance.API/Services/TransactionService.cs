using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.API.Dtos.Transaction;
using Finance.API.Enums;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;

namespace Finance.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IAccountRepository _accountRepo;

        public TransactionService(ITransactionRepository transactionRepo, IAccountRepository accountRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
        }

        public async Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId)
        {
            using var transaction = await _transactionRepo.BeginTransactionAsync();

            try
            {
                var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

                var transactionEntity = createTransactionDto.FromDtoToEntity(account.Id);

                switch (transactionEntity.Type)
                {
                    case TransactionType.Income:
                        account.Balance += transactionEntity.Amount;
                        break;
                    case TransactionType.Expense:
                        if (transactionEntity.Amount > account.Balance)
                            throw new InvalidOperationException("Insufficient balance.");
                        account.Balance -= transactionEntity.Amount;
                        break;
                }


                await _transactionRepo.AddAsync(transactionEntity);

                await _accountRepo.UpdateAsync(account);

                await transaction.CommitAsync();

                return transactionEntity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Transaction>> GetAllAsync(int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);
            return await _transactionRepo.GetAllAsync(account);
        }

        public async Task<Transaction> GetByIdAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);
            return await _transactionRepo.GetByIdAsync(id, account);
        }
    }
}
