using MediatR;
using WebApplication2.Models;
using WebApplication2.Queries;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public GetOrderByIdHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<Order> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepo.GetOrderById(query.OrderId);
        }
    }
}
