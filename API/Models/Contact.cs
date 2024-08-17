using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Contacts")]
    public class Contact
    {

        public int Id { get; set; }

        public string PhoneNumber{ get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
