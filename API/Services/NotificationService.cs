using API.Dtos.Notification;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Mappers;
using API.Models;

namespace API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IAccountRepository _accountRepo;


        public NotificationService(INotificationRepository notificationRepository, IAccountRepository accountRepo)
        {
            _notificationRepo = notificationRepository;
            _accountRepo = accountRepo;
        }
        public async Task<List<Notification>> GetAllAsync(int userId)
        {
            return await _notificationRepo.GetAllAsync(userId);
        }

        public async Task<List<Notification>> GetAllOrderedByTimeAsync(int userId)
        {
            return await _notificationRepo.GetAllOrderedByTimeAsync(userId);
        }

        public async Task<Notification> GetByIdAsync(int id, int userId)
        {
            return await _notificationRepo.GetByIdAsync(id, userId);
        }

        public async Task<Notification> CreateAsync(CreateNotificationDto notificationDto, int userId)
        {
            var account = await _accountRepo.GetByIdAsync(userId);
            if (account == null) return null;

            return await _notificationRepo.CreateAsync(notificationDto.toEntity(account), account);
        }

        public async Task<Notification> UpdateAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByIdAsync(userId);
            if (account == null) return null;

            var notification = await _notificationRepo.GetByIdAsync(id, userId);
            if (notification == null) return null;

            return await _notificationRepo.UpdateAsync(notification, account);
        }
    }
}
