using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net.Http.Json;
using WebApplication2.Models;
using System.Net;
using WebApplication2.Dto;
using Newtonsoft.Json;
using System.Text;
using WebApplication2.Enums;

namespace IntegrationTest
{
    public class OrderIntegrationTests
        : IClassFixture<ApiTestFixture>
    {
        private readonly HttpClient _client;

        public OrderIntegrationTests(ApiTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task TestPostOrderAsync()
        {
            var requestBody = new
            {
                UserId = new Guid("18DFE984-80F0-4FA2-8767-EE41892D9564")
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/orders", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestGetOrdersAsync()
        {
            var response = await _client.GetAsync("/api/v1/orders");
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<List<Order>>(responseString);

            responseData.Should().HaveCount(1);
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            var id = new Guid("5B06247D-3278-443B-A676-A775B0A552D9");

            var response = await _client.GetAsync($"/api/v1/orders/{id}");
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Order>(responseString);

            responseData.Id.Should().Be(new Guid("5B06247D-3278-443B-A676-A775B0A552D9"));
        }

        [Fact]
        public async Task TestPutOrderAsync()
        {
            var requestBody = new
            {
                Status = Status.Cancelled
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var id = new Guid("5B06247D-3278-443B-A676-A775B0A552D9");

            var response = await _client.PutAsync($"/api/v1/orders/{id}", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Order>(responseString);

            responseData.Status.Should().Be(Status.Cancelled);
        }

        [Fact]
        public async Task TestDeleteOrderAsync()
        {
            var id = new Guid("5B06247D-3278-443B-A676-A775B0A552D9");

            var response = await _client.DeleteAsync($"/api/v1/orders/{id}");
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }
    }
}
