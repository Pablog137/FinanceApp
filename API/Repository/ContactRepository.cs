using API.Data;
using API.Dtos.Contact;
using API.Helpers;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
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

        public async Task<List<Contact>> GetAllAsync(Account account, QueryObject query)
        {
            var contacts = _context.Contacts.Where(c => c.Accounts.Any(a => a.Id == account.Id));

            if (!string.IsNullOrEmpty(query.Email))
            {
                contacts = contacts.Where(c => c.Email.ToLower() == query.Email.ToLower());
            }
            if (!string.IsNullOrEmpty(query.PhoneNumber))
            {
                contacts = contacts.Where(c => c.PhoneNumber == query.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(query.Username))
            {
                contacts = contacts.Where(c => c.Username == query.Username);
            }
            return await contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id, Account account)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && c.Accounts.Any(a => a.Id == account.Id));
        }

        public async Task<Contact> CreateAsync(Contact contact, Account account)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                await _context.Contacts.AddAsync(contact);
                account.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return contact;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Contact> DeleteAsync(Contact contact, Account account)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact?> ContactExists(Contact contact, Account account)
        {

            return await _context.Contacts.
                FirstOrDefaultAsync(c => c.Email == contact.Email && c.Accounts.
                Any(a => a.Id == account.Id) || c.PhoneNumber == contact.PhoneNumber && c.Accounts.
                Any(a => a.Id == account.Id) || c.Username == contact.Username && c.Accounts.
                Any(a => a.Id == account.Id));
        }
    }
}
