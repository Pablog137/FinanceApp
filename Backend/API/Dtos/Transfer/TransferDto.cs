namespace API.Dtos.Transfer
{
    public class TransferDto
    {
        public int Id { get; set; }

        public int SenderAccountId { get; set; }

        public int RecipientAccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }
    }
}
