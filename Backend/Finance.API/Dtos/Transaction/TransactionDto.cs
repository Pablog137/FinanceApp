using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.API.Enums;
using Finance.API.Models;

namespace Finance.API.Dtos.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        // public Account Account { get; set; }

    }
}