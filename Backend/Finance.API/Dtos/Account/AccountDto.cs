﻿using Finance.API.Dtos.Transaction;
using Finance.API.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace Finance.API.Dtos.Account
{
    public class AccountDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();


    }
}
