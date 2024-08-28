using System.ComponentModel.DataAnnotations;

namespace Finance.API.Dtos.Contact
{
    public class CreateContactDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
    }
}
