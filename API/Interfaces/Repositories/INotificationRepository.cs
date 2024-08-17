using API.Dtos.Notification;
using API.Models;

namespace API.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<Notification?> GetByIdAsync(int id, int userId);
        Task<List<Notification>> GetAllAsync(int userId);
        Task<List<Notification>> GetAllOrderedByTimeAsync(int userId);
        Task<Notification?> CreateAsync(CreateNotificationDto notification, int userId);
        Task<Notification?> UpdateAsync(int id, int userId);
    }
}
