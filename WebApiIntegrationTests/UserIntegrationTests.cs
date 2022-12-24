using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using WebApplication2.Models;
using FluentAssertions;
using System.Net;

namespace IntegrationTest
{
    public class UserIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public UserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("2f9deb05-14ab-48a4-aae1-87b51681cbc3", HttpStatusCode.OK)]
        [InlineData("b05316e5-dacf-4a80-8ac6-419ec7fe3d7b", HttpStatusCode.NoContent)]
        public async Task TestGetUserAsync(Guid id, HttpStatusCode statusCode)
        {
            var request = $"/api/v1/users/{id}";

            var response = await _client.GetAsync(request);

            response.Should().HaveStatusCode(statusCode);
        }

        [Fact]
        public async Task TestPostUserAsync()
        {
            var request = new
            {
                Url = "/api/v1/users",
                Body = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "benjamin",
                }
            };

            var response = await _client.PostAsJsonAsync(request.Url, request.Body);

            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }
    }
}
