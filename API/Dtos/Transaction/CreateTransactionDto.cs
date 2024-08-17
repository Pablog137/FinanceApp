using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Transaction
{

    public enum TransactionType
    {
        Income,
        Expense
    }
    public class CreateTransactionDto
    {

        [Required]
        [EnumDataType(typeof(TransactionType))]
        public string Type { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}