using API.Dtos.Users;
using API.Models;

namespace API.Mappers
{
    public static class UserMappers
    {

        public static UserDto ToDto(this AppUser user, string token)
        {
            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
