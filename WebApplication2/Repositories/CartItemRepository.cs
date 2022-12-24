using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Commands;
using WebApplication2.Controllers;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Queries;

namespace WebApplication2.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CartItemsController> _logger;
        public CartItemRepository(AppDbContext context, ILogger<CartItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems()
        {
            try
            {
                var query = "SELECT * FROM CartItems";

                var connection = _context.Database.GetDbConnection();

                _logger.LogInformation("Fetching Cart Items...");
                var items = await connection.QueryAsync<CartItem>(query);

                return items;
            }
            catch (Exception)
            {
                var ex = new Exception("Couldn't Retrieve Cart Items");

                _logger.LogInformation($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<CartItem> AddCartItem(AddCartItemCommand command)
        {
            try
            {
                var pendingOrder = _context.Orders.Where(x => x.CartItems.FirstOrDefault().UserId == command.UserId && x.Status == Status.Pending).FirstOrDefault();
                var newCartItem = new CartItem();

                if (pendingOrder == null)
                {
                    var newOrder = new Order()
                    {
                        Id = Guid.NewGuid(),
                        Status = Status.Pending,
                    };

                    newCartItem = new()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = newOrder.Id,
                        ProductName = command.ProductName,
                        UserId = command.UserId,
                    };

                    _logger.LogInformation("Creating Order...");
                    await _context.Orders.AddAsync(newOrder);

                    _logger.LogInformation("Creating Cart Item...");
                    await _context.CartItems.AddAsync(newCartItem);

                    _logger.LogInformation("Saving Changes...");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    newCartItem = new()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = pendingOrder.Id,
                        ProductName = command.ProductName,
                        UserId = command.UserId
                    };

                    _logger.LogInformation("Adding Cart Item...");
                    await _context.CartItems.AddAsync(newCartItem);

                    _logger.LogInformation("Saving Changes...");
                    await _context.SaveChangesAsync();
                }

                return newCartItem;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(AddCartItem)} could not be saved");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<CartItem> UpdateCartItem(UpdateCartItemCommand command)
        {
            try
            {
                var cartItemUpdate = _context.CartItems.FirstOrDefault(x => x.Id == command.Id);
                cartItemUpdate.ProductName = command.ProductName;

                _logger.LogInformation("Updating Cart Item...");
                _context.Update(cartItemUpdate);

                _logger.LogInformation("Saving Changes...");
                await _context.SaveChangesAsync();

                return cartItemUpdate;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(UpdateCartItem)} could not be updated");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<CartItem> DeleteCartItem(Guid itemId)
        {
            try
            {
                var cartItemDelete = _context.CartItems.FirstOrDefault(x => x.Id == itemId);

                _logger.LogInformation("Removing Cart Item...");
                _context.Remove(cartItemDelete);

                _logger.LogInformation("Saving Changes...");
                await _context.SaveChangesAsync();

                return cartItemDelete;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(DeleteCartItem)} could not be deleted");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }

        }
    }
}
