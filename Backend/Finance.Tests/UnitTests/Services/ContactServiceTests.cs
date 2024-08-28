using Bogus;
using Finance.API.Helpers;
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
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _contactRepoMock;
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly ContactService _contactService;


        public ContactServiceTests()
        {
            _contactRepoMock = new Mock<IContactRepository>();
            _accountRepoMock = new Mock<IAccountRepository>();
            _contactService = new ContactService(_contactRepoMock.Object, _accountRepoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnNull_WhenAccountNotFound()
        {
            _accountRepoMock.Setup(x => x.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Account)null);

            var result = await _contactService.GetAllAsync(1, new QueryObject());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfContacts_WhenAccountExists()
        {
            var mockAccount = new Account { Id = 1 };

            _accountRepoMock.Setup(x => x.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(mockAccount);

            var mockContacts = new List<Contact>() { new Faker<Contact>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.UserName, f => f.Person.UserName)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .Generate() };

            _contactRepoMock.Setup(repo => repo.GetAllAsync(mockAccount, It.IsAny<QueryObject>()))
                .ReturnsAsync(mockContacts);

            var result = await _contactService.GetAllAsync(1, new QueryObject());

            Assert.Equal(mockContacts, result);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAccountNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Account)null);

            var result = await _contactService.GetByIdAsync(1, 1);

            Assert.Null(result);

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenContactNotFound()
        {

            _accountRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync((Contact)null);

            var result = await _contactService.GetByIdAsync(1, 1);

            Assert.Null(result);

        }

        public async Task GetByIdAsync_ShouldReturnContact_WhenContactIsFound()
        {

            _accountRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Faker<Contact>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.UserName, f => f.Person.UserName)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .Generate());

            var result = await _contactService.GetByIdAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


    }
}
