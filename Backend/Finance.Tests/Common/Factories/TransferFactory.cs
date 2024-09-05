using Finance.API.Dtos.Transfer;

namespace Finance.Tests.Common.Factories
{
    internal static class TransferFactory
    {

        public static CreateTransferDto GenerateTransferDto(int recipientAccountId, decimal amount)
        {
            return new CreateTransferDto
            {
                RecipientAccountId = recipientAccountId,
                Amount = amount
            };
        }
    }
}
