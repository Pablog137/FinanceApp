﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.API.Models
{
    [Table("Transfers")]
    public class Transfer
    {
        public int Id { get; set; }

        public int SenderAccountId { get; set; }

        public Account SenderAccount { get; set; }

        public int RecipientAccountId { get; set; }

        public Account RecipientAccount { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }
    }
}
