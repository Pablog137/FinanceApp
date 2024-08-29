using Finance.API.Dtos.Contact;
using Finance.API.Exceptions;
using Finance.API.Helpers;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Services
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _contactRepo;
        private readonly IAccountRepository _accountRepo;

        public ContactService(IContactRepository contactRepo, IAccountRepository accountRepo)
        {
            _contactRepo = contactRepo;
            _accountRepo = accountRepo;
        }
        public async Task<List<Contact>> GetAllAsync(int userId, QueryObject query)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _contactRepo.GetAllAsync(account, query);

        }

        public async Task<Contact?> GetByIdAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _contactRepo.GetByIdAsync(id, account);
        }

        public async Task<Contact?> AddContactAsync(CreateContactDto createContactDto, int userId)
        {

            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            var contact = createContactDto.toEntity();

            var contactAlreadyExists = await _contactRepo.ContactExistsAsync(contact);

            if (!contactAlreadyExists) throw new Exception("The contact does not exist");

            var contactAlreadyIsInUserRegister = await _contactRepo.ContactExistsInUsersContactRegister(contact, account);

            if (contactAlreadyIsInUserRegister != null) throw new ContactAlreadyExistsException("User already has this contact.");

            return await _contactRepo.AddContactAsync(contact, account);
        }

        public async Task<Contact?> DeleteAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            var contact = await _contactRepo.GetByIdAsync(id, account);
            if (contact == null) return null;

            return await _contactRepo.DeleteAsync(contact, account);
        }

    }
}
