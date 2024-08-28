using Finance.API.Dtos.Account;
using Finance.API.Dtos.Transaction;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public static class AccountMapper
    {
        public static AccountDto toDto(this Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                UserId = account.UserId,
                Name = account.Name,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt,
                Transactions = account.Transactions.Select(t => t.toDto()).ToList()
            };
        }
    }
}
