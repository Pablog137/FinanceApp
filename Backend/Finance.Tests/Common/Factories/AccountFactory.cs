using Bogus;
using Finance.API.Models;
using Finance.Tests.Common.Constants;

namespace Finance.Tests.Common.Factories
{
    internal static class AccountFactory
    {
        private static readonly Dictionary<int, Account> PredefinedAccounts = new()
        {
            { 1, new Account { UserId = 1, Balance = TestConstants.AMOUNT } },
            { 2, new Account { UserId = 2, Balance = TestConstants.INITIAL_BALANCE } },
            { 3, new Account { UserId = 3, Balance = TestConstants.INITIAL_BALANCE } },
            { 4, new Account { UserId = 4, Balance = 0 } },

        };

        public static Account GenerateAccount(int userId, decimal? balance)
        {
            if (PredefinedAccounts.ContainsKey(userId))
            {
                var predefinedAccount = PredefinedAccounts[userId].Clone();
                return predefinedAccount;
            }
            return GenerateRandomAccount(userId, balance);
        }

        public static List<Account> GetPredefinedAccounts()
        {
            return new List<Account>(PredefinedAccounts.Values);
        }

        private static Account GenerateRandomAccount(int userId, decimal? balance)
        {
            return new Faker<Account>()
                  .RuleFor(a => a.Name, f => f.Person.FullName)
                  .RuleFor(a => a.UserId, f => userId)
                  .RuleFor(a => a.Balance, f => balance ?? f.Random.Decimal())
                  .Generate();
        }
    }
}
