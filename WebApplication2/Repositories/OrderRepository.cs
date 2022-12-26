using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var query = "SELECT * FROM Orders AS A INNER JOIN CartItems AS B ON A.Id = B.OrderId";
                var orderDictionary = new Dictionary<Guid, Order>();

                var connection = _context.Database.GetDbConnection();

                    var list = connection.Query<Order, CartItem, Order>(
                        query,
                        (order, cartItem) =>
                            {
                                Order orderEntry;

                                if (!orderDictionary.TryGetValue(order.Id, out orderEntry))
                                {
                                    orderEntry = order;
                                    orderEntry.CartItems = orderEntry.CartItems ?? new List<CartItem>();
                                    orderDictionary.Add(orderEntry.Id, orderEntry);
                                }

                                orderEntry.CartItems.Add(cartItem);
                                return orderEntry;
                            },
                        splitOn: "Id"
                        ).Distinct()
                        .ToList();

                return orderDictionary.Values;
            }
            catch (Exception)
            {
                var ex = new Exception("Could Not Fetch Orders");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Orders WHERE Id = @Id;" + "SELECT * FROM CartItems WHERE OrderId = @Id";

                var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(query, new { id });

                _logger.LogInformation($"Getting Order With Id: {id}");
                var order = await multi.ReadSingleOrDefaultAsync<Order>();

                if (order == null)
                {
                    var ex = new ArgumentNullException($"{nameof(GetOrderById)} must not be null");

                    _logger.LogError($"Error: {ex}");

                    throw ex;
                }
                else
                {
                    order.CartItems = (await multi.ReadAsync<CartItem>()).ToList();

                    return order;
                }
            }
            catch (Exception)
            {
                var ex = new Exception($"Order With Id {id} Could Not Be Fetch");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<Order> Checkout(Guid userId)
        {
            try
            {
                var order = _context.Orders.Where(x => x.CartItems.Any(x => x.UserId == userId) && x.Status == Status.Pending).FirstOrDefault();

                if (order == null)
                {
                    var ex = new ArgumentNullException($"{nameof(Checkout)} order must not be null");

                    _logger.LogInformation($"Error: {ex}");

                    throw ex;
                }
                else
                {
                    order.Status = Status.Processed;

                    _context.Update(order);

                    await _context.SaveChangesAsync();

                    return order;
                }
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(Checkout)} Could Not Save Changes");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<Order> UpdateOrder(UpdateOrderCommand command)
        {
            try
            {
                var orderUpdate = _context.Orders.Where(x => x.Id == command.OrderId).FirstOrDefault();
                orderUpdate.Status = command.Status;

                _logger.LogInformation("Updating Order...");
                _context.Update(orderUpdate);

                _logger.LogInformation("Saving Changes...");
                await _context.SaveChangesAsync();

                return orderUpdate;
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(UpdateOrder)} could not be updated");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }

        public async Task<Order> DeleteOrder(Guid orderId)
        {
            try
            {
                var orderToDelete = _context.Orders.FirstOrDefault(x => x.Id == orderId);

                if (orderToDelete == null)
                {
                    var ex = new ArgumentNullException($"{nameof(DeleteOrder)} order must not be null");

                    _logger.LogInformation($"Error: {ex}");

                    throw ex;
                }
                else
                {
                    _logger.LogInformation("Removing Order...");
                    _context.Remove(orderToDelete);

                    _logger.LogInformation("Saving Changes...");
                    await _context.SaveChangesAsync();

                    return orderToDelete;
                }
            }
            catch (Exception)
            {
                var ex = new Exception($"{nameof(DeleteOrder)} could not be deleted");

                _logger.LogError($"Error: {ex}");

                throw ex;
            }
        }
    }
}
