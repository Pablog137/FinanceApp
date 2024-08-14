using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public List<Transaction> Transactions { get; set; }

        public User User { get; set; }


    }
}