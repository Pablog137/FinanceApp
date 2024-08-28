using Finance.API.Dtos.Notification;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public static class NotificationMappers
    {

        public static NotificationDto toDto(this Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                AccountId = notification.AccountId,
                Message = notification.Message,
                Type = notification.Type,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            };
        }

        public static Notification toEntity(this CreateNotificationDto notificationDto, Account account)
        {
             return new Notification
            {
                AccountId = account.Id,
                Type = notificationDto.Type,
                Message = notificationDto.Message,
            };

        }
    }
}
