using API.Dtos.Transfer;
using API.Models;

namespace API.Mappers
{
    public static class TransferMappers
    {

        public static TransferDto ToDto(this Transfer transfer)
        {
            return new TransferDto
            {
                Id = transfer.Id,
                SenderAccountId = transfer.SenderAccountId,
                RecipientAccountId = transfer.RecipientAccountId,
                Amount = transfer.Amount,
                Date = transfer.Date,
                Description = transfer.Description
            };
        }

        public static Transfer ToEntity(this CreateTransferDto dto, int senderAccountId)
        {
            return new Transfer
            {
                Amount = dto.Amount,
                RecipientAccountId = dto.RecipientAccountId,
                Description = dto.Description,
                SenderAccountId = senderAccountId
            };
        }
    }
}
