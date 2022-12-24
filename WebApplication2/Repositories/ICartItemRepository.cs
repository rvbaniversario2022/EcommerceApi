using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface ICartItemRepository
    {
        public Task<IEnumerable<CartItem>> GetCartItems();
        public Task<CartItem> AddCartItem(AddCartItemCommand command);
        public Task<CartItem> UpdateCartItem(UpdateCartItemCommand command);
        public Task<CartItem> DeleteCartItem(Guid itemId);
    }
}
