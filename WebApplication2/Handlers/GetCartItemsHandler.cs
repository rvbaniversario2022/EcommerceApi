using MediatR;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class GetCartItemsHandler : IRequestHandler<GetCartItemsQuery, IEnumerable<CartItem>>
    {
        private readonly ICartItemRepository _cartItemRepo;

        public GetCartItemsHandler(ICartItemRepository cartItemRepo) => _cartItemRepo = cartItemRepo;

        public async Task<IEnumerable<CartItem>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            return await _cartItemRepo.GetCartItems();
        }
    }
}
