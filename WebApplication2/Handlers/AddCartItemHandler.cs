using MediatR;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepo;

        public AddCartItemHandler(ICartItemRepository cartItemRepo) => _cartItemRepo = cartItemRepo;

        public async Task<CartItem> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
        {
            return await _cartItemRepo.AddCartItem(command);
        }
    }
}
