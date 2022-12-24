using WebApplication2.Models;
using FluentAssertions;
using WebApplication2.Dto;
using WebApplication2.Data;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using WebApplication2.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace IntegrationTest
{
    public class Tests
        : IClassFixture<ApiTestFixture>
    {
        private readonly HttpClient _client;

        public Tests(ApiTestFixture fixture)
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

            var response = await _client.PostAsync("/api/v1/checkout", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestPostCartItemAsync()
        {
            var requestBody = new
            {
                Id = new Guid("{BCF2FCB1-62D4-4A57-813A-0601D5E3F254}"),
                OrderId = new Guid("5B06247D-3278-443B-A676-A775B0A552D9"),
                ProductName = "Nvidia GPU",
                UserId = new Guid("18DFE984-80F0-4FA2-8767-EE41892D9564")
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/cart-items", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestPostUserAsync()
        {
            var requestBody = new
            {
                Id = new Guid("{67ABF624-9BFE-4076-9BFA-B904292A4854}"),
                Name = "John"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/users", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestGetCartItemsAsync()
        {
            var response = await _client.GetAsync("/api/v1/cart-items");
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestGetOrdersAsync()
        {
            var response = await _client.GetAsync("/api/v1/orders");
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            var orderId = new Guid("5B06247D-3278-443B-A676-A775B0A552D9");

            var response = await _client.GetAsync($"/api/v1/orders/{orderId}");
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Order>(responseString);

            responseData.Id.Should().Be(new Guid("5B06247D-3278-443B-A676-A775B0A552D9"));
        }

        [Fact]
        public async Task TestGetUserAsync()
        {
            var userId = new Guid("18DFE984-80F0-4FA2-8767-EE41892D9564");

            var response = await _client.GetAsync($"/api/v1/users/{userId}");
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<User>(responseString);

            responseData.Id.Should().Be(userId);
        }

        [Fact]
        public async Task TestPutCartItemAsync()
        {
            var requestBody = new
            {
                ProductName = "Product Name Updated"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var id = new Guid("7A70FE90-EE42-4898-B288-60FD419ABEFB");
            
            var response = await _client.PutAsync($"/api/v1/cart-items/{id}", requestContent);
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<CartItem>(responseString);

            responseData.ProductName.Should().Be("Product Name Updated");
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
        public async Task TestDeleteCartItemAsync()
        {
            var id = new Guid("7A70FE90-EE42-4898-B288-60FD419ABEFB");

            var response = await _client.DeleteAsync($"/api/v1/cart-items/{id}");
            response.Should().HaveStatusCode(HttpStatusCode.OK);
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