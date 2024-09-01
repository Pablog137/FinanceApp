using Bogus;
using Finance.API.Data;
using Finance.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.InteropServices;
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
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            // Arrange
            //var accounts = new List<Faker<Account>>()
            //    {
            //    new Faker<Account>()
            //        .RuleFor(a => a.Id, f => 1)
            //        .RuleFor(a => a.Name, f => f.Person.FullName),
            //    new Faker<Account>()
            //        .RuleFor(a => a.Id, f => 2)
            //        .RuleFor(a => a.Name, f => f.Person.FullName),
            //    new Faker<Account>()
            //        .RuleFor(a => a.Id, f => 3)
            //        .RuleFor(a => a.Name, f => f.Person.FullName)
            //}.Select(f => f.Generate()).ToList();

            using (var scope = Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<AppDbContext>();
                await context.Database.EnsureCreatedAsync();

                //context.Accounts.AddRange(accounts);
                //await context.SaveChangesAsync();
            }
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }
    }
}
