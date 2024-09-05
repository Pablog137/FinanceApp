using Bogus;
using Finance.API.Models;
using Finance.Tests.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public static Account GenerateRandomAccount(int userId, decimal? balance)
        {
            return new Faker<Account>()
                  .RuleFor(a => a.Name, f => f.Person.FullName)
                  .RuleFor(a => a.UserId, f => userId)
                  .RuleFor(a => a.Balance, f => balance ?? f.Random.Decimal())
                  .Generate();
        }

        public static List<Account> GetPredefinedAccounts()
        {
            return new List<Account>(PredefinedAccounts.Values);
        }

        public static Account GenerateAccount(int userId, decimal? balance)
        {
            return new Faker<Account>()
                  .RuleFor(a => a.Name, f => f.Person.FullName)
                  .RuleFor(a => a.UserId, f => userId)
                  .RuleFor(a => a.Balance, f => balance ?? f.Random.Decimal())
                  .Generate();
        }
    }
}
