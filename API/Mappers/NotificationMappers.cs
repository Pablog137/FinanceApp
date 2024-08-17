using API.Dtos.Notification;
using API.Models;

namespace API.Mappers
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
    }
}
