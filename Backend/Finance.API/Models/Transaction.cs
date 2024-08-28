using Finance.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.API.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Account Account { get; set; }

        // Will have transactions between contacts

        
    }
}