using Bogus;
using Finance.API.Data;
using Finance.API.Interfaces.Services;
using Finance.API.Models;
using Finance.API.Services;
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
                var user = new AppUser
                {
                    UserName = "username777",
                    Email = "test@gmail.com",
                };
                var result = await userManager.CreateAsync(user, "YourSecurePassword123!");

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create test user.");
                }

                var account = new Faker<Account>()
                    .RuleFor(a => a.Name, f => f.Person.FullName)
                    .RuleFor(a => a.UserId, f => user.Id)
                    .RuleFor(a => a.Balance, f => 0)
                    .Generate();

                context.Accounts.Add(account);
                await context.SaveChangesAsync();


                var refreshToken = new Faker<RefreshToken>()
                    .RuleFor(x => x.Token, f => "e6b6c233-545b-4033-8f50-7bbb76553ebb")
                    .RuleFor(x => x.Expires, f => f.Date.Future(10))
                    .RuleFor(x => x.AppUserId, f => user.Id)
                    .Generate();

                context.RefreshTokens.Add(refreshToken);
                await context.SaveChangesAsync();

            }
        }


        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }
    }
}
