using MediatR;
using WebApplication2.Commands;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public DeleteOrderHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<Order> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            return await _orderRepo.DeleteOrder(command.OrderId);
        }
    }
}
