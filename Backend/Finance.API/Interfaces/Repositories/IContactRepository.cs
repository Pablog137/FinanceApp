using Finance.API.Dtos.Contact;
using Finance.API.Helpers;
using Finance.API.Models;

namespace Finance.API.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync(Account account, QueryObject query);

        Task<Contact?> GetByIdAsync(int id, Account account);
        Task<Contact?> DeleteAsync(Contact contact, Account account);

        Task<Contact?> AddContactAsync(Contact contact, Account account);

        Task<Contact?> ContactExistsInUsersContactRegister(Contact contact, Account account);

        Task<bool> ContactExistsAsync(Contact contact);


    }
}
