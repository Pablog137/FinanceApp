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

                await GenerateUsersAndAccounts(4, userManager, context);
            }
        }



        private async Task GenerateUsersAndAccounts(int numberOfUsers, UserManager<AppUser> userManager, AppDbContext context)
        {
            for (int i = 0; i < numberOfUsers; i++)
            {
                var user = UserFactory.GenerateUserById(i + 1);
                int userId = user.Id;
                user.Id = 0;

                var result = await userManager.CreateAsync(user, TestConstants.PASSWORD);

                if (!result.Succeeded)
                    throw new Exception($"Failed to create test user with id {user.Id} .");

                Account account;
                switch (userId)
                {
                    case 1:
                        account = AccountFactory.GenerateAccount(user.Id, TestConstants.AMOUNT);
                        break;
                    case 2:
                        account = AccountFactory.GenerateAccount(user.Id, TestConstants.INITIAL_BALANCE);
                        break;
                    case 3:
                        account = AccountFactory.GenerateAccount(user.Id, TestConstants.INITIAL_BALANCE);
                        break;
                    case 4:
                        account = AccountFactory.GenerateAccount(user.Id, 0);
                        break;
                    default:
                        account = AccountFactory.GenerateAccount(user.Id, TestConstants.AMOUNT);
                        break;
                }

                context.Accounts.Add(account);
                await context.SaveChangesAsync();

            }
            context.RefreshTokens.Add(RefreshTokenFactory.GenerateRefreshToken(1));
            await context.SaveChangesAsync();
        }
        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }
    }
}
