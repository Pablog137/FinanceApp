using Finance.API.Dtos.Notification;
using Finance.API.Models;

namespace Finance.API.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<Notification?> GetByIdAsync(int id, int userId);
        Task<List<Notification>> GetAllAsync(int userId);
        Task<List<Notification>> GetAllOrderedByTimeAsync(int userId);
        Task<Notification?> CreateAsync(Notification notification, Account account);
        Task<Notification?> UpdateAsync(Notification notification, Account account);
    }
}
