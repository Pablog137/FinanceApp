using Finance.API.Dtos.Users;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public static class UserMappers
    {

        public static UserDto ToDto(this AppUser user, string token, string refreshToken)
        {
            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}
