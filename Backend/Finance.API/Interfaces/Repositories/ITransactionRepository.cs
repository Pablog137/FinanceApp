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
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetAllAsync(Account account);
        Task<Transaction> GetByIdAsync(int id, Account account);

        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}