using Finance.API.Dtos.Contact;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public static class ContactMappers
    {

        public static ContactDto toDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Email = contact.Email,
                UserName = contact.UserName
            };
        }

        public static Contact toEntity(this CreateContactDto createContactDto)
        {
            return new Contact
            {
                Email = createContactDto.Email,
                UserName = createContactDto.UserName
            };
        }

    }
}
