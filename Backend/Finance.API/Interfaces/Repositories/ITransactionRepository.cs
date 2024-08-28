using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.API.Dtos.Transaction;
using Finance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.API.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        //Task<List<Transaction>> GetAllTransactionAsync(int userId);
        //Task<Transaction> GetByIdAsync(int id, int userId);
        //Task<Transaction> AddTransactionAsync(CreateTransactionDto createTransactionDto, int userId);

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetAllTransactionsAsync(int userId);
        Task<Transaction> GetByIdAsync(int id, int userId);

    }
}