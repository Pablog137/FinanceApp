﻿namespace API.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser  User{ get; set; }
        public string Message { get; set; } = string.Empty;
        public string Type{ get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }


    }
}
