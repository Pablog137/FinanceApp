using Finance.Tests.Common.Factories;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using Finance.API.Dtos.Transfer;
using Finance.API.Dtos.Account;
using Finance.Tests.Common.Constants;
using Finance.API.Enums;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public class TransferControllerTests : BaseIntegrationTest
    {
        public TransferControllerTests(DockerWebApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task CreateTransfer_ShouldReturnCreateAtAction_WhenSuccessfullyCreated()
        {
            const decimal TRANSFER_AMOUNT = 100.000M;

            var createTransferDto = TransferFactory.GenerateTransferDto(4, TRANSFER_AMOUNT);

            var user = UserFactory.GenerateUser(3);

            var token = GenerateToken(user);

            SetAuthorization(token);

            var responseTransfer = await PostRequestAsync("api/transfer", createTransferDto);

            responseTransfer.StatusCode.Should().Be(HttpStatusCode.Created);

            var resultTransfer = await responseTransfer.Content.ReadFromJsonAsync<TransferDto>();

            if (resultTransfer == null) throw new Exception("Result transfer is null");

            resultTransfer.SenderAccountId.Should().Be(3);
            resultTransfer.RecipientAccountId.Should().Be(4);
            resultTransfer.Amount.Should().Be(TRANSFER_AMOUNT);

            // Get First account

            var responseFirstAccount = await GetRequestAsync("api/account/" + 3);

            responseFirstAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultFirstAccount = await responseFirstAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultFirstAccount == null) throw new Exception("Result first account is null");

            resultFirstAccount.Balance.Should().Be(TestConstants.BALANCE - TRANSFER_AMOUNT);
            resultFirstAccount.Transactions[0].Amount.Should().Be(-TRANSFER_AMOUNT);
            resultFirstAccount.Transactions[0].Type.Should().Be(TransactionType.Expense);

            // Get Second account

            var responseSecondAccount = await GetRequestAsync("api/account/" + 4);

            responseSecondAccount.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultSecondAccount = await responseSecondAccount.Content.ReadFromJsonAsync<AccountDto>();

            if (resultSecondAccount == null) throw new Exception("Result second account is null");

            resultSecondAccount.Balance.Should().Be(TRANSFER_AMOUNT);
            resultSecondAccount.Transactions[0].Amount.Should().Be(TRANSFER_AMOUNT);
            resultSecondAccount.Transactions[0].Type.Should().Be(TransactionType.Income);


        }
    }
}
