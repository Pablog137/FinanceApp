using System.ComponentModel.DataAnnotations;

namespace Finance.API.Dtos.Transfer
{
    public class CreateTransferDto
    {

        [Required]
        public int RecipientAccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }
    }
}
