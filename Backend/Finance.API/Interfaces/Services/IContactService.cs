using Finance.API.Dtos.Contact;
using Finance.API.Helpers;
using Finance.API.Models;

namespace Finance.API.Interfaces.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync(int userId, QueryObject query);

        Task<Contact?> GetByIdAsync(int id, int userId);
        Task<Contact?> DeleteAsync(int id, int userId);

        Task<Contact?> AddContactAsync(CreateContactDto createContactDto, int userId);
    }
}
