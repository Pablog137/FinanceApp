using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Transfer
{
    public class CreateTransferDto
    {

        [Required]
        public int RecipientAccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }
    }
}
