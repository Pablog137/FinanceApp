using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Contact
{
    public class CreateContactDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
    }
}
