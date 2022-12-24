using FluentAssertions;
using Moq;
using WebApplication2.Commands;
using WebApplication2.Handlers;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;
using UnitTest.Mocks;
using WebApplication2.Dto;
using WebApplication2.Enums;

namespace UnitTest
{
    public class CartItemUnitTests
    {
        private readonly Mock<ICartItemRepository> _mockRepo;
        public CartItemUnitTests()
        {
            _mockRepo = MockCartItemRepository.GetCartItemRepository();
        }

        [Fact]
        public async Task GetCartItemsTest()
        {
            var handler = new GetCartItemsHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetCartItemsQuery(), CancellationToken.None);

            result.Should().BeOfType<List<CartItem>>();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddCartItem()
        {
            var command = new AddCartItemCommand
            {
                ProductName = "Air Force 1",
                UserId = new Guid("{C98E8774-A82D-4C1E-857E-A5E68EFF9158}"),
            };

            var handler = new AddCartItemHandler(_mockRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            var getItems = await _mockRepo.Object.GetCartItems();

            result.Should().BeOfType<CartItem>();
            getItems.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateCartItem()
        {
            var command = new UpdateCartItemCommand
            {
                Id = new Guid("{6241FDD3-3276-438A-BF7D-9023A760A7DA}"),
                ProductName = "Product Name Updated",
            };

            var handler = new UpdateCartItemHandler(_mockRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType<CartItem>();
            result.ProductName.Should().Be("Product Name Updated");
        }

        [Fact]
        public async Task DeleteCartItem()
        {
            var command = new DeleteCartItemCommand
            {
                ItemId = new Guid("{6241FDD3-3276-438A-BF7D-9023A760A7DA}")
            };

            var handler = new DeleteCartItemHandler(_mockRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType<CartItem>();
            result.Id.Should().Be(new Guid("{6241FDD3-3276-438A-BF7D-9023A760A7DA}"));
        }
    }
}
