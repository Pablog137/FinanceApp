using Bogus;
using Finance.API.Dtos.Contact;
using Finance.API.Exceptions;
using Finance.API.Helpers;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Finance.API.Services;
using Moq;

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


        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldThrowAccountNotFoundException_WhenAccountNotFound()
        {
            _accountRepoMock.Setup(x => x.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ThrowsAsync(new AccountNotFoundException("Account not found"));

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _contactService.GetAllAsync(1, new QueryObject()));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfContacts_WhenAccountExists()
        {
            var mockAccount = new Account { Id = 1 };

            _accountRepoMock.Setup(x => x.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
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

        #endregion GetAllAsync

        #region GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_ShouldThrowAccountNotFoundException_WhenAccountNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ThrowsAsync(new AccountNotFoundException("Account not found"));

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _contactService.GetByIdAsync(1, 1));

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenContactNotFound()
        {

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync((Contact)null);

            var result = await _contactService.GetByIdAsync(1, 1);

            Assert.Null(result);

        }

        public async Task GetByIdAsync_ShouldReturnContact_WhenContactIsFound()
        {

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Contact { Id = 1 });

            var result = await _contactService.GetByIdAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        #endregion GetByIdAsync

        #region AddContactAsync

        [Fact]
        public async Task AddContactAsync_ShouldThrowAccountNotFoundException_WhenAccountNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ThrowsAsync(new AccountNotFoundException("Account not found"));

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _contactService.AddContactAsync(new CreateContactDto(), 1));
        }
        [Fact]
        public async Task AddContactAsync_ShouldThrowException_WhenContactDoesNotExist()
        {

            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.ContactExistsAsync(It.IsAny<Contact>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<Exception>(() => _contactService.AddContactAsync(new CreateContactDto(), 1));

        }

        [Fact]
        public async Task AddContactAsync_ShouldThrowContactAlreadyExistsException_WhenContactAlreadyExistsInUser()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
               .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.ContactExistsAsync(It.IsAny<Contact>()))
                .ReturnsAsync(true);

            _contactRepoMock.Setup(repo => repo.ContactExistsInUsersContactRegister(It.IsAny<Contact>(), It.IsAny<Account>()))
                .ReturnsAsync(new Contact { });

            await Assert.ThrowsAsync<ContactAlreadyExistsException>(() => _contactService.AddContactAsync(new CreateContactDto(), 1));

        }

        [Fact]
        public async Task AddContactAsync_ShouldReturnContact_WhenSuccessfullyAdded()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
             .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.ContactExistsAsync(It.IsAny<Contact>()))
                .ReturnsAsync(true);

            _contactRepoMock.Setup(repo => repo.ContactExistsInUsersContactRegister(It.IsAny<Contact>(), It.IsAny<Account>()))
                .ReturnsAsync((Contact)null);

            _contactRepoMock.Setup(repo => repo.AddContactAsync(It.IsAny<Contact>(), It.IsAny<Account>()))
                .ReturnsAsync(new Contact { Id = 1 });

            var result = await _contactService.AddContactAsync(new CreateContactDto(), 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


        #endregion AddContactAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldThrowAccountNotFoundException_WhenAccountIsNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ThrowsAsync(new AccountNotFoundException("Account not found"));

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _contactService.DeleteAsync(1, 1));
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenContactIsNotFound()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
                .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync((Contact)null);

            var result = await _contactService.DeleteAsync(1, 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnContact_WhenSuccesfullyDeleted()
        {
            _accountRepoMock.Setup(repo => repo.GetByUserIdAsyncOrThrowException(It.IsAny<int>()))
              .ReturnsAsync(new Account { Id = 1 });

            _contactRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(new Contact {  Id = 1});

            _contactRepoMock.Setup(repo => repo.DeleteAsync(It.IsAny<Contact>(), It.IsAny<Account>()))
                .ReturnsAsync(new Contact { Id = 1 });

            var result = await _contactService.DeleteAsync(1, 1);

            Assert.Equal(1, result.Id);
        }

        #endregion DeleteAsync


    }
}
