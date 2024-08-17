using API.Dtos.Notification;
using API.Models;

namespace API.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<Notification?> GetByIdAsync(int id);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetAllOrderedByTimeAsync();
        Task<Notification> CreateAsync(CreateNotificationDto notification);
        Task<Notification> UpdateAsync(int id);
    }
}
