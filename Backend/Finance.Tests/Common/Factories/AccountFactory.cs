using Bogus;
using Finance.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common.Factories
{
    internal static class AccountFactory
    {

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
