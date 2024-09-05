using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using Finance.API.Dtos.Transaction;
using Finance.API.Enums;
using Finance.API.Dtos.Account;
using Finance.Tests.Common.Constants;
using Finance.Tests.Common.Factories;
using Finance.API.Models;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public class TransactionControllerTests : BaseIntegrationTest
    {

        public TransactionControllerTests(DockerWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task AddTransaction_ShouldReturnCreatedAtAction_WhenIncomeTransactionIsSuccessfullyCreated()
        {
            var user = UserFactory.GenerateUser(1);

            var token = GenerateToken(user);

            SetAuthorization(token);

            var createTransactionDto = TransactionFactory.GenerateTransaction(TransactionType.Income, TestConstants.AMOUNT);

            var responseTransaction = await PostRequestAsync(TestConstants.TRANSACTION_URL, createTransactionDto);

            responseTransaction.StatusCode.Should().Be(HttpStatusCode.Created);

            var resultTransaction = await responseTransaction.Content.ReadFromJsonAsync<TransactionDto>();

            if (resultTransaction == null) throw new Exception("Result transaction is null");

            var responseAccount = await GetRequestAsync(TestConstants.GET_ACCOUNT_URL);

            responseAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultAccount = await responseAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccount == null) throw new Exception("Result account is null");

            CommonTransactionAssertions(resultTransaction, createTransactionDto, resultAccount, user);
            resultAccount.Balance.Should().Be(TestConstants.AMOUNT + createTransactionDto.Amount);

        }


        [Fact]
        public async Task AddTransaction_ShouldReturnCreatedAtAction_WhenExpenseTransactionIsSuccessfullyCreated()
        {
            var user = UserFactory.GenerateUser(2);

            var token = GenerateToken(user);

            SetAuthorization(token);

            var createTransactionDto = TransactionFactory.GenerateTransaction(TransactionType.Expense, TestConstants.AMOUNT);

            var responseTransaction = await PostRequestAsync(TestConstants.TRANSACTION_URL, createTransactionDto);

            responseTransaction.StatusCode.Should().Be(HttpStatusCode.Created);

            var resultTransaction = await responseTransaction.Content.ReadFromJsonAsync<TransactionDto>();

            if (resultTransaction == null) throw new Exception("Result transaction is null");

            var responseAccount = await GetRequestAsync(TestConstants.GET_ACCOUNT_URL);

            responseAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultAccount = await responseAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccount == null) throw new Exception("Result account is null");

            CommonTransactionAssertions(resultTransaction, createTransactionDto, resultAccount, user);
            resultAccount.Balance.Should().Be(TestConstants.BALANCE - TestConstants.AMOUNT);

        }

        [Fact]
        public async Task AddTransaction_ShouldReturnBadRequest_WhenExpenseIsHigherThanBalance()
        {
            var user = UserFactory.GenerateUser(2);

            var token = GenerateToken(user);

            SetAuthorization(token);

            var createTransactionDto = TransactionFactory.GenerateTransaction(TransactionType.Expense, TestConstants.EXPENSE_AMOUNT);

            // Get initial account balance
            var responseAccount = await GetRequestAsync(TestConstants.GET_ACCOUNT_URL);

            responseAccount.Should().NotBeNull();

            var resultAccount = await responseAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccount == null) throw new Exception("Result account is null");

            var initialBalance = resultAccount.Balance;

            // Try to create transaction
            var responseTransaction = await PostRequestAsync(TestConstants.TRANSACTION_URL, createTransactionDto);

            responseTransaction.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Check if balance is the same
            var responseAccountAfterTransaction = await GetRequestAsync(TestConstants.GET_ACCOUNT_URL);

            responseAccountAfterTransaction.Should().NotBeNull();

            var resultAccountAfterTransaction = await responseAccountAfterTransaction.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccountAfterTransaction == null) throw new Exception("Result account is null");

            var finalBalance = resultAccountAfterTransaction.Balance;

            initialBalance.Should().Be(finalBalance);


        }


        #region Private methods

        private void CommonTransactionAssertions(TransactionDto resultTransaction, CreateTransactionDto createTransactionDto, AccountDto resultAccount, AppUser user)
        {
            resultTransaction.AccountId.Should().Be(user.Id);
            resultTransaction.Type.Should().Be(createTransactionDto.Type);
            resultTransaction.Amount.Should().Be(createTransactionDto.Amount);
            resultTransaction.Description.Should().Be(createTransactionDto.Description);
            resultTransaction.Date.Should().NotBe(null);

            resultAccount.UserId.Should().Be(user.Id);
            resultAccount.Transactions.Should().NotBeEmpty();
            resultAccount.Transactions[0].Id.Should().Be(resultTransaction.Id);
        }



        #endregion Private methods

    }
}
