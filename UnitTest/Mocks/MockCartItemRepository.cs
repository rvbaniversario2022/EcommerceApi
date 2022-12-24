using Bogus;
using Moq;
using WebApplication2.Commands;
using WebApplication2.Enums;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace UnitTest.Mocks
{
    public static class MockCartItemRepository
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
            Faker<Order> OrderGenerator = new Faker<Order>()
                .RuleFor(order => order.Id, new Guid("42408F17-7C54-44A8-B6BB-4EBC9C972685"))
                .RuleFor(order => order.Status, Status.Pending)
                .RuleFor(order => order.CartItems, GenerateCartItems());

            return OrderGenerator.Generate(1);
        }

        public static Mock<ICartItemRepository> GetCartItemRepository()
        {
            var items = new List<CartItem>(GenerateCartItems());
            var orders = new List<Order>(GenerateOrders());

            var mockRepo = new Mock<ICartItemRepository>();

            mockRepo.Setup(r => r.GetCartItems()).ReturnsAsync(items);

            mockRepo.Setup(r => r.AddCartItem(It.IsAny<AddCartItemCommand>())).ReturnsAsync((AddCartItemCommand command) =>
            {
                var pendingOrder = orders.FirstOrDefault(x => x.CartItems.FirstOrDefault().UserId == command.UserId && x.Status == Status.Pending);
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

                    orders.Add(newOrder);

                    items.Add(newCartItem);
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

                    items.Add(newCartItem);
                }

                return newCartItem;
            });

            mockRepo.Setup(r => r.UpdateCartItem(It.IsAny<UpdateCartItemCommand>())).ReturnsAsync((UpdateCartItemCommand command) =>
            {
                var cartItemUpdate = items.FirstOrDefault(x => x.Id == command.Id);
                cartItemUpdate.ProductName = command.ProductName;

                return cartItemUpdate;
            });

            mockRepo.Setup(r => r.DeleteCartItem(It.IsAny<Guid>())).ReturnsAsync((Guid itemId) =>
            {
                var cartItemDelete = items.FirstOrDefault(x => x.Id == itemId);
                items.Remove(cartItemDelete);

                return cartItemDelete;
            });

            return mockRepo;
        }
    }
}