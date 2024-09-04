using Bogus;
using Finance.API.Models;

namespace Finance.Tests.Common.Factories
{
    internal static class RefreshTokenFactory
    {

        public static RefreshToken GenerateRefreshToken(int userId)
        {
            return new Faker<RefreshToken>()
                    .RuleFor(x => x.Token, f => "e6b6c233-545b-4033-8f50-7bbb76553ebb")
                    .RuleFor(x => x.Expires, f => f.Date.Future(10))
                    .RuleFor(x => x.AppUserId, f => userId)
                    .Generate();
        }
    }
}
