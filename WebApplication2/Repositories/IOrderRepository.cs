using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<Order> GetOrderById(Guid id);
        public Task<Order> Checkout(Guid userId);
        public Task<Order> UpdateOrder(UpdateOrderCommand command);
        public Task<Order> DeleteOrder(Guid orderId);
    }
}
