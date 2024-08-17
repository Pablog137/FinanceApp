using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Transaction;
using API.Enums;
using API.Interfaces.Repositories;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);

                if (account == null)
                {
                    await transaction.RollbackAsync();
                    return null;
                }

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

                await _context.Transactions.AddAsync(transactionEntity);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return transactionEntity;


            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                // Re throw the exception
                throw;
            }


        }

        public async Task<List<Transaction>> GetAllTransactionAsync(int userId)
        {
            var transactions = await _context.Transactions.Where(t => t.AccountId == userId).ToListAsync();

            return transactions;
        }

        public async Task<Transaction> GetByIdAsync(int id, int userId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.AccountId == userId);

            return transaction;
        }

    }
}