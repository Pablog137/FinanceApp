using Finance.API.Interfaces.Services;
using Finance.API.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Finance.Tests.IntegrationTests.Controllers
{
    public abstract class BaseIntegrationTest : IClassFixture<DockerWebApplicationFactory>
    {

        private readonly DockerWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public BaseIntegrationTest(DockerWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

        }


        protected string GenerateToken(AppUser user)
        {
            using var scope = _factory.Services.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            return tokenService.GenerateToken(user);
        }

        protected void SetAuthorization(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }

        protected async Task<HttpResponseMessage> GetRequestAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        protected async Task<HttpResponseMessage> PostRequestAsync(string url, Object o)
        {
            return await _client.PostAsJsonAsync(url, o);
        }

    }
}
