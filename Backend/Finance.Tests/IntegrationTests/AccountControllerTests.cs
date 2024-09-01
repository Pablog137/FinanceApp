using Finance.API.Dtos.Account;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<DockerWebApplicationFactory>
    {

        private readonly DockerWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public AccountControllerTests(DockerWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

        }


        [Fact]
        public async Task GetAll_ShouldReturnAllAccounts_WhenExecuteController()
        {
            // Act
            var response = await _client.GetAsync("/api/account");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseAccounts = JsonConvert.DeserializeObject<List<AccountDto>>(responseString);

            // Assert
           responseAccounts.Should().HaveCount(0);
        }
    }
}
