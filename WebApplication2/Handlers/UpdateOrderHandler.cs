using MediatR;
using WebApplication2.Commands;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public UpdateOrderHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<Order> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            return await _orderRepo.UpdateOrder(command);
        }
    }
}
