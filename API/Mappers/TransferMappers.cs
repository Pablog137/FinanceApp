using API.Dtos.Transfer;
using API.Models;

namespace API.Mappers
{
    public static class TransferMappers
    {

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
