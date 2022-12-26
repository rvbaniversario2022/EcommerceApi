using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using WebApplication2.Commands;
using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public CheckoutHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public Task<Order> Handle(CheckoutCommand command, CancellationToken cancellationToken)
        {
            return _orderRepo.Checkout(command.UserId);
        }
    }
}
