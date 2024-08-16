using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Transaction
{
    public class CreateTransactionDto
    {

        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}