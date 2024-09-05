using Bogus;
using Finance.API.Models;

namespace Finance.Tests.Common.Factories
{
    internal static class UserFactory
    {


        public static AppUser GenerateUser(int? id)
        {
            if (id.HasValue)
            {
                return GenerateUserById(id.Value);
            }
            return GenerateRandomUser();
        }

        private static AppUser GenerateUserById(int id) =>
     id switch
     {
         1 => new AppUser
         {
             Id = 1,
             UserName = "username777",
             Email = "test@gmail.com"
         },
         2 => new AppUser
         {
             Id = 2,
             UserName = "username888",
             Email = "test2@gmail.com"
         },
         3 => new AppUser
         {
             Id = 3,
             UserName = "username999",
             Email = "test3@gmail.com",
         },
         4 => new AppUser
         {
             Id = 4,
             UserName = "username000",
             Email = "test4@gmail.com",
         },

         _ => GenerateRandomUser()
     };


        private static AppUser GenerateRandomUser()
        {
            return new Faker<AppUser>()
                .RuleFor(x => x.Id, f => f.Random.Number(1, 10000))
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.Email, f => f.Person.Email)
                .Generate();
        }
    }
}