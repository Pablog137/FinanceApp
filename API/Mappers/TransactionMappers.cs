using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Transaction;
using API.Models;

namespace API.Mappers
{
    public static class TransactionMappers
    {

        public static TransactionDto toDto(this Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Type = transaction.Type,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date
            };
        }

    }
}