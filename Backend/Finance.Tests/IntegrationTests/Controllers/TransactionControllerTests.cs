using System.Net;
using FluentAssertions;
using System.Net.Http.Headers;
using Finance.API.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Finance.API.Models;
using Finance.API.Dtos.Users;
using System.Net.Http.Json;
using Finance.API.Dtos.Transaction;
using Bogus;
using Finance.API.Enums;
using Finance.API.Dtos.Account;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public class TransactionControllerTests : IClassFixture<DockerWebApplicationFactory>
    {

        private readonly DockerWebApplicationFactory _factory;
        private readonly HttpClient _client;
        public TransactionControllerTests(DockerWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

        }

        [Fact]
        public async Task AddTransaction_ShouldReturnCreatedAtAction_WhenSuccessfullyCreated()
        {
            using var scope = _factory.Services.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

            var user = new AppUser
            {
                Id = 1,
                UserName = "username777",
                Email = "test@gmail.com"
            };

            const decimal AMOUNT = 100.000M;

            var token = tokenService.GenerateToken(user);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var createTransactionDto = new Faker<CreateTransactionDto>()
               .RuleFor(x => x.Type, f => TransactionType.Income)
               .RuleFor(x => x.Amount, f => AMOUNT)
               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
               .Generate();

            var responseTransaction = await _client.PostAsJsonAsync("api/transaction/add-transaction", createTransactionDto);

            responseTransaction.StatusCode.Should().Be(HttpStatusCode.Created);

            var resultTransaction = await responseTransaction.Content.ReadFromJsonAsync<TransactionDto>();

            if (resultTransaction == null) throw new Exception("Result transaction is null");

            var responseAccount = await _client.GetAsync("api/account/get-by-userId");

            responseAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultAccount = await responseAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccount == null) throw new Exception("Result account is null");

            resultTransaction.AccountId.Should().Be(user.Id);
            resultTransaction.Type.Should().Be(createTransactionDto.Type);
            resultTransaction.Amount.Should().Be(createTransactionDto.Amount);
            resultTransaction.Description.Should().Be(createTransactionDto.Description);
            resultTransaction.Date.Should().NotBe(null);

            resultAccount.UserId.Should().Be(user.Id);
            resultAccount.Balance.Should().Be(AMOUNT);
            resultAccount.Transactions.Should().NotBeEmpty();
            resultAccount.Transactions[0].Id.Should().Be(resultTransaction.Id);

        }


    }
}
