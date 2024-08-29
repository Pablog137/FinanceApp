using Bogus;
using Finance.API.Dtos.Users;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Migrations;
using Finance.API.Models;
using Finance.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.UnitTests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _userManagerMock = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null
            );
            _signInManagerMock = new Mock<SignInManager<AppUser>>(
                _userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
                null, null, null, null
            );
            _tokenServiceMock = new Mock<ITokenService>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _authenticationService = new AuthenticationService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _tokenServiceMock.Object,
                _accountRepositoryMock.Object,
                _userRepositoryMock.Object
            );
        }


        #region LoginAsync

        [Fact]
        public async Task LoginAsync_ThrowUnauthorizedAccessException_WhenUserDoesNotExist()
        {
            _accountRepositoryMock.Setup(repo => repo.BeginTransactionAsync())
                .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null);

            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authenticationService.LoginAsync(loginDto));

        }

        [Fact]
        public async Task LoginAsync_ThrowUnauthorizedAccessException_WhenSignInManagerCheckPasswordSignInAsyncFails()
        {
            _accountRepositoryMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            var user = new AppUser { Id = 1, Email = "whatever" };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(manager => manager.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
                .ReturnsAsync(SignInResult.Failed);

            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authenticationService.LoginAsync(loginDto));

        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUserDto_WhenLoginIsSuccessful()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var user = new AppUser { UserName = "testuser", Email = "test@example.com" };
            var refreshToken = new RefreshToken { Token = "refresh-token" };

            _accountRepositoryMock.Setup(repo => repo.BeginTransactionAsync())
               .ReturnsAsync((Mock.Of<IDbContextTransaction>()));

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(manager => manager.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
                .ReturnsAsync(SignInResult.Success);
            _tokenServiceMock.Setup(service => service.GenerateRefreshToken()).Returns(refreshToken);
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<AppUser>())).Returns("jwt-token");

            var result = await _authenticationService.LoginAsync(loginDto);

            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("jwt-token", result.Token);
            Assert.Equal("refresh-token", result.RefreshToken);
        }


        #endregion LoginAsync

    }
}
