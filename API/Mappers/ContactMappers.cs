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
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Username = contact.Username
            };
        }

        public static Contact toEntity(this CreateContactDto createContactDto)
        {
            return new Contact
            {
                Email = createContactDto.Email,
                PhoneNumber = createContactDto.PhoneNumber,
                Username = createContactDto.Username
            };
        }   

    }
}
