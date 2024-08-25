using API.Dtos.Contact;
using API.Models;

namespace API.Mappers
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
