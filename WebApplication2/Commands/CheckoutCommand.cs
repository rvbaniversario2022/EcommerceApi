using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public record CheckoutCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
