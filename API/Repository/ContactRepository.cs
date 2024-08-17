using API.Data;
using API.Dtos.Contact;
using API.Helpers;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> CreateAsync(CreateContactDto createContactDto, int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (account == null) return null;

            var contact = new Contact
            {
                Email = createContactDto.Email,
                PhoneNumber = createContactDto.PhoneNumber,
                Username = createContactDto.Username
            };
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            account.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> DeleteAsync(int id, int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (account == null) return null;
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && c.Accounts.Any(a => a.Id == account.Id));
            if (contact == null) return null;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<List<Contact>> GetAllAsync(int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (account == null) return null;
            var contacts = await _context.Contacts.Where(c => c.Accounts.Any(a => a.Id == account.Id)).ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetByIdAsync(int id, int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (account == null) return null;
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && c.Accounts.Any(a => a.Id == account.Id));
            return contact;
        }

        public Task<Contact> GetByQueryAsync(QueryObject query, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
