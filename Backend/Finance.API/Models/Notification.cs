﻿using Finance.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.API.Models
{
    [Table("Notifications")]
    public class Notification
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
