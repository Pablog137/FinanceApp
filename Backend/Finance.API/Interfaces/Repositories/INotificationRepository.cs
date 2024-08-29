using Finance.API.Dtos.Notification;
using Finance.API.Models;

namespace Finance.API.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<Notification?> GetByIdAsync(int id, Account account);
        Task<List<Notification>> GetAllAsync(Account account);
        Task<List<Notification>> GetAllOrderedByTimeAsync(Account account);
        Task<Notification?> CreateAsync(Notification notification, Account account);
        Task<Notification?> UpdateAsync(Notification notification, Account account);
    }
}
