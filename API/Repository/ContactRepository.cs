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

        public async Task<List<Contact>> GetAllAsync(Account account)
        {
            return await _context.Contacts.Where(c => c.Accounts.Any(a => a.Id == account.Id)).ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id, Account account)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && c.Accounts.Any(a => a.Id == account.Id));
        }

        // TODO
        public async Task<Contact> GetByQueryAsync(QueryObject query, Account account)
        {
            throw new NotImplementedException();
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

    }
}
