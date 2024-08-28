using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Transaction;
using API.Enums;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Mappers;
using API.Models;

namespace API.Services
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
                var account = await _accountRepo.GetByUserIdAsync(userId);
                if (account == null)
                    throw new InvalidOperationException("Account not found.");

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

        public async Task<List<Transaction>> GetAllTransactionsAsync(int userId)
        {
            return await _transactionRepo.GetAllTransactionsAsync(userId);
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id, int userId)
        {
            return await _transactionRepo.GetByIdAsync(id, userId);
        }
    }
}
