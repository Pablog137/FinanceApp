using Bogus;
using Finance.API.Data;
using Finance.API.Models;
using Finance.Tests.Common.Constants;
using Finance.Tests.Common.Factories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Finance.Tests
{
    public class DockerWebApplicationFactory : WebApplicationFactory<Finance.API.Program>, IAsyncLifetime
    {

        private MsSqlContainer _dbContainer;

        public DockerWebApplicationFactory()
        {
            _dbContainer = new MsSqlBuilder().Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connectionStr = _dbContainer.GetConnectionString();

            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionStr);
                });

                services.AddAuthentication("TestScheme")
                           .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using (var scope = Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();

                var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
                // User 1
                var user1 = new AppUser
                {
                    UserName = "username777",
                    Email = "test@gmail.com"
                };

                var result = await userManager.CreateAsync(user1, TestConstants.PASSWORD);

                if (!result.Succeeded) throw new Exception("Failed to create test user.");

                var account = AccountFactory.GenerateAccount(user1.Id, TestConstants.AMOUNT);

                context.Accounts.Add(account);
                await context.SaveChangesAsync();

                // User 2
                var user2 = new AppUser
                {
                    UserName = "username888",
                    Email = "test2@gmail.com"
                };

                var result2 = await userManager.CreateAsync(user2, TestConstants.PASSWORD);

                if (!result2.Succeeded) throw new Exception("Failed to create test user 2.");

                var account2 = AccountFactory.GenerateAccount(user2.Id, TestConstants.BALANCE);

                context.Accounts.Add(account2);
                await context.SaveChangesAsync();

                // User 3
                var user3 = new AppUser
                {
                    UserName = "username999",
                    Email = "test3@gmail.com",
                };
                var result3 = await userManager.CreateAsync(user3, TestConstants.PASSWORD);
                if(!result3.Succeeded) throw new Exception("Failed to create test user 3.");
                var account3 = AccountFactory.GenerateAccount(user3.Id, TestConstants.BALANCE);
                context.Accounts.Add(account3);
                await context.SaveChangesAsync();

                // User 4
                var user4 = new AppUser
                {
                    UserName = "username000",
                    Email = "test4@gmail.com",
                };
                var result4 = await userManager.CreateAsync(user4, TestConstants.PASSWORD);
                if(!result4.Succeeded) throw new Exception("Failed to create test user 4.");
                var account4 = AccountFactory.GenerateAccount(user4.Id, 0);
                context.Accounts.Add(account4);
                await context.SaveChangesAsync();


                context.RefreshTokens.Add(RefreshTokenFactory.GenerateRefreshToken(user1.Id));
                await context.SaveChangesAsync();

            }
        }


        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }
    }
}
