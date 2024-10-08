﻿using Finance.API.Enums;

namespace Finance.API.Dtos.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
