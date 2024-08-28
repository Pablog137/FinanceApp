using API.Dtos.Account;
using API.Dtos.Transaction;
using API.Models;

namespace API.Mappers
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
