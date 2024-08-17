using API.Dtos.Notification;
using API.Models;

namespace API.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<Notification?> GetById(int id);
        Task<List<Notification>> GetAll();
        Task<List<Notification>> GetAllOrderedByTime();
        Task<Notification> Create(CreateNotificationDto notification);
        Task<Notification> Update(int id);
    }
}
