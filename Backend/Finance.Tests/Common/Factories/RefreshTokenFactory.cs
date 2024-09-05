using Bogus;
using Finance.API.Models;
using Finance.Tests.Common.Constants;

namespace Finance.Tests.Common.Factories
{
    internal static class RefreshTokenFactory
    {

        public static RefreshToken GenerateRefreshToken(int userId)
        {
            return new Faker<RefreshToken>()
                    .RuleFor(x => x.Token, f => TestConstants.REFRESH_TOKEN)
                    .RuleFor(x => x.Expires, f => f.Date.Future(10))
                    .RuleFor(x => x.AppUserId, f => userId)
                    .Generate();
        }
    }
}
