using MediatR;
using WebApplication2.Commands;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepo;

        public DeleteCartItemHandler(ICartItemRepository cartItemRepo) => _cartItemRepo = cartItemRepo;

        public async Task<CartItem> Handle(DeleteCartItemCommand command, CancellationToken cancellationToken)
        {
            return await _cartItemRepo.DeleteCartItem(command.ItemId);
        }
    }
}
