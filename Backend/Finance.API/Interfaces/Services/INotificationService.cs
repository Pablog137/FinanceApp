using Finance.API.Dtos.Notification;
using Finance.API.Models;

namespace Finance.API.Interfaces.Services
{
    public interface INotificationService
    {

        Task<Notification?> GetByIdAsync(int id, int userId);
        Task<List<Notification>> GetAllAsync(int userId);
        Task<List<Notification>> GetAllOrderedByTimeAsync(int userId);
        Task<Notification?> CreateAsync(CreateNotificationDto notification, int userId);
        Task<Notification?> UpdateAsync(int id, int userId);
    }
}
