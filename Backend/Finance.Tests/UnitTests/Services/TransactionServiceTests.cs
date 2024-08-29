using Finance.API.Dtos.Transaction;
using Finance.API.Enums;
using Finance.API.Exceptions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Finance.API.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Finance.Tests.UnitTests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _accountRepoMock = new Mock<IAccountRepository>();
            _transactionService = new TransactionService(_transactionRepoMock.Object, _accountRepoMock.Object);
        }


        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTransactions_WhenAccountExists()
        {

            var account = new Account { Id = 1 };
            var transactions = new List<Transaction> { new Transaction { AccountId = 1 } };

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(account);

            _transactionRepoMock.Setup(repo => repo.GetAllAsync(It.IsAny<Account>()))
                .ReturnsAsync(transactions);

            var result = await _transactionService.GetAllAsync(1);

            Assert.NotNull(result);
            Assert.Equal(transactions.Count, result.Count);
            Assert.Equal(transactions[0].AccountId, result[0].AccountId);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTransaction_WhenTransactionExists()
        {
            var account = new Account { Id = 1 };
            var transaction = new Transaction { Id = 1, AccountId = 1 };

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(account);

            _transactionRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(transaction);

            var result = await _transactionService.GetByIdAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.AccountId, result.AccountId);
        }


        #endregion GetByIdAsync

        #region AddTransactionAsync


        [Fact]
        public async Task AddTransactionAsync_ShouldThrowInvalidOperationException_WhenAccountNotFound()
        {

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ThrowsAsync(new AccountNotFoundException("Account not found"));

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _transactionService.AddTransactionAsync(new CreateTransactionDto(), 1));
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldThrowInvalidOperationException_WhenInsufficientBalance()
        {
            var account = new Account { Id = 1, Balance = 100 };
            var createTransactionDto = new CreateTransactionDto { Amount = 200, Type = TransactionType.Expense };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(account);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _transactionService.AddTransactionAsync(createTransactionDto, 1));
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldReturnTransaction_WhenTransactionAdded()
        {
            var account = new Account { Id = 1, Balance = 100 };
            var createTransactionDto = new CreateTransactionDto { Amount = 50, Type = TransactionType.Expense };

            _transactionRepoMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(account);

            _transactionRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Transaction>()))
                .Returns(Task.CompletedTask);

            _accountRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);

            var result = await _transactionService.AddTransactionAsync(createTransactionDto, 1);

            Assert.NotNull(result);
            Assert.Equal(createTransactionDto.Amount, result.Amount);
            Assert.Equal(createTransactionDto.Type, result.Type);
        }



        #endregion AddTransactionAsync
    }
}
