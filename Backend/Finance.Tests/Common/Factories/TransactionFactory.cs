using Bogus;
using Finance.API.Dtos.Transaction;
using Finance.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common.Factories
{
    internal static class TransactionFactory
    {



        public static CreateTransactionDto GenerateTransaction(TransactionType transactionType, decimal? amount)
        {
            return new Faker<CreateTransactionDto>()
               .RuleFor(x => x.Type, f => transactionType)
               .RuleFor(x => x.Amount, f => amount ?? f.Random.Decimal())
               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
               .Generate();
        }
    }
}
