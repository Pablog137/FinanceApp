﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.API.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Revoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
