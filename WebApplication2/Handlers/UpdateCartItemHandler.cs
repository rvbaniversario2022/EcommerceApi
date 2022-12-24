using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Commands;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepo;

        public UpdateCartItemHandler(ICartItemRepository cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }

        public Task<CartItem> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
        {
            return _cartItemRepo.UpdateCartItem( command);
        }
    }
}
