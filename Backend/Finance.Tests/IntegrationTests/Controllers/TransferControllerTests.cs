using Finance.Tests.Common.Factories;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using Finance.API.Dtos.Transfer;
using Finance.API.Dtos.Account;
using Finance.API.Enums;
using Finance.Tests.Common.Constants;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public class TransferControllerTests : BaseIntegrationTest
    {

        private const int SENDER_ID = 3;
        private const int RECIPIENT_ID = 4;


        public TransferControllerTests(DockerWebApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task CreateTransfer_ShouldReturnCreateAtAction_WhenSuccessfullyCreated()
        {
            var createTransferDto = TransferFactory.GenerateTransferDto(RECIPIENT_ID, TestConstants.AMOUNT);

            var user = UserFactory.GenerateUser(SENDER_ID);

            var token = GenerateToken(user);

            SetAuthorization(token);

            var responseTransfer = await PostRequestAsync(TestConstants.TRANSFER_URL, createTransferDto);

            responseTransfer.StatusCode.Should().Be(HttpStatusCode.Created);

            var resultTransfer = await responseTransfer.Content.ReadFromJsonAsync<TransferDto>();

            if (resultTransfer == null) throw new Exception("Result transfer is null");

            resultTransfer.SenderAccountId.Should().Be(SENDER_ID);
            resultTransfer.RecipientAccountId.Should().Be(RECIPIENT_ID);
            resultTransfer.Amount.Should().Be(TestConstants.AMOUNT);

            var resultFirstAccountValidated = await GetAccountAndValidateAsync(SENDER_ID, TestConstants.INITIAL_BALANCE - TestConstants.AMOUNT, -TestConstants.AMOUNT, TransactionType.Expense);

            var resultSecondAccountValidated = await GetAccountAndValidateAsync(RECIPIENT_ID, TestConstants.AMOUNT, TestConstants.AMOUNT, TransactionType.Income);


        }


        private async Task<AccountDto?> GetAccountAndValidateAsync(int accountId, decimal expectedBalance, decimal transferAmount, TransactionType type)
        {
            var responseAccount = await GetRequestAsync(TestConstants.ACCOUNT_URL + accountId);

            responseAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultAccount = await responseAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultAccount == null) throw new Exception($"Result {accountId} account is null");

            resultAccount.Balance.Should().Be(expectedBalance);
            resultAccount.Transactions[0].Amount.Should().Be(transferAmount);
            resultAccount.Transactions[0].Type.Should().Be(type);

            return resultAccount;
        }

    }
}
