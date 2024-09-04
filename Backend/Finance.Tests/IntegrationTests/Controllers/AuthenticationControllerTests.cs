using Bogus;
using Finance.API.Dtos.Token;
using Finance.API.Dtos.Users;
using Finance.Tests.Common.Constants;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public class AuthenticationControllerTests : BaseIntegrationTest
    {
        public AuthenticationControllerTests(DockerWebApplicationFactory factory) : base(factory)
        {
        }



        #region Register

        [Fact]
        public async Task Register_ShouldReturnOK_WhenSuccessfullyRegistered()
        {
            var registerDto = new Faker<RegisterDto>()
                .RuleFor(x => x.Username, f => f.Person.UserName)
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Password, f => f.Internet.Password(8))
                .RuleFor(x => x.ConfirmPassword, (f, u) => u.Password)
                .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/register", registerDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            if (result == null) throw new Exception("Response is null");

            result.Username.Should().Be(registerDto.Username);
            result.Email.Should().Be(registerDto.Email);
            result.Token.Should().NotBeNullOrEmpty();
            result.RefreshToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Register_ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            var registerDto = new Faker<RegisterDto>()
                .RuleFor(x => x.Username, f => f.Person.UserName)
                .RuleFor(x => x.Email, f => "test@gmail.com")
                .RuleFor(x => x.Password, f => f.Internet.Password(8))
                .RuleFor(x => x.ConfirmPassword, (f, u) => u.Password)
                .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/register", registerDto);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);


        }

        [Fact]
        public async Task Register_SHouldReturnConflict_WhenUserNameAlreadyExists()
        {
            var registerDto = new Faker<RegisterDto>()
               .RuleFor(x => x.Username, f => "username777")
               .RuleFor(x => x.Email, f => f.Internet.Email())
               .RuleFor(x => x.Password, f => f.Internet.Password(8))
               .RuleFor(x => x.ConfirmPassword, (f, u) => u.Password)
               .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/register", registerDto);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidModel()
        {
            var registerDto = new Faker<RegisterDto>()
                //.RuleFor(x => x.Username, f => f.Person.UserName)
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Password, f => f.Internet.Password(8))
                .RuleFor(x => x.ConfirmPassword, (f, u) => u.Password)
                .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/register", registerDto);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        #endregion Register

        #region Login

        [Fact]
        public async Task Login_ShouldReturnOk_WhenSuccessfullyLoggedIn()
        {

            var loginDto = new Faker<LoginDto>()
                .RuleFor(x => x.Email, f => "test@gmail.com")
                .RuleFor(x => x.Password, f => TestConstants.PASSWORD)
                 .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/login", loginDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            if (result == null) throw new Exception("Response is null");

            result.Email.Should().Be(loginDto.Email);
            result.Token.Should().NotBeNullOrEmpty();
            result.RefreshToken.Should().NotBeNullOrEmpty();

        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorizedAccessException_WhenUserDoesNotExist()
        {
            var loginDto = new Faker<LoginDto>()
                           .RuleFor(x => x.Email, f => f.Person.Email)
                           .RuleFor(x => x.Password, f => f.Internet.Password(10))
                            .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/login", loginDto);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);


        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorizedAccessException_WhenPasswordIsWrong()
        {
            var loginDto = new Faker<LoginDto>()
                        .RuleFor(x => x.Email, f => "test@gmail.com")
                        .RuleFor(x => x.Password, f => f.Internet.Password(10))
                         .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/login", loginDto);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }


        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenLoginDtoIsNotValid()
        {
            var loginDto = new Faker<LoginDto>()
                 .RuleFor(x => x.Email, f => "test@gmail.com")
                  //.RuleFor(x => x.Password, f => f.Internet.Password(10))
                  .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/login", loginDto);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }



        #endregion Login

        #region Refresh token


        [Fact]
        public async Task RefreshToken_ShouldReturnOk_WhenSuccessfullyCreated()
        {
            var refreshTokenDto = new Faker<RefreshTokenDto>()
                .RuleFor(x => x.RefreshToken, f => "e6b6c233-545b-4033-8f50-7bbb76553ebb")
                .Generate();

            var content = new StringContent(JsonConvert.SerializeObject(refreshTokenDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/refresh-token", refreshTokenDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<TokenDto>();

            if (result == null) throw new Exception("Response is null");

            result.Token.Should().NotBeNullOrEmpty();
            result.RefreshToken.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public async Task RefreshToken_ShouldReturnUnauthorized_WhenTokenDoesNotExist()
        {
            var refreshTokenDto = new Faker<RefreshTokenDto>()
                .RuleFor(x => x.RefreshToken, f => f.Random.String(10))
                .Generate();
            var content = new StringContent(JsonConvert.SerializeObject(refreshTokenDto), Encoding.UTF8, "application/json");

            var response = await PostRequestAsync("api/authentication/refresh-token", refreshTokenDto);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        #endregion Refresh token

    }
}
