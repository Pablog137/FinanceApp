using API.Dtos.Contact;
using API.Helpers;
using API.Models;

namespace API.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync(Account account, QueryObject query);

        Task<Contact?> GetByIdAsync(int id, Account account);
        Task<Contact?> DeleteAsync(Contact contact, Account account);

        Task<Contact?> CreateAsync(Contact contact, Account account);


    }
}
