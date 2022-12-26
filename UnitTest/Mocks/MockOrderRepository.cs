using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Repositories;
using Xunit.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitTest.Mocks
{
    public static class MockOrderRepository
    {
        public static IEnumerable<CartItem> GenerateCartItems()
        {
            Faker<CartItem> CartItemGenerator = new Faker<CartItem>()
                .RuleFor(item => item.Id, new Guid("{6241FDD3-3276-438A-BF7D-9023A760A7DA}"))
                .RuleFor(item => item.OrderId, new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685"))
                .RuleFor(item => item.ProductName, bogus => bogus.Commerce.ProductName())
                .RuleFor(item => item.UserId, new Guid("0A82109D-A736-41BB-8A8A-2F94AF66AD50"));

            return CartItemGenerator.Generate(1);
        }
        public static IEnumerable<Order> GenerateOrders()
        {
            Faker<Order> OrdersGenerator = new Faker<Order>()
                .RuleFor(order => order.Id, new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685"))
                .RuleFor(order => order.Status, Status.Pending)
                .RuleFor(order => order.CartItems, GenerateCartItems());

            return OrdersGenerator.Generate(1);
        }

        public static Mock<IOrderRepository> GetOrderRepository()
        {
            var orders = new List<Order>(GenerateOrders());

            var mockRepo = new Mock<IOrderRepository>();

            mockRepo.Setup(r => r.GetOrders()).ReturnsAsync(orders);

            mockRepo.Setup(r => r.GetOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                var order = orders.Where(x => x.Id == id).FirstOrDefault();

                return order;
            });

            mockRepo.Setup(r => r.Checkout(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
            {
                var order = orders.Where(x => x.CartItems.Any(x => x.UserId == userId) && x.Status == Status.Pending).FirstOrDefault();

                order.Status = Status.Processed;

                return order;
            });

            mockRepo.Setup(r => r.UpdateOrder(It.IsAny<UpdateOrderCommand>())).ReturnsAsync((UpdateOrderCommand command) =>
            {
                var orderUpdate = orders.Where(x => x.Id == command.OrderId).FirstOrDefault();
                orderUpdate.Status = command.Status;

                return orderUpdate;
            });

            mockRepo.Setup(r => r.DeleteOrder(It.IsAny<Guid>())).ReturnsAsync((Guid orderId) =>
            {
                var order = orders.FirstOrDefault(x => x.Id == orderId);
                orders.Remove(order);

                return order;
            });

            return mockRepo;
        }
    }
}