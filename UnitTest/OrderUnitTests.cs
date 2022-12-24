using FluentAssertions;
using Moq;
using Shouldly;
using WebApplication2.Commands;
using WebApplication2.Handlers;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;
using UnitTest.Mocks;
using WebApplication2.Dto;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Enums;

namespace UnitTest
{
    public class OrderUnitTests
    {
        private readonly Mock<IOrderRepository> _mockRepo;
        private readonly Mock<ICartItemRepository> _mockCartItemRepo;

        public OrderUnitTests()
        {
            _mockRepo = MockOrderRepository.GetOrderRepository();
            _mockCartItemRepo = MockCartItemRepository.GetCartItemRepository();
        }

        [Fact]
        public async Task GetOrdersTest()
        {
            var handler = new GetOrdersHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetOrdersQuery(), CancellationToken.None);

            result.Should().BeOfType<List<Order>>();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetOrderByIdTest()
        {
            var orderId = new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685");

            var handler = new GetOrderByIdHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetOrderByIdQuery { OrderId = orderId }, CancellationToken.None);

            result.Should().BeOfType<Order>();
            result.Id.Should().Be(orderId);
        }

        [Fact]
        public async Task CheckoutTest()
        {
            var orders = MockOrderRepository.GenerateOrders();
            var userId = new Guid("0A82109D-A736-41BB-8A8A-2F94AF66AD50");

            var order = orders.Where(x => x.CartItems.Any(x => x.UserId == userId) && x.Status == Status.Pending).FirstOrDefault();

            var handler = new CheckoutHandler(_mockRepo.Object);
            var result = await handler.Handle(new CheckoutCommand { Order = order }, CancellationToken.None);

            result.Status.Should().Be(Status.Processed);
        }

        [Fact]
        public async Task UpdateOrderTest()
        {
            var command = new UpdateOrderCommand
            {
                OrderId = new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685"),
                Status = Status.Cancelled,
            };

            var handler = new UpdateOrderHandler(_mockRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType<Order>();
            result.Status.Should().Be(Status.Cancelled);
        }

        [Fact]
        public async Task DeleteOrderTest()
        {
            var command = new DeleteOrderCommand
            {
                OrderId = new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685")
            };

            var handler = new DeleteOrderHandler(_mockRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType<Order>();
            result.Id.Should().Be(command.OrderId);
        }
    }
}
