using Microsoft.AspNetCore.Mvc.Testing;

namespace Finance.Tests.IntegrationTests
{
    public class MyIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MyIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory; ;
        }


        [Fact]
        public async Task Get_Values_Returns_Success()
        {
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/health");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
