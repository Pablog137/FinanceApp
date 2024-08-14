using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Dtos.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        // public Account Account { get; set; }

    }
}