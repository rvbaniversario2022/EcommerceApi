using Bogus;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace IntegrationTest
{
    public static class CartItemRepoApi
    {
        private static readonly string _apiEndpoint = "/api/v1/cart-items";
        internal static readonly CartItem newCartItem = GetSampleCartItem();
        private static CartItem GetSampleCartItem()
        {
            Faker<CartItem> CartItemFaker = new Faker<CartItem>()
                .RuleFor(o => o.Id, new Guid("0E9E3D4D-2B9F-472C-9441-05756C3BFAD5"))
                .RuleFor(o => o.ProductName, b => b.Commerce.ProductName())
                .RuleFor(o => o.Status, "Pending")
                .RuleFor(o => o.UserId, new Guid("8E5FEAB2-0608-4E23-BCF0-8C22FAD2E35B"));

            return CartItemFaker.Generate();
        }

        public static async Task<HttpResponseMessage> GetCartItems(HttpClient _httpClient)
        {
            return await _httpClient.GetAsync(_apiEndpoint);
        }

        public static async Task<HttpResponseMessage> PostCartItem(HttpClient _httpClient, CartItem cartItem = null!)
        {
            return cartItem != null ?
                await _httpClient.PostAsJsonAsync(_apiEndpoint, cartItem) :
                await _httpClient.PostAsJsonAsync(_apiEndpoint, newCartItem);
        }

        public static async Task<HttpResponseMessage> PutCartItem(HttpClient _httpClient, UpdateCartItemDto updateCartItemDto)
        {
            return await _httpClient.PutAsJsonAsync($"{_apiEndpoint}", updateCartItemDto);
        }

        //public static async Task<HttpResponseMessage> DeleteCartItem(HttpClient _httpClient, DeleteCartItemDto deleteCartItemDto)
        //{
        //    return await _httpClient.DeleteAsync($"{_apiEndpoint}");
        //}
    }
}

        //private static List<CartItem> GetSampleNotesItemData()
        //{
        //    Faker<CartItem> itemFaker = new Faker<CartItem>()
        //        .RuleFor(o => o.Id, f => f.Random.Guid())
        //        .RuleFor(o => o.ProductName, f => f.Commerce.ProductName())
        //        .RuleFor(o => o.Status, "Pending")
        //        .RuleFor(o => o.UserId, f => f.Random.Guid());

        //    return itemFaker.Generate(10);
        //}