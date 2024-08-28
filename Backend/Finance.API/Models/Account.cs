using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.API.Models
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,3)")]
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public List<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();



    }
}