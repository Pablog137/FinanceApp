using API.Dtos.Contact;
using API.Helpers;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
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
        public async Task<List<Contact>> GetAllAsync(int userId)
        {
            var account = await _accountRepo.GetByUserIdAsync(userId);
            if (account == null) return null;

            return await _contactRepo.GetAllAsync(account);

        }

        public async Task<Contact?> GetByIdAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsync(userId);
            if (account == null) return null;

            return await _contactRepo.GetByIdAsync(id, account);
        }

        public async Task<Contact?> GetByQueryAsync(QueryObject query, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsync(userId);
            if (account == null) return null;

            return await _contactRepo.GetByQueryAsync(query, account);
        }
        public async Task<Contact?> CreateAsync(CreateContactDto createContactDto, int userId)
        {

            var account = await _accountRepo.GetByUserIdAsync(userId);
            if (account == null) return null;

            var contact = new Contact
            {
                Email = createContactDto.Email,
                PhoneNumber = createContactDto.PhoneNumber,
                Username = createContactDto.Username
            };
            return await _contactRepo.CreateAsync(contact, account);
        }

        public async Task<Contact?> DeleteAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsync(userId);
            if (account == null) return null;

            var contact = await _contactRepo.GetByIdAsync(id, account);
            if (contact == null) return null;

            return await _contactRepo.DeleteAsync(contact, account);
        }

    }
}
