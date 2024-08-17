using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Transaction;
using API.Enums;
using API.Interfaces.Repositories;
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
            
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);

            if (account == null) return null;

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Type = createTransactionDto.Type,
                Amount = createTransactionDto.Amount,
                Description = createTransactionDto.Description,
                Date = DateTime.Now
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;

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