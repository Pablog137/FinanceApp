using Finance.API.Dtos.Notification;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;

namespace Finance.API.Services
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
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _notificationRepo.GetAllAsync(account);
        }

        public async Task<List<Notification>> GetAllOrderedByTimeAsync(int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _notificationRepo.GetAllOrderedByTimeAsync(account);
        }

        public async Task<Notification> GetByIdAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _notificationRepo.GetByIdAsync(id, account);
        }

        public async Task<Notification> CreateAsync(CreateNotificationDto notificationDto, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            return await _notificationRepo.CreateAsync(notificationDto.toEntity(account), account);
        }

        public async Task<Notification> UpdateAsync(int id, int userId)
        {
            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(userId);

            var notification = await _notificationRepo.GetByIdAsync(id, account);
            if (notification == null) return null;

            return await _notificationRepo.UpdateAsync(notification, account);
        }
    }
}
