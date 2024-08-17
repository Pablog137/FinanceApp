using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Notification
{

    public enum NotificationType
    {
        Success,
        Error,
        Warning,
        Info
    }
    public class CreateNotificationDto
    {
        [Required]
        [MaxLength(100)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(NotificationType))]
        public string Type { get; set; }

    }
}
