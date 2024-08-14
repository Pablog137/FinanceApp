using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Transaction;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransaction(int userId);
        Task<Transaction> GetById(int id, int userId);
        Task<Transaction> AddTransaction(CreateTransactionDto createTransactionDto, int userId);


    }
}