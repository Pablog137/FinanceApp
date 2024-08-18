using API.Dtos.Contact;
using API.Helpers;
using API.Models;

namespace API.Interfaces.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync(int userId);

        Task<Contact?> GetByIdAsync(int id, int userId);
        Task<Contact?> GetByQueryAsync(QueryObject query, int userId);
        Task<Contact?> DeleteAsync(int id, int userId);

        Task<Contact?> CreateAsync(CreateContactDto createContactDto, int userId);
    }
}
