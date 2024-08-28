using Finance.API.Enums;
using Finance.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Finance.API.Dtos.Notification
{

 
    public class CreateNotificationDto
    {
        [Required]
        [MaxLength(100)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(NotificationType))]

        public NotificationType Type { get; set; }

    }
}
