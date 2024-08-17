using API.Models;

namespace API.Dtos.Notification
{
    public class CreateNotificationDto
    {
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; }

    }
}
