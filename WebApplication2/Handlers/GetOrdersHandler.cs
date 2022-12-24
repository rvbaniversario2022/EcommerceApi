using MediatR;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository _orderRepo;

        public GetOrdersHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepo.GetOrders();
        }
    }
}
