using Finance.API.Dtos.Transfer;
using Finance.API.Exceptions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Finance.API.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Finance.Tests.UnitTests.Services
{
    public class TransferServiceTests
    {

        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly Mock<ITransferRepository> _transferRepoMock;
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly TransferService _transferService;


        public TransferServiceTests()
        {
            _accountRepoMock = new Mock<IAccountRepository>();
            _transferRepoMock = new Mock<ITransferRepository>();
            _transactionRepoMock = new Mock<ITransactionRepository>();

            _transferService = new TransferService(_accountRepoMock.Object, _transferRepoMock.Object, _transactionRepoMock.Object);
        }


        #region GetByIdAsync

        [Fact]

        public async Task GetByIdAsync_ShouldReturnTransfer_WhenAccountExists()
        {

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account());
            _transferRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Transfer { Id = 1 });

            var result = await _transferService.GetByIdAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


        #endregion GetByIdAsync

        #region CreateTransferAsync

        [Fact]
        public async Task CreateTransferAsync_ShouldSucceed_WhenTransferIsValid()
        {
            var userId = 1;
            var transferDto = new CreateTransferDto
            {
                RecipientAccountId = 2,
                Amount = 100
            };

            var senderAccount = new Account { Id = 1, Balance = 200 };
            var recipientAccount = new Account { Id = 2, Balance = 300 };
            var transfer = new Transfer { Id = 1, SenderAccountId = 1, RecipientAccountId = 2, Amount = 100 };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(userId))
                .ReturnsAsync(senderAccount);
            _accountRepoMock.Setup(repo => repo.GetByIdAsync(transferDto.RecipientAccountId))
                .ReturnsAsync(recipientAccount);
            _accountRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);
            _transferRepoMock.Setup(repo => repo.CreateTransferAsync(It.IsAny<Transfer>()))
                .ReturnsAsync(transfer);
            _transactionRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Transaction>()))
                .Returns(Task.CompletedTask);

            var expectedSenderBalance = senderAccount.Balance - transferDto.Amount;
            var expectedRecipientBalance = recipientAccount.Balance + transferDto.Amount;


            var result = await _transferService.CreateTransferAsync(userId, transferDto);

            Assert.NotNull(result);
            Assert.Equal(transfer.Id, result.Id);
            Assert.Equal(expectedSenderBalance, senderAccount.Balance);
            Assert.Equal(expectedRecipientBalance, recipientAccount.Balance);

            _accountRepoMock.Verify(repo => repo.UpdateAsync(senderAccount), Times.Once);
            _accountRepoMock.Verify(repo => repo.UpdateAsync(recipientAccount), Times.Once);
            _transferRepoMock.Verify(repo => repo.CreateTransferAsync(It.IsAny<Transfer>()), Times.Once);
            _transactionRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Transaction>()), Times.Exactly(2));
        }


        [Fact]
        public async Task CreateTransferAsync_ShouldThrowException_WhenTransferToSameAccount()
        {
            var userId = 1;
            var transferDto = new CreateTransferDto
            {
                RecipientAccountId = 1,
                Amount = 100
            };

            var senderAccount = new Account { Id = 1, Balance = 200 };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(userId))
                .ReturnsAsync(senderAccount);
            _accountRepoMock.Setup(repo => repo.GetByIdAsync(transferDto.RecipientAccountId))
                .ReturnsAsync(senderAccount);

            await Assert.ThrowsAsync<Exception>(() => _transferService.CreateTransferAsync(userId, transferDto));
        }

        [Fact]
        public async Task CreateTransferAsync_ShouldThrowException_WhenInsufficientFunds()
        {
            var userId = 1;
            var transferDto = new CreateTransferDto
            {
                RecipientAccountId = 2,
                Amount = 300
            };

            var senderAccount = new Account { Id = 1, Balance = 200 };
            var recipientAccount = new Account { Id = 2, Balance = 300 };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(userId))
                .ReturnsAsync(senderAccount);
            _accountRepoMock.Setup(repo => repo.GetByIdAsync(transferDto.RecipientAccountId))
                .ReturnsAsync(recipientAccount);

            await Assert.ThrowsAsync<Exception>(() => _transferService.CreateTransferAsync(userId, transferDto));
        }

        [Fact]
        public async Task CreateTransferAsync_ShouldThrowException_WhenRecipientAccountNotFound()
        {
            var userId = 1;
            var transferDto = new CreateTransferDto
            {
                RecipientAccountId = 2,
                Amount = 100
            };

            var senderAccount = new Account { Id = 1, Balance = 200 };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(userId))
                .ReturnsAsync(senderAccount);
            _accountRepoMock.Setup(repo => repo.GetByIdAsync(transferDto.RecipientAccountId))
                .ReturnsAsync((Account)null);

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _transferService.CreateTransferAsync(userId, transferDto));
        }



        #endregion CreateTransferAsync


    }
}
