using Finance.API.Dtos.Notification;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Finance.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.UnitTests.Services
{
    public class NotificationServiceTests
    {

        private readonly Mock<INotificationRepository> _notificationRepoMock;
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly NotificationService _notificationService;


        public NotificationServiceTests()
        {
            _notificationRepoMock = new Mock<INotificationRepository>();
            _accountRepoMock = new Mock<IAccountRepository>();
            _notificationService = new NotificationService(_notificationRepoMock.Object, _accountRepoMock.Object);
        }


        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldReturnNotifications_WhenAccountExists()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.GetAllAsync(It.IsAny<Account>()))
                .ReturnsAsync(new List<Notification> { new Notification { Id = 1 } });

            var result = await _notificationService.GetAllAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result[0].Id);
            Assert.Single(result);

        }

        #endregion GetAllAsync

        #region GetAllOrderedByTimeAsync

        [Fact]
        public async Task GetAllOrderedByTimeAsync_ShouldReturnNotifications_WhenAccountExists()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.GetAllOrderedByTimeAsync(It.IsAny<Account>()))
                .ReturnsAsync(new List<Notification> { new Notification { Id = 1, CreatedAt = DateTime.UtcNow }, new Notification { Id = 2, CreatedAt = DateTime.UtcNow.AddSeconds(1) } });

            var result = await _notificationService.GetAllOrderedByTimeAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);

        }

        #endregion GetAllOrderedByTimeAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationExists()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
              .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Notification { Id = 1 });

            var result = await _notificationService.GetByIdAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        #endregion GetByIdAsync

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_ShouldReturnNotification_WhenSuccessfullyCreated()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<Notification>(), It.IsAny<Account>()))
                .ReturnsAsync(new Notification { Id = 1 });

            var result = await _notificationService.CreateAsync(new CreateNotificationDto(), 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        #endregion CreateAsync

        #region UpdateAsync

        [Fact]
        public async Task UpdateAsync_ShouldReturnNotification_WhenSuccessfullyUpdated()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
             .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Notification { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Notification>(), It.IsAny<Account>()))
                .ReturnsAsync(new Notification { Id = 1, IsRead = true });

            var result = await _notificationService.UpdateAsync(1, 1);

            Assert.NotNull(result);
            Assert.True(result.IsRead);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenNotificationNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
             .ReturnsAsync(new Account { Id = 1 });
            _notificationRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync((Notification)null);

            var result = await _notificationService.UpdateAsync(1, 1);

            Assert.Null(result);
        }

        #endregion UpdateAsync
    }
}
